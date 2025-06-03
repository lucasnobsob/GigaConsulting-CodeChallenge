using Refit;

namespace GigaConsulting.Domain.Services
{
    public interface IFooClient
    {
        [Get("/")]
        Task<object> GetAll();
    }
}
