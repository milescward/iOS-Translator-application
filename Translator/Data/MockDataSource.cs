using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Translator.Data
{
    public class MockDataSource : IDataStore
    {
        public ObservableCollection<Language> Languages;

        public MockDataSource()
        {
            LoadData();
        }

        private void LoadData()
        {
            Languages = new ObservableCollection<Language>
            {
                new Language { Name = "English US", Code="en-US"},
                new Language { Name ="English UK", Code="en-UK"},
                new Language { Name ="Italian", Code="it" },
                new Language { Name ="Chinese Simplified", Code="zh-Hans"},
                new Language { Name ="Chinese Traditional", Code="zh-Hant"},
                new Language { Name ="Spanish", Code="es"},
                new Language { Name ="Russian", Code="ru"},
                new Language { Name ="Norwegian", Code="nb"},
                new Language { Name ="Korean", Code="ko"},
                new Language { Name ="Hungarian", Code="hu"},
                new Language { Name ="Dutch", Code="nl"},
            };
        }

        public async Task<ObservableCollection<Language>> GetLanguagesAsync()
        {
            return await Task.FromResult(Languages);
        }
    }
}
