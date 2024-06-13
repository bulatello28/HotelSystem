using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using HotelSystem.ADO;

namespace HotelSystem
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static HotelPracticeEntities2 Connection = new HotelPracticeEntities2();
        public static User currentUser;
    }
}
