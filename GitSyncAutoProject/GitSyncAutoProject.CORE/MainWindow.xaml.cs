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
using GitSyncAutoProject.CORE;
using GitSyncAutoProject.CORE.ServiceClasses;

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
            HideLabels();
            setConnectionStatusDISCONNECT();
        }





        //Sets Connection Message to GREEN
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

        //Sets Connection Message to RED
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

        //On 'Connect' Click
        private void button_Click(object sender, RoutedEventArgs e)
        {
            if(textBox_username.Text =="")
            {
                MessageBox.Show("Please enter a username to continue");
                return;
            }

            userInfo = GithubService.GetGithubUser(textBox_username.Text);
            if (userInfo != null)
            {
                setConnectionStatusOK();
                PopulateUserInfo();
            }
            else
            {
                setConnectionStatusDISCONNECT();
            }
        }

        public void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= TextBox_GotFocus;
        }

        //Populate Window with Profile Information
        public void PopulateUserInfo()
        {
            ShowLabels();
           
            //Get Image
            var image = new Image();
            var fullFilePath = userInfo.avatar_url;

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(fullFilePath, UriKind.Absolute);
            bitmap.EndInit();
 
            label_Followers.Content = userInfo.followers;
            image_profilePic.Source = bitmap;
            image_profilePic.Visibility = Visibility.Visible;
            label_login.Content = userInfo.login;
            label_location.Content = userInfo.location;
            label_following.Content = userInfo.following;


        }
        public void ShowLabels()
        {
            L_profileInfo.Visibility = Visibility.Visible;
            L_followers.Visibility = Visibility.Visible;
            L_Login.Visibility = Visibility.Visible;
            L_following.Visibility = Visibility.Visible;
            L_locatoon.Visibility = Visibility.Visible;

        }

        public void HideLabels()
        {
            L_profileInfo.Visibility = Visibility.Hidden;
            L_followers.Visibility = Visibility.Hidden;
            L_Login.Visibility = Visibility.Hidden;
            L_following.Visibility = Visibility.Hidden;
            L_locatoon.Visibility = Visibility.Hidden;
        }

        //Disconnect Button
        private void button_disconnect_Click(object sender, RoutedEventArgs e)
        {

            //Get Image
        

            setConnectionStatusDISCONNECT();
            HideLabels();
            userInfo = null;
            label_Followers.Content = "";
            image_profilePic.Visibility = Visibility.Hidden;
                label_login.Content = "";
            label_location.Content = "";
            label_following.Content = "";
            textBox_username.Text = "Github Username";
        }

        private void textBox_username_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
        }
    }
}
