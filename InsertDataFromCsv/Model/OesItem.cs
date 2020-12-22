using System;

namespace InsertDataFromCsv.Model
{
    internal class OesItem
    {
        public OesItem(string id, float x, float y, DateTime testTime)
        {
            this.ID = id;
            this.X = x;
            this.Y = y;
            this.TEST_TIME = testTime;
        }
        public string ID { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public DateTime TEST_TIME { get; set; }
    }
}