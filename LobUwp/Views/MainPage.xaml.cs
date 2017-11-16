using System;

using LobUwp.ViewModels;

using Windows.UI.Xaml.Controls;

namespace LobUwp.Views
{
    public sealed partial class MainPage : Page
    {
        private MainViewModel ViewModel
        {
            get { return DataContext as MainViewModel; }
        }

        public MainPage()
        {
            InitializeComponent();
        }
    }
}
