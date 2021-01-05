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
                //AppendHelper(DataType.OES);
                //AppendHelper(DataType.CP);                
                //AppendHelper(DataType.LP);
                //AppendHelper(DataType.EQP);
                AppendHelper(DataType.PSM);
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
                    oes.SetParentDir("C:\\Users\\LNB1705Y001\\Downloads\\OES");
                    oes.Append();
                    break;
                case DataType.CP:
                    CPWorker cp = new CPWorker();
                    cp.SetParentDir("C:\\Users\\LNB1705Y001\\Downloads\\CP");
                    cp.Append();
                    break;
                case DataType.LP:
                    LPWorker lp = new LPWorker();
                    lp.SetParentDir("C:\\Users\\LNB1705Y001\\Downloads\\LP");
                    lp.Append();
                    break;
                case DataType.EQP:
                    EQPHeaderWorker eqp_header = new EQPHeaderWorker();                    
                    eqp_header.SetParentDir("C:\\Users\\LNB1705Y001\\Downloads\\EQP");
                    eqp_header.Append();
                    EQPWorker eqp = new EQPWorker();
                    eqp.SetParentDir("C:\\Users\\LNB1705Y001\\Downloads\\EQP");
                    eqp.Append();
                    break;
                case DataType.PSM:
                    PSMHeaderWorker psm_header = new PSMHeaderWorker();
                    psm_header.SetParentDir("C:\\Users\\LNB1705Y001\\Downloads\\PSM");
                    psm_header.Append();
                    PSMWorker psm = new PSMWorker();
                    psm.SetParentDir("C:\\Users\\LNB1705Y001\\Downloads\\PSM");
                    psm.Append();
                    break;
                default:
                    break;
            }
        }
    }
}
