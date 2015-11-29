using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitSyncAutoProject.CORE.ServiceClasses.AngularWebProject
{
    public class AngularFileService
    {
        //String for index.html of Angular Web Project
        public static string HTMLAngularStringBuilder()
        {
            return @"
<!DOCTYPE html>
<html lang = 'en' ng-app='app'>
    <head>
        <meta charset = 'utf-8'>
        <link rel = 'stylesheet' media = 'screen' href = 'https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.3.5/css/bootstrap.min.css'>
        <link rel = 'stylesheet' href = 'css/main.css'>        
        <title> Your Title </title>         
    </head>   
    <body ng-controller='AppController'>
    



            
    <script src = 'https://cdnjs.cloudflare.com/ajax/libs/jquery/1.11.3/jquery.min.js' ></script>
    <script src = 'https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.3.5/js/bootstrap.min.js' ></script>
    <script src= 'https://ajax.googleapis.com/ajax/libs/angularjs/1.3.15/angular.min.js'></script>

    <script src = 'js/app.js'></script>
    <script src = 'js/app.ctrl.js'></script>
                   
    </body>
                   
 </html> ";



        }

        //String for app.js of Angular Web Project
        public static string AppJSStringBuilder()
        {
            return @"//Angular Module App
angular.module('app', []);";
        }


        //String for app.ctrl.js of Angular Web Project
        public static string AppCTRLStringBuilder()
        {
            return @"angular.module('app').controller('AppController', function ($scope, $http) {

   


    


}); // End Controller";


        }


    }
}
