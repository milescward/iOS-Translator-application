using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Translator.Services
{
    public class TextTranslationService : ITextTranslationService
    {
        IAuthenticationService authenticationService;

        private const string key_var = "aae9cbdcca3a4fe8b2ebfb598dcef38d";
        private static readonly string subscriptionKey = Environment.GetEnvironmentVariable(key_var);

        private const string endpoint_var = "https://translationtest.cognitiveservices.azure.com/sts/v1.0/issuetoken";
        private static readonly string endpoint = Environment.GetEnvironmentVariable(endpoint_var);
        HttpClient httpClient;

        public TextTranslationService(IAuthenticationService authService)
        {
            authenticationService = authService;
        }

        public async Task<string> TranslateTextAsync(string text)
        {
            if (string.IsNullOrWhiteSpace(authenticationService.GetAccessToken()))
            {
                await authenticationService.InitializeAsync();
            }

            string requestUri = GenerateRequestUri(endpoint, text, "en", "de");
            string accessToken = authenticationService.GetAccessToken();
            var response = await SendRequestAsync(requestUri, accessToken);
            var xml = XDocument.Parse(response);
            return xml.Root.Value;
        }

        string GenerateRequestUri(string endpoint2, string text, string from, string to)
        {
            string requestUri = endpoint2;
            requestUri += string.Format("?text={0}", Uri.EscapeUriString(text));
            requestUri += string.Format("&from={0}", from);
            requestUri += string.Format("&to={0}", to);
            return requestUri;
        }

        async Task<string> SendRequestAsync(string url, string bearerToken)
        {
            if (httpClient == null)
            {
                httpClient = new HttpClient();
            }
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

            var response = await httpClient.GetAsync(url);
            return await response.Content.ReadAsStringAsync();
        }
    }
}