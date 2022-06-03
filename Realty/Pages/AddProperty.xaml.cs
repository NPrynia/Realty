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
using Realty.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Realty.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddProperty.xaml
    /// </summary>
    public partial class AddProperty : Page
    {
        public AddProperty()
        {
            InitializeComponent();
        }

        private void btnAddHouse_Click(object sender, RoutedEventArgs e)
        {
            AddHouseWindow addHouseWindow = new AddHouseWindow();
            addHouseWindow.ShowDialog();
        }

        private void btnAddApartment_Click(object sender, RoutedEventArgs e)
        {
            AddApartmentWindow addApartmentWindow = new AddApartmentWindow();
            addApartmentWindow.ShowDialog();
        }
    }
}
