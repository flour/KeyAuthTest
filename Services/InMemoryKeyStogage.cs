using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiKeyTest.Authentication.Models;

namespace ApiKeyTest.Services
{
    public class InMemoryKeyStogage : IKeyStorage
    {
        private readonly IDictionary<string, ApiKey> _apiKeys;

        public InMemoryKeyStogage()
        {
            _apiKeys = GetSeed();
        }

        public Task<ApiKey> GetApiKey(string providedApiKey)
        {
            _apiKeys.TryGetValue(providedApiKey, out var result);
            return Task.FromResult(result);
        }

        private IDictionary<string, ApiKey> GetSeed()
        {
            var owners = new string[] { "Manager", "Developer", "Accountant", "Supply manager" };
            var keys = new string[] { "4u7h9iO1YPYDssGODN7b", "eXMClUNOlbFubMOYIQWG", "g54WVlpuIvkSw62sNyGz", "EtHSI6VxF9xMsDIhwWH1" };
            var roles = new List<List<string>>
            {
                new List<string> { Roles.Manager, Roles.Employee },
                new List<string> { Roles.Employee },
                new List<string> { Roles.Accountant },
                new List<string> { Roles.SupplyManager }
            };
            return Enumerable.Range(0, owners.Length)
                .Select(i => new ApiKey(i, owners[i], keys[i], roles[i]))
                .ToDictionary(e => e.Key, e => e);
        }

    }
}