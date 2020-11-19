using System;
using System.Collections.Concurrent;
using System.Globalization;
using System.Reflection;
using DDDNETBB.Domain.Converters;
using Newtonsoft.Json;

namespace DDDNETBB.Domain.Converters
{
    public class SingleValueObjectConverter : JsonConverter
    {
        private readonly string _fieldName;
        private static readonly ConcurrentDictionary<Type, Type> _constructorArgumentTypes = new ConcurrentDictionary<Type, Type>();


        private SingleValueObjectConverter(ISingleValueObjectNamingPolicy valueFieldPolicy){
            if(valueFieldPolicy is null)
                throw new ArgumentNullException(nameof(valueFieldPolicy));

            _fieldName = valueFieldPolicy.Execute();             
        }
        public static SingleValueObjectConverter For(SingleValueObjectConverterType converterType)
            => new SingleValueObjectConverter(new SingleValueObjectConverterPolicy()); 


        public override bool CanConvert(Type objectType)
        {
            var currentType = objectType;
            while (currentType != null)
            {
                if (currentType.IsGenericType &&
                    currentType.GetGenericTypeDefinition() == typeof(SingleValueObject<>))
                {
                    _constructorArgumentTypes[objectType] = currentType.GenericTypeArguments[0];
                    return true;
                }

                currentType = currentType.BaseType;
            }

            return false;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var parameterType = _constructorArgumentTypes[objectType];
            var value = serializer.Deserialize(reader, parameterType);

            return Activator.CreateInstance(objectType,
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, 
                null, new[] {value}, CultureInfo.CurrentCulture);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var identityValue = value?.GetType().GetProperty(_fieldName)?.GetValue(value, null);
            serializer.Serialize(writer, identityValue);
        }
    }
}