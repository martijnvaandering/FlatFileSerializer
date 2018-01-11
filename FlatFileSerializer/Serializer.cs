using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace FlatFileSerializer
{
    public class Serializer<T> where T : class
    {
        public int Position { get; set; }
        public bool IsMultiLine { get; set; }

        public T Deserialize(string input, bool trim = false)
        {
            if (typeof(IEnumerable).IsAssignableFrom(typeof(T)))
            {
                var tInner = typeof(T).GetGenericArguments().First();
                T itemList = this.GetType().GetMethod("DeserializeIEnumerable", BindingFlags.NonPublic | BindingFlags.Instance).MakeGenericMethod(tInner).Invoke(this, new object[] { input }) as T;

                return itemList as T;
            }

            var attribute = typeof(T).GetCustomAttributes(true).FirstOrDefault(a => a is FixedLengthSerializableAttribute) as FixedLengthSerializableAttribute;
            if (attribute == null)
            {
                throw new Exception("Not a FlatFileSerializer class");
            }
            IsMultiLine = attribute.Multiline;

            var output = Activator.CreateInstance<T>();

            var properties = typeof(T).GetProperties();

            var stringLengthProperties = properties
               .Select(a => new SerializableProperty(a))
               .Where(a => a.StringLengthAttribute != null)
               .OrderBy(a => a.StringLengthAttribute.Order);

            var nestedFixedLengthClassProperties = properties
                .Select(a => new SerializableClassProperty(a))
                .Where(a => a.FixedLengthSerializableAttribute != null).
                OrderBy(a => a.FixedLengthSerializableAttribute.Order);

            IEnumerable<string> lines = input.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            foreach (var prop in stringLengthProperties)
            {
                string value;
                if (IsMultiLine)
                {
                    lines = lines.Skip(attribute.Line);
                    value = lines.ElementAt(prop.StringLengthAttribute.Order);
                    Position = attribute.Line + prop.StringLengthAttribute.Order;
                }
                else
                {
                    var line = lines.ElementAt(attribute.Line);
                    value = line.Substring(Position, prop.StringLengthAttribute.Length);
                    Position += prop.StringLengthAttribute.Length;
                }
                if (trim)
                {
                    value = value.Trim();
                }
                prop.SetValue(output, value);
            }

            foreach (var scp in nestedFixedLengthClassProperties)
            {
                var serializer = Activator.CreateInstance(typeof(Serializer<>).MakeGenericType(scp.ClassType));
                var data = string.Empty;
                if (IsMultiLine)
                {
                    data = string.Join(Environment.NewLine, input.Split(new[] { Environment.NewLine }, StringSplitOptions.None).Skip(Position + 1));
                }
                else
                {
                    data = input.Substring(Position);
                }
                object obj = serializer.GetType().GetMethod("Deserialize").Invoke(serializer, new object[] { data, true });
                scp.SetValue(output, obj);
            }

            return output;
        }

        private IEnumerable<TInner> DeserializeIEnumerable<TInner>(string input) where TInner : class
        {
            var serializer = new Serializer<TInner>();
            var currentSerializerType = serializer.GetType();
            string data = input;
            while (data.Length > 0)
            {
                var o = serializer.Deserialize(data, true);
                var pos = serializer.Position;
                var ml = serializer.IsMultiLine;
                if (ml)
                {
                    data = string.Join(Environment.NewLine, data.Split(new[] { Environment.NewLine }, StringSplitOptions.None).Skip(pos + 1));
                }
                else
                {
                    data = data.Substring(pos);
                }
                yield return o;
            }
        }

        public string Serialize(T obj, bool trim = false)
        {
            if (typeof(IEnumerable).IsAssignableFrom(typeof(T)))
            {
                var tInner = typeof(T).GetGenericArguments().First();
                var itemList = this.GetType().GetMethod("SerializeIEnumerable", BindingFlags.NonPublic | BindingFlags.Instance).MakeGenericMethod(tInner).Invoke(this, new object[] { obj, trim }) as IEnumerable<string>;

                return string.Join("\r\n", itemList);
            }

            var attribute = typeof(T).GetCustomAttributes(true).FirstOrDefault(a => a is FixedLengthSerializableAttribute) as FixedLengthSerializableAttribute;
            if (attribute == null)
            {
                throw new Exception("Not a FlatFileSerializer class");
            }

            IsMultiLine = attribute.Multiline;

            var properties = typeof(T).GetProperties();

            var stringLengthProperties = properties
               .Select(a => new SerializableProperty(a))
               .Where(a => a.StringLengthAttribute != null)
               .OrderBy(a => a.StringLengthAttribute.Order);

            var nestedFixedLengthClassProperties = properties
                .Select(a => new SerializableClassProperty(a))
                .Where(a => a.FixedLengthSerializableAttribute != null).
                OrderBy(a => a.FixedLengthSerializableAttribute.Order);

            var output = string.Empty;
            foreach (var prop in stringLengthProperties)
            {
                var value = prop.GetValue(obj);

                if (value.Length > prop.StringLengthAttribute.Length)
                {
                    value = value.Substring(0, prop.StringLengthAttribute.Length);
                }
                value += string.Join(string.Empty, Enumerable.Repeat(" ", prop.StringLengthAttribute.Length - value.Length));
                if (trim && !prop.StringLengthAttribute.IgnoreTrim)
                {
                    value = value.Trim();
                }
                output += value + attribute.Separator + (IsMultiLine ? Environment.NewLine : string.Empty);
            }

            foreach (var scp in nestedFixedLengthClassProperties)
            {
                var serializer = Activator.CreateInstance(typeof(Serializer<>).MakeGenericType(scp.ClassType));
                var inputObj = scp.GetValue(obj);
                if (inputObj != null)
                {
                    var data = serializer.GetType().GetMethod("Serialize").Invoke(serializer, new object[] { inputObj, trim }) as string;
                    if (!string.IsNullOrEmpty(data))
                    {
                        output += "\r\n" + data;
                    }
                }
            }

            return output.Replace("\r\n\r\n", "\r\n"); //TODO: damn that replace is ugly

        }

        private IEnumerable<string> SerializeIEnumerable<TInner>(IEnumerable<TInner> input, bool trim = false) where TInner : class
        {
            if (input == null)
            {
                yield break;
            }
            var serializer = new Serializer<TInner>();
            var currentSerializerType = serializer.GetType();

            foreach (var item in input)
            {
                var ouput = serializer.Serialize(item, trim);
                yield return ouput;
            }
        }

        public class SerializableProperty
        {
            public Action<object, string> SetValue { get; set; }
            public Func<object, string> GetValue { get; set; }
            public string Name { get; set; }
            public FixedLengthStringAttribute StringLengthAttribute { get; set; }
            private string StringFormat { get; set; }

            public SerializableProperty(PropertyInfo propertyInfo)
            {
                Name = propertyInfo.Name;
                StringLengthAttribute = propertyInfo.GetCustomAttribute<FixedLengthStringAttribute>();
                var stringFormatAttribute = propertyInfo.GetCustomAttribute<StringFormatAttribute>();
                if (stringFormatAttribute != null)
                {
                    StringFormat = stringFormatAttribute.Format;
                }

                if (propertyInfo.CanWrite)
                {
                    SetValue = (obj, value) =>
                    {
                        if (propertyInfo.PropertyType != typeof(string))
                        {
                            var converter = TypeDescriptor.GetConverter(propertyInfo.PropertyType);
                            if (converter.CanConvertFrom(typeof(string)))
                            {
                                var convertedValue = converter.ConvertFrom(value);
                                propertyInfo.SetValue(obj, convertedValue);
                            }
                        }
                        else
                        {
                            propertyInfo.SetValue(obj, value);
                        }
                    };
                }
                else
                {
                    SetValue = (a, b) =>
                    {
                        Debug.WriteLine($"Property \"{a}\" can not be written");
                    };
                }

                if (propertyInfo.CanRead)
                {
                    if (string.IsNullOrEmpty(StringFormat))
                    {
                        GetValue = (obj) => propertyInfo.GetValue(obj).ToString();
                    }
                    else
                    {
                        GetValue = (obj) => propertyInfo.PropertyType == typeof(DateTime) ? ((DateTime)propertyInfo.GetValue(obj)).ToString(StringFormat) : string.Format(StringFormat, propertyInfo.GetValue(obj));
                    }
                }
                else
                {
                    GetValue = (obj) => null;
                }
            }
        }

        public class SerializableClassProperty
        {
            public Action<object, object> SetValue { get; set; }
            public Func<object, object> GetValue { get; set; }
            public string Name { get; set; }
            public FixedLengthSerializableAttribute FixedLengthSerializableAttribute { get; set; }
            public Type ClassType { get; set; }

            public SerializableClassProperty(PropertyInfo propertyInfo)
            {
                if (propertyInfo.CanWrite)
                {
                    SetValue = propertyInfo.SetValue;
                }
                else
                {
                    SetValue = (a, b) =>
                    {
                        Debug.WriteLine($"Property \"{a}\" can not be written");
                    };
                }

                if (propertyInfo.CanRead)
                {
                    GetValue = propertyInfo.GetValue;
                }
                else
                {
                    GetValue = (obj) => null;
                }

                Name = propertyInfo.Name;
                FixedLengthSerializableAttribute = propertyInfo.GetCustomAttribute<FixedLengthSerializableAttribute>();
                ClassType = propertyInfo.PropertyType;
            }
        }
    }
}