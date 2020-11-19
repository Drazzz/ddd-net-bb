namespace DDDNETBB.Domain
{
    public abstract record SingleValueObject<TValue>(TValue Value) : ValueObject
    {
        public override string ToString()
            => ReferenceEquals(Value, null) ? string.Empty : Value.ToString();        
    }
}