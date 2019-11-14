using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Translator.Data
{
    public class MockDataSource : IDataStore
    {
        public static readonly IDictionary<string, string> LanguageD;
        public static readonly IList<string> Languages;
        public static readonly IList<string> LangCodes;

        static MockDataSource()
        {
            LanguageD = new Dictionary<string, string>
            {
                { "English US", "en-US"},
                { "English UK", "en-UK"},
                { "Italian", "it" },
                { "Chinese Simplified", "zh-Hans"},
                { "Chinese Traditional", "zh-Hant"},
                { "Spanish", "es"},
                { "Russian", "ru"},
                { "Norwegian", "nb"},
                { "Korean", "ko"},
                { "Hungarian", "hu"},
                { "Dutch", "nl"},
            };

            Languages = LanguageD.Keys.ToList();
            LangCodes = LanguageD.Values.ToList();
        }


        public async Task<IDictionary<string, string>> GetLangDictAsync()
        {
            return await Task.FromResult(LanguageD);
        }

        public async Task<IList<string>> GetLanguagesAsync()
        {
            return await Task.FromResult(Languages);
        }

        public async Task<IList<string>> GetLangCodesAsync()
        {
            return await Task.FromResult(LangCodes);
        }


    }
}
