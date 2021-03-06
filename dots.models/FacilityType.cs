﻿namespace dots.models
{
    using System;
    
    public class FacilityType
    {
        public long RecordId { get; set; }
        public string Name { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public byte[] Timestamp { get; set; }
    }
}
