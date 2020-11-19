using DDDNETBB.Domain.Abstractions;

namespace DDDNETBB.Domain.Tests.Fakes
{
    internal sealed class BusinessRuleToCheck : IBusinessRule
    {
        public int SomeData { get; }
        public string Message { get; }


        private BusinessRuleToCheck(int data)
        {
            SomeData = data;
            Message = $"Rule has been broken because {SomeData}";
        }
        public static BusinessRuleToCheck For(int someData) => new BusinessRuleToCheck(someData);

        public bool IsBroken() => SomeData < 1;
    }
}