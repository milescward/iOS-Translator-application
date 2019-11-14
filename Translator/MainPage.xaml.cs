using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;
using Translator.Services;
using Xamarin.Forms;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net;
using System.Xml.Linq;

namespace Translator
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private const string key_var = "";
        private static readonly string subscriptionKey = Environment.GetEnvironmentVariable(key_var);
        private const string endpoint_var = "https://translationtest.cognitiveservices.azure.com/sts/v1.0/issuetoken";
        private static readonly string endpoint = Environment.GetEnvironmentVariable(endpoint_var);
        private const string speech_key = "";
        private const string route = "https://api.cognitive.microsofttranslator.com/translate?api-version=3.0&to=zh";


        public MainPage()
        {
            InitializeComponent();

            SourceLanguage.BindingContext = this;
            TargetLanguage.BindingContext = this;
        }

        private void Save_ClickedAsync(object sender, EventArgs eventArgs)
        {
            DisplayAlert("Information", "Some more", "Test", "Test");
            //var translation = (Translation)BindingContext;
            //translation.Date = DateTime.UtcNow;
            //await App.Database.SaveItemAsync(translation);
            //await Navigation.PopAsync();
        }

        private async void OnRecordButtonClicked(object sender, EventArgs e)
        {
            try
            {
                var config = SpeechConfig.FromSubscription(speech_key, "westus");

                using (var recognizer = new SpeechRecognizer(config))
                {
                    var result = await recognizer.RecognizeOnceAsync().ConfigureAwait(false);

                    // Checks result.
                    StringBuilder sb = new StringBuilder();
                    if (result.Reason == ResultReason.RecognizedSpeech)
                    {
                        sb.AppendLine(result.Text);
                    }
                    else if (result.Reason == ResultReason.NoMatch)
                    {
                        sb.AppendLine($"NOMATCH: Speech could not be recognized.");
                    }
                    else if (result.Reason == ResultReason.Canceled)
                    {
                        var cancellation = CancellationDetails.FromResult(result);
                        sb.AppendLine($"CANCELED: Reason={cancellation.Reason}");

                        if (cancellation.Reason == CancellationReason.Error)
                        {
                            sb.AppendLine($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                            sb.AppendLine($"CANCELED: ErrorDetails={cancellation.ErrorDetails}");
                            sb.AppendLine($"CANCELED: Did you update the subscription info?");
                        }
                    }

                    UpdateUI(sb.ToString());
                }
            }
            catch (Exception ex)
            {
                UpdateUI("Exception: " + ex);
            }
        }

        private async void OnEnableMicrophoneButtonClicked(object sender, EventArgs e)
        {
            bool micAccessGranted = await DependencyService.Get<IMicrophoneService>().GetPermissionsAsync();
            if (!micAccessGranted)
            {
                UpdateUI("Please give access to microphone");
            }
        }

        private void UpdateUI(String message)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                RecognitionText.Text = message;
            });
        }

        private void UpdateUI2(String message)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                TranslatedText.Text = message;
            });
        }

        private async void OnReadButtonClicked(object sender, EventArgs e)
        {
            try
            {
                var config = SpeechConfig.FromSubscription(speech_key, "westus");

                using (var synthesizer = new SpeechSynthesizer(config))
                {
                    StringBuilder sb = new StringBuilder();
                    using (var result = await synthesizer.SpeakTextAsync(TranslatedText.Text))
                        if (result.Reason == ResultReason.SynthesizingAudioCompleted)
                        {
                            sb.Append($"Speech synthesized to speaker for text [{RecognitionText.Text}]");
                        }
                        else if (result.Reason == ResultReason.Canceled)
                        {
                            var cancellation = SpeechSynthesisCancellationDetails.FromResult(result);
                            sb.Append($"CANCELED: Reason={cancellation.Reason}");

                            if (cancellation.Reason == CancellationReason.Error)
                            {
                                sb.Append($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                                sb.Append($"CANCELED: ErrorDetails=[{cancellation.ErrorDetails}]");
                                sb.Append($"CANCELED: Did you update the subscription info?");
                            }
                        }
                }
            }
            catch (Exception ex)
            {
                UpdateUI(ex.Message);
            }
        }

        //private async void OnTranslateButtonClicked(object sender, EventArgs e)
        //{
        //    string result = await TranslateTextRequest(subscriptionKey, route, RecognitionText.Text);
        //    UpdateUI(result);
        //}

        private async void OnTranslateButtonClicked(object sender, EventArgs e)
        {
            string inputText = RecognitionText.Text;
            try
            {
                object[] body = { new { Text = inputText } };
                var requestBody = JsonConvert.SerializeObject(body);

                using (var client = new HttpClient())
                using (var request = new HttpRequestMessage())
                {
                    // Build the request.
                    // Set the method to Post.
                    request.Method = HttpMethod.Post;
                    // Construct the URI and add headers.
                    request.RequestUri = new Uri(endpoint + route);
                    request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                    request.Headers.Add("Ocp-Apim-Subscription-Key", key_var);

                    HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);

                    string result = await response.Content.ReadAsStringAsync();
                    StringBuilder sb = new StringBuilder();
                    TranslationResult[] deserializedOutput = JsonConvert.DeserializeObject<TranslationResult[]>(result);
                    // Iterate over the deserialized results.
                    // Print the detected input language and confidence score.
                    //sb.Append("Detected input language: {0}\nConfidence score: {1}\n", o.DetectedLanguage.Language, o.DetectedLanguage.Score);
                    // Iterate over the results and print each translation.
                    foreach (TranslationResult o in deserializedOutput)
                    {
                        foreach (Translation t in o.Translations)
                        {
                            sb.Append(t.Text);
                        }
                    }
                    UpdateUI2(sb.ToString());
                }
            }
            catch (Exception ex)
            {
                UpdateUI2(ex.Message);
            }
        }
    }
}

