using System.Threading.Tasks;

namespace DCompAdventure.Contracts.Services
{
    public interface IActivationService
    {
        Task ActivateAsync(object activationArgs);
    }
}
