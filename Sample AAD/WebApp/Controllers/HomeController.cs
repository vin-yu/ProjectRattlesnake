using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using WebApp_OpenIDConnect_DotNet.Models;
using System.Configuration;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OpenIdConnect;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Net.Http.Headers;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;

using System.Xml.Linq;
using System;
using System.Text;
using System.Data.SqlClient;
using System.Diagnostics;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.RetryPolicies;
using Microsoft.WindowsAzure;

using System.Configuration;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.RetryPolicies;
using Microsoft.WindowsAzure.Storage.Shared.Protocol;

using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.RetryPolicies;
using System.Web.UI.HtmlControls;



namespace WebApp.Controllers
{
    using Microsoft.Azure;
    public class HomeController : Controller
    {
        public static string clientId = ConfigurationManager.AppSettings["ida:ClientId"];
        private static string appKey = ConfigurationManager.AppSettings["ida:ClientSecret"];
        private static string redirectUri = ConfigurationManager.AppSettings["ida:RedirectUri"];
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<ActionResult> Welcome()
        {
            ViewBag.Name = ClaimsPrincipal.Current.FindFirst("name").Value;
            return View();
        }

        [Authorize]
        public async Task<ActionResult> Upload()
        {
            ViewBag.Name = ClaimsPrincipal.Current.FindFirst("name").Value;

            //ATTEMPT TO GET USER'S PROFILE PICTURE, FIX LATER:

            //var servicePoint = new Uri("https://graph.windows.net");
            //var serviceRoot = new Uri(servicePoint, "<your tenant>"); //e.g. xxx.onmicrosoft.com
            //const string clientId = "<clientId>";
            //const string secretKey = "<secretKey>";// ClientID and SecretKey are defined when you register application with Azure AD
            //var authContext = new AuthenticationContext("https://login.windows.net/<tenant>/oauth2/token");
            //var credential = new ClientCredential(clientId, secretKey);
            //ActiveDirectoryClient directoryClient = new ActiveDirectoryClient(serviceRoot, async () =>
            //{
            //    var result = await authContext.AcquireTokenAsync("https://graph.windows.net/", credential);
            //    return result.AccessToken;
            //});

            //var user = await directoryClient.Users.Where(x => x.UserPrincipalName == "<username>").ExecuteSingleAsync();
            //DataServiceStreamResponse photo = await user.ThumbnailPhoto.DownloadAsync();
            //using (MemoryStream s = new MemoryStream())
            //{
            //    photo.Stream.CopyTo(s);
            //    var encodedImage = Convert.ToBase64String(s.ToArray());
            //}


            return View();
        }

        [Authorize]
        public async Task<ActionResult> Record()
        {
            ViewBag.Name = ClaimsPrincipal.Current.FindFirst("name").Value;
            return View();
        }

        [Authorize]
        public async Task<ActionResult> Settings()
        {
            ViewBag.Name = ClaimsPrincipal.Current.FindFirst("name").Value;
            return View();
        }

        [Authorize]
        public async Task<ActionResult> Transcripts()
        {

            //TODO: Search through transcripts:
            //https://docs.microsoft.com/en-us/azure/search/search-howto-indexing-azure-blob-storage







            //ViewBag.Name = ClaimsPrincipal.Current.FindFirst("name").Value;



            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference("container");

            CloudBlobDirectory dira = container.GetDirectoryReference(ClaimsPrincipal.Current.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value);
            foreach (IListBlobItem item in dira.ListBlobs())
            {
                Debug.WriteLine(dira.ListBlobs());
                CloudBlockBlob blob = (CloudBlockBlob)item;
                string myfilestr = System.IO.Path.GetFileName(blob.Uri.LocalPath);
                Debug.WriteLine(myfilestr);

                string extension = Path.GetExtension(myfilestr);
                Debug.WriteLine(extension);
                if (extension == ".txt")
                {
                    
                    //CloudBlob blob = container.GetBlobReference(item.ToString());
                    using (var stream = blob.OpenRead())
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {

                                Debug.WriteLine(reader.ReadToEnd());

                        }
                    }
                }

            }
            
