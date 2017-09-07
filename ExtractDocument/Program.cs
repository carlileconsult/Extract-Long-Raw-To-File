using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using Oracle.DataAccess.Client;

namespace ExtractDocument
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting the extraction process...");
            try
            {
                GetFiles();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            Console.WriteLine("Ending the extraction process...");
            Console.Read();

        }

        public static void GetFiles()
        {
            var connectionString = ConfigurationManager.AppSettings["DBConnection"];
            var commandString = ConfigurationManager.AppSettings["DBCommand"];
            var fileNameField = ConfigurationManager.AppSettings["DBFileNameColumn"];
            var dataField = ConfigurationManager.AppSettings["DBLongRawColumn"];
            var outputFolder = ConfigurationManager.AppSettings["OutputFolder"]; 

            var connection = new OracleConnection(connectionString);
            connection.Open();

            try
            {
                var command = new OracleCommand(commandString, connection) { InitialLONGFetchSize = -1 };

                var reader = command.ExecuteReader();


                var dataTable = new DataTable();
                dataTable.Load(reader);

                foreach (DataRow row in dataTable.Rows)
                {
                    // Build path
                    var path = string.Format(@"{0}{1}", outputFolder, row[fileNameField]);
                    var file = (byte[])row[dataField];

                    using (var fs = new FileStream(path, FileMode.Create))
                    {
                        fs.Write(file, 0, file.Count());
                    }
                }
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
