using System;
using System.Buffers;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Fusion.Shared
{
    public static class Common
    {
        public static bool NullOrZero(this int? myint)
        {
            return myint == null || myint == 0;
        }

        public static T ToObject<T>(this JsonElement element, JsonSerializerOptions options = null)
        {
            var bufferWriter = new ArrayBufferWriter<byte>();
            using (var writer = new Utf8JsonWriter(bufferWriter))
                element.WriteTo(writer);
            return JsonSerializer.Deserialize<T>(bufferWriter.WrittenSpan, options);
        }

        public static T ToObject<T>(this JsonDocument document, JsonSerializerOptions options = null)
        {
            if (document == null)
                throw new ArgumentNullException(nameof(document));
            return document.RootElement.ToObject<T>(options);
        }

        public static T SetPropertyValues<T>(this T obj)
        {
            foreach (var propertyInfo in obj.GetType().GetProperties())
            {
                if (propertyInfo.GetValue(obj, null) == null)
                {
                    if (propertyInfo.PropertyType == typeof(string))
                    {
                        propertyInfo.SetValue(obj, String.Empty, null);
                    }
                    else if ((propertyInfo.PropertyType == typeof(Nullable<Int32>)) || (propertyInfo.PropertyType == typeof(Int64)) || (propertyInfo.PropertyType == typeof(SByte)))
                    {
                        propertyInfo.SetValue(obj, 0, null);
                    }
                    else if (propertyInfo.PropertyType == typeof(Nullable<float>))
                    {
                        propertyInfo.SetValue(obj, Convert.ToSingle(0), null);
                    }
                    else if (propertyInfo.PropertyType == typeof(Nullable<Decimal>))
                    {
                        propertyInfo.SetValue(obj, Convert.ToDecimal(0), null);
                    }
                    else if (propertyInfo.PropertyType == typeof(Nullable<Int16>))
                    {
                        propertyInfo.SetValue(obj, Convert.ToInt16(0), null);
                    }
                }
            }

            return obj;
        }

        public static string GetDescription<T>(this T e) where T : IConvertible
        {
            if (e is Enum)
            {
                Type type = e.GetType();
                Array values = System.Enum.GetValues(type);

                foreach (int val in values)
                {
                    if (val == e.ToInt32(CultureInfo.InvariantCulture))
                    {
                        var memInfo = type.GetMember(type.GetEnumName(val));
                        var descriptionAttribute = memInfo[0]
                            .GetCustomAttributes(typeof(DescriptionAttribute), false)
                            .FirstOrDefault() as DescriptionAttribute;

                        if (descriptionAttribute != null)
                        {
                            return descriptionAttribute.Description;
                        }
                    }
                }
            }

            return " ";
        }
    }
}
