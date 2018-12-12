using FitnessApp.SQLdatabase;
using System.Windows.Controls;

namespace FitnessApp.AdminMainWindowPages
{
    /// <summary>
    /// Interaction logic for AdminHomePage.xaml
    /// </summary>
    public partial class AdminHomePage : Page
    {
        public static AdminHomePage AdminHomePageObject = new AdminHomePage();
        SQLqueries SQLqueriesObject = new SQLqueries();

        public AdminHomePage()
        {
            InitializeComponent();
        }
    }
}
