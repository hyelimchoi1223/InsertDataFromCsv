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
        protected override string ParentDirPath { get; set; }

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
                    DateTime cu_date = ConvertDateTime(names[2], names[3]);
                    ExecuteInsert<OesItem>(GetCsvFile<OesItem>(file.FullName, cu_date, dir.Name));

                    Console.WriteLine(file.Name + ": Success");
                }
            }
        }
        public override void ExecuteInsert<T>(List<T> list)
        {
            StringBuilder query = new StringBuilder();
            foreach (OesItem item in list.Cast<OesItem>().ToList())
            {
                query.AppendFormat($"INSERT tb_0001_oes (CD_TAG, VALUE_X, VALUE_Y, ELAPSED_TIME, CU_DATE, DT_REG) VALUES('{item.CD_TAG}', '{item.VALUE_X}','{item.VALUE_Y}', '{item.ELAPSED_TIME}', '{item.CU_DATE.ToString("yyyy-MM-dd HH:mm:ss.fff")}', NOW());");
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
                OesItem item = new OesItem(name, float.Parse(data[0]), float.Parse(data[1]),0, testDate);
                result.Add(item);                
            }
            return result.Cast<T>().ToList();
        }
    }
}
