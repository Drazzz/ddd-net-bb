namespace DDDNETBB.Domain.Abstractions
{
    public interface IPolicy<out TResult>
    {
        TResult Execute();
    }
}