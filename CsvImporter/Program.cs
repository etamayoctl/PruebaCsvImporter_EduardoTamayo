using Azure.Storage.Blobs;
using CsvImporter.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;

namespace CsvImporter
{
    class Program
    {

        static async System.Threading.Tasks.Task Main(string[] args)
        {
            Console.Title = "Acme Corporation";
            BlobClient blobClient = new BlobClient(new Uri(Util.GetUrl()), null);
            if (await blobClient.ExistsAsync())
            {
                var response = await blobClient.DownloadAsync();
                using (var streamReader = new StreamReader(response.Value.Content))
                {
                    if (ConnectToBD.ConectBD())
                    {
                        if (!streamReader.EndOfStream)
                        {
                            ConnectToBD.CreateTable(await streamReader.ReadLineAsync());
                        }
                        while (!streamReader.EndOfStream)
                        {
                            ConnectToBD.CastCsvLineToObject(await streamReader.ReadLineAsync());
                        }
                    }
                }
            }
        }

    }
}
