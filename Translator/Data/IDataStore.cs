using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Translator.Data
{
    public interface IDataStore
    {
        Task<IDictionary<string, string>> GetLangDictAsync();
        Task<IList<string>> GetLanguagesAsync();
        Task<IList<string>> GetLangCodesAsync();
    }
}
