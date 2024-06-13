using HotelSystem.ADO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HotelSystem.Windows
{
    /// <summary>
    /// Логика взаимодействия для BookingRoomWindow.xaml
    /// </summary>
    public partial class BookingRoomWindow : Window
    {
        public BookingRoomWindow(Room room)
        {
            InitializeComponent();
            TbCostForNight.Text = room.Cost.ToString();
            idRoom.Text = room.Id.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (DpEnd.SelectedDate != null && DpStart.SelectedDate != null)
            {
                if (DpEnd.SelectedDate > DpStart.SelectedDate)
                {
                    App.Connection.Booking.Add(new Booking
                    {
                        Id_User = App.currentUser.Id,
                        Id_Room = Convert.ToInt32(idRoom.Text),
                        StartDate = (DateTime)DpStart.SelectedDate,
                        EndDate = (DateTime)DpEnd.SelectedDate,
                    });
                    App.Connection.SaveChanges();
                    MessageBox.Show("Комната успешно забранировано", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();
                    
                }
                else
                {
                    MessageBox.Show("Дата выселения не может быть раньше даты заселения", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите даты заселения и выселения", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private int GetCountOfDays()
        {
            if (DpStart.SelectedDate != null && DpEnd.SelectedDate != null)
            {
                if (DpEnd.SelectedDate > DpStart.SelectedDate)
                {
                    TimeSpan days = DpEnd.SelectedDate.Value.Subtract(DpStart.SelectedDate.Value);
                    int countOfDays = (int)days.TotalDays;
                    return countOfDays;
                }
                else
                {
                    MessageBox.Show("Дата выселения не может быть раньше даты заселения", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return 0;
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите даты заселения и выселения", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return 0;
            }
            
        }
        private void BtnCheckPrice_Click(object sender, RoutedEventArgs e)
        {
            int price = Convert.ToInt32(TbCostForNight.Text);
            TbCost.Text = (price * GetCountOfDays()).ToString();
        }
    }
}
