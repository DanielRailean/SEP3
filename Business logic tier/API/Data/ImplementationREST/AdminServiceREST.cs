using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using API.Models;

namespace API.Data.ImplementationREST
{
    public class AdminServiceREST : IAdminService
    {
        private string uri = "https://localhost:8080/Administrator";
        private HttpClient client;

        public AdminServiceREST()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (message, certificate2, arg3, arg4) =>
            {
                return true;
            };
            client = new HttpClient(clientHandler);
        }
        public Administrator ValidateAdministrator(string email, string password)
        {
            throw new System.NotImplementedException();
        }
    }
}