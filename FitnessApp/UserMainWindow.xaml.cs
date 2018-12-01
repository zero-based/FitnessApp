﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
            // Close Side Menu Drawer
            SideMenuDrawer.IsLeftDrawerOpen = false;

            // Navigate to the selected Page and Highlight the chosen Item
            switch (UserMainWindowPagesListBox.SelectedIndex)
            {
                case 0:
                    UserWindowPagesFrame.NavigationService.Navigate(UserMainWindowPages.HomePage.HomePageObject);
                    HighlightItem(HomeTextBlock, HomeIcon);
                    break;

                case 1:
                    UserWindowPagesFrame.NavigationService.Navigate(UserMainWindowPages.ChallengesPage.ChallengesPageObject);
                    HighlightItem(ChallengesTextBlock, ChallengesIcon);
                    break;

                case 2:
                    UserWindowPagesFrame.NavigationService.Navigate(UserMainWindowPages.FitnessPlansPage.FitnessPlansPageObject);
                    HighlightItem(FitnessPlansTextBlock, FitnessPlansIcon);
                    break;

                case 3:
                    UserWindowPagesFrame.NavigationService.Navigate(UserMainWindowPages.CaloriesCalculatorPage.CaloriesCalculatorPageObject);
                    HighlightItem(CaloriesCalculatorTextBlock, CaloriesCalculatorIcon);
                    break;

                case 4:
                    UserWindowPagesFrame.NavigationService.Navigate(UserMainWindowPages.SettingsPage.SettingsPageObject);
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

            // Challenges Item
            ChallengesTextBlock.Foreground = new SolidColorBrush(Colors.Black);
            ChallengesIcon     .Foreground = new SolidColorBrush(Colors.Black);

            // Challenges Item
            FitnessPlansTextBlock.Foreground = new SolidColorBrush(Colors.Black);
            FitnessPlansIcon     .Foreground = new SolidColorBrush(Colors.Black);

            // Challenges Item
            CaloriesCalculatorTextBlock.Foreground = new SolidColorBrush(Colors.Black);
            CaloriesCalculatorIcon     .Foreground = new SolidColorBrush(Colors.Black);

            // Challenges Item
            SettingsTextBlock.Foreground = new SolidColorBrush(Colors.Black);
            SettingsIcon     .Foreground = new SolidColorBrush(Colors.Black);



            // Highlight the required Item only
            _pageTextBlock.Foreground = (Brush)Application.Current.Resources["PrimaryHueDarkBrush"];
            _pageIcon     .Foreground = (Brush)Application.Current.Resources["PrimaryHueDarkBrush"];
        }
    }
}
