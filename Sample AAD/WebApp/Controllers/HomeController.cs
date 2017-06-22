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
            try
            {
                Console.WriteLine("Connect to SQL Server and demo Create, Read, Update and Delete operations.");

                // Build connection string
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "DESKTOP-FL3GVJK";   // update me
                builder.UserID = "sa";              // update me
                builder.Password = "password123";      // update me
                builder.InitialCatalog = "master";

                // Connect to SQL
                Console.Write("Connecting to SQL Server ... ");
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    Console.WriteLine("Done.");

                    // Create a sample database
                    string DatabaseName = "TheButtonWorks";
                    string sqlcommand = "";
                    Console.Write("Dropping and creating database '" + DatabaseName + "' ... ");
                    StringBuilder sql = new StringBuilder();
                    sql.Append("USE master; ");
                    sql.Append("IF NOT EXISTS (SELECT * FROM master.dbo.sysdatabases WHERE name = '" + DatabaseName + "')");
                    sql.Append("    CREATE DATABASE[" + DatabaseName + "]   ");
                    sql.Append("    CONTAINMENT = NONE  ");
                    sql.Append("    ON PRIMARY");
                    sql.Append("    (");
                    sql.Append("        NAME = N'" + DatabaseName + "',   ");
                    sql.Append("        FILENAME = N'C:\\Rattlesnake\\" + DatabaseName + ".mdf',  ");
                    sql.Append("        SIZE = 100MB,  ");
                    sql.Append("        MAXSIZE = UNLIMITED,  ");
                    sql.Append("        FILEGROWTH = 1MB");
                    sql.Append("    ),   ");
                    sql.Append("    FILEGROUP[FS] CONTAINS FILESTREAM DEFAULT");
                    sql.Append("    (");
                    sql.Append("        NAME = N'FS1',");
                    sql.Append("        FILENAME = N'C:\\Rattlesnake\\FS1',");
                    sql.Append("        MAXSIZE = UNLIMITED");
                    sql.Append("    ),   ");
                    sql.Append("    (");
                    sql.Append("        NAME = N'FS2',  ");
                    sql.Append("        FILENAME = N'C:\\Rattlesnake\\FS2',  ");
                    sql.Append("        MAXSIZE = 100MB");
                    sql.Append("    )  ");
                    sql.Append("    LOG ON");
                    sql.Append("    (");
                    sql.Append("        NAME = N'" + DatabaseName + "_log',  ");
                    sql.Append("        FILENAME = N'C:\\Rattlesnake\\" + DatabaseName + "_log.ldf',  ");
                    sql.Append("        SIZE = 100MB,  ");
                    sql.Append("        MAXSIZE = 1GB,  ");
                    sql.Append("        FILEGROWTH = 1MB");
                    sql.Append("    );");

                    sqlcommand = sql.ToString();
                    using (SqlCommand command = new SqlCommand(sqlcommand, connection))
                    {
                        command.ExecuteNonQuery();
                        Console.WriteLine("Done.");
                    }

                    //// Create a Table and insert some sample data
                    //Console.Write("Creating sample table with data, press any key to continue...");
                    //Console.ReadKey(true);
                    //StringBuilder sb = new StringBuilder();
                    //sb.Append("USE " + DatabaseName + "; ");
                    //sb.Append("CREATE TABLE Employees ( ");
                    //sb.Append(" Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY, ");
                    //sb.Append(" Name NVARCHAR(50), ");
                    //sb.Append(" Location NVARCHAR(50) ");
                    //sb.Append("); ");
                    //sb.Append("INSERT INTO Employees (Name, Location) VALUES ");
                    //sb.Append("(N'Jared', N'Australia'), ");
                    //sb.Append("(N'Nikita', N'India'), ");
                    //sb.Append("(N'Tom', N'Germany'); ");
                    //sql = sb.ToString();
                    //using (SqlCommand command = new SqlCommand(sql, connection))
                    //{
                    //    command.ExecuteNonQuery();
                    //    Console.WriteLine("Done.");
                    //}

                    // INSERT demo
                    Console.Write("Inserting a new row into table, press any key to continue...");
                    Console.ReadKey(true);
                    string Name = "BOB";
                    sql.Clear();
                    sql.Append("ALTER DATABASE[" + DatabaseName + "]");
                    sql.Append("ADD FILE");
                    sql.Append("(");
                    sql.Append("NAME = N'" + Name + "',  ");
                    sql.Append("FILENAME = N'C:\\Rattlesnake\\" + Name + "',  ");
                    sql.Append("MAXSIZE = 100MB");
                    sql.Append(")  ");
                    sql.Append("TO FILEGROUP[FS];");
                    sqlcommand = sql.ToString();
                    using (SqlCommand command = new SqlCommand(sqlcommand, connection))
                    {
                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine(rowsAffected + " row(s) inserted");
                    }

                    //// UPDATE demo
                    //String userToUpdate = "Nikita";
                    //Console.Write("Updating 'Location' for user '" + userToUpdate + "', press any key to continue...");
                    //Console.ReadKey(true);
                    //sb.Clear();
                    //sb.Append("UPDATE Employees SET Location = N'United States' WHERE Name = @name");
                    //sql = sb.ToString();
                    //using (SqlCommand command = new SqlCommand(sql, connection))
                    //{
                    //    command.Parameters.AddWithValue("@name", userToUpdate);
                    //    int rowsAffected = command.ExecuteNonQuery();
                    //    Console.WriteLine(rowsAffected + " row(s) updated");
                    //}

                    //// DELETE demo
                    //String userToDelete = "Jared";
                    //Console.Write("Deleting user '" + userToDelete + "', press any key to continue...");
                    //Console.ReadKey(true);
                    //sb.Clear();
                    //sb.Append("DELETE FROM Employees WHERE Name = @name;");
                    //sql = sb.ToString();
                    //using (SqlCommand command = new SqlCommand(sql, connection))
                    //{
                    //    command.Parameters.AddWithValue("@name", userToDelete);
                    //    int rowsAffected = command.ExecuteNonQuery();
                    //    Console.WriteLine(rowsAffected + " row(s) deleted");
                    //}

                    //// READ demo
                    //Console.WriteLine("Reading data from table, press any key to continue...");
                    //Console.ReadKey(true);
                    //sql = "SELECT Id, Name, Location FROM Employees;";
                    //using (SqlCommand command = new SqlCommand(sql, connection))
                    //{

                    //    using (SqlDataReader reader = command.ExecuteReader())
                    //    {
                    //        while (reader.Read())
                    //        {
                    //            Console.WriteLine("{0} {1} {2}", reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
                    //        }
                    //    }
                    //}
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("All done. Press any key to finish...");
            Console.ReadKey(true);
            return null;
        }

        public class WebApp : System.Web.UI.Page
        {
            protected System.Web.UI.HtmlControls.HtmlInputFile File1;
            protected System.Web.UI.HtmlControls.HtmlInputButton Submit1;

            private void Submit1_ServerClick(object sender, System.EventArgs e)
            {
                if ((File1.PostedFile != null) && (File1.PostedFile.ContentLength > 0))
                {
                    string fn = System.IO.Path.GetFileName(File1.PostedFile.FileName);
                    string SaveLocation = Server.MapPath("App_Data") + "\\" + fn;
                    try
                    {
                        File1.PostedFile.SaveAs(SaveLocation);
                        Response.Write("The file has been uploaded.");
                    }
                    catch (Exception ex)
                    {
                        Response.Write("Error: " + ex.Message);
                        //Note: Exception.Message returns detailed message that describes the current exception. 
                        //For security reasons, we do not recommend you return Exception.Message to end users in 
                        //production environments. It would be better just to put a generic error message. 
                    }


                        Console.WriteLine("Connect to SQL Server and demo Create, Read, Update and Delete operations.");

                        // Build connection string
                        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                        builder.DataSource = "DESKTOP-FL3GVJK";   // update me
                        builder.UserID = "sa";              // update me
                        builder.Password = "password123";      // update me
                        builder.InitialCatalog = "master";

                        // Connect to SQL
                        Console.Write("Connecting to SQL Server ... ");
                        using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                        {
                            connection.Open();
                            Console.WriteLine("Done.");

                            // Create a sample database
                            string DatabaseName = "Meetingsss";
                            string sqlcommand = "";
                            Console.Write("Dropping and creating database '" + DatabaseName + "' ... ");
                            StringBuilder sql = new StringBuilder();
                            sql.Append("USE master; ");
                            sql.Append("IF NOT EXISTS (SELECT * FROM master.dbo.sysdatabases WHERE name = '" + DatabaseName + "')");
                            sql.Append("    CREATE DATABASE[" + DatabaseName + "]   ");
                            sql.Append("    CONTAINMENT = NONE  ");
                            sql.Append("    ON PRIMARY");
                            sql.Append("    (");
                            sql.Append("        NAME = N'" + DatabaseName + "',   ");
                            sql.Append("        FILENAME = N'C:\\Rattlesnake\\" + DatabaseName + ".mdf',  ");
                            sql.Append("        SIZE = 100MB,  ");
                            sql.Append("        MAXSIZE = UNLIMITED,  ");
                            sql.Append("        FILEGROWTH = 1MB");
                            sql.Append("    ),   ");
                            sql.Append("    FILEGROUP[FS] CONTAINS FILESTREAM DEFAULT");
                            sql.Append("    (");
                            sql.Append("        NAME = N'FS1',");
                            sql.Append("        FILENAME = N'C:\\Rattlesnake\\FS1',");
                            sql.Append("        MAXSIZE = UNLIMITED");
                            sql.Append("    ),   ");
                            sql.Append("    (");
                            sql.Append("        NAME = N'FS2',  ");
                            sql.Append("        FILENAME = N'C:\\Rattlesnake\\FS2',  ");
                            sql.Append("        MAXSIZE = 100MB");
                            sql.Append("    )  ");
                            sql.Append("    LOG ON");
                            sql.Append("    (");
                            sql.Append("        NAME = N'" + DatabaseName + "_log',  ");
                            sql.Append("        FILENAME = N'C:\\Rattlesnake\\" + DatabaseName + "_log.ldf',  ");
                            sql.Append("        SIZE = 100MB,  ");
                            sql.Append("        MAXSIZE = 1GB,  ");
                            sql.Append("        FILEGROWTH = 1MB");
                            sql.Append("    );");

                            sqlcommand = sql.ToString();
                            using (SqlCommand command = new SqlCommand(sqlcommand, connection))
                            {
                                command.ExecuteNonQuery();
                                Console.WriteLine("Done.");
                            }

                            //// Create a Table and insert some sample data
                            //Console.Write("Creating sample table with data, press any key to continue...");
                            //Console.ReadKey(true);
                            //StringBuilder sb = new StringBuilder();
                            //sb.Append("USE " + DatabaseName + "; ");
                            //sb.Append("CREATE TABLE Employees ( ");
                            //sb.Append(" Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY, ");
                            //sb.Append(" Name NVARCHAR(50), ");
                            //sb.Append(" Location NVARCHAR(50) ");
                            //sb.Append("); ");
                            //sb.Append("INSERT INTO Employees (Name, Location) VALUES ");
                            //sb.Append("(N'Jared', N'Australia'), ");
                            //sb.Append("(N'Nikita', N'India'), ");
                            //sb.Append("(N'Tom', N'Germany'); ");
                            //sql = sb.ToString();
                            //using (SqlCommand command = new SqlCommand(sql, connection))
                            //{
                            //    command.ExecuteNonQuery();
                            //    Console.WriteLine("Done.");
                            //}

                            // INSERT demo
                            Console.Write("Inserting a new row into table, press any key to continue...");
                            Console.ReadKey(true);
                            string Name = "BOB";
                            sql.Clear();
                            sql.Append("ALTER DATABASE[" + DatabaseName + "]");
                            sql.Append("ADD FILE");
                            sql.Append("(");
                            sql.Append("NAME = N'" + Name + "',  ");
                            sql.Append("FILENAME = N'C:\\Rattlesnake\\" + Name + "',  ");
                            sql.Append("MAXSIZE = 100MB");
                            sql.Append(")  ");
                            sql.Append("TO FILEGROUP[FS];");
                            sqlcommand = sql.ToString();
                            using (SqlCommand command = new SqlCommand(sqlcommand, connection))
                            {
                                int rowsAffected = command.ExecuteNonQuery();
                                Console.WriteLine(rowsAffected + " row(s) inserted");
                            }

                            //// UPDATE demo
                            //String userToUpdate = "Nikita";
                            //Console.Write("Updating 'Location' for user '" + userToUpdate + "', press any key to continue...");
                            //Console.ReadKey(true);
                            //sb.Clear();
                            //sb.Append("UPDATE Employees SET Location = N'United States' WHERE Name = @name");
                            //sql = sb.ToString();
                            //using (SqlCommand command = new SqlCommand(sql, connection))
                            //{
                            //    command.Parameters.AddWithValue("@name", userToUpdate);
                            //    int rowsAffected = command.ExecuteNonQuery();
                            //    Console.WriteLine(rowsAffected + " row(s) updated");
                            //}

                            //// DELETE demo
                            //String userToDelete = "Jared";
                            //Console.Write("Deleting user '" + userToDelete + "', press any key to continue...");
                            //Console.ReadKey(true);
                            //sb.Clear();
                            //sb.Append("DELETE FROM Employees WHERE Name = @name;");
                            //sql = sb.ToString();
                            //using (SqlCommand command = new SqlCommand(sql, connection))
                            //{
                            //    command.Parameters.AddWithValue("@name", userToDelete);
                            //    int rowsAffected = command.ExecuteNonQuery();
                            //    Console.WriteLine(rowsAffected + " row(s) deleted");
                            //}

                            //// READ demo
                            //Console.WriteLine("Reading data from table, press any key to continue...");
                            //Console.ReadKey(true);
                            //sql = "SELECT Id, Name, Location FROM Employees;";
                            //using (SqlCommand command = new SqlCommand(sql, connection))
                            //{

                            //    using (SqlDataReader reader = command.ExecuteReader())
                            //    {
                            //        while (reader.Read())
                            //        {
                            //            Console.WriteLine("{0} {1} {2}", reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
                            //        }
                            //    }
                            //}
                        }
                    //catch (SqlException e)
                    //{
                    //    Console.WriteLine(e.ToString());
                    //}

                    //Console.WriteLine("All done. Press any key to finish...");
                    //Console.ReadKey(true);




                }
                else
                {
                    Response.Write("Please select a file to upload.");
                }
            }
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


    }
    
}

