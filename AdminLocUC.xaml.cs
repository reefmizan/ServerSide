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

        }
    }
}
