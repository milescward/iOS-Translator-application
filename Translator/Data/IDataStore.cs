using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Translator.Data
{
    public interface IDataStore
    {
        Task<ObservableCollection<Language>> GetLanguagesAsync();
    }
}
