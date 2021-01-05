using System;

namespace InsertDataFromCsv.Model
{   
    internal class CpItem
    {
        public CpItem(string cd_tag, float value_x, float value_y, int elapsed_time, DateTime cu_date)
        {
            this.CD_TAG = cd_tag;
            this.VALUE_X = value_x;
            this.VALUE_Y = value_y;
            this.ELAPSED_TIME = elapsed_time;
            this.CU_DATE = cu_date;
        }
        public string CD_TAG { get; set; }
        public float VALUE_X { get; set; }
        public float VALUE_Y { get; set; }
        public int ELAPSED_TIME { get; set; }
        public DateTime CU_DATE { get; set; }
    }
}