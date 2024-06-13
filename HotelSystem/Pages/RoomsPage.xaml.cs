using HotelSystem.ADO;
using HotelSystem.Classes;
using HotelSystem.Windows;
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
    /// Логика взаимодействия для RoomsPage.xaml
    /// </summary>
    public partial class RoomsPage : Page
    {
        public RoomsPage(HotelInfo hotel)
        {
            InitializeComponent();
            Hotel currHotel = App.Connection.Hotel.Where(x => x.Id == hotel.Id).FirstOrDefault();
            var rooms = App.Connection.Room.Where(x => x.Id_Hotel == hotel.Id).ToList();
            LvRooms.ItemsSource = Query(hotel);
            TbIdHotel.Text = hotel.Id.ToString();
            
        }
        
        private bool IsRoomFree(Room room)
        {
            var booking = App.Connection.Booking.Where(x => x.Id_Room == room.Id);
            if (booking == null)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

        private void BtnBook_Click(object sender, RoutedEventArgs e)
        {
            var id = (int)((Button)sender).Tag;
            var currRoom = App.Connection.Room.Where(x => x.Id == id).FirstOrDefault();
            var widnow = new BookingRoomWindow(currRoom);
            widnow.Show();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ClientPage());
        }

        private List<Room> Query(HotelInfo hotel)
        {
            var bookingRooms = App.Connection.Booking.Select(x => x.Id_Room).ToList();
            var freeRooms = App.Connection.Room.Where(x => !bookingRooms.Contains(x.Id) && x.Id_Hotel == hotel.Id).ToList();
            return freeRooms;
        }


    }
}
