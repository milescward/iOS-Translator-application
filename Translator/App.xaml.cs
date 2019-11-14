using System;
using System.IO;
using SQLite;
using Translator.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Translator
{
    public partial class App : Application
    {
        //static TranslationDb database;

        //public static TranslationDb Database
        //{
        //    get
        //    {
        //        if (database == null)
        //        {
        //            database = new TranslationDb(
        //              Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TranslationSQLite.db3"));
        //        }
        //        return database;
        //    }
        //}

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
