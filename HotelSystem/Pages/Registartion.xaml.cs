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
using HotelSystem.ADO;

namespace HotelSystem.Pages
{
    /// <summary>
    /// Логика взаимодействия для Registartion.xaml
    /// </summary>
    public partial class Registartion : Page
    {
        public Registartion()
        {
            InitializeComponent();
        }

        

        private void BtnSignUp_Click(object sender, RoutedEventArgs e)
        {
            if (TbFName.Text != "" && TbLastName.Text != "" && TbPhoneNubmer.Text != "" && PbPassword.Password != null && Login.Text != "")
            {
                var logger = App.Connection.Logger.FirstOrDefault(x => x.Login == Login.Text);
                if (logger == null)
                {
                    var user = new User
                    {
                        FirstName = TbFName.Text,
                        LastName = TbLastName.Text,
                        PhoneNumber = TbPhoneNubmer.Text,
                        Id_role = 2
                    };
                    var log = new Logger
                    {
                        Login = Login.Text,
                        Password = PbPassword.Password
                    };
                    App.Connection.Logger.Add(log);
                    App.Connection.User.Add(user);
                    App.Connection.SaveChanges();
                    MessageBox.Show("Пользователь успешно зарегистрирован", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.Navigate(new AuthPage());
                }
                else
                {
                    MessageBox.Show("Пользователь с таким логином уже сущесвует", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
