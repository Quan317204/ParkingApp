using Microsoft.Maui.Controls;
using System.Linq;

namespace ParkingApp.Views
{
    public partial class StatsPage : ContentPage
    {
        public StatsPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var list = await App.Database.GetEntriesAsync();
            var today = DateTime.Today;
            var todayList = list.Where(x => x.EntryTime.Date == today);
            var revenue = todayList.Where(x => x.Fee > 0).Sum(x => x.Fee);

            CountLabel.Text = $"Số lượt xe vào hôm nay: {todayList.Count()}";
            RevenueLabel.Text = $"Tổng doanh thu hôm nay: {revenue:N0} đ";
        }
    }
}
