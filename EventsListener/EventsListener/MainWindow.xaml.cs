using System;
using System.Collections.Generic;
using System.IO;
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
            Helper.SaveCategories();
        }
       
        private void ListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TextBlock.Text = (((ListBoxItem) ListBox.SelectedItem).Content as RoutedEvent).Name;
            //(e.AddedItems[0] as RoutedEvent).
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

        public static void SaveEvents()
        {
            File.WriteAllText("Events.txt", Helper.Events().Aggregate("", (s,x) => s+x+'\n'));
        }

        public static void SaveCategories()
        {            
            File.WriteAllText("EventCategories.txt", Helper.Events()
                .Select(x => x.ToString())
                //.Where(x => x.Contains('.'))
                .Select(x => x.Substring(0, x.IndexOf('.')))
                .Distinct()
                .Aggregate("", (s, x) => s + x + '\n'));
        }
    }
}
