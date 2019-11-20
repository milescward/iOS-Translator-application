using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Translator
{
    public class Language : INotifyPropertyChanged
    {
        private string _name;
        private string _code;
        private string _synthCode;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }
        public string Code
        {
            get => _code;
            set
            {
                _code = value;
                RaisePropertyChanged(nameof(Code));
            }
        }
        public string SynthCode
        {
            get => _synthCode;
            set
            {
                _synthCode = value;
                RaisePropertyChanged(nameof(_synthCode));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
