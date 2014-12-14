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

namespace EventsListener
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            TextBlock.Text = Helper.ListOfEvents();
        }
    }

    public class Helper
    {
        public static string ListOfEvents()
        {
            var res = EventManager.GetRoutedEventsForOwner(typeof(UIElement));
            if (res != null)
                return res.Select(x => x.Name).Aggregate("", (seed, x) => seed + x + '\n');
            return "";
        }

    }
}
