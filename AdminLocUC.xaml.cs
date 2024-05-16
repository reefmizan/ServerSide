using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Configuration;
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
    /// Interaction logic for AdminLocUC.xaml
    /// </summary>
    public partial class AdminLocUC : UserControl
    {
        private ServiceSurfClient ServiceSurf;
        private LocationsList Locations;
        public AdminLocUC()
        {
            InitializeComponent();
            ServiceSurf = new ServiceSurfClient();
            Locations = ServiceSurf.GetAllLocations();
            LocLV.ItemsSource = Locations;
        }

        private void map_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            map.Children.Clear();
            Point mousePosition = e.GetPosition(this);
            Location pinLocation = map.ViewportPointToLocation(mousePosition);

            Pushpin pin = new Pushpin();
            pin.Location = new Location(pinLocation.Latitude, pinLocation.Longitude);

            tbCord.Text = new Location(pinLocation.Latitude,pinLocation.Longitude).ToString();
            map.Children.Add(pin);
        }

        private void LocLV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Locations location = LocLV.SelectedItem as Locations;
            if (location != null)
            {
                DataSP.DataContext=location;
                map.Children.Clear();

                Pushpin pushpin = new Pushpin();
                pushpin.Tag = location;
                pushpin.ToolTip = location.Name;
                string[] loc = location.Cord.Split(',');
                pushpin.Location = new Location(double.Parse(loc[0]), double.Parse(loc[1]));
                map.Children.Add(pushpin);
                map.Center = new Location(double.Parse(loc[0]), double.Parse(loc[1]));
                map.ZoomLevel = 12;
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {

            Locations location = LocLV.SelectedItem as Locations;
            ServiceSurf.UpdateLocations(location);
            MessageBox.Show("Succesfull Update!");
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            Locations location = new Locations();
            location.Name = tbName.Text;
            location.Cord = tbCord.Text;
            location.Description = tbDescription.Text;
            ServiceSurf.InsertLocations(location);
            MessageBox.Show("Succesfully Created!");

        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            Locations location = LocLV.SelectedItem as Locations;
            this.DataContext = location = new Locations();

        }
    }
}
