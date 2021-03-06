using Gu.Wpf.FlipView;
using Realty.ClassHelper;
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

namespace Realty.Pages
{
    /// <summary>
    /// Логика взаимодействия для BuyHouse.xaml
    /// </summary>
    public partial class BuyApartment : Page
    {
        List<string> listSort = new List<string>()
        {
            "По умолчанию",
            "По Цене",
            "По Адресу",
            "По количетсву комнат",
            "По количетсву этажей"
           ,
        };
        List<EF.Apartment> apartments = new List<EF.Apartment>();
        int qtyElementl = 0;
        public BuyApartment()
        {
            InitializeComponent();
            cbSort.ItemsSource = listSort;
            apartments = ClassHelper.AppData.Context.Apartment.Where(i => i.IsDelete == false && i.ForRent == false).ToList();
            lvBuyApartment.ItemsSource = apartments;
        }
       

        private void Filter()
        {

            qtyElementl = 0;
            List<EF.Apartment> apartments = new List<EF.Apartment>();
            apartments = ClassHelper.AppData.Context.Apartment.Where(i => i.IsDelete == false && i.ForRent == false).ToList();
            apartments = apartments.Where
                (i => i.adressApartment.ToLower().Contains(tbSearch.Text.ToLower()) ||
                 Convert.ToString(i.Cost).Contains(tbSearch.Text.ToLower())).ToList();

            switch (cbSort.SelectedIndex)
            {

                case 0:
                    apartments = apartments.OrderBy(i => i.IDApartment).ToList();
                    lvBuyApartment.ItemsSource = apartments;
                    break;
                case 1:
                    apartments = apartments.OrderBy(i => i.Cost).ToList();
                    lvBuyApartment.ItemsSource = apartments;
                    break;
                case 2:
                    apartments = apartments.OrderBy(i => i.adressApartment).ToList();
                    lvBuyApartment.ItemsSource = apartments;
                    break;
                case 3:
                    apartments = apartments.OrderBy(i => i.QtyFloor).ToList();
                    lvBuyApartment.ItemsSource = apartments;
                    break;
                case 4:
                    apartments = apartments.OrderBy(i => i.QtyRoom).ToList();
                    lvBuyApartment.ItemsSource = apartments;
                    break;
            }
            lvBuyApartment.ItemsSource = apartments;

        }


        private void lvBuyApartment_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lvBuyApartment.SelectedItem is EF.Apartment)
            {
                qtyElementl = 0;
                var apartment = lvBuyApartment.SelectedItem as EF.Apartment;
                Windows.AddApartmentWindow addApartment = new Windows.AddApartmentWindow(apartment);
                addApartment.ShowDialog();
                Filter();
            }

        }
        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void LoadedLv(object sender, RoutedEventArgs e)
        {
            try 
            {
                List<Image> images = new List<Image>();
                FlipView fv = new FlipView();

                var idApart = apartments[qtyElementl];
                var idImageApart = AppData.Context.ImageApartment.ToList().
                       Where(i => i.IDApartment == idApart.IDApartment).FirstOrDefault();

                if (idImageApart != null)
                {
                    var imageCollection = ClassHelper.AppData.Context.ImageApartment.Where(a => a.IDApartment == idApart.IDApartment).ToList();
                    var image = from p in imageCollection select new { p.Image }.Image;
                    foreach (byte[] byteImg in image)
                    {

                        using (MemoryStream stream = new MemoryStream(byteImg))
                        {
                            Image i = new Image();
                            BitmapImage bitmapImage = new BitmapImage();
                            bitmapImage.BeginInit();
                            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                            bitmapImage.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                            bitmapImage.StreamSource = stream;
                            bitmapImage.EndInit();
                            i.Source = bitmapImage;
                            images.Add(i);
                        }
                    }
                    fv = sender as FlipView;
                    fv.ItemsSource = images;
                }
                else
                {
                    Image img = new Image();
                    img.Source = new BitmapImage(new Uri("/Res/DefaultApartment.png", UriKind.RelativeOrAbsolute));
                    images.Add(img);
                    fv.ItemsSource = images;
                }
                qtyElementl++;
            }
            catch
            {

            }
           
        }

        private void lvDel_KeyDown(object sender, KeyEventArgs e)
        {
            if (lvBuyApartment.SelectedItem is EF.Apartment)
            {
                var resClick = MessageBox.Show("Удалить квартиру  ?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (resClick == MessageBoxResult.No)
                {
                    return;
                }
                var apartment = lvBuyApartment.SelectedItem as EF.Apartment;
                apartment.IsDelete = true;
                ClassHelper.AppData.Context.SaveChanges();
                MessageBox.Show("Квартира удалена");
                Filter();

            }

        }
    }
}
