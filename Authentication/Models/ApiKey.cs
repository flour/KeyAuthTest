using System;
using System.Collections.Generic;

namespace ApiKeyTest.Authentication.Models
{
    public class ApiKey
    {
        public int Id { get; }
        public string OwnerName { get; }
        public string Key { get; }
        public DateTime Created { get; } = DateTime.UtcNow;
        public IReadOnlyCollection<string> Roles { get; }

        public ApiKey(int id, string owner, string key, IReadOnlyCollection<string> roles)
        {
            Id = id;
            OwnerName = owner;
            Key = key;
            Roles = roles;
        }
    }
}