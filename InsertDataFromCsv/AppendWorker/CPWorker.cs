using InsertDataFromCsv.Abstract;
using InsertDataFromCsv.EnumData;
using InsertDataFromCsv.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace InsertDataFromCsv.AppendWorker
{
    class CPWorker : AppendWorkerBase
    {
        public override DataType DataType { get => DataType.CP; }
        public override string ParentDirPath { get; set; }

        public override void SetParentDir(string path)
        {
            ParentDirPath = path;
        }
        public override void Append()
        {
            string dirPath = Path.Combine(ParentDirPath);
            DirectoryInfo di = new DirectoryInfo(dirPath);
            foreach (FileInfo file in di.GetFiles())
            {
                string[] names = file.Name.Split(' ', '_', '.');
                DateTime testDate = ConvertDateTime(names[1], names[2]);
                ExecuteInsert<CpItem>(GetCsvFile<CpItem>(file.FullName, testDate));

                Console.WriteLine(file.Name + ": Success");
            }
        }

        public override void ExecuteInsert<T>(List<T> list)
        {
            StringBuilder query = new StringBuilder();
            foreach (CpItem item in list.Cast<CpItem>().ToList())
            {
                query.AppendFormat($"INSERT tb_cp_real_sample (X, Y, TEST_TIME) VALUES('{item.X}', '{item.Y}', '{item.TEST_TIME.ToString("yyyy-MM-dd HH:mm:ss.fff")}');");
            }

            ExecuteQuery(query.ToString());
        }

        public override List<T> GetCsvFile<T>(string path, DateTime testDate, string name = "")
        {
            StreamReader sr = new StreamReader(path);
            List<CpItem> result = new List<CpItem>();
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                string[] data = line.Split(',');
                CpItem item = new CpItem(float.Parse(data[0]), float.Parse(data[1]), testDate);
                result.Add(item);
            }
            return result.Cast<T>().ToList();
        }
    }
}
