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

namespace EventsListener
{
    /// <summary>
    /// Interaction logic for Animations.xaml
    /// </summary>
    public partial class Animations : Window
    {
        public Animations()
        {
            InitializeComponent();
            AnimationHelper.Shapes().Take(100).ForEach(x=>MainGrid.Children.Add(x));
        }
    }

    public class AnimationHelper
    {
        public static IEnumerable<Shape> Shapes()
        {
            var xs = Helper.RandomStream(min: 0, max: 800);
            var ys = Helper.RandomStream(min: 0, max: 600);
            var ws = Helper.RandomStream(min: 0, max: 800);
            var hs = Helper.RandomStream(min: 0, max: 600);
            var brs = Helper.Brushes();
            return 
                xs.Zip(ys, (x,y)=>new {X=x,Y=y}).Zip(
                ws.Zip(hs, (w,h)=>new {W=w,H=h}),
                (_,__)=> new {X=_.X, Y=_.Y, W=__.W, H=__.H})
                .Zip(brs, (p,c)=> new Rectangle {Width = p.W, Height = p.H, Fill = c});
        }
    }
}
