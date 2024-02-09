namespace InventoryAPI.Model
{
    public class TimesheetModel
    {
        public int totalSize { get; set; }
        public bool done { get; set; }
        public List<Record> records { get; set; }
    }


    public class Attributes
    {
        public string type { get; set; }
        public string url { get; set; }
    }

    public class Record
    {
        public Attributes attributes { get; set; }
        public string Assigned_Personnel__c { get; set; }
        public string CreatedById { get; set; }
        public object CreatedDate { get; set; }
        public double? Elepsed_Time__c { get; set; }
        public object End_Work__c { get; set; }
        public string Id { get; set; }
        public bool IsDeleted { get; set; }
        public bool Page__c { get; set; }
        public bool StatusToday__c { get; set; }
        public string LastModifiedById { get; set; }
        public object LastModifiedDate { get; set; }
        public object LatitudeCpt__c { get; set; }
        public object LatitudeTrv__c { get; set; }
        public object LatitudeWrk__c { get; set; }
        public object LongitudeCpt__c { get; set; }
        public object LongitudeTrv__c { get; set; }
        public object LongitudeWrk__c { get; set; }
        public object MALatitude__c { get; set; }
        public object MALongitude__c { get; set; }
        public object MAQuality__c { get; set; }
        public object MASimilarity__c { get; set; }
        public bool MASkipGeocoding__c { get; set; }
        public object MAVerifiedLatitude__c { get; set; }
        public object MAVerifiedLongitude__c { get; set; }
        public string Name { get; set; }
        public object Start_Travel__c { get; set; }
        public object Start_Work__c { get; set; }
        public string Status__c { get; set; }
        public object SystemModstamp { get; set; }
        public double? Travel_Time__c { get; set; }
        public object Visit__c { get; set; }
        public string Work_Order__c { get; set; }
        public double? Work_Time__c { get; set; }
    }
}
