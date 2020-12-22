using InsertDataFromCsv.AppendWorker;
using InsertDataFromCsv.EnumData;
using System;

namespace InsertDataFromCsv
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                AppendHelper(DataType.OES);
                AppendHelper(DataType.CP);                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error : {ex.Message}");
            }            
        }

        private static void AppendHelper(DataType dataType)
        {            
            switch (dataType)
            {
                case DataType.OES:
                    OESWorker oes = new OESWorker();
                    oes.SetParentDir("OESPosition");
                    oes.Append();
                    break;
                case DataType.CP:
                    CPWorker cp = new CPWorker();
                    cp.SetParentDir("CPPosition");
                    cp.Append();
                    break;
                default:
                    break;
            }
        }
    }
}
