namespace InventoryAPI.Model
{
    public class EmployeeWOCModel
    {
        public int totalSize { get; set; }
        public bool done { get; set; }
        public List<Record2> records { get; set; }
    }

    public class Attributes2
    {
        public string type { get; set; }
        public string url { get; set; }
    }

    public class Record2
    {
        public Attributes2 attributes { get; set; }
        public string CreatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public double Hourly_Rate__c { get; set; }
        public string Id { get; set; }
        public bool IsDeleted { get; set; }
        public string LastModifiedById { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public object LastReferencedDate { get; set; }
        public object LastViewedDate { get; set; }
        public string Name { get; set; }
        public string OwnerId { get; set; }
        public DateTime SystemModstamp { get; set; }
        public string User__c { get; set; }
    }
}
