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
using System.Windows.Shapes;
using WpfClientReef.SurfServiceReference;

namespace WpfClientReef
{
    /// <summary>
    /// Interaction logic for AdminPG.xaml
    /// </summary>
    public partial class AdminPG : Window
    {
        private User user;
        public AdminPG(User user)
        {
            InitializeComponent();
            Storyboard s = (Storyboard)TryFindResource("OpenMenu");
            s.Begin();
            this.user = user;
        }
        private void Clear_Grid()
        {
            UserControls.Children.Clear();
        }
        private void SurfingTypes_Selected(object sender, RoutedEventArgs e)
        {
            Clear_Grid();
            UserControls.Children.Add(new Surfing_Types());

        }
        private void Map_Selected(object sender, RoutedEventArgs e)
        {
            Clear_Grid();
            Storyboard s = (Storyboard)TryFindResource("CloseMenu");
            s.Begin();
            UserControls.Children.Add(new Map_Locations());

        }
        private void SurfClubs_Selected(object sender, RoutedEventArgs e)
        {
            Clear_Grid();
            UserControls.Children.Add(new SurfClubsUC());

        }

        private void AdminLoc_Selected(object sender, RoutedEventArgs e)
        {
            Clear_Grid();
            UserControls.Children.Add(new AdminLocUC());
        }

        private void ListViewItem_Selected(object sender, RoutedEventArgs e)
        {
            Clear_Grid();
            UserControls.Children.Add(new UsersUC());
        }
        private void Update_Selected(object sender, RoutedEventArgs e)
        {
            Clear_Grid();
            UserControls.Children.Add(new UpdateUC(user));
        }

        private void LogOut_Selected(object sender, RoutedEventArgs e)
        {
            user = null;
            LoginWindow loginWindow = new LoginWindow();
            this.Hide();
            loginWindow.ShowDialog();
            this.Close();
        }
    }
}
