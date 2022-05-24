using System.Threading.Tasks;

namespace DCompAdventure.Activation
{
    public interface IActivationHandler
    {
        bool CanHandle(object args);

        Task HandleAsync(object args);
    }
}
