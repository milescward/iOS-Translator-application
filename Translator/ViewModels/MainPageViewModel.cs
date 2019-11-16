using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Translator.Data;

namespace Translator.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private readonly MockDataSource _dataSource;

        public ObservableCollection<Language> Languages { get; set; }
        public Dictionary<string, string> LangCodeDictionary { get; set; }

        public MainPageViewModel()
        {
            _dataSource = new MockDataSource();
            Languages = _dataSource.Languages;
            LoadData();

        }

        private void LoadData()
        {
            LangCodeDictionary = new Dictionary<string, string>();
  
            foreach (var language in Languages)
            {
                LangCodeDictionary.Add(language.Name, language.Code);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

