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

namespace HsEmulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var deck1 = Enumerable.Range(0, 30).Select(x => Card.Generate()).ToList();
            var deck2 = Enumerable.Range(0, 30).Select(x => Card.Generate()).ToList();

            var hand1 = deck1.Take(3).ToList();
            var hand2 = deck2.Take(4).ToList();
            deck1.RemoveRange(0,3);
            deck2.RemoveRange(0,4);
        }
    }

    public class Card
    {
        public int Mana { get; set; }
        public int Attack { get; set; }
        public int Health { get; set; }
        public string Name { get; set; }

        public static Random Random = new Random();
        public static Card Generate()
        {
            var mana = Random.Next(10);
            return new Card
            {
                Mana = mana,
                Attack = Random.Next(mana),
                Health = Random.Next(mana) + 1
            };
        }
    }
}
