using System.Threading.Tasks;

namespace DDDNETBB.Core.Abstractions.Dependencies
{
    public interface IInitializer
    {
        Task Initialize();
    }
}