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
using GitSyncAutoProject.CORE.ServiceClasses.BasicWebProject;
using GitSyncAutoProject.CORE.ServiceClasses.AngularWebProject;
using System.Diagnostics;

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

        //Paths to Web Project JS CSS IMG
        public static string Project_js_DirectoryPath;
        public static string Project_css_DirectoryPath;
        public static string Project_img_DirectoryPath;

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
            if(!validateBuild())
            {
                MessageBox.Show("Please Fill in all location and selection a directory locaiton");
                return;
            }
        }

        //Validation Method for Build
        private bool validateBuild()
        {
          
            var RepoName = textBox_RepoName.Text;
            var RepoDescription = textBox_RepoDescription.Text;
            var LocalDirectoryLocation = label_localSelectedPath.Content;
            var localDirecoryName = textBox_LocalDirectoryName.Text;

            //Validates that there is NEW content in RepoName, RepoDescription, LocalDirectoryLocation, LocalDirectoryName 
            if (((localDirecoryName == "Enter File Name") || (localDirecoryName == "")) || ((RepoName == "My Repo Name") || (RepoName == "")) || ((RepoDescription == "Description....") || (RepoDescription == "")) || (localDirecoryName == ""))
            {
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

            if ((WebProject == false && AngularProject == false) || WebProject == true && AngularProject == true)
            {
                return false;
            }
            return true;
        }



        //Current Development:
        //Test 'Click'method for creating directories
        private void button_testCreateDirectory_Click(object sender, RoutedEventArgs e)
        {
            CreateTopLevelDirectory();
            RadioButtonProjectHandler();
            MessageBox.Show("Success! Project successfully created.");
        }

        //Determines which project is selected and then builds files
        private void RadioButtonProjectHandler()
        {
            //If Basic Project is selected
            if(radioButton_BasicWebProject.IsChecked == true)
            {
                CreateBasicWebProjectDirectories();
                CreateFilesForBasicWebProject();
            }

            //If Angular Project is selected
            if(radioButton_AngularWebProject.IsChecked == true)
            {
                CreateAngularWebDirectories();
                CreateFilesForAngularWebProject();
                RunGitCommands(){ }
            }
         
        }

        //Run Git Terminal Commnds
        private void RunGitCommands()
        {
            ProcessStartInfo psi = new ProcessStartInfo("git", "init");
            psi.WorkingDirectory = ProjectTopLevelFolder;

            Process p = Process.Start(psi);

            ProcessStartInfo psi1 = new ProcessStartInfo("git", "add .");
            psi1.WorkingDirectory = ProjectTopLevelFolder;

            Process p2 = Process.Start(psi1);

            ProcessStartInfo psi2 = new ProcessStartInfo("git", "commit -m \"Created by Github Creator\"");
            psi1.WorkingDirectory = ProjectTopLevelFolder;

            Process p3 = Process.Start(psi2);
        }


        //Create 'Top-Level' Directory with Project Name for the fold name
        private void CreateTopLevelDirectory()
        {
            //Validation
            if (label_localSelectedPath.Content == "")
            {
                MessageBox.Show("Please select a location");
                return;
            }

            //Save Directory Name
            var DirectoryName = textBox_LocalDirectoryName.Text;

            //Save Directory Location
            var directoryLocation = (string)label_localSelectedPath.Content;

            //Path to new folder
            ProjectTopLevelFolder = System.IO.Path.Combine(directoryLocation, DirectoryName);

            //Create new Project Folder with Project Name
            System.IO.Directory.CreateDirectory(ProjectTopLevelFolder);
        }

        //Creates Directories for 'Basic Web Project'
        private void CreateBasicWebProjectDirectories()
        {
            //Path to 'Top-Level' Directory
            var pathtoProjectDirecory = ProjectTopLevelFolder;

            //
            //---Img  Folder Creation---
            //Path to img folder
            string imgFolderName = "img";
            Project_img_DirectoryPath = System.IO.Path.Combine(pathtoProjectDirecory, imgFolderName);
            //Create new img folder
            System.IO.Directory.CreateDirectory(Project_img_DirectoryPath);


            //
            //---CSS  Folder Creation---
            //Path to css folder
            string cssFolderName = "css";
            Project_css_DirectoryPath = System.IO.Path.Combine(pathtoProjectDirecory, cssFolderName);
            System.IO.Directory.CreateDirectory(Project_css_DirectoryPath);

            //
            //==JS FOlder Creation
            //Path to js folder
            string jsFolderName = "js";
            Project_js_DirectoryPath = System.IO.Path.Combine(pathtoProjectDirecory, jsFolderName);
            System.IO.Directory.CreateDirectory(Project_js_DirectoryPath);

            ProcessStartInfo psi = new ProcessStartInfo("git", "init");
            psi.WorkingDirectory = pathtoProjectDirecory;

            Process p = Process.Start(psi);

            ProcessStartInfo psi1 = new ProcessStartInfo("git", "add .");
            psi1.WorkingDirectory = pathtoProjectDirecory;

            Process p2 = Process.Start(psi1);

            ProcessStartInfo psi2 = new ProcessStartInfo("git", "commit -m \"Created by Github Creator\"");
            psi1.WorkingDirectory = pathtoProjectDirecory;

            Process p2 = Process.Start(psi1);

        }

        //Creates Files for 'Basic Web Project'
        private void CreateFilesForBasicWebProject()
        {
            //Create index.html file
            CreateFileWithContent(ProjectTopLevelFolder, BWPFileService.HTMLBWPStringBuilder(), "index", "html");

            //Create main.css file
            CreateFileWithContent(Project_css_DirectoryPath, "/*Generated by GitSyncAutoProject*/", "main", "css");

            //Create script.js file
            CreateFileWithContent(Project_js_DirectoryPath, "//Generated by GitSyncAutoProject", "script", "js");

        }


        //Create Directories for 'Angular Web Project'
        private void CreateAngularWebDirectories()
        {
            //Path to 'Top-Level' Directory
            var pathtoProjectDirecory = ProjectTopLevelFolder;

            //
            //---Img  Folder Creation---
            //Path to img folder
            string imgFolderName = "img";
            Project_img_DirectoryPath = System.IO.Path.Combine(pathtoProjectDirecory, imgFolderName);
            //Create new img folder
            System.IO.Directory.CreateDirectory(Project_img_DirectoryPath);


            //
            //---CSS  Folder Creation---
            //Path to css folder
            string cssFolderName = "css";
            Project_css_DirectoryPath = System.IO.Path.Combine(pathtoProjectDirecory, cssFolderName);
            System.IO.Directory.CreateDirectory(Project_css_DirectoryPath);

            //
            //==JS FOlder Creation
            //Path to js folder
            string jsFolderName = "js";
            Project_js_DirectoryPath = System.IO.Path.Combine(pathtoProjectDirecory, jsFolderName);
            System.IO.Directory.CreateDirectory(Project_js_DirectoryPath);           
        }

        
        //Create Files for 'Angular Web Project'
        private void CreateFilesForAngularWebProject()
        {
            //Create index.html file
            CreateFileWithContent(ProjectTopLevelFolder, BWPFileService.HTMLBWPStringBuilder(), "index", "html");

            //Create main.css file
            CreateFileWithContent(Project_css_DirectoryPath, "/*Generated by GitSyncAutoProject*/", "main", "css");

            //Create app.js file
            CreateFileWithContent(Project_js_DirectoryPath, AngularFileService.AppJSStringBuilder(), "app", "js");

            //Create app.ctrl.js file
            CreateFileWithContent(Project_js_DirectoryPath, AngularFileService.AppCTRLStringBuilder(), "app.ctrl", "js");
        }
    



        //File Builder Method
        private void CreateFileWithContent(string path, string text, string Filename, string fileType)
        {
            path = path + "/" + Filename + "." + fileType;
            try
            {
                // Delete the file if it exists.
                if (File.Exists(path))
                {
                    // Note that no lock is put on the
                    // file and the possibility exists
                    // that another process could do
                    // something with it between
                    // the calls to Exists and Delete.
                    File.Delete(path);
                }

                // Create the file.
                using (FileStream fs = File.Create(path))
                {
                    Byte[] info = new UTF8Encoding(true).GetBytes(text);
                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }

                // Open the stream and read it back.
                using (StreamReader sr = File.OpenText(path))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(s);
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }








        //if Radio Button 'Basic Project' is checked
        private void radioButton_BasicWebProject_Checked(object sender, RoutedEventArgs e)
        {

        }

        //If Radio Button 'Angular Project' is checked
        private void radioButton_AngularWebProject_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
