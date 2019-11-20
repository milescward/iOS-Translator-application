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
                new Language { Name ="Italian", Code="it-IT" },
                new Language { Name ="Chinese Simplified", Code="zh-CN"},
                new Language { Name ="Spanish", Code="es-MX"},
                new Language { Name ="Russian", Code="ru-RU"},
                new Language { Name ="Norwegian", Code="nb-NO"},
                new Language { Name ="Korean", Code="ko-KR"},
                new Language { Name ="Hungarian", Code="hu-HU"},
                new Language { Name ="Dutch", Code="nl-NL"},
                new Language { Name ="Arabic (Egypt)", Code="ar-EG"},
                new Language { Name ="Arabic (Saudi Arabia)", Code="ar-SA"},
                new Language { Name ="Afrikaans", Code="af"},
                new Language { Name ="Bangla", Code="bn"},
                new Language { Name ="Bulgarian", Code="bg-BG"},
                new Language { Name ="Catalan", Code="ca-ES"},
                new Language { Name ="Croatian", Code="hr-HR"},
                new Language { Name ="Danish", Code="da-DK"},
                new Language { Name ="Finnish", Code="fi-FI"},
                new Language { Name ="French", Code="fr-FR"},
                new Language { Name ="Greek", Code="hl-GR"},
                new Language { Name ="Hindi", Code="hi-IN"},
                new Language { Name ="Icelandic", Code="is"},
                new Language { Name ="Persian", Code="fa"},
                new Language { Name ="Queretaro Otomi", Code="otq"},
                new Language { Name ="Tahitian", Code="ty"},
                new Language { Name ="Tamil", Code="ta-IN"},
                new Language { Name ="Telugu", Code="te-IN"},
                new Language { Name ="Thai", Code="th-TH"},
                new Language { Name ="Ukranian", Code="uk"},
                new Language { Name ="Urdu", Code="ur"},
                new Language { Name ="Welsh", Code="cy"},
                new Language { Name ="Yucatec Maya", Code="yua"},
                new Language { Name ="Romanian", Code="ro-RO"},
                new Language { Name ="Fijian", Code="fj"},
                new Language { Name ="Swedish", Code="sv-SE"}
            };
        }

        public async Task<ObservableCollection<Language>> GetLanguagesAsync()
        {
            return await Task.FromResult(Languages);
        }
    }
}
