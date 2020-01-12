using System.Threading.Tasks;
using ApiKeyTest.Authentication.Models;

namespace ApiKeyTest.Services
{
    public interface IKeyStorage
    {
        Task<ApiKey> GetApiKey(string providedApiKey);
    }
}