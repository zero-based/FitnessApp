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

namespace FitnessApp.SignUpPages
{
    /// <summary>
    /// Interaction logic for Page2.xaml
    /// </summary>
    public partial class Page2 : Page
    {
        public Page2()
        {
            InitializeComponent();
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new SignUpPages.Page3());

            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(SigningWindow))
                {
                    (window as SigningWindow).BackCardHeaderTextBlock.Text = "Set up your Profile";
                }
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new SignUpPages.Page1());
        }
    }
}
