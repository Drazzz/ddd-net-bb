using DDDNETBB.Domain.Abstractions;
using DDDNETBB.Domain.Exceptions;

namespace DDDNETBB.Domain
{
    public abstract record ValueObject
    {
        protected void Check(IBusinessRule rule)
        {
            if(rule.IsBroken())
                throw new BusinessRuleValidationException(rule);
        }
    }
}