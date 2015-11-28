using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitSyncAutoProject.CORE.ServiceClasses.BasicWebProject
{
    public class BWPFileService
    {
        public static string HTMLStringBuilder()
        {
            return @"
<!DOCTYPE html>
<html lang = 'en'>
    <head>
        <meta charset = 'utf-8'>
        <link rel = 'stylesheet' media = 'screen' href = 'https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.3.5/css/bootstrap.min.css'>
        <link rel = 'stylesheet' href = 'css/main.css'>        
        <title> Your Title </title>         
    </head>   
    <body>
    



            
    <script src = 'https://cdnjs.cloudflare.com/ajax/libs/jquery/1.11.3/jquery.min.js' ></script>
    <script src = 'https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.3.5/js/bootstrap.min.js' ></script>
    <script src = 'js/script.js'></script>
                   
    </body>
                   
 </html> ";
        }

        


    }
}
