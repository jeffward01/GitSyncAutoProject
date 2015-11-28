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
            button_newProject.IsEnabled = true;


        }

        public void HideLabels()
        {
            L_profileInfo.Visibility = Visibility.Hidden;
            L_followers.Visibility = Visibility.Hidden;
            L_Login.Visibility = Visibility.Hidden;
            L_following.Visibility = Visibility.Hidden;
            L_locatoon.Visibility = Visibility.Hidden;
            button_newProject.IsEnabled = false;
            HideProjectLabels();
        }
        public void HideProjectLabels()
        {
            L_Step1.Visibility = Visibility.Hidden;
            L_Step2.Visibility = Visibility.Hidden;
            L_Step3.Visibility = Visibility.Hidden;
            L_Step4.Visibility = Visibility.Hidden;
            L_projectType.Visibility = Visibility.Hidden;
            radioButton_AngularWebProject.Visibility = Visibility.Hidden;
            radioButton_BasicWebProject.Visibility = Visibility.Hidden;
            L_RepoName.Visibility = Visibility.Hidden;
            textBox_RepoDescription.Visibility = Visibility.Hidden;
            L_RepoLocation.Visibility = Visibility.Hidden;
            L_LocalDirectioryName.Visibility = Visibility.Hidden;
            textBox_LocalDirectoryName.Visibility = Visibility.Hidden;
           textBox_RepoName.Visibility = Visibility.Hidden;
            L_RepoDescription.Visibility = Visibility.Hidden;
            button_newProject.Visibility = Visibility.Visible;
            button_hideProject.Visibility = Visibility.Hidden;
            button_setLocalDirectory.Visibility = Visibility.Hidden;
        }

    public void ShowProjectLabels()
        {
            L_Step1.Visibility = Visibility.Visible;
            L_Step2.Visibility = Visibility.Visible;
            L_Step3.Visibility = Visibility.Visible;
            L_Step4.Visibility = Visibility.Visible;
            L_projectType.Visibility = Visibility.Visible;
            radioButton_AngularWebProject.Visibility = Visibility.Visible;
            radioButton_BasicWebProject.Visibility = Visibility.Visible;
            textBox_RepoDescription.Visibility = Visibility.Visible;
            L_RepoLocation.Visibility = Visibility.Visible;
            L_LocalDirectioryName.Visibility = Visibility.Visible;
            textBox_LocalDirectoryName.Visibility = Visibility.Visible;
            textBox_RepoName.Visibility = Visibility.Visible;
            L_RepoDescription.Visibility = Visibility.Visible;
            L_RepoName.Visibility = Visibility.Visible;
            button_newProject.Visibility = Visibility.Hidden;
            button_hideProject.Visibility = Visibility.Visible;
            button_setLocalDirectory.Visibility = Visibility.Visible;

        }

        //Disconnect Button
        private void button_disconnect_Click(object sender, RoutedEventArgs e)
        {

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

        private void button_newProject_Click(object sender, RoutedEventArgs e)
        {
            ShowProjectLabels();
        }

        private void button_hideProject_Click(object sender, RoutedEventArgs e)
        {
            HideProjectLabels();
        }

        private void button_setLocalDirectory_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();
            label_localSelectedPath.Visibility = Visibility.Visible;
            label_localSelectedPath.Content = dialog.SelectedPath;
        }
    }
}
