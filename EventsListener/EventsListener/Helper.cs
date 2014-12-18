using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace EventsListener
{
    public class Helper
    {
        public static Random Random = new Random("tetet".GetHashCode());

        public static IEnumerable<Color> Colors()
        {
            var names = typeof (Colors).GetProperties().Select(x => x.Name).ToArray();
            return RandomStream(max: names.Length)
                .Select(i => names[i])
                .Select(ColorConverter.ConvertFromString)
                .Cast<Color>();
        }

        public static IEnumerable<Brush> Brushes()
        {
            var converter = new BrushConverter();
            var names = typeof(Brushes).GetProperties().Select(x => x.Name).ToArray();
            return RandomStream(max: names.Length)
                .Select(i => names[i])
                .Select(converter.ConvertFromString)
                .Cast<Brush>();
        }

        public static IEnumerable<int> RandomStream(int max = int.MaxValue, int min = 0)
        {
            while (true)
                yield return Random.Next(min, max);
        }

        public static IEnumerable<ColorAnimation> Animations()
        {
            var duration = TimeSpan.FromSeconds(1);
            return Colors()
                .Zip(Colors(),
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