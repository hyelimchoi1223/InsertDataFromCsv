using Dapper;
using InsertDataFromCsv.Abstract;
using InsertDataFromCsv.EnumData;
using InsertDataFromCsv.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace InsertDataFromCsv.AppendWorker
{
    class OESWorker : AppendWorkerBase
    {
        public override DataType DataType { get => DataType.OES; }
        public override string ParentDirPath { get; set; }

        public override void SetParentDir(string path)
        {
            ParentDirPath = path;
        }
        public override void Append()
        {
            string dirPath = Path.Combine(ParentDirPath);
            DirectoryInfo di = new DirectoryInfo(dirPath);
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                foreach (FileInfo file in dir.GetFiles())
                {
                    string[] names = file.Name.Split(' ', '_', '.');
                    DateTime testDate = ConvertDateTime(names[2], names[3]);
                    ExecuteInsert<OesItem>(GetCsvFile<OesItem>(file.FullName, testDate, dir.Name));

                    Console.WriteLine(file.Name + ": Success");
                }
            }
        }
        public override void ExecuteInsert<T>(List<T> list)
        {
            StringBuilder query = new StringBuilder();
            foreach (OesItem item in list.Cast<OesItem>().ToList())
            {
                query.AppendFormat($"INSERT tb_oes_real_sample (ID, X, Y, TEST_TIME) VALUES('{item.ID}', '{item.X}', '{item.Y}', '{item.TEST_TIME.ToString("yyyy-MM-dd HH:mm:ss.fff")}');");
            }
            ExecuteQuery(query.ToString());
        }
        
        public override List<T> GetCsvFile<T>(string path, DateTime testDate, string name = "")
        {            
            StreamReader sr = new StreamReader(path);
            List<OesItem> result = new List<OesItem>();            
            while (!sr.EndOfStream)
            {                
                string line = sr.ReadLine();
                string[] data = line.Split(',');                                
                OesItem item = new OesItem(name, float.Parse(data[0]), float.Parse(data[1]), testDate);
                result.Add(item);                
            }
            return result.Cast<T>().ToList();
        }
    }
}
