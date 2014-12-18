using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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
            AddMouseMove();
            new Animations().Show();
            //Helper.SaveCategories();
        }

        public void AddMouseMove()
        {
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
        }

        private void AddRandomAnimations()
        {
            var animations = Helper.Animations();
            
            ListBox.Items.Cast<ListBoxItem>()
                .Zip( animations, (x, y) =>
                {
                    var brush = new SolidColorBrush();
                    x.Foreground = brush;
                    brush.BeginAnimation(SolidColorBrush.ColorProperty, y);
                    return x;
                })
                .Zip( animations, (x, y) =>
                {
                    var brush = new SolidColorBrush();
                    x.Background = brush;
                    brush.BeginAnimation(SolidColorBrush.ColorProperty, y);
                    return x;
                })
                .ForEach(_=>{});
        }

        private void ListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = e.AddedItems[0] as ListBoxItem;
            
            //(e.AddedItems[0] as RoutedEvent).
        }
    }
}
