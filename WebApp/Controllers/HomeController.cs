﻿using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Protocols;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using WebApp_OpenIDConnect_DotNet.Models;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OpenIdConnect;
using System.Runtime.Serialization.Formatters.Binary;
using System.Net.Http.Headers;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;

using System.Xml.Linq;
using System.Data.SqlClient;
using System.Diagnostics;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.RetryPolicies;
using Microsoft.WindowsAzure;

using System.Configuration;
using System.Globalization;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.ServiceModel.Channels;
using Microsoft.Azure;

using Microsoft.WindowsAzure.Storage.Shared.Protocol;



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

            //TODO: Search through transcripts:
            //https://docs.microsoft.com/en-us/azure/search/search-howto-indexing-azure-blob-storage

            ActionResult results = GetUserFiles();
            return View();
        }

        [Authorize]
        public ActionResult GetUserFiles()
        {
            string audio_filename = "";
            string transcription_filename = "";
            string analysis_filename = "";
            ViewBag.results = new List<Tuple<String, string[], String>>();
            try
            {
                Debug.WriteLine("Connect to SQL Server and demo Create, Read, Update and Delete operations.");
                // Build connection string
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "projectrattlensake.database.windows.net";  
                builder.UserID = "PRadmin@projectrattlensake";              
                builder.Password = "Passw0rd!";      
                builder.InitialCatalog = "ProjectRattlesnakeDB";

                Debug.Write("Connecting to SQL Server ... ");
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    Debug.WriteLine("Done.");
                    
                    string sqlcommand = "";
                    StringBuilder sql = new StringBuilder();
                    sql.Append("USE ProjectRattlesnakeDB; ");
                    sql.Append("SELECT Audio_File, Transcription_Status, Transcription_File, Analysis_Status, Analysis_File FROM RattleSnakeTable WHERE UserID = '" + (ClaimsPrincipal.Current.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value).ToString() + "';");

                    sqlcommand = sql.ToString();

                    Debug.WriteLine(sqlcommand);

                    using (SqlCommand command = new SqlCommand(sqlcommand, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                audio_filename = reader.GetString(0);
                                Debug.WriteLine(audio_filename);
                                if (reader.GetInt32(1) == 2)
                                {
                                    transcription_filename = reader.GetString(2);
                                }
                                else
                                {
                                    transcription_filename = "NULL";
                                }
                                if (reader.GetInt32(3) == 2)
                                {
                                    analysis_filename = reader.GetString(4);
                                }
                                else
                                {
                                    analysis_filename = "NULL";
                                }

                                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                    CloudConfigurationManager.GetSetting("StorageConnectionString"));

                                // Create the blob client.
                                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                                // Retrieve reference to a previously created container.
                                CloudBlobContainer container = blobClient.GetContainerReference("container");

                                // Retrieve reference to a blob named "myblob".

                                string audio_file = container.GetBlockBlobReference(ClaimsPrincipal.Current.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value + "/" + audio_filename).Uri.ToString();
                                string transcription_file = "The file has not been transcribed yet.";
                                string analysis_file = "The file has not been analyzed yet.";

                                if (transcription_filename != "NULL")
                                {
                                    CloudBlockBlob blob = container.GetBlockBlobReference(ClaimsPrincipal.Current.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value + "/" + transcription_filename);
                                    using (var stream = blob.OpenRead())
                                    {
                                        using (StreamReader reader1 = new StreamReader(stream))
                                        {
                                            transcription_file = "";
                                            string line = "";
                                            while ((line = reader1.ReadLine()) != null)
                                            {

                                                transcription_file+=line;
                                                transcription_file +="\n";
                                            }
                                        }
                                    }
                                }
                                var lines = transcription_file.Split('\n');
                                Debug.WriteLine(lines);
                                string[] transcription_file_lines = lines;
                                Debug.WriteLine(transcription_file_lines);
                                if (analysis_filename != "NULL")
                                {
                                    CloudBlockBlob blob = container.GetBlockBlobReference(ClaimsPrincipal.Current.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value + "/" + analysis_filename);
                                    using (var stream = blob.OpenRead())
                                    {
                                        using (StreamReader reader2 = new StreamReader(stream))
                                        {

                                            analysis_file = reader2.ReadToEnd();

                                        }
                                    }
                                }
                                ViewBag.results.Add(
                                    Tuple.Create(audio_file, transcription_file_lines, analysis_file)
                                );

                            }
                        }
                        else
                        {
                            Debug.WriteLine("No rows found.");
                        }

                        reader.Close();

                        Debug.WriteLine("Done.");
                    }



                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());
            }

            return View("Upload");

        }
        
        [Authorize]
        public async Task<ActionResult> UploadFile()
        {
            // Retrieve storage account from connection string.
            string timestamp = System.DateTime.Now.ToString(new CultureInfo("en-US"));
            Guid g = Guid.NewGuid();
            var myFile = Request.Files["file"];
            

            string myfilestr = myFile.FileName;

            string extension = Path.GetExtension(myfilestr);
            
            if (extension == ".mp3" || extension == ".mp4" || extension == ".wav")
            {
                ViewBag.Subject = ClaimsPrincipal.Current.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
                ViewBag.Name = ClaimsPrincipal.Current.FindFirst("name").Value;
                CloudStorageAccount storageAccount;
                try
                {
                    storageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["ConnectionAzure"].ConnectionString);
                    //CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
                }
                catch
                {
                    return View("Upload"); // connection to blob store failed.
                }

                // Create the blob client.
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                // Retrieve reference to a previously created container.
                CloudBlobContainer container = blobClient.GetContainerReference("container");

                // Retrieve reference to a blob named "myblob".
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(ClaimsPrincipal.Current.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value + "/" + g.ToString() + Path.GetFileName(myfilestr));

                // Upload Blob
                try
                {
                    using (myFile.InputStream)
                    {
                        blockBlob.UploadFromStream(myFile.InputStream);
                    }
                }
                catch
                {
                    return View("Upload"); // failed to upload blob
                }
            }
            else
            {
                return View("Upload"); // incorrect format
            }

            try
            {
                Debug.WriteLine("Connect to SQL Server and demo Create, Read, Update and Delete operations.");
                // Build connection string
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "projectrattlensake.database.windows.net";   // update me
                builder.UserID = "PRadmin@projectrattlensake";              // update me
                builder.Password = "Passw0rd!";      // update me
                builder.InitialCatalog = "ProjectRattlesnakeDB";

                // Connect to SQL
                Debug.Write("Connecting to SQL Server ... ");
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    Debug.WriteLine("Done.");

                    // Create a sample database
                    string DatabaseName = "ProjectRattlesnakeDB";
                    string sqlcommand = "";
                    StringBuilder sql = new StringBuilder();
                    sql.Append("USE ProjectRattlesnakeDB; ");

                    sql.Append("INSERT INTO RattleSnakeTable");
                    sql.Append("([FileGuid],[UserName], [UserID], [Upload_Time], [Audio_File], [Transcription_Status],[Transcription_File],[Analysis_Status],[Analysis_File])");
                    sql.Append("VALUES");
                    sql.Append("('"+g.ToString()+"','" + (ClaimsPrincipal.Current.FindFirst("name").Value).ToString() + "', '"+ (ClaimsPrincipal.Current.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value).ToString() + "','"+timestamp+"','"+ g.ToString()+Path.GetFileName(myfilestr) + "', 0,NULL, 0, NULL);");
                    
                    sqlcommand = sql.ToString();

                    Debug.WriteLine(sqlcommand);
                    using (SqlCommand command = new SqlCommand(sqlcommand, connection))
                    {
                        command.ExecuteNonQuery();
                        Debug.WriteLine("Done.");
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());
            }
            

            return View("Upload");
        }
        
        

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
