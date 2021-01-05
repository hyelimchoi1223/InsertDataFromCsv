using InsertDataFromCsv.Abstract;
using InsertDataFromCsv.EnumData;
using InsertDataFromCsv.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsertDataFromCsv.AppendWorker
{
    public class LPWorker : AppendWorkerBase
    {
        public override DataType DataType { get => DataType.LP; }

        protected override string ParentDirPath { get; set; }

        public override void Append()
        {
            string dirPath = Path.Combine(ParentDirPath);
            DirectoryInfo di = new DirectoryInfo(dirPath);
            foreach (FileInfo file in di.GetFiles())
            {
                string[] names = file.Name.Split(' ', '_', '.');
                DateTime cu_date = ConvertDateTime(names[1], names[2]);
                ExecuteInsert<LpItem>(GetCsvFile<LpItem>(file.FullName, cu_date));

                Console.WriteLine(file.Name + ": Success");
            }
        }

        public override void ExecuteInsert<T>(List<T> list)
        {
            StringBuilder query = new StringBuilder();
            foreach (LpItem item in list.Cast<LpItem>().ToList())
            {
                query.AppendFormat($"INSERT tb_0001_lp (CD_TAG, VALUE_X, VALUE_Y, ELAPSED_TIME, CU_DATE, DT_REG) VALUES('', '{item.VALUE_X}','{item.VALUE_Y}', '0', '{item.CU_DATE.ToString("yyyy-MM-dd HH:mm:ss.fff")}', NOW());");
            }
            ExecuteQuery(query.ToString());
        }

        public override List<T> GetCsvFile<T>(string path, DateTime testDate, string name = "")
        {
            StreamReader sr = new StreamReader(path);
            List<LpItem> result = new List<LpItem>();
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                string[] data = line.Split('\t');
                LpItem item = new LpItem("", float.Parse(data[0]), float.Parse(data[1]), 0, testDate);
                result.Add(item);
            }
            return result.Cast<T>().ToList();
        }

        public override void SetParentDir(string path)
        {
            ParentDirPath = path;
        }
    }
}
