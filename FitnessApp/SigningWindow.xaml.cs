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

namespace FitnessApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class SigningWindow : Window
    {
        public static SigningWindow SigningWindowObject;

        public SigningWindow()
        {
            InitializeComponent();
            SigningWindowObject = this;
        }

        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            UserMainWindow AppWindow = new UserMainWindow();
            AppWindow.Show();
            SigningWindowObject.Close();
        }


        private void CreateAnAccount_Click(object sender, RoutedEventArgs e)
        {
            SignUpPagesFrame.NavigationService.Navigate(new SignUpPages.Page1());
        }
    }
}
