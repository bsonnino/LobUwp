using LobUwp.Models;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using LobUwp.Services;

namespace LobUwp.Views
{
    public sealed partial class SalesDetailControl : UserControl
    {
        public Order MasterMenuItem
        {
            get { return GetValue(MasterMenuItemProperty) as Order; }
            set { SetValue(MasterMenuItemProperty, value); }
        }

        public static readonly DependencyProperty MasterMenuItemProperty =
            DependencyProperty.Register("MasterMenuItem", typeof(Order), typeof(SalesDetailControl),
                new PropertyMetadata(null, OnMasterMenuItemChangedAsync));

        private static async void OnMasterMenuItemChangedAsync(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var newOrder = e.NewValue as Order;
            if (newOrder != null && newOrder.OrderItems == null)
                newOrder.OrderItems = await DataService.GetOrderItemsAsync((int)newOrder.OrderId);
        }

        public SalesDetailControl()
        {
            InitializeComponent();
        }
    }
}
