using System;
using System.Threading.Tasks;

namespace Translator.Services
{
    public interface IAuthenticationService
    {
        Task InitializeAsync();
        string GetAccessToken();
    }
}
