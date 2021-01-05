using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsertDataFromCsv.Model
{
    public class DeviceFileHeadHistoryItem
    {
        public DeviceFileHeadHistoryItem(string cd_device, string cd_tag_head, string de_text)
        {
            this.CD_DEVICE = cd_device;
            this.CD_TAG_HEAD = cd_tag_head;
            this.DE_TEXT = de_text;            
        }
        public string CD_DEVICE { get; set; }
        public string CD_TAG_HEAD { get; set; }
        public string DE_TEXT { get; set; }
    }
}
