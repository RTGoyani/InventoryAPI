namespace InventoryAPI.Model
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Currency1
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class CustomField1
    {
        public int id { get; set; }
        public string name { get; set; }
        public string value { get; set; }
        public string dataType { get; set; }
    }

    public class Datum1
    {
        public int id { get; set; }
        public int starred { get; set; }
        public int syncToken { get; set; }
        public string number { get; set; }
        public DateTime date { get; set; }
        public Vendor1 vendor { get; set; }
        public Location1 location { get; set; }
        public Terms1 terms { get; set; }
        public object department { get; set; }
        public Currency1 currency { get; set; }
        public object taxCode { get; set; }
        public object linkedTransaction { get; set; }
        public double exchangeRate { get; set; }
        public string vendorMessage { get; set; }
        public string comment { get; set; }
        public string vendorNotes { get; set; }
        public string payment { get; set; }
        public object vendorInvoiceDate { get; set; }
        public object vendorInvoiceDueDate { get; set; }
        public List<CustomField1> customFields { get; set; }
        public double depositPercent { get; set; }
        public double depositAmount { get; set; }
        public double subTotal { get; set; }
        public double taxAmount { get; set; }
        public double total { get; set; }
        public double overhead { get; set; }
        public bool archived { get; set; }
        public bool summaryOnly { get; set; }
        public bool hasSignature { get; set; }
        public bool updateDefaultCosts { get; set; }
        public bool autoSerialLots { get; set; }
        public object keys { get; set; }
        public object values { get; set; }
        public List<Line1> lines { get; set; }
        public List<object> otherCosts { get; set; }
    }

    public class Item1
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class Line1
    {
        public int id { get; set; }
        public int lineNumber { get; set; }
        public Item1 item { get; set; }
        public object vendorPartNumber { get; set; }
        public object @class { get; set; }
        public object job { get; set; }
        public object workcenter { get; set; }
        public object customer { get; set; }
        public Tax1 tax { get; set; }
        public object linkedTransaction { get; set; }
        public string description { get; set; }
        public double quantity { get; set; }
        public double weight { get; set; }
        public double volume { get; set; }
        public string weightunit { get; set; }
        public string volumeunit { get; set; }
        public double unitprice { get; set; }
        public double amount { get; set; }
        public double received { get; set; }
        public object uom { get; set; }
        public object bin { get; set; }
        public object lot { get; set; }
        public object lotExpiration { get; set; }
        public List<object> serials { get; set; }
    }

    public class Location1
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class ItemReceiptModel1
    {
        public int count { get; set; }
        public int totalCount { get; set; }
        public List<Datum1> data { get; set; }
        public string status { get; set; }
        public string message { get; set; }
    }

    public class Tax1
    {
        public bool taxable { get; set; }
        public object taxCode { get; set; }
    }

    public class Terms1
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class Vendor1
    {
        public int id { get; set; }
        public string name { get; set; }
    }



}
