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
            Helper.LbEvents().ForEach(x=>ListBox.Items.Add(x));
            //ListBox.Items
        }
       
        private void ListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TextBlock.Text = (((ListBoxItem) ListBox.SelectedItem).Content as RoutedEvent).Name;
        }
    }

    public static class Extensions
    {
        public static void ForEach<T>(this IEnumerable<T> sequence, Action<T> action)
        {
            // argument null checking omitted
            foreach (T item in sequence) action(item);
        }
    }

    public class Helper
    {
        public static RoutedEvent[] Events()
        {
            return EventManager.GetRoutedEvents();
        }

        public static IEnumerable<ListBoxItem> LbEvents()
        {
            return Events().Select(x => new ListBoxItem {Content = x});
        }
    }
}
