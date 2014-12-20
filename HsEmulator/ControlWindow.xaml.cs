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
using System.Windows.Shapes;

namespace HsEmulator
{
    /// <summary>
    /// Interaction logic for ControlWindow.xaml
    /// </summary>
    public partial class ControlWindow : Window
    {
        public ControlWindow()
        {
            InitializeComponent();
        }

        public Engine VisEngine { get; set; }
        public MainWindow MainWindow { get; set; }

        private void OnStep(object sender, RoutedEventArgs e)
        {
            var res = VisEngine.Turn();
            if (!String.IsNullOrEmpty(res)) ((Button) sender).Content = res;
            MainWindow.Hand1Box.Items.Refresh();
            MainWindow.Hand2Box.Items.Refresh();
            MainWindow.Deck1Box.Items.Refresh();
            MainWindow.Deck2Box.Items.Refresh();
            MainWindow.Board1Box.Items.Refresh();
            MainWindow.Board2Box.Items.Refresh();
        }

        private void OnBattle(object sender, RoutedEventArgs e)
        {   
            var deck1 = CardCollection.MixedDeck("1-1-1 4-5-2", "20 10").Shuffle();
            var deck2 = CardCollection.MixedDeck("3-3-1 2-2-2", "10 20").Shuffle();
            var engine = new Engine(deck1, deck2);
            var result = String.Format("{0} wins on {1} turn", engine.Battle(), engine.TurnNumber );
            Console.WriteLine(result);
        }

        private void OnBattles(object sender, RoutedEventArgs e)
        {
            var deck1 = CardCollection.MixedDeck("1-1-1 4-5-2", "20 10").Shuffle();
            var deck2 = CardCollection.MixedDeck("3-3-1 2-2-2", "10 20").Shuffle();
            var total = 10000;
            var startTime = DateTime.Now;
            var res = Enumerable.Range(1, total).AsParallel()
                .Select(num => new { num, Engine = new Engine(deck1, deck2) })
                .Select(x => new {x.num, Winner = x.Engine.Battle(), Turn = x.Engine.TurnNumber})
                .ToArray();
            
            //res.Select(x => String.Format("#{0}\t{1} wins on turn {2}", x.num, x.Winner, x.Turn))
              //  .ToList().ForEach(Console.WriteLine);

            var p1win = res.Count(x => x.Winner.Contains("Player1"));
            Console.WriteLine("{0}/{1}", p1win, total - p1win);
            Console.WriteLine(DateTime.Now.Subtract(startTime).Milliseconds);
        }
    }
}
