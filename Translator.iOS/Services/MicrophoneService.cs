using System;
using System.Threading.Tasks;
using AVFoundation;
using Translator.Services;

namespace Translator.iOS.Services
{
    public class MicrophoneService : IMicrophoneService
    {
        private TaskCompletionSource<bool> tcsPermissions;

        public Task<bool> GetPermissionsAsync()
        {
            tcsPermissions = new TaskCompletionSource<bool>();
            RequestMicPermission();
            return tcsPermissions.Task;
        }

        private void RequestMicPermission()
        {
            var session = AVAudioSession.SharedInstance();
            session.RequestRecordPermission((granted) =>
            {
                Console.WriteLine($"Audio Permission: {granted}");
                if (granted)
                {
                    tcsPermissions.TrySetResult(granted);
                }
                else
                {
                    tcsPermissions.TrySetResult(false);
                    Console.WriteLine("YOU MUST ENABLE MICROPHONE PERMISSION");
                }
            });
        }

        public void OnRequestPermissionsResult(bool isGranted)
        {
            tcsPermissions.TrySetResult(isGranted);
        }
    }
}
