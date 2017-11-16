using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using LobUwp.Models;
using LobUwp.Services;

using Microsoft.Toolkit.Uwp.UI.Controls;
using LobUwp.Helpers;

namespace LobUwp.ViewModels
{
    public class SalesViewModel : ViewModelBase
    {
        private Order _selected;

        public Order Selected
        {
            get => _selected;
            set
            {
                Set(ref _selected, value);
            }
        }

        public ObservableCollection<Order> Orders { get; private set; }

        public async Task LoadDataAsync(MasterDetailsViewState viewState)
        {
            var orders = await DataService.GetOrdersAsync();
            if (orders != null)
            {
                Orders = new ObservableCollection<Order>(orders);
                RaisePropertyChanged("Orders");
                Singleton<LiveTileService>.Instance.UpdateOrderCount(Orders.Count);
            }
            if (viewState == MasterDetailsViewState.Both)
            {
                Selected = Orders.FirstOrDefault();
            }
        }
    }
}
