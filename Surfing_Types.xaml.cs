﻿using CefSharp;
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
    /// Interaction logic for Surfing_Types.xaml
    /// </summary>
    public partial class Surfing_Types : UserControl
    {
        private TypeSurfList typeSurves;
        private ServiceSurfClient serviceSurfClient;
        WebBrowser webBrowser;
        public Surfing_Types()
        {
            InitializeComponent();
            serviceSurfClient = new ServiceSurfClient();
            typeSurves = serviceSurfClient.GetAllTypeSurf();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            spData.Children.Clear();
            string name = (sender as Button).Name;
            List<TypeSurf> list = typeSurves.FindAll(t => t.Name.StartsWith(name)).ToList();
            foreach(TypeSurf tp in list)
            {
                TextBlock tbHeader=new TextBlock();
                tbHeader.Text = tp.Name;
                tbHeader.FontSize = 35;
                TextBlock tbDesc = new TextBlock();
                tbDesc.Text=tp.Description;
                tbDesc.TextWrapping = TextWrapping.WrapWithOverflow;
                tbDesc.Margin=new Thickness(0,10,0,10);
                tbDesc.FontSize = 20;
                StackPanel stackPanel = new StackPanel();
                stackPanel.Children.Add(tbHeader);
                stackPanel.Children.Add(tbDesc);
                stackPanel.Margin = new Thickness(10);
                stackPanel.MouseDown += StackPanel_MouseDown;
                stackPanel.Tag = tp;
                spData.Children.Add(stackPanel);              
            }

        }
        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TypeSurf tp = (sender as StackPanel).Tag as TypeSurf;
            //https://www.youtube.com/watch?v=TT6fHQR8SD8";
            string url = tp.URLS.ToString();
            url = url.Substring(url.LastIndexOf("?v=") + 3);
            string html = "<html style='width: 100%; height: 100%; margin: 0; padding: 0;'><head>";
            html += "<meta content='IE=Edge' http-equiv='X-UA-Compatible'/>";
            html += "</head><body style='width: 100%; height: 100%; margin: 0; padding: 0;'>";
            html += $"<iframe id='video' src='https://www.youtube.com/embed/{url}' style=\"padding: 0px; width: 100%; height: 100%; border: none; display: block;\" allowfullscreen></iframe>";
            html += "</body></html>";
            webBrowser = new WebBrowser();
            webBrowser.NavigateToString(html);
            videoME.Children.Add(webBrowser);
            
        }
    }
}
