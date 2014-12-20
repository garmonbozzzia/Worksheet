using System;
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
        public Engine Engine { get; set; }
        public ControlWindow ControlWindow { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            var deck1 = CardCollection.MixedDeck("1-1-1 4-5-2", "20 10").Shuffle();
            var deck2 = CardCollection.MixedDeck("3-3-1 2-2-2", "10 20").Shuffle();
            Engine = new Engine(deck1, deck2);

            ControlWindow = new ControlWindow {VisEngine = Engine, MainWindow = this};
            ControlWindow.Show();
            
            Deck1Box.ItemsSource = Engine.Player1.Deck;
            Deck2Box.ItemsSource = Engine.Player2.Deck;
            Hand1Box.ItemsSource = Engine.Player1.Hand;
            Hand2Box.ItemsSource = Engine.Player2.Hand;
            Board1Box.ItemsSource = Engine.Player1.Board;
            Board2Box.ItemsSource = Engine.Player2.Board;
        }
    }

    public class HeroCard : Card
    {
        public HeroCard()
        {
            Health = 30;
            Mana = 0;
            AttackValue = 0;
        }

        public override string ToString()
        {
            return String.Format("{0}: {1}", Name, Health);
        }
    }

    public class BoardCard : Card
    {
        public override string ToString()
        {
            return String.Format("A:{0} H:{1}", AttackValue, Health);
        }
    }
}
