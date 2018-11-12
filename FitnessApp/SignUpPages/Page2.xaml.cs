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

            //Change Back Card Header
            FitnessApp.SigningWindow.SigningWindowObject.BackArrowButton.Visibility = Visibility.Hidden;
            FitnessApp.SigningWindow.SigningWindowObject.BackCardHeaderTextBlock.Text = "Set up your Profile";
            FitnessApp.SigningWindow.SigningWindowObject.BackCardHeaderTextBlock.Margin = new Thickness(-15);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new SignUpPages.Page1());
        }
    }
}
