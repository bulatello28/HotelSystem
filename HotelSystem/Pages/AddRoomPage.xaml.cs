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
    /// Логика взаимодействия для AddRoomPage.xaml
    /// </summary>
    public partial class AddRoomPage : Page
    {
        public byte[] bytesForFirstImg;
        public byte[] bytesForSecondImg;
        public byte[] bytesForThirdImg;
        public AddRoomPage()
        {
            InitializeComponent();
            var hotels = App.Connection.Hotel.ToList();
            cbHotels.ItemsSource = hotels;
        }

        private void OpenThirdImg_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();

            if (dialog.ShowDialog() == true)
            {
                ImgRoom3.Source = new BitmapImage(new Uri(dialog.FileName));
                bytesForThirdImg = File.ReadAllBytes(dialog.FileName);
            }
        }

        private void OpenSecondImg_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();

            if (dialog.ShowDialog() == true)
            {
                ImgRoom2.Source = new BitmapImage(new Uri(dialog.FileName));
                bytesForSecondImg = File.ReadAllBytes(dialog.FileName);
            }
        }

        private void OpenFirstImg_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();

            if (dialog.ShowDialog() == true)
            {
                ImgRoom1.Source = new BitmapImage(new Uri(dialog.FileName));
                bytesForFirstImg = File.ReadAllBytes(dialog.FileName);
            }
        }

        private void AddRoom_Click(object sender, RoutedEventArgs e)
        {
            if (cbHotels.SelectedItem != null && TbCost.Text != "" && ImgRoom1.Source != null && ImgRoom2.Source != null && ImgRoom3.Source != null)
            {
                if (int.TryParse(TbCost.Text, out int a))
                {
                    int price = Convert.ToInt32(TbCost.Text);
                    App.Connection.Room.Add(new ADO.Room
                    {
                        Id_Hotel = (cbHotels.SelectedItem as ADO.Hotel).Id,
                        Cost = price,
                        RoomImg1 = bytesForFirstImg,
                        RoomImg2 = bytesForSecondImg,
                        RoomImg3 = bytesForThirdImg,

                    });
                    App.Connection.SaveChanges();
                    MessageBox.Show("Комната успешно добавлена", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Пожалуйста, введите корректную цену", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, заполните все поля", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
