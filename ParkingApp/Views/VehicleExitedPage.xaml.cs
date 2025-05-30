using Microsoft.Maui.Controls;
using ParkingApp.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingApp.Views
{
    public partial class VehicleExitedPage : ContentPage
    {
        public VehicleExitedPage()
        {
            InitializeComponent();
            LoadYearOptions();
        }

        private void LoadYearOptions()
        {
            var currentYear = DateTime.Today.Year;
            for (int i = currentYear; i >= currentYear - 10; i--)
                YearPicker.Items.Add(i.ToString());
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadAllExitedVehicles();
        }

        private async Task LoadAllExitedVehicles()
        {
            var list = await App.Database.GetEntriesAsync();
            var exited = list.Where(x => x.ExitTime != null)
                             .OrderByDescending(x => x.ExitTime);
            ExitedVehiclesCollection.ItemsSource = exited;
        }

        private async void OnFilterByDateClicked(object sender, EventArgs e)
        {
            var date = FilterDatePicker.Date;
            var list = await App.Database.GetEntriesAsync();
            var result = list.Where(x => x.ExitTime != null && x.ExitTime.Value.Date == date.Date);
            ExitedVehiclesCollection.ItemsSource = result;
        }

        private async void OnFilterByMonthYearClicked(object sender, EventArgs e)
        {
            if (MonthPicker.SelectedItem == null || YearPicker.SelectedItem == null)
            {
                await DisplayAlert("Thiếu thông tin", "Vui lòng chọn tháng và năm.", "OK");
                return;
            }

            int month = (int)MonthPicker.SelectedItem;
            int year = int.Parse(YearPicker.SelectedItem.ToString());

            var list = await App.Database.GetEntriesAsync();
            var result = list.Where(x => x.ExitTime != null &&
                                         x.ExitTime.Value.Month == month &&
                                         x.ExitTime.Value.Year == year);
            ExitedVehiclesCollection.ItemsSource = result;
        }

        private async void OnViewAllClicked(object sender, EventArgs e)
        {
            await LoadAllExitedVehicles();
        }
    }
}
