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
using System.Windows.Media.Animation;
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
            TextBlock.Text = 0.ToString();
            Helper.LbEvents().ForEach(x=>ListBox.Items.Add(x));
            AddRandomAnimations();
            int count = 0;
            MouseMove += delegate(object s, MouseEventArgs e)
            {
                TextBlock.Text = String.Join("\n", new object[]
                {
                    "Count", count++,
                    "Timestamp", e.Timestamp,
                    "Device", e.Device,
                    "LeftButton", e.LeftButton,
                    "OriginalSource", e.OriginalSource,
                    "Source", e.Source,
                    "RoutedEvent", e.RoutedEvent,
                    "Handled", e.Handled,
                }.Select(x => x.ToString()));
            };
            //Helper.SaveCategories();
        }

        private void AddRandomAnimations()
        {
            ListBox.Items.Cast<ListBoxItem>().Zip(Helper.Animations(), delegate(ListBoxItem x, ColorAnimation y)
            {
                var brush = new SolidColorBrush();
                x.Foreground = brush;
                brush.BeginAnimation(SolidColorBrush.ColorProperty, y);
                return x;
            }).ForEach(_=>{});
        }

        private void ListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

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
        public static IEnumerable<Color> Colors()
        {
            
            var names = typeof (Colors).GetProperties().Select(x => x.Name).ToArray();
            return Random(max: names.Length)
                .Select(i => names[i])
                .Select(ColorConverter.ConvertFromString)
                .Where(x=>x != null)
                .Select(x=> (Color) x);
            //System.Windows.Media.Colors.
        }

        public static IEnumerable<int> Random(int max = int.MaxValue, int min = 0)
        {
            var random = new Random();
            while (true)
                yield return random.Next(min, max);
        }

        public static IEnumerable<ColorAnimation> Animations()
        {
            var duration = TimeSpan.FromSeconds(1);
            return Colors()
                .Zip(Colors().Skip(25),
                    (x, y) =>
                        new ColorAnimation
                        {
                            RepeatBehavior = RepeatBehavior.Forever,
                            AutoReverse = true,
                            From = x,
                            To = y,
                            Duration = duration
                        });
        }

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
