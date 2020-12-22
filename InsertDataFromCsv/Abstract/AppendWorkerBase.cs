using InsertDataFromCsv.EnumData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsertDataFromCsv.Abstract
{
    public abstract class AppendWorkerBase
    {
        public abstract DataType DataType { get; }
        public abstract void SetParentDir(string path);
        public abstract string ParentDirPath { get; set; }
        public abstract void Append();
        public abstract void ExecuteInsert<T>(List<T> list);
        public abstract List<T> GetCsvFile<T>(string path, DateTime testDate, string name = "");
        protected DateTime ConvertDateTime(string date, string time)
        {
            int year = 0, month = 0, day = 0, hour = 0, minute = 0, second = 0, milli = 0;
            if (!int.TryParse(date.Substring(0, 4), out year)) throw new Exception("Convert error");
            if (!int.TryParse(date.Substring(4, 2), out month)) throw new Exception("Convert error");
            if (!int.TryParse(date.Substring(6, 2), out day)) throw new Exception("Convert error");
            if (!int.TryParse(time.Substring(0, 2), out hour)) throw new Exception("Convert error");
            if (!int.TryParse(time.Substring(2, 2), out minute)) throw new Exception("Convert error");
            if (!int.TryParse(time.Substring(4, 2), out second)) throw new Exception("Convert error");
            if (!int.TryParse(time.Substring(6), out milli)) throw new Exception("Convert error");
            DateTime result = new DateTime(year, month, day, hour, minute, second, milli);
            return result;
        }

        protected void ExecuteQuery(string query)
        {
            DBHelper helper = new DBHelper();
            if (!helper.ExecuteNonQuery(query.ToString()))
                throw new Exception("Query Error");
        }
    }
}
