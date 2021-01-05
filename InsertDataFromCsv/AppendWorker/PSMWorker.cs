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
    public class PSMWorker : AppendWorkerBase
    {
        public override DataType DataType { get => DataType.PSM; }

        protected override string ParentDirPath { get; set; }

        public override void Append()
        {
            string dirPath = Path.Combine(ParentDirPath);
            DirectoryInfo di = new DirectoryInfo(dirPath);
            foreach (FileInfo file in di.GetFiles())
            {
                string[] names = file.Name.Split(' ', '_', '.');                
                ExecuteInsert<PsmItem>(GetCsvFile<PsmItem>(file.FullName, new DateTime(), file.Name));
                Console.WriteLine(file.Name + ": Success");
            }
        }

        public override void ExecuteInsert<T>(List<T> list)
        {
            StringBuilder query = new StringBuilder();
            foreach (PsmItem item in list.Cast<PsmItem>().ToList())
            {
                query.AppendFormat($"INSERT tb_0001_psm (FILE_NAME, CD_TAG_HEAD, CD_TAG, VALUE_X, VALUE_Y, ELAPSED_TIME, CU_DATE, DT_REG) VALUES('{item.FILE_NAME}','{item.CD_TAG_HEAD}','{item.CD_TAG}','{item.VALUE_X}', '{item.VALUE_Y}','0', null, NOW());");
            }

            ExecuteQuery(query.ToString());
        }
        public override List<T> GetCsvFile<T>(string path, DateTime testDate, string name = "")
        {
            StreamReader sr = new StreamReader(path);
            List<PsmItem> result = new List<PsmItem>();
            bool dataStart = false;
            string[] columnHeader = new string[] { };
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                string[] data = line.Split(',');
                if (data[0] == "\"Cycle\"")
                {
                    dataStart = true;
                    columnHeader = data;
                    continue;
                }

                if (!dataStart) continue;
                string tag_header = $"{columnHeader[3].Split(' ')[0].Replace("\"","")} [{columnHeader[3].Split(' ')[1].Replace("\"", "")}]";
                PsmItem item = new PsmItem(name, tag_header, int.Parse(data[0]).ToString(), float.Parse(data[3]), float.Parse(data[4]), 0, new DateTime());
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
