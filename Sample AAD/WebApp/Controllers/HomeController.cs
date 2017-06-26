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
            ViewBag.Name = ClaimsPrincipal.Current.FindFirst("name").Value;
            return View();
        }

        
        [Authorize]
              public async Task<ActionResult> UploadFile()
        {
            // Retrieve storage account from connection string.
            //HtmlInputFile filMyFile;
            
     
            //UploadThis();
            string myfilestr = Request.Form["filMyFile"];


            HttpPostedFile myFile;

        //myFile = filMyFile.PostedFile;
        //string strFilename = Path.GetFileName(myFile.FileName);

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
        CloudConfigurationManager.GetSetting("StorageConnectionString"));

        // Create the blob client.
        CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

        // Retrieve reference to a previously created container.
        CloudBlobContainer container = blobClient.GetContainerReference("container");

        // Retrieve reference to a blob named "myblob".
        CloudBlockBlob blockBlob = container.GetBlockBlobReference("victest");

            // Create or overwrite the "myblob" blob with contents from a local file.
            using (var fileStream = System.IO.File.OpenRead(@myfilestr))
            {
                blockBlob.UploadFromStream(fileStream);
            }

        Debug.WriteLine("All done. Press any key to finish...");

            return View("Upload");
        }
       
        protected void UploadThis()
        { 
            string PcId = Request.Form["PcId"];

            var nameBox = Request.Files["file"];

            string strFilename = Path.GetFileName(nameBox.FileName);
            string fileName = Path.GetFileName(nameBox.FileName);


            

       //     // Create or overwrite the "myblob" blob with contents from a local file.
       //     //using (var fileStream = System.IO.File.OpenRead(@strFilename))
       //     //{
       //     if (FileUpload1.HasFile)
       //     {
       //         // Get the name of the file to upload.
       //         string fileName = FileUpload1.FileName;
       //         CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
       //CloudConfigurationManager.GetSetting("StorageConnectionString"));

       //         // Create the blob client.
       //         CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

       //         // Retrieve reference to a previously created container.
       //         CloudBlobContainer container = blobClient.GetContainerReference("container");

       //         // Retrieve reference to a blob named "myblob".
       //         CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName);

       //         blockBlob.UploadFromStream(FileUpload1);
       //     }
       //     //}

       //     Debug.WriteLine("All done. Press any key to finish...");

        }


        // // Block blob basics
        //Console.WriteLine("Block Blob Sample");
        //BasicStorageBlockBlobOperationsAsync().Wait();

        //// Block Blobs basics using Account SAS

        //// Page blob basics
        //Console.WriteLine("\nPage Blob Sample");
        //BasicStoragePageBlobOperationsAsync().Wait();
        










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




    public partial class DefaultPage : System.Web.UI.Page
    {


        protected void UploadButton_Click(object sender, EventArgs e)
        {
            // Specify the path on the server to
            // save the uploaded file to.
            String savePath = @"c:\temp\uploads\";
            FileUpload FileUpload1 = (FileUpload)form1.FindControl("FileUpload1");
            // Before attempting to perform operations
            // on the file, verify that the FileUpload 
            // control contains a file.
            if (FileUpload1.HasFile)
            {
                // Get the name of the file to upload.
                String fileName = FileUpload1.FileName;

                // Append the name of the file to upload to the path.
                savePath += fileName;


                // Call the SaveAs method to save the 
                // uploaded file to the specified path.
                // This example does not perform all
                // the necessary error checking.               
                // If a file with the same name
                // already exists in the specified path,  
                // the uploaded file overwrites it.
                FileUpload1.SaveAs(savePath);

                // Notify the user of the name of the file
                // was saved under.

            }
            else
            {
                // Notify the user that a file was not uploaded.

            }

        }
    }


    }
