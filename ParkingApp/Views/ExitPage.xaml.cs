using Microsoft.Maui.Controls;
using System;
using System.Linq;

namespace ParkingApp.Views
{
    public partial class ExitPage : ContentPage
    {
        public ExitPage()
        {
            InitializeComponent();
        }

        private async void OnExitClicked(object sender, EventArgs e)
        {
            var plate = LicensePlateEntry.Text?.Trim();
            var entries = await App.Database.GetEntriesAsync();
            var entry = entries.FirstOrDefault(x => x.LicensePlate == plate && x.ExitTime == null);

            if (entry == null)
            {
                await DisplayAlert("Không tìm thấy", "Xe không tồn tại hoặc đã ra", "OK");
                return;
            }

            entry.ExitTime = DateTime.Now;
            var duration = (entry.ExitTime.Value - entry.EntryTime).TotalHours;
            entry.Fee = (double)((decimal)Math.Ceiling(duration) * 3000);

            await App.Database.SaveVehicleEntryAsync(entry);
            await DisplayAlert("Thông tin", $"Xe ra thành công. Phí: {entry.Fee:N0} đ", "OK");
            LicensePlateEntry.Text = "";
        }
    }
}