using System.Net.Mail;
using System.Xml.Linq;
using Grocery.Core.Helpers;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;

namespace Grocery.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly IClientService _clientService;
        public AuthService(IClientService clientService)
        {
            _clientService = clientService;
        }
        public Client? Login(string email, string password)
        {
            Client? client = _clientService.Get(email);
            if (client == null) return null;
            if (PasswordHelper.VerifyPassword(password, client.Password)) return client;
            return null;
        }

        public Client? Register(string name, string email, string password)
        {
            Client? existingClient = _clientService.Get(email);
            if (existingClient != null) return null; // email already exists
            // hash password
            string hashedPassword = PasswordHelper.HashPassword(password);
            // register new client
            if (!string.IsNullOrEmpty(hashedPassword)) {
                
                return _clientService.Add( name, email, hashedPassword );
            }
            else
            {
                return null;
            }
        }
    }
}
