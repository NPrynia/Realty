using ModernWpf.Controls;
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
using System.Windows.Shapes;

namespace Realty.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddApartmentWindow.xaml
    /// </summary>
    public partial class AddApartmentWindow : Window
    {

        List<Image> images = new List<Image>();
        List<int> listQtyFlour = new List<int>()
        {
            1,
            2,
            3,
            4
           ,
        };


        List<byte[]> imageInByte = new List<byte[]>();
        public string pathPhoto;
        List<string> listQtyRoom = new List<string>();
        EF.Apartment editApartment = new EF.Apartment();
        bool isChange = false;
        public AddApartmentWindow()
        {
            InitializeComponent();
            for (int i = 1; i < 32; i++)
            {
                listQtyRoom.Add(Convert.ToString(i));
            }
            cbQtyFloor.ItemsSource = listQtyFlour;
            cbQtyRoom.ItemsSource = listQtyRoom;
            cbQtyRoom.MaxDropDownHeight = 200;


        }

        public AddApartmentWindow(EF.Apartment apartment)
        {
            InitializeComponent();
            btnOffer.Width = 200;
            List <string> qtyRoom= new List<string>();
            qtyRoom.Add(Convert.ToString(apartment.QtyRoom));
            List<string> qtyFloor = new List<string>();
            qtyFloor.Add(Convert.ToString(apartment.QtyFloor));
            btnOffer.Width = 200;
            tbCost.Text = Convert.ToString(apartment.Cost);
            tbSpaceHome.Text = Convert.ToString(apartment.Space);
            cbQtyFloor.ItemsSource = qtyFloor;
            cbQtyRoom.ItemsSource = qtyRoom;
            cbQtyFloor.SelectedIndex =0;
            cbQtyRoom.SelectedIndex =0;
            tbCost.Text = Convert.ToString(apartment.Cost);
            tbNumHouse.Text = Convert.ToString(apartment.NumberHouse);
            tbNumApartment.Text = Convert.ToString(apartment.NumberApartment);
            tbDesciption.Text = Convert.ToString(apartment.Description);

            if (apartment.HasBalcony)
            {

                tsBalcony.IsOn = true;
            }
            if ( apartment.ForRent == true)
            {
                rbRennt.IsChecked = true;
            }
            else
            {
                rbSale.IsChecked = true;
            }




            
            var imageCollection = ClassHelper.AppData.Context.ImageApartment.Where(a => a.IDApartment == apartment.IDApartment).ToList();
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
                    imageInByte.Add(byteImg);
                }
            }
            fvImg.ItemsSource = images;
            editApartment = apartment;
            isChange = true;
            var adressCollection = ClassHelper.AppData.Context.Adress.Where(i => i.IDStreet == editApartment.IDStreet).ToList();
            var adress = from p in adressCollection select new { p.Adress1 }.Adress1;
            asbAdress.ItemsSource = adress;
            asbAdress.Text = adress.ToList()[0];


        }


        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            //Validation
            if (ClassHelper.Validation.validationNum(tbCost.Text) == false)
            {
                MessageBox.Show("Некорректная цена", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (ClassHelper.Validation.validationNum(tbSpaceHome.Text) == false)
            {
                MessageBox.Show("Некорректная площадь квартиры", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (cbQtyFloor.SelectedIndex < 0)
            {
                MessageBox.Show("Выберите количество этажей", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (cbQtyRoom.SelectedIndex < 0)
            {
                MessageBox.Show("Выберите количество комнат", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var adressCollection = ClassHelper.AppData.Context.Adress.ToList();

            var idStreet = from p in adressCollection
                           where p.Adress1 == asbAdress.Text
                           select new { p.IDStreet }.IDStreet;

            if (idStreet.Count() == 0)
            {
                MessageBox.Show("Выберите адресс", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (ClassHelper.Validation.validationNum(tbNumHouse.Text) == false)
            {
                MessageBox.Show("Некорректный номер дома", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (ClassHelper.Validation.validationNum(tbNumApartment.Text) == false)
            {
                MessageBox.Show("Некорректный номер квартиры", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

           

            if (rbRennt.IsChecked == false && rbSale.IsChecked == false)
            {
                MessageBox.Show("Выберите для аренды или для продажи", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }




            //add
            if (isChange == false)
            {

                EF.Apartment addApartment = new EF.Apartment();
                addApartment.IsDelete = false;
                addApartment.Cost = Convert.ToDecimal(tbCost.Text);
                addApartment.Space = Convert.ToDouble(tbSpaceHome.Text);
                addApartment.QtyRoom = Convert.ToInt32(cbQtyFloor.Text);
                addApartment.QtyFloor = Convert.ToInt32(cbQtyRoom.Text);
                addApartment.IDStreet = idStreet.ToList()[0];
                addApartment.NumberHouse = tbNumHouse.Text;
                addApartment.NumberApartment = Convert.ToInt32(tbNumApartment.Text);
                addApartment.Description = (tbDesciption.Text);
                if (tsBalcony.IsOn)
                {

                    addApartment.HasBalcony = true;
                }
                if (rbRennt.IsChecked == true)
                {

                    addApartment.ForRent = true;
                }
                else
                {
                    addApartment.ForRent = false;
                }

                
                AppData.Context.Apartment.Add(addApartment);
                AppData.Context.SaveChanges();
                EF.ImageApartment image = new EF.ImageApartment();
                if (images.Count != 0)
                {
                    foreach (byte[] b in imageInByte)
                    {
                        image.Image = b;
                        image.IDApartment = addApartment.IDApartment;
                        AppData.Context.ImageApartment.Add(image);
                        AppData.Context.SaveChanges();
                    }
                }
                else
                {
                    var imageDefault = AppData.Context.ImageApartment.Where(i => i.IDImageApartment == 13).ToList().FirstOrDefault();
                    image.Image = imageDefault.Image;
                    image.IDApartment = addApartment.IDApartment;
                    AppData.Context.ImageApartment.Add(image);
                    AppData.Context.SaveChanges();
                }

              

                MessageBox.Show("Квартира добавлена");
                this.Close();

            }


            //change
            else
            {
                editApartment.Cost = Convert.ToDecimal(tbCost.Text);
                editApartment.Space = Convert.ToDouble(tbSpaceHome.Text);
                editApartment.QtyRoom = Convert.ToInt32(cbQtyFloor.Text);
                editApartment.QtyFloor = Convert.ToInt32(cbQtyRoom.Text);
                editApartment.IDStreet = idStreet.ToList()[0];
                editApartment.NumberHouse = tbNumHouse.Text;
                editApartment.NumberApartment = Convert.ToInt32(tbNumApartment.Text);
                editApartment.Description = (tbDesciption.Text);
                if (tsBalcony.IsOn)
                {

                    editApartment.HasBalcony = true;
                }
                if (rbRennt.IsChecked == true)
                {

                    editApartment.ForRent = true;
                }
                else
                {
                    editApartment.ForRent = false;
                }

                EF.ImageApartment image = new EF.ImageApartment();
                var imgApart = ClassHelper.AppData.Context.ImageApartment.Where(i => i.IDApartment == editApartment.IDApartment).ToList();
                var imgApartCopy = ClassHelper.AppData.Context.ImageApartment.Where(i => i.IDApartment == editApartment.IDApartment).ToList();
                foreach (var i in imgApartCopy)
                {
                    imgApart.Remove(i);
                    AppData.Context.ImageApartment.Remove(i);
                    AppData.Context.SaveChanges();
                }

                if (images.Count != 0)  
                {
                    foreach (byte[] b in imageInByte)
                    {
                        image.Image = b;
                        image.IDApartment = editApartment.IDApartment;
                        AppData.Context.ImageApartment.Add(image);
                        AppData.Context.SaveChanges();
                    }
                }
                else
                {
                    var imageDefault = AppData.Context.ImageApartment.Where(i => i.IDImageApartment == 13).ToList().FirstOrDefault();
                    image.Image = imageDefault.Image;
                    image.IDApartment = editApartment.IDApartment;
                    AppData.Context.ImageApartment.Add(image);
                    AppData.Context.SaveChanges();
                }


                MessageBox.Show("Квартира охранена");
                AppData.Context.SaveChanges();



            }







            }

        public byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, imageIn.RawFormat);
                return ms.ToArray();
            }
        }


        private void asbAdress_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {

            //listEmployee = listEmployee.ToList();
            if (isChange == false)
            {
                var adressCollection = ClassHelper.AppData.Context.Adress.ToList();
                var adress = from p in adressCollection select new { p.Adress1 }.Adress1;
                if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
                {

                    var suitableItems = new List<string>();
                    var splitText = sender.Text.ToLower().Split(' ');
                    foreach (var i in adress)
                    {
                        var found = splitText.All((key) =>
                        {
                            return i.ToLower().Contains(key);
                        });
                        if (found)
                        {
                            suitableItems.Add(i);
                        }
                    }
                    if (suitableItems.Count == 0)
                    {
                        suitableItems.Add("Нет результатов");
                    }
                    sender.ItemsSource = suitableItems;
                }
            }
           
            else
            {
               
            }
        }



        private void btnAddImage_Click(object sender, RoutedEventArgs e)
        {

            Microsoft.Win32.OpenFileDialog openFile = new Microsoft.Win32.OpenFileDialog();
            if (openFile.ShowDialog() == true)
            {
                Image i = new Image();
                i.Source = new BitmapImage(new Uri(openFile.FileName));
                pathPhoto = openFile.FileName;
                images.Add(i);
                imageInByte.Add(File.ReadAllBytes(pathPhoto));
                fvImg.ItemsSource = images;

            }

        }

        private void btnOffer_Click(object sender, RoutedEventArgs e)
        {
            Windows.OfferWindow offerWindow = new Windows.OfferWindow(editApartment);
            offerWindow.Show();
        }

        private void btnCleanPhoto_Click(object sender, RoutedEventArgs e)
        {
            if (imageInByte.Count == 0)
            {

                MessageBox.Show("Все фото удалены");
                List<Image> emptyimg = new List<Image>();
                fvImg.ItemsSource = emptyimg;
            }
            else
            {
                images.Remove(images[images.Count - 1]);
                imageInByte.Remove(imageInByte[imageInByte.Count - 1]);

                fvImg.ItemsSource = images;
            }




        }
    }
}
