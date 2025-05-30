using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using ParkingApp.Services;
using ParkingApp.Views;
using System.IO;
using System.Threading.Tasks;

namespace ParkingApp
{
    public partial class App : Application
    {
        // Đảm bảo không null
        public static DatabaseService Database { get; private set; } = null!;

        public App()
        {
            InitializeComponent();

            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "parking1.db");
            Database = new DatabaseService(dbPath);

            _ = EnsureAdminAccountExists();

            var role = Preferences.Get("role", null);
            if (role == "Admin")
                MainPage = new AppShell();  // test trực tiếp trang

            //MainPage = new AdminShell();

            else if (role == "Staff")
                MainPage = new StaffShell();
            else
                MainPage = new NavigationPage(new LoginPage());
        }

        private async Task EnsureAdminAccountExists()
        {
            var user = await Database.GetUserAsync("admin", "123456");
            if (user == null)
            {
                await Database.SaveUserAsync(new Models.User
                {
                    Username = "admin",
                    Password = "123456",
                    Role = "Admin"
                });
            }
        }

        internal static void ResetDatabase()
        {
            throw new NotImplementedException();
        }
    }
}
