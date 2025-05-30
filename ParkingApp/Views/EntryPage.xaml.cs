using Microsoft.Maui.Controls;
using ParkingApp.Models;
using System;

namespace ParkingApp.Views
{
    public partial class EntryPage : ContentPage
    {
        public EntryPage()
        {
            InitializeComponent();
        }

        private async void OnEntryClicked(object sender, EventArgs e)
        {
            var plate = LicensePlateEntry.Text?.Trim();
            var spot = ParkingSpotEntry.Text?.Trim();

            if (string.IsNullOrWhiteSpace(plate) || string.IsNullOrWhiteSpace(spot))
            {
                await DisplayAlert("Lỗi", "Vui lòng nhập đầy đủ biển số và vị trí", "OK");
                return;
            }

            var entry = new VehicleEntry { LicensePlate = plate, ParkingSpot = spot, EntryTime = DateTime.Now };
            await App.Database.SaveVehicleEntryAsync(entry);
            await DisplayAlert("Thành công", "Xe đã vào bãi", "OK");
            LicensePlateEntry.Text = ParkingSpotEntry.Text = "";
        }
    }
}
