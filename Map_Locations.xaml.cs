using Microsoft.Maps.MapControl.WPF;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfClientReef.SurfServiceReference;

namespace WpfClientReef
{
    /// <summary>
    /// Interaction logic for Map_Locations.xaml
    /// </summary>
    public partial class Map_Locations : UserControl
    {
        ServiceSurfClient ServiceSurf;
        LocationsList locations;
        public Map_Locations()
        {
            InitializeComponent();
            ServiceSurf = new ServiceSurfClient();
            locations = ServiceSurf.GetAllLocations();
            cmbLocation.ItemsSource = locations;
            cmbLocation.DisplayMemberPath = "Name";
            LoadLocations();
        }

        private void LoadLocations()
        {
            foreach (Locations location in locations)
            {
                Pushpin pushpin = new Pushpin();
                pushpin.Tag = location;
                pushpin.ToolTip = location.Name;
                pushpin.MouseDown += Location_MouseDown;
                string[] loc = location.Cord.Split(',');
                pushpin.Location = new Location(double.Parse(loc[0]), double.Parse(loc[1]));
                map.Children.Add(pushpin);
            }
        }   
        
        private void Location_MouseDown(object sender, MouseButtonEventArgs e)
        {

            Locations location = (Locations)cmbLocation.SelectedItem;
            string city = location.Name.Substring(location.Name.LastIndexOf(", ") + 2);
            WindMeter1.Children.Clear();
            WindMeter1.Children.Add(new WindMeterUC(city));

            SurfClubsList surfClubs = ServiceSurf.SelectByLocation(location.ID);
            if (location.ID != 0)
            {
                foreach (SurfClubs surfclub in surfClubs)
                {
                    Pushpin pushpin = new Pushpin();
                    pushpin.Background = new SolidColorBrush(Color.FromArgb(100, 0, 72, 186));
                    pushpin.Tag = surfclub;
                    pushpin.ToolTip = surfclub.Name;
                    pushpin.MouseDown += SurfClub_MouseDown;
                    string[] loc = surfclub.Cord.Split(',');
                    pushpin.Location = new Location(double.Parse(loc[0]), double.Parse(loc[1]));
                    map.Children.Add(pushpin);
                }
            }
        }
        public void Clear_Grid()
        {
            UserControlsMap.Children.Clear();
        }
        private void SurfClub_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Clear_Grid();
            UserControlsMap.Children.Add(new SurfClubsUC());
        }
        private void cmbLocation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //center map to location
            Locations location = (Locations)cmbLocation.SelectedItem;
            string[] loc = location.Cord.Split(',');
            map.Center = new Location(double.Parse(loc[0]), double.Parse(loc[1]));
            map.ZoomLevel = 12;
        }


    }
}
