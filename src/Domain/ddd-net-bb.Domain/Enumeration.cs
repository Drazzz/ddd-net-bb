using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DDDNETBB.Domain
{
    public abstract class Enumeration: IComparable
    {
        public int Id {get; private set;}
        public string Name{get; private set;}


        protected Enumeration(int id, string name)
        {
            Id = id;
            Name = name;
        }


        protected static T FromValue<T>(int value) where T : Enumeration
            => Parse<T, int>(value, "value", item => item.Id == value);

        protected static T FromDisplayName<T>(string displayName) where T : Enumeration
            => Parse<T, string>(displayName, "display name", item => item.Name == displayName);

        protected static IEnumerable<T> GetAll<T>() where T : Enumeration
        {
            var fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);
            return fields.Select(f => f.GetValue(null)).Cast<T>();
        }

        private static T Parse<T, K>(K value, string description, Func<T, bool> predicate) where T : Enumeration
        {
            var matchingItem = GetAll<T>().FirstOrDefault(predicate);
            if (matchingItem is null)
                throw new InvalidOperationException($"'{value}' is not a valid {description} in {typeof(T)}");

            return matchingItem;
        }

        public static int AbsoluteDifference(Enumeration firstValue, Enumeration secondValue)
            => Math.Abs(firstValue.Id - secondValue.Id);

        public int CompareTo(object other) => Id.CompareTo(((Enumeration)other).Id);

        public override string ToString() => Name;

        public override int GetHashCode() => Id.GetHashCode();

        public override bool Equals(object obj)
        {
            var otherValue = obj as Enumeration;
            if (otherValue is null)
                return false;

            var typeMatches = GetType().Equals(obj.GetType());
            var valueMatches = Id.Equals(otherValue.Id);

            return typeMatches && valueMatches;
        }        
    }
}