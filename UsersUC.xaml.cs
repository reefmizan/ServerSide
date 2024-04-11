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
    /// Interaction logic for Users.xaml
    /// </summary>
    public partial class UsersUC : UserControl
    {
        private ServiceSurfClient ServiceSurf;
        private UserList users;
        public UsersUC()
        {
            InitializeComponent();
            ServiceSurf = new ServiceSurfClient();
            users = ServiceSurf.GetAllUser();
            UsersLV.ItemsSource=users;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("are you sure you want to change this","YesNo",MessageBoxButton.YesNo)==MessageBoxResult.Yes)
            {

            }
        }
    }
}
