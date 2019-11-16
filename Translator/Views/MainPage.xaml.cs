using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Microsoft.CognitiveServices.Speech;
using Translator.Services;
using Xamarin.Forms;
using System.Net.Http;
using Newtonsoft.Json;
using Translator.Data;
using System.Collections.ObjectModel;
using Translator.ViewModels;
using System.Linq;

namespace Translator
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private const string key_var = "";
        private static readonly string subscriptionKey = Environment.GetEnvironmentVariable(key_var);
        private const string endpoint_var = "";
        private static readonly string endpoint = Environment.GetEnvironmentVariable(endpoint_var);
        private const string speech_key = "";
        protected string route = "https://api.cognitive.microsofttranslator.com/translate?api-version=3.0";

        MainPageViewModel viewModel = new MainPageViewModel();


        public MainPage()
        {
            InitializeComponent();
            
            this.BindingContext = viewModel;
            LoadData();
        }

        private void LoadData()
        {
            TargetLanguage.ItemsSource = viewModel.LangCodeDictionary.Keys.ToList();
            SourceLanguage.ItemsSource = viewModel.LangCodeDictionary.Keys.ToList();
        }

        private void OnTargetLanguageChosen(object sender, EventArgs eventArgs)
        {
            var selectedIndex = ((Picker)sender).SelectedIndex;
            if (selectedIndex != -1)
            {
                TargetLanguage.SelectedItem = (string)TargetLanguage.ItemsSource[selectedIndex];
            }
        }

        private void OnSourceLanguageChosen(object sender, EventArgs eventArgs)
        {
            var selectedIndex = ((Picker)sender).SelectedIndex;
            if (selectedIndex != -1)
            {
                SourceLanguage.SelectedItem = (string)SourceLanguage.ItemsSource[selectedIndex];
            }
        }

        private void Save_ClickedAsync(object sender, EventArgs eventArgs)
        {
            DisplayAlert("", "", "", "");
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

        private async void OnTranslateButtonClicked(object sender, EventArgs e)
        {
            string targetCode = "&to=" + viewModel.LangCodeDictionary[(string)TargetLanguage.SelectedItem];
            string sourceCode = "&from=" + viewModel.LangCodeDictionary[(string)SourceLanguage.SelectedItem];
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
                    request.RequestUri = new Uri(endpoint + route + sourceCode + targetCode);
                    request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                    request.Headers.Add("Ocp-Apim-Subscription-Key", key_var);

                    HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);

                    string result = await response.Content.ReadAsStringAsync();
                    StringBuilder sb = new StringBuilder();
                    TranslationResult[] deserializedOutput = JsonConvert.DeserializeObject<TranslationResult[]>(result);
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

