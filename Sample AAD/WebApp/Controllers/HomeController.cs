using Microsoft.Identity.Client;
using System.Configuration;
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
using Microsoft.Azure;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
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



namespace WebApp.Controllers
{

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
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference("container");

            // Retrieve reference to a blob named "myblob".
            CloudBlockBlob blockBlob = container.GetBlockBlobReference("meeting.wav");

            // Create or overwrite the "myblob" blob with contents from a local file.
            using (var fileStream = System.IO.File.OpenRead(@"C:\Users\Victoria Cheng\ProjectRattlesnake\SARA_website_stuff\VIDEOS\test.wav"))
            {
                blockBlob.UploadFromStream(fileStream);
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

            Debug.WriteLine("All done. Press any key to finish...");
            
            return View("Upload");
        }


        private const string ContainerPrefix = "sample-";

        /// <summary>
        /// Calls each of the methods in the getting started samples.
        /// </summary>
        public static void CallBlobGettingStartedSamples()
        {
           
        }

        /// <summary>
        /// Basic operations to work with block blobs
        /// </summary>
        /// <returns>A Task object.</returns>
        private static async Task BasicStorageBlockBlobOperationsAsync()
        {
            const string ImageToUpload = "test.wav";
            string containerName = ContainerPrefix + Guid.NewGuid();

            // Retrieve storage account information from connection string
            CloudStorageAccount storageAccount = Common.CreateStorageAccountFromConnectionString();

            // Create a blob client for interacting with the blob service.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Create a container for organizing blobs within the storage account.
            Console.WriteLine("1. Creating Container");
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);
            try
            {
                // The call below will fail if the sample is configured to use the storage emulator in the connection string, but 
                // the emulator is not running.
                // Change the retry policy for this call so that if it fails, it fails quickly.
                BlobRequestOptions requestOptions = new BlobRequestOptions() { RetryPolicy = new NoRetry() };
                await container.CreateIfNotExistsAsync(requestOptions, null);
            }
            catch (StorageException)
            {
                Console.WriteLine("If you are running with the default connection string, please make sure you have started the storage emulator. Press the Windows key and type Azure Storage to select and run it from the list of applications - then restart the sample.");
                Console.ReadLine();
                throw;
            }

            // To view the uploaded blob in a browser, you have two options. The first option is to use a Shared Access Signature (SAS) token to delegate 
            // access to the resource. See the documentation links at the top for more information on SAS. The second approach is to set permissions 
            // to allow public access to blobs in this container. Uncomment the line below to use this approach. Then you can view the image 
            // using: https://[InsertYourStorageAccountNameHere].blob.core.windows.net/democontainer/HelloWorld.png
            // await container.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

            // Upload a BlockBlob to the newly created container
            Console.WriteLine("2. Uploading BlockBlob");
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(ImageToUpload);

            // Set the blob's content type so that the browser knows to treat it as an image.
            blockBlob.Properties.ContentType = "audio/wav";
            await blockBlob.UploadFromFileAsync(ImageToUpload);

            // List all the blobs in the container.
            /// Note that the ListBlobs method is called synchronously, for the purposes of the sample. However, in a real-world
            /// application using the async/await pattern, best practices recommend using asynchronous methods consistently.
            Console.WriteLine("3. List Blobs in Container");
            foreach (IListBlobItem blob in container.ListBlobs())
            {
                // Blob type will be CloudBlockBlob, CloudPageBlob or CloudBlobDirectory
                // Use blob.GetType() and cast to appropriate type to gain access to properties specific to each type
                Console.WriteLine("- {0} (type: {1})", blob.Uri, blob.GetType());
            }

            // Download a blob to your file system
            Console.WriteLine("4. Download Blob from {0}", blockBlob.Uri.AbsoluteUri);
            await blockBlob.DownloadToFileAsync(string.Format("./CopyOf{0}", ImageToUpload), FileMode.Create);

            // Create a read-only snapshot of the blob
            Console.WriteLine("5. Create a read-only snapshot of the blob");
            CloudBlockBlob blockBlobSnapshot = await blockBlob.CreateSnapshotAsync(null, null, null, null);

