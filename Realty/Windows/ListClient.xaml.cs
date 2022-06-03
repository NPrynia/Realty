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
    /// Логика взаимодействия для ListClient.xaml
    /// </summary>
    public partial class ListClient : Window
    {

        public ListClient()
        {
            InitializeComponent();
            ClassHelper.Client.client =  new EF.Client();
            lvClient.ItemsSource = ClassHelper.AppData.Context.Client.ToList();
        }

        private void lvClient_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            if (lvClient.SelectedItem is EF.Client)
            {

                var clientInList = lvClient.SelectedItem as EF.Client;
                ClassHelper.Client.client = clientInList;
                this.DialogResult = true;
                this.Close();
            }

        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<EF.Client> clients = new List<EF.Client>();
            clients = ClassHelper.AppData.Context.Client.Where((i => i.FIO.Contains(tbSearch.Text.ToLower()) ||
                i.Phone.ToLower().Contains(tbSearch.Text.ToLower()) ||
                i.SerialPassport.ToLower().Contains(tbSearch.Text.ToLower()) ||
                i.NumberPassport.ToLower().Contains(tbSearch.Text.ToLower()))).ToList();
            lvClient.ItemsSource = clients;
                
        }

        private void btnClost_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
