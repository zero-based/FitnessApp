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

namespace FitnessApp.UserMainWindowPages
{
    /// <summary>
    /// Interaction logic for CaloriesCalculatorPage.xaml
    /// </summary>
    public partial class CaloriesCalculatorPage : Page
    {
        public static CaloriesCalculatorPage CaloriesCalculatorPageObject = new CaloriesCalculatorPage();

        public CaloriesCalculatorPage()
        {
            InitializeComponent();
            CaloriesCalculatorPageObject = this;
        }
    }
}