            // Clean up after the demo. This line is not strictly necessary as the container is deleted in the next call.
            // It is included for the purposes of the example. 
            Console.WriteLine("6. Delete block blob and all of its snapshots");
            await blockBlob.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots, null, null, null);

            // Note that deleting the container also deletes any blobs in the container, and their snapshots.
            // In the case of the sample, we delete the blob and its snapshots, and then the container,
            // to show how to delete each kind of resource.
            Console.WriteLine("7. Delete Container");
            await container.DeleteIfExistsAsync();
        }

        /// <summary>
        /// Basic operations to work with block blobs
        /// </summary>
        /// <returns>A Task object.</returns>
        
        /// <summary>
        /// Basic operations to work with page blobs
        /// </summary>
        /// <returns>A Task object.</returns>
        private static async Task BasicStoragePageBlobOperationsAsync()
        {
            const string PageBlobName = "samplepageblob";
            string containerName = ContainerPrefix + Guid.NewGuid();

            // Retrieve storage account information from connection string
            CloudStorageAccount storageAccount = Common.CreateStorageAccountFromConnectionString();

            // Create a blob client for interacting with the blob service.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Create a container for organizing blobs within the storage account.
            Console.WriteLine("1. Creating Container");
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);
            await container.CreateIfNotExistsAsync();

            // Create a page blob in the newly created container.  
            Console.WriteLine("2. Creating Page Blob");
            CloudPageBlob pageBlob = container.GetPageBlobReference(PageBlobName);
            await pageBlob.CreateAsync(512 * 2 /*size*/); // size needs to be multiple of 512 bytes

            // Write to a page blob 
            Console.WriteLine("2. Write to a Page Blob");
            byte[] samplePagedata = new byte[512];
            Random random = new Random();
            random.NextBytes(samplePagedata);
            await pageBlob.UploadFromByteArrayAsync(samplePagedata, 0, samplePagedata.Length);

            // List all blobs in this container. Because a container can contain a large number of blobs the results 
            // are returned in segments with a maximum of 5000 blobs per segment. You can define a smaller maximum segment size
            // using the maxResults parameter on ListBlobsSegmentedAsync.
            Console.WriteLine("3. List Blobs in Container");
            BlobContinuationToken token = null;
            do
            {
                BlobResultSegment resultSegment = await container.ListBlobsSegmentedAsync(token);
                token = resultSegment.ContinuationToken;
                foreach (IListBlobItem blob in resultSegment.Results)
                {
                    // Blob type will be CloudBlockBlob, CloudPageBlob or CloudBlobDirectory
                    Console.WriteLine("{0} (type: {1}", blob.Uri, blob.GetType());
                }
            }
            while (token != null);

            // Read from a page blob
            Console.WriteLine("4. Read from a Page Blob");
            int bytesRead = await pageBlob.DownloadRangeToByteArrayAsync(samplePagedata, 0, 0, samplePagedata.Count());

            // Clean up after the demo 
            Console.WriteLine("6. Delete page Blob");
            await pageBlob.DeleteIfExistsAsync();

            Console.WriteLine("7. Delete Container");
            await container.DeleteIfExistsAsync();
        }

        /// <summary>
        /// Returns the container URI.  
        /// </summary>
        /// <param name="containerName">The container name</param>
        /// <returns>A URI for the container.</returns>
        private static Uri GetContainerUri(string containerName)
        {
            // Retrieve storage account information from connection string
            CloudStorageAccount storageAccount = Common.CreateStorageAccountFromConnectionString();

            return storageAccount.CreateCloudBlobClient().GetContainerReference(containerName).Uri;
        }

        /// <summary>
        /// Creates an Account SAS Token
        /// </summary>
        /// <returns>A SAS token.</returns>

    }

    /* 
    [Authorize]
    public async Task<ActionResult> About()
    {
        ViewBag.Name = ClaimsPrincipal.Current.FindFirst("name").Value;
        ViewBag.AuthorizationRequest = string.Empty;
        // The object ID claim will only be emitted for work or school accounts at this time.
        Claim oid = ClaimsPrincipal.Current.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier");
        ViewBag.ObjectId = oid == null ? string.Empty : oid.Value;

        // The 'preferred_username' claim can be used for showing the user's primary way of identifying themselves
        ViewBag.Username = ClaimsPrincipal.Current.FindFirst("preferred_username").Value;

        // The subject or nameidentifier claim can be used to uniquely identify the user
        ViewBag.Subject = ClaimsPrincipal.Current.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
        return View();
    }

    [Authorize]
    public async Task<ActionResult> SendMail()
    {            
        // try to get token silently
        string signedInUserID = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
        TokenCache userTokenCache = new MSALSessionCache(signedInUserID, this.HttpContext).GetMsalCacheInstance();            
        ConfidentialClientApplication cca = new ConfidentialClientApplication(clientId, redirectUri,new ClientCredential(appKey), userTokenCache, null);
        if (cca.Users.Count() > 0)
        {
            string[] scopes = { "Mail.Send" };
            try
            {
                AuthenticationResult result = await cca.AcquireTokenSilentAsync(scopes,cca.Users.First());
            }
            catch (MsalUiRequiredException)
            {
                try
                {// when failing, manufacture the URL and assign it
                    string authReqUrl = await WebApp.Utils.OAuth2RequestManager.GenerateAuthorizationRequestUrl(scopes, cca, this.HttpContext, Url);
                    ViewBag.AuthorizationRequest = authReqUrl;
                }
                catch (Exception ee)
                {

                }
            }
        }
        else
        {

        }
        return View();
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult> SendMail(string recipient, string subject, string body)
    {
        string messagetemplate = @"{{
""Message"": {{
""Subject"": ""{0}"",
""Body"": {{
            ""ContentType"": ""Text"",
  ""Content"": ""{1}""
}},
""ToRecipients"": [
  {{
    ""EmailAddress"": {{
      ""Address"": ""{2}""
    }}
}}
],
""Attachments"": []
}},
""SaveToSentItems"": ""false""
}}
";
        string message = String.Format(messagetemplate, subject, body, recipient);


        HttpClient client = new HttpClient();            
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://graph.microsoft.com/v1.0/me/microsoft.graph.sendMail");

        request.Content = new StringContent(message, Encoding.UTF8, "application/json");

        // try to get token silently
        string signedInUserID = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
        TokenCache userTokenCache = new MSALSessionCache(signedInUserID, this.HttpContext).GetMsalCacheInstance();
        ConfidentialClientApplication cca = new ConfidentialClientApplication(clientId, redirectUri, new ClientCredential(appKey), userTokenCache, null);
        if (cca.Users.Count() > 0)
        {
            string[] scopes = { "Mail.Send" };
            try
            {
                AuthenticationResult result = await cca.AcquireTokenSilentAsync(scopes,cca.Users.First());

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", result.AccessToken);
                HttpResponseMessage response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    ViewBag.AuthorizationRequest = null;
                    return View("MailSent");
                }
            }
            catch (MsalUiRequiredException)
            {
                try
                {// when failing, manufacture the URL and assign it
                    string authReqUrl = await WebApp.Utils.OAuth2RequestManager.GenerateAuthorizationRequestUrl(scopes, cca, this.HttpContext, Url);
                    ViewBag.AuthorizationRequest = authReqUrl;
                }
                catch (Exception ee)
                {

                }
            }
        }
        else { }
        return View();
    }

    public async Task<ActionResult> ReadMail()
    {            
        try
        {
            string signedInUserID = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            TokenCache userTokenCache = new MSALSessionCache(signedInUserID, this.HttpContext).GetMsalCacheInstance();

            ConfidentialClientApplication cca = 
                new ConfidentialClientApplication(clientId, redirectUri, new ClientCredential(appKey), userTokenCache, null);
            if (cca.Users.Count() > 0)
            {
                string[] scopes = { "Mail.Read" };
                AuthenticationResult result = await cca.AcquireTokenSilentAsync(scopes, cca.Users.First());

                HttpClient hc = new HttpClient();
                hc.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", result.AccessToken);
                HttpResponseMessage hrm = await hc.GetAsync("https://graph.microsoft.com/v1.0/me/messages");
                string rez = await hrm.Content.ReadAsStringAsync();
                ViewBag.Message = rez;
            }
            else { }
            return View();
        }
        catch (MsalUiRequiredException)
        {
            ViewBag.Relogin = "true";
            return View();
        }
        catch (Exception eee)
        {
            ViewBag.Error = "An error has occurred. Details: " + eee.Message;
            return View();
        }
    }
    public void RefreshSession()
    {
        HttpContext.GetOwinContext().Authentication.Challenge(
            new AuthenticationProperties { RedirectUri = "/Home/ReadMail" },
            OpenIdConnectAuthenticationDefaults.AuthenticationType);
    }
    */


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
