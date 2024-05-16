using CefSharp;
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
using System.Xml.Linq;
using WpfClientReef.SurfServiceReference;

namespace WpfClientReef
{
    /// <summary>
    /// Interaction logic for SurfClubs.xaml
    /// </summary>
    public partial class SurfClubsUC : UserControl
    {
        private SurfClubsList surfclubs;
        private ServiceSurfClient serviceSurfClient;
        public SurfClubsUC()
        {
            InitializeComponent();
            serviceSurfClient = new ServiceSurfClient();
            surfclubs = serviceSurfClient.GetAllSurfClubs();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            spData.Children.Clear();
            Button btn = sender as Button;
            foreach (SurfClubs sc in surfclubs)
            {
                if (sc.Name == btn.Name)
                {
                    TextBlock tbHeader = new TextBlock();
                    tbHeader.Text = sc.Name;
                    tbHeader.FontSize = 35;
                    TextBlock tbDesc = new TextBlock();
                    tbDesc.Text = sc.Description;
                    tbDesc.TextWrapping = TextWrapping.WrapWithOverflow;
                    tbDesc.Margin = new Thickness(0, 10, 0, 10);
                    tbDesc.FontSize = 20;
                    StackPanel stackPanel = new StackPanel();
                    stackPanel.Children.Add(tbHeader);
                    stackPanel.Children.Add(tbDesc);
                    stackPanel.Margin = new Thickness(10);
                    spData.Children.Add(stackPanel);
                }

            }


        }
    }
}
    

