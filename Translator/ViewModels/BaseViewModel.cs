using System;
using System.ComponentModel;

namespace Translator.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
