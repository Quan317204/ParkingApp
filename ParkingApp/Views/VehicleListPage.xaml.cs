using Microsoft.Maui.Controls;
using System.Linq;

namespace ParkingApp.Views
{
    public partial class VehicleListPage : ContentPage
    {
        public VehicleListPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var list = await App.Database.GetEntriesAsync();
            VehicleCollection.ItemsSource = list.Where(x => x.ExitTime == null);
        }
    }
}