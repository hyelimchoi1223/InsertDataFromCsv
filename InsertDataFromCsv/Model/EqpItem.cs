using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsertDataFromCsv.Model
{
    public class EqpItem
    {
        public EqpItem(string file_name, string cd_tag_head, string cd_tag, float value_x, float value_y, int elapsed_time, DateTime cu_date)
        {
            this.FILE_NAME = file_name;
            this.CD_TAG_HEAD = cd_tag_head;
            this.CD_TAG = cd_tag;
            this.VALUE_X = value_x;
            this.VALUE_Y = value_y;
            this.ELAPSED_TIME = elapsed_time;
            this.CU_DATE = cu_date;
        }
        public string FILE_NAME { get; set; }
        public string CD_TAG_HEAD { get; set; }
        public string CD_TAG { get; set; }
        public float VALUE_X { get; set; }
        public float VALUE_Y { get; set; }
        public int ELAPSED_TIME { get; set; }
        public DateTime CU_DATE { get; set; }
    }
}
