using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HotelSystem.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        public AuthPage()
        {
            InitializeComponent();
        }

        private void BtnSignUp_Click(object sender, RoutedEventArgs e)
        {
            if (Login.Text != "" && PbPassword.Password != null)
            {
                var logger = App.Connection.Logger.FirstOrDefault(x => x.Login == Login.Text);
                if (logger != null)
                {
                    if (logger.Password == PbPassword.Password)
                    {
                        var user = App.Connection.User.Where(x => x.Id_Logger == logger.Id).FirstOrDefault();
                        if (user.Id_role == 2)
                        {
                            App.currentUser = user;
                            NavigationService.Navigate(new ClientPage());
                        }
                        else
                        {
                            App.currentUser = user;
                            NavigationService.Navigate(new AdminPage());
                        }
                    }
                    else
                    {
                        MessageBox.Show("Неверный пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Такого пользователя не существует", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
