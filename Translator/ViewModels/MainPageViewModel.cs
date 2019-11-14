using System;
using System.Collections.Generic;

namespace Translator.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        public ICollection<String> Languages { get; set; }
        public ICollection<String> LangCodes { get; set; }

        public MainPageViewModel()
        {
        }
    }
}
