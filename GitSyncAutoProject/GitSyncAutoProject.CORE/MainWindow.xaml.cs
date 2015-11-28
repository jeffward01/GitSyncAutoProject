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
using CSharp.GitHub;
using System.IO;


namespace GitSyncAutoProject.CORE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //
        //-- Properties --  |(mainly saved for caching purposes)

        public static UserInformation userInfo;
        //Save path to local directory orginal Root folder
        public static string LocalDirctoryPath;

        //Path to newly created 'Project Name' Folder
        public static string ProjectTopLevelFolder;


        //Web Project inner contents of file - script string
        public static string JsFileTextString;
        public static string CssFileTextString;
        public static string HTMLFileTextString;






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
            button_CreateProject.Visibility = Visibility.Hidden;
            HideProjectLabels();
        }
        //Hides project labels on 'Hide Project Options' Click
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
            button_CreateProject.Visibility = Visibility.Hidden;

        }

        //Shows all nessesary prject labels and input area on 'New Project' Click.
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
            button_CreateProject.Visibility = Visibility.Visible;


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

        //Textbox:Control
        private void textBox_username_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
        }

        //Shows newProject Textboxes
        private void button_newProject_Click(object sender, RoutedEventArgs e)
        {
            ShowProjectLabels();
        }

        //Hides Project div on click
        private void button_hideProject_Click(object sender, RoutedEventArgs e)
        {
            HideProjectLabels();
        }

        //Set local directory button
        private void button_setLocalDirectory_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();
            label_localSelectedPath.Visibility = Visibility.Visible;
            LocalDirctoryPath = dialog.SelectedPath;
            label_localSelectedPath.Content = dialog.SelectedPath;
        }

        //Build new Project
        private void button_CreateProject_Click(object sender, RoutedEventArgs e)
        {
            //Validation
            validateBuild();
        }

        //Validation Method for Build
        private bool validateBuild()
        {
            var RepoName = textBox_RepoName.Text;
            var RepoDescription = textBox_RepoDescription.Text;
            var LocalDirectoryLocation = label_localSelectedPath.Content;
            var localDirecoryName = textBox_LocalDirectoryName.Text;

            //Validates that there is NEW content in RepoName, RepoDescription, LocalDirectoryLocation, LocalDirectoryName 
            if(((localDirecoryName == "Enter File Name") ||(localDirecoryName == "")) || ((RepoName == "My Repo Name") || ( RepoName == "")) ||((RepoDescription == "Description....") ||( RepoDescription == "")) || (localDirecoryName == ""))
                {
                MessageBox.Show("Please Fill in all location and selection a directory locaiton");
                return false;
            }

            if (validateRadioBtns() == false)
            {
                return false;
            }
            return true;
        } 

        //Validate Radio Buttons
        private bool validateRadioBtns()
        {
            var WebProject = radioButton_BasicWebProject.IsChecked;
            var AngularProject = radioButton_AngularWebProject.IsChecked;

            if((WebProject == false && AngularProject == false) || WebProject == true && AngularProject == true )
            {
                MessageBox.Show("Please select a Project Type.");
                return false;
            }
            return true;
        }

        //Current Development:
        //Test 'Click'method for creating directories
        private void button_testCreateDirectory_Click(object sender, RoutedEventArgs e)
        {
            BWPDirectoryService.CreateTopLevelDirectory();
            BWPDirectoryService.CreateBasicWebProjectDirectories();
        }

  
    }
}
