using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace FitnessApp.SignUpPages
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public static Page1 PageOneObject = new Page1();

        public Page1()
        {
            InitializeComponent();
            PageOneObject = this;
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(Page2.PageTwoObject);
        }
    }
}
