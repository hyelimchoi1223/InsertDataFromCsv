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
        protected override string ParentDirPath { get; set; }

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
                DateTime cu_date = ConvertDateTime(names[1], names[2]);
                ExecuteInsert<CpItem>(GetCsvFile<CpItem>(file.FullName, cu_date));

                Console.WriteLine(file.Name + ": Success");
            }
        }

        public override void ExecuteInsert<T>(List<T> list)
        {
            StringBuilder query = new StringBuilder();
            foreach (CpItem item in list.Cast<CpItem>().ToList())
            {
                query.AppendFormat($"INSERT tb_0001_cp (CD_TAG, VALUE_X, VALUE_Y, ELAPSED_TIME, CU_DATE, DT_REG) VALUES('','{item.VALUE_X}', '{item.VALUE_Y}','0', '{item.CU_DATE.ToString("yyyy-MM-dd HH:mm:ss.fff")}', NOW());");
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
                CpItem item = new CpItem("",float.Parse(data[0]), float.Parse(data[1]),0, testDate);
                result.Add(item);
            }
            return result.Cast<T>().ToList();
        }
    }
}
