using System;

using LobUwp.ViewModels;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace LobUwp.Views
{
    public sealed partial class SalesPage : Page
    {
        private SalesViewModel ViewModel
        {
            get { return DataContext as SalesViewModel; }
        }

        public SalesPage()
        {
            InitializeComponent();
            Loaded += SalesPage_Loaded;
        }

        private async void SalesPage_Loaded(object sender, RoutedEventArgs e)
        {
            await ViewModel.LoadDataAsync(MasterDetailsViewControl.ViewState);
        }
    }
}
