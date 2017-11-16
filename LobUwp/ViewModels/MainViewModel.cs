using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using LobUwp.Services;
using System.Threading.Tasks;
using LobUwp.Models;
using LobUwp.Helpers;

namespace LobUwp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<Customer> Customers { get; private set; }

        public async Task LoadCustomersAsync()
        {
            var customers = await DataService.GetCustomersAsync();
            if (customers != null)
            {
                Customers = new ObservableCollection<Customer>(customers);
                RaisePropertyChanged("Customers");
                Singleton<LiveTileService>.Instance.UpdateCustomerCount(Customers.Count);
            }
        }
        public MainViewModel()
        {
            LoadCustomersAsync();
        }
    }
}
