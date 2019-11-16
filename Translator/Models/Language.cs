using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Translator
{
    public class Language : INotifyPropertyChanged
    {
        private string _name;
        private string _code;

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

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