            // Loop over items within the container and output the length and URI.
            //foreach (IListBlobItem item in container.ListBlobs(null, false))
            //{
            //    if (item.GetType() == typeof(CloudBlobDirectory))
            //    {
            //        //string myfilestr = Request.Form["file"];
            //        //Label myLabel = this.FindControl("myLabel") as Label;
            //        //tab_content1.InnerHtml=
            //        //characters.Length

            //        // we know this is a sub directory now
            //        CloudBlobDirectory subFolder = (CloudBlobDirectory)item;

            //        String filename = (subFolder.Uri).ToString();
            //        Debug.WriteLine(filename);
            //        string[] words = filename.Split('/');
            //        Debug.WriteLine(words);
            //        Debug.WriteLine(words[words.Length - 2]);

            //        if (words[words.Length - 2] == ClaimsPrincipal.Current.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value)
            //        {
            //            foreach (IListBlobItem item2 in subFolder.ListBlobs())
            //            {
            //                Debug.WriteLine("HERE");
            //            } }

            //        // Retrieve reference to a blob named "photo1.jpg".
            //        CloudBlockBlob blockBlob = container.GetBlockBlobReference("photo1.jpg");

            //        //// Save blob contents to a file.
            //        //using (var fileStream = System.IO.File.OpenWrite(@"path\myfile"))
            //        //{
            //        //    blockBlob.DownloadToStream(fileStream);
            //        //}

            //        //        string[] readText = File.ReadAllLines(blockBlob);
            //        StringBuilder strbuild = new StringBuilder();
            //        //foreach (string s in blockBlob)
            //        //{
            //        strbuild.Append(blockBlob);
            //        strbuild.AppendLine();
            //        //}
            //        //TextBox1.Text = strbuild.ToString();
            //    }
            //}

            return View();
                } 


        [Authorize]
        public async Task<ActionResult> UploadFile()
        {
            // Retrieve storage account from connection string.
            //HtmlInputFile filMyFile

            //UploadThis();
            string myfilestr = Request.Form["file"];
            

            var myFile = Request.Files["file"];
            

            myfilestr = myFile.FileName;

            string extension = Path.GetExtension(myfilestr);
            if (extension == ".mp3" || extension == ".mp4" || extension == ".wav")
            {
                Debug.WriteLine("YEAH");
                ViewBag.Subject = ClaimsPrincipal.Current.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
                ViewBag.Name = ClaimsPrincipal.Current.FindFirst("name").Value;
                //myFile = filMyFile.PostedFile;
                //string strFilename = Path.GetFileName(myFile.FileName);
                Debug.WriteLine("file: " + ClaimsPrincipal.Current.FindFirst(ClaimsPrincipal.Current.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value));
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
            CloudConfigurationManager.GetSetting("StorageConnectionString"));

                // Create the blob client.
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                // Retrieve reference to a previously created container.
                CloudBlobContainer container = blobClient.GetContainerReference("container");

                // Retrieve reference to a blob named "myblob".
                Debug.WriteLine("file: " + ClaimsPrincipal.Current.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value + "/" + Path.GetFileName(myfilestr));

                

                CloudBlockBlob blockBlob = container.GetBlockBlobReference(ClaimsPrincipal.Current.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value + "/" + Path.GetFileName(myfilestr));


                // Create or overwrite the "myblob" blob with contents from a local file.
                using (var fileStream = System.IO.File.OpenRead(@myfilestr))
                {
                    blockBlob.UploadFromStream(fileStream);
                }
            }

            Debug.WriteLine("All done. Press any key to finish...");

