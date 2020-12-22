using System;

namespace InsertDataFromCsv.Model
{   
    internal class CpItem
    {
        public CpItem(float x, float y, DateTime testTime)
        {
            this.X = x;
            this.Y = y;
            this.TEST_TIME = testTime;
        }
        public float X { get; set; }
        public float Y { get; set; }
        public DateTime TEST_TIME { get; set; }
    }
}