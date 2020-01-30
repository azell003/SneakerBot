using System;
using System.IO;
using System.Windows;
using OpenQA.Selenium;
using SneakerBot.Framework;
using SneakerBot.Framework.PageObjects;
using System.Configuration;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace SneakerBot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string profilePath = ConfigurationManager.AppSettings["profileBin"];
        Profile userProfile;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, EventArgs e)
        {
            ShoeSize.ItemsSource = ExtendendRange(4.0, 15.0, 0.5).Select(x => x.ToString()).ToList();
            Quantity.ItemsSource = ExtendendRange(1, 3, 1).Select(x => x.ToString()).ToList();

            if(File.Exists(profilePath))
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(profilePath,
                                          FileMode.Open,
                                          FileAccess.Read,
                                          FileShare.Read);
                Profile savedProfile = (Profile)formatter.Deserialize(stream);
                stream.Close();

                FirstName.Text = savedProfile.firstName;
                LastName.Text = savedProfile.lastName;
                Email.Text = savedProfile.email;
                PhoneNo.Text = savedProfile.phoneNo;
                BillingAddress1.Text = savedProfile.billingAddressLine1;
                BillingAddress2.Text = savedProfile.billingAddressLine2;
                City.Text = savedProfile.city;
                Quantity.Text = savedProfile.quantity;
                ZipCode.Text = savedProfile.zipcode;
                ShoeSize.Text = savedProfile.shoeSize;
                CCNumber.Text = savedProfile.ccNumber;
                ExpDate.Text = savedProfile.ccExpDate;
                CVC.Text = savedProfile.ccCvc;
                productUrl.Text = savedProfile.siteUrl;
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            userProfile = Load_Profile();
            IFormatter formatter = new BinaryFormatter();

            Stream stream = new FileStream(profilePath,
                                     FileMode.Create,
                                     FileAccess.Write, FileShare.None);

            userProfile.ccNumber = string.Empty;
            userProfile.ccExpDate = string.Empty;
            userProfile.ccCvc = string.Empty;

            formatter.Serialize(stream, userProfile);
            stream.Close();
        }

        private void Launch_Click(object sender, RoutedEventArgs e)
        {
            IWebDriver driver = null;

            try
            {
                driver = Browser.SetDriver(BrowserType.Firefox);
                IPageObject webPage = (IPageObject)PageFactory.CreateInstance(TemplateBox.SelectedValue.ToString(), driver);
                userProfile = Load_Profile();

                driver.Manage().Window.Maximize();
                driver.Navigate().GoToUrl(userProfile.siteUrl);

                webPage.Execute(userProfile);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (driver != null)
                    driver.Quit();
            }
        }

        private Profile Load_Profile()
        {
            return new Profile
            {
                firstName = FirstName.Text,
                lastName = LastName.Text,
                email = Email.Text,
                phoneNo = PhoneNo.Text,
                billingAddressLine1 = BillingAddress1.Text,
                billingAddressLine2 = BillingAddress2.Text,
                city = City.Text,
                quantity = Quantity.Text,
                zipcode = ZipCode.Text,
                shoeSize = ShoeSize.Text,
                ccNumber = CCNumber.Text, 
                ccExpDate = ExpDate.Text, 
                ccCvc = CVC.Text,
                siteUrl = productUrl.Text
            };
        }

        public IEnumerable<double> ExtendendRange(double start, double end, double step)
        {
            for (double i = start; i <= end; i += step)
            {
                yield return i;
            }
        }

        private void TemplateBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
    }
}
