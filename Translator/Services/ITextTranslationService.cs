using System.Threading.Tasks;

namespace Translator.Services
{
    public interface ITextTranslationService
    {
        Task<string> TranslateTextAsync(string text);
    }
}
