using System;
using System.Text;
using System.Data.SqlClient;

namespace SqlServerSample
{
    class Program
    {
        static void Main(string[] args)
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
                    string DatabaseName = "Meetingsss";
                    string sqlcommand = "";
                    Console.Write("Dropping and creating database '"+DatabaseName+"' ... ");
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
        }
    }
}