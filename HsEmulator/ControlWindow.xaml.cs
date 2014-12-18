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

        public Engine Engine { get; set; }
        public MainWindow MainWindow { get; set; }

        private void OnStep(object sender, RoutedEventArgs e)
        {
            Engine.Turn();
            MainWindow.Hand1Box.Items.Refresh();
            MainWindow.Hand2Box.Items.Refresh();
            MainWindow.Deck1Box.Items.Refresh();
            MainWindow.Deck2Box.Items.Refresh();
            MainWindow.Board1Box.Items.Refresh();
            MainWindow.Board2Box.Items.Refresh();
        }

        private void OnBattle(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(Engine.Battle());
        }
    }
}
