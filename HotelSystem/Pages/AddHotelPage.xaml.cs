using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для AddHotelPage.xaml
    /// </summary>
    public partial class AddHotelPage : Page
    {
        public byte[] bytes;
        public AddHotelPage()
        {
            InitializeComponent();
            var counties = App.Connection.Country.ToList();
            cbCounrty.ItemsSource = counties;
        }

        private void OpenImg_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();

            if (dialog.ShowDialog() == true)
            {
                img.Source = new BitmapImage(new Uri(dialog.FileName));
                bytes = File.ReadAllBytes(dialog.FileName);
            }
        }

        private void AddHotel_Click(object sender, RoutedEventArgs e)
        {
            if (TbAddress.Text != "" && CbRate.SelectedItem != null && cbCounrty.SelectedItem != null && TbName.Text != "" && img.Source != null)
            {
                string rate = CbRate.SelectedIndex.ToString();
                App.Connection.Hotel.Add(new ADO.Hotel
                {
                    Name = TbName.Text,
                    Address = TbAddress.Text,
                    Photo = bytes,
                    Rate = Convert.ToInt32(rate),
                    Id_Country = (cbCounrty.SelectedItem as ADO.Country).Id
                });
                App.Connection.SaveChanges();
                MessageBox.Show("Отель успешно добавлен", "Успех", MessageBoxButton.OK, MessageBoxImage.Information); 
            }
            else
            {
                MessageBox.Show("Пожалуйста, заполните все поля, в том числе выберите изображение", "Ошибка добавления", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminPage());
        }
    }
}
