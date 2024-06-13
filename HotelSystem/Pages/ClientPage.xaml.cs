using HotelSystem.Classes;
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
    /// Логика взаимодействия для ClientPage.xaml
    /// </summary>
    public partial class ClientPage : Page
    {
        public ClientPage()
        {
            InitializeComponent();
            LvHotels.ItemsSource = FillHotelsInfo();
            CbCounties.ItemsSource = App.Connection.Country.ToList();

        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthPage());
        }

        private List<HotelInfo> FillHotelsInfo()
        {
            var hotels = App.Connection.Hotel.ToList();
            var hotelsInfo = new List<HotelInfo>();

            foreach (var hotel in hotels)
            {
                hotelsInfo.Add(new HotelInfo(hotel.Id, hotel.Photo, App.Connection.Country.Where(x => x.Id == hotel.Id_Country).FirstOrDefault().Name, hotel.Name, hotel.Rate, hotel.Address));
            }
            return hotelsInfo;
        }

        private void LvHotels_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var hotel = LvHotels.SelectedItem as HotelInfo;
            if (hotel == null)
            {
                return;
            }
            else
            {
                NavigationService.Navigate(new RoomsPage(hotel));
            }
        }

        private void CbCounties_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var country = CbCounties.SelectedItem as ADO.Country;
            //LvHotels.ItemsSource = FillHotelsInfo().Where(x => x.CountryName == country.Name).ToList();
            LvHotels.ItemsSource = LvHotels.ItemsSource.Cast<HotelInfo>().Where(x => x.CountryName == country.Name).ToList();
            LvHotels.UpdateLayout();

        }


        private void CbRateFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //LvHotels.ItemsSource = FillHotelsInfo().Where(x => x.Rate == (CbRateFilter.SelectedIndex + 1)).ToList();
            LvHotels.ItemsSource = LvHotels.ItemsSource.Cast<HotelInfo>().Where(x => x.Rate == (CbRateFilter.SelectedIndex + 1)).ToList();
            LvHotels.UpdateLayout();
        }

        private void RebootFilter_Click(object sender, RoutedEventArgs e)
        {
            LvHotels.ItemsSource = FillHotelsInfo();
            LvHotels.UpdateLayout();
        }

        private void TbSearhc_TextChanged(object sender, TextChangedEventArgs e)
        {
            var list = LvHotels.ItemsSource.Cast<HotelInfo>();
            var newlist = new List<HotelInfo>();
            foreach (var item in list)
            {
                if (item.Name.ToLower().Contains(TbSearhc.Text.ToLower()))
                {
                    newlist.Add(item);
                }
            }
            LvHotels.ItemsSource = newlist;

            if (TbSearhc.Text == "")
            {
                LvHotels.ItemsSource = FillHotelsInfo();
            }
        }
    }
}
