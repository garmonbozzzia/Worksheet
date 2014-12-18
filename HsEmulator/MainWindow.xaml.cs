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

            var board1 = Enumerable.Repeat(new Card {Health = 30, Mana = 0, Attack = 0, Name = "Player1"}, 1).ToList();
            var board2 = Enumerable.Repeat(new Card {Health = 30, Mana = 0, Attack = 0, Name = "Player2"}, 1).ToList();


            deck1.RemoveRange(0,3);
            deck2.RemoveRange(0,4);

            Deck1Box.ItemsSource = deck1;
            Deck2Box.ItemsSource = deck2;
            Hand1Box.ItemsSource = hand1;
            Hand2Box.ItemsSource = hand2;
            Board1Box.ItemsSource = board1;
            Board2Box.ItemsSource = board2;
        }
    }

    public class Card
    {
        public int Mana { get; set; }
        public int Attack { get; set; }
        public int Health { get; set; }
        public string Name { get; set; }

        public static Random Random = new Random();

        public override string ToString()
        {
            return String.Format("M:{0}, A:{1}, H:{2}", Mana, Attack, Health);
        }

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
