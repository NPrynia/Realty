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
using System.Windows.Shapes;

namespace Realty.Windows
{
    /// <summary>
    /// Логика взаимодействия для extendTimeRentWindow.xaml
    /// </summary>
    public partial class extendTimeRentWindow : Window
    {

        EF.RentApartment rentApartment;
        EF.RentHouse rentHouse;
        bool isHouse = false;

        public extendTimeRentWindow(EF.RentApartment rent)
        {
            InitializeComponent();
            dpTimeRentEnd.DisplayDateStart = rent.DateEndRent;
            rentApartment = rent;
        }


        public extendTimeRentWindow(EF.RentHouse rent)
        {
            InitializeComponent();
            dpTimeRentEnd.DisplayDateStart = rent.DateEndRent;
            rentHouse = rent;
            isHouse = true;
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch
            {
            }
        }

        private void btnClose_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if(dpTimeRentEnd.SelectedDate == null)
            {
                MessageBox.Show("Выберите дату");
                return;
            }
            if (isHouse == false)
            {

                rentApartment.DateEndRent = (DateTime)dpTimeRentEnd.SelectedDate;
            }
            else
            {

                rentHouse.DateEndRent = (DateTime)dpTimeRentEnd.SelectedDate;
            }
            ClassHelper.AppData.Context.SaveChanges();
            this.Close();
        }
    }
}
