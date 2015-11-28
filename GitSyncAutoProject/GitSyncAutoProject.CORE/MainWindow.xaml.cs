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
using GitSharp;
using GitSharp.Core;


namespace GitSyncAutoProject.CORE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static UserInformation userInfo;


        //Run Application
        public MainWindow()
        {
            InitializeComponent();
            setConnectionStatusDISCONNECT();
        }






        public void setConnectionStatusOK()
        {

            label_ConnectedStatus.Content = "Connected";
            label_ConnectedStatus.FontSize = 13.33;
            label_ConnectedStatus.Foreground = Brushes.Green;
            label_ConnectedStatus.FontWeight = FontWeights.Bold;
            label_ConnectedStatus.HorizontalAlignment = HorizontalAlignment.Left;
            label_ConnectedStatus.VerticalAlignment = VerticalAlignment.Top;
            label_ConnectedStatus.Width = 81;

        }

        public void setConnectionStatusDISCONNECT()
        {

            label_ConnectedStatus.Content = "Disconnected";
            label_ConnectedStatus.FontSize = 13.33;
            label_ConnectedStatus.Foreground = Brushes.Red;
            label_ConnectedStatus.FontWeight = FontWeights.Bold;
            label_ConnectedStatus.HorizontalAlignment = HorizontalAlignment.Left;
            label_ConnectedStatus.VerticalAlignment = VerticalAlignment.Top;
            label_ConnectedStatus.Width = 99;

        }
    }
}
