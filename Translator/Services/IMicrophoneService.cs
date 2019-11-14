using System;
using System.Threading.Tasks;

namespace Translator.Services
{
    public interface IMicrophoneService
    {
        Task<bool> GetPermissionsAsync();
        void OnRequestPermissionsResult(bool isGranted);
    }
}
