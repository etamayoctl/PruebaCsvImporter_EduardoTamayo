using CsvImporter.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvImporter
{
    public class ConnectToBD
    {
        public static SqlConnection conexion;

        public static bool ConectBD()
        {
            var ruta = System.IO.Path.GetFullPath(@"..\..\..\");
            var file = "Configs\\SqlServer.json";
            Connection conn;

            using (StreamReader r = new StreamReader(ruta + file))
            {
                string json = r.ReadToEnd();
                conn = JsonConvert.DeserializeObject<Connection>(json);
            }

            if (!String.IsNullOrEmpty(conn.Password))
            {
                conexion = new SqlConnection("Server =" + conn.Server + ";Initial Catalog=" + conn.BD + ";User ID=" + conn.User + ";Password=" + conn.Password + ";Min Pool Size=2;Connect Timeout=340");
            }
            else
            {
                conexion = new SqlConnection("Server=" + conn.Server + ";Initial Catalog=" + conn.BD + ";Integrated Security=True");
            }

            try
            {
                conexion.Open();
                Console.WriteLine("( Conexión Exitosa al servidor SQL!.)");
                return true;
            }
            catch
            {
                conexion.Close();
                Console.WriteLine("( No se pudo establecer la Conexión SQL!.)");
                return false;
            }
        }

        public static void InsertData(string tablasCreate)
        {
            try
            {
                SqlCommand comando = new SqlCommand(tablasCreate, conexion);
                comando.ExecuteNonQuery();
            }
            catch
            {
                Console.WriteLine("No se pudieron crear las tablas, verifique la conexión.");
            }
        }

        public static void CreateTable(string cabezera)
        {
            var tablasCreate = @"DROP TABLE IF EXISTS [Stocks] 
                                CREATE TABLE [dbo].[Stocks](
	                            [PointOfSale] [int] NOT NULL,
	                            [Product] [bigint] NOT NULL,
	                            [Date] [DateTime] NOT NULL,
	                            [Stock] [int] NOT NULL);";
            InsertData(tablasCreate);
        }

        public static void CastCsvLineToObject(string line)
        {
            var split = line.Split(';');
            var tableInsert = @"INSERT INTO STOCKS (PointOfSale,Product,Date,Stock) VALUES (" + split[0] + "," + split[1] + ",'" + split[2] + "'," + split[3] + ")";
            InsertData(tableInsert);
        }
    }
}
