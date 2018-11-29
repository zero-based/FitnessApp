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
using LiveCharts;
using LiveCharts.Wpf;

namespace FitnessApp
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class UserMainWindow : Window
    {
        public UserMainWindow()
        {
            InitializeComponent();
        }

        private void UserMainWindowPagesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Navigate to selected Page
            switch (UserMainWindowPagesListBox.SelectedIndex)
            {
                case 0:
                    // Navigate to Challenges Page
                    break;

                case 1:
                    // Navigate to Fitness Plans Page
                    break;

                case 2:
                    // Navigate to Calories Calculator Page
                    break;

                case 3:
                    // Navigate to Settings Page
                    break;
            }

            // Close Drawer after Navigation
            SideMenuDrawer.IsLeftDrawerOpen = false;
        }
    }
}
