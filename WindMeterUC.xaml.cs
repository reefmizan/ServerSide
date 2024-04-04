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
using WpfClientReef.SurfServiceReference;

namespace WpfClientReef
{
    /// <summary>
    /// Interaction logic for WindMeterUC.xaml
    /// </summary>
    public partial class WindMeterUC : UserControl
    {
        private string city;
        public string[] props;
        public WindMeterUC( string city)
        {
            InitializeComponent();
            this.DataContext = props;
            this.city = city;   
            Windetails(city);
        }

        public void Windetails(string city)
        {
            
            props = WindManager.getWindSpeed(city);
            SpeedMeter.Text = props[0];
            
            gusts.Text += (double.Parse(props[0]) + 3).ToString();
            double degrees = double.Parse(props[1]);
            double x = 220 + (159 * Math.Sin(degrees));
            double y = 195 + (159 * Math.Cos(degrees));
            Canvas.SetLeft(needle, x);
            Canvas.SetTop(needle, y);
        }


    }
}