            return View("Upload");
        }











        //try
        //{
        //    Debug.WriteLine("Connect to SQL Server and demo Create, Read, Update and Delete operations.");
        //    // Build connection string
        //    SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
        //    builder.DataSource = "localhost";   // update me
        //    builder.UserID = "sa";              // update me
        //    builder.Password = "password123";      // update me
        //    builder.InitialCatalog = "master";

        //    // Connect to SQL
        //    Debug.Write("Connecting to SQL Server ... ");
        //    using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
        //    {
        //        connection.Open();
        //        Debug.WriteLine("Done.");

        //        // Create a sample database
        //        string DatabaseName = "TheButtonWorks";
        //        string sqlcommand = "";
        //        Debug.Write("Dropping and creating database '" + DatabaseName + "' ... ");
        //        StringBuilder sql = new StringBuilder();
        //        sql.Append("USE master; ");
        //        sql.Append("IF NOT EXISTS (SELECT * FROM master.dbo.sysdatabases WHERE name = '" + DatabaseName + "')");
        //        sql.Append("    CREATE DATABASE[" + DatabaseName + "]   ");
        //        sql.Append("    CONTAINMENT = NONE  ");
        //        sql.Append("    ON PRIMARY");
        //        sql.Append("    (");
        //        sql.Append("        NAME = N'" + DatabaseName + "',   ");
        //        sql.Append("        FILENAME = N'C:\\Rattlesnake\\" + DatabaseName + ".mdf',  ");
        //        sql.Append("        SIZE = 100MB,  ");
        //        sql.Append("        MAXSIZE = UNLIMITED,  ");
        //        sql.Append("        FILEGROWTH = 1MB");
        //        sql.Append("    ),   ");
        //        sql.Append("    FILEGROUP[FS] CONTAINS FILESTREAM DEFAULT");
        //        sql.Append("    (");
        //        sql.Append("        NAME = N'FS9',");
        //        sql.Append("        FILENAME = N'C:\\Rattlesnake\\FS9',");
        //        sql.Append("        MAXSIZE = UNLIMITED");
        //        sql.Append("    ),   ");
        //        sql.Append("    (");
        //        sql.Append("        NAME = N'FS10',  ");
        //        sql.Append("        FILENAME = N'C:\\Rattlesnake\\FS10',  ");
        //        sql.Append("        MAXSIZE = 100MB");
        //        sql.Append("    )  ");
        //        sql.Append("    LOG ON");
        //        sql.Append("    (");
        //        sql.Append("        NAME = N'" + DatabaseName + "_log',  ");
        //        sql.Append("        FILENAME = N'C:\\Rattlesnake\\" + DatabaseName + "_log.ldf',  ");
        //        sql.Append("        SIZE = 100MB,  ");
        //        sql.Append("        MAXSIZE = 1GB,  ");
        //        sql.Append("        FILEGROWTH = 1MB");
        //        sql.Append("    );");

        //        sqlcommand = sql.ToString();
        //        using (SqlCommand command = new SqlCommand(sqlcommand, connection))
        //        {
        //            command.ExecuteNonQuery();
        //            Debug.WriteLine("Done.");
        //        }




        // INSERT demo
        //Debug.Write("Inserting a new row into table, press any key to continue...");

        //string Name = "CARLOS";
        //sql.Clear();
        //sql.Append("ALTER DATABASE[" + DatabaseName + "]");
        //sql.Append("ADD FILE");
        //sql.Append("(");
        //sql.Append("NAME = N'" + Name + "',  ");
        //sql.Append("FILENAME = N'C:\\Rattlesnake\\" + Name + "',  ");
        //sql.Append("MAXSIZE = 100MB");
        //sql.Append(")  ");
        //sql.Append("TO FILEGROUP[FS];");
        //sqlcommand = sql.ToString();
        //using (SqlCommand command = new SqlCommand(sqlcommand, connection))
        //{
        //    int rowsAffected = command.ExecuteNonQuery();
        //    Debug.WriteLine(rowsAffected + " row(s) inserted");
        //}







        //    }


        //}
        //catch (SqlException e)
        //{
        //    Debug.WriteLine(e.ToString());
        //}





        /// <summary>
        /// Contains public method for validating the storage connection string.
        /// </summary>
        /// using System;
        public static class Common
        {

            /// <summary>
            /// Validates the connection string information in app.config and throws an exception if it looks like 
            /// the user hasn't updated this to valid values. 
            /// </summary>
            /// <returns>CloudStorageAccount object</returns>
            public static CloudStorageAccount CreateStorageAccountFromConnectionString()
            {
                CloudStorageAccount storageAccount;
                const string Message = "Invalid storage account information provided. Please confirm the AccountName and AccountKey are valid in the app.config file - then restart the sample.";

                try
                {
                    storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
                }
                catch (FormatException)
                {
                    Console.WriteLine(Message);
                    Console.ReadLine();
                    throw;
                }
                catch (ArgumentException)
                {
                    Console.WriteLine(Message);
                    Console.ReadLine();
                    throw;
                }

                return storageAccount;
            }
        }
    }
}