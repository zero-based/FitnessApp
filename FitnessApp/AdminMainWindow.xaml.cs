using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FitnessApp
{
    /// <summary>
    /// Interaction logic for AdminMainWindow.xaml
    /// </summary>
    public partial class AdminMainWindow : Window
    {
        public static AdminMainWindow AdminMainWindowObject;
        public AdminMainWindow()
        {
            InitializeComponent();

            // Initialize Listbox Selected Index
            AdminMainWindowPagesListBox.SelectedIndex = 0;
        }

        

        private void LogoutListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SigningWindow SigningWindowTemp = new SigningWindow();
            Close();
            SigningWindowTemp.ShowDialog();
        }

        private void AdminMainWindowPagesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Close Side Menu Drawer
            SideMenuDrawer.IsLeftDrawerOpen = false;

            // Navigate to the selected Page and Highlight the chosen Item
            switch (AdminMainWindowPagesListBox.SelectedIndex)
            {
                case 0:
                    HighlightItem(HomeTextBlock, HomeIcon);
                    break;

                case 1:
                    HighlightItem(SetupChallengesTextBlock, ChallengesIcon);
                    break;

                case 2:
                    HighlightItem(AddNewAdminTextBlock, AddNewAdminIcon);
                    break;

                case 3:
                    HighlightItem(SettingsTextBlock, SettingsIcon);
                    break;
            }
        }
        public void HighlightItem(TextBlock _pageTextBlock, MaterialDesignThemes.Wpf.PackIcon _pageIcon)
        {
            // Set all Items' Foreground to Black 

            // Home Item
            HomeTextBlock.Foreground = new SolidColorBrush(Colors.Black);
            HomeIcon     .Foreground = new SolidColorBrush(Colors.Black);

            // Setup Challenges Item
            SetupChallengesTextBlock.Foreground = new SolidColorBrush(Colors.Black);
            ChallengesIcon          .Foreground = new SolidColorBrush(Colors.Black);

            // Add New Admin Item
            AddNewAdminTextBlock.Foreground = new SolidColorBrush(Colors.Black);
            AddNewAdminIcon     .Foreground = new SolidColorBrush(Colors.Black);

            // Settings Item
            SettingsTextBlock.Foreground = new SolidColorBrush(Colors.Black);
            SettingsIcon     .Foreground = new SolidColorBrush(Colors.Black);

            // Highlight the required Item only and Change Page Header
            _pageTextBlock.Foreground = (Brush)Application.Current.Resources["PrimaryHueDarkBrush"];
            _pageIcon     .Foreground = (Brush)Application.Current.Resources["PrimaryHueDarkBrush"];
            PageHeaderTextBlock .Text = _pageTextBlock.Text;
        }
    }

}
