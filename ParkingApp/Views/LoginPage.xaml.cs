using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using ParkingApp.Models;

namespace ParkingApp.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            var username = UsernameEntry.Text?.Trim();
            var password = PasswordEntry.Text?.Trim();

            var user = await App.Database.GetUserAsync(username, password);

            if (user == null)
            {
                await DisplayAlert("Sai thông tin", "Đăng nhập thất bại", "OK");
                return;
            }

            Preferences.Set("username", user.Username);
            Preferences.Set("role", user.Role);

            if (user.Role == "Admin")
                Application.Current.MainPage = new AdminShell();
            else
                Application.Current.MainPage = new StaffShell();
        }
    }
}
