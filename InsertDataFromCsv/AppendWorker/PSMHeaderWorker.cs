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
    public class PSMHeaderWorker : AppendWorkerBase
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
                ExecuteInsert<DeviceFileHeadHistoryItem>(GetCsvFile<DeviceFileHeadHistoryItem>(file.FullName, new DateTime(), file.Name));                
                Console.WriteLine(file.Name + ": Success");
            }
        }

        public override void ExecuteInsert<T>(List<T> list)
        {
            StringBuilder query = new StringBuilder();
            foreach (DeviceFileHeadHistoryItem item in list.Cast<DeviceFileHeadHistoryItem>().ToList())
            {
                query.AppendFormat($"INSERT tb_0001_device_file_head_history (CD_DEVICE, CD_TAG_HEAD, DE_TEXT, DT_REG) VALUES('{item.CD_DEVICE}','{item.CD_TAG_HEAD}','{item.DE_TEXT}', NOW());");
            }

            ExecuteQuery(query.ToString());
        }

        public override List<T> GetCsvFile<T>(string path, DateTime testDate, string name = "")
        {
            StreamReader sr = new StreamReader(path);
            List<DeviceFileHeadHistoryItem> result = new List<DeviceFileHeadHistoryItem>();
            StringBuilder headerContent = new StringBuilder();
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                string[] data = line.Split(',');
                if (data[0] == "\"Cycle\"")
                {
                    break;
                }
                headerContent.Append(line);
                headerContent.Append(',');
            }
            headerContent.Remove(headerContent.Length - 1, 1);
            DeviceFileHeadHistoryItem item = new DeviceFileHeadHistoryItem(this.DataType.ToString(), name, headerContent.ToString());
            result.Add(item);
            return result.Cast<T>().ToList();
        }

        public override void SetParentDir(string path)
        {
            ParentDirPath = path;
        }
    }
}
