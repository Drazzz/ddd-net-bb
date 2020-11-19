using System.Threading.Tasks;

namespace DDDNETBB.Domain.Tests.Fakes
{
    internal sealed record FakeValueObject(string Name, int Age, bool IsAdult) : ValueObject
    {
        public async Task AsyncMethodThrows_BusinessRuleValidationException(int data)
            => await Task.Run(() => Check(BusinessRuleToCheck.For(data)));
    }
}