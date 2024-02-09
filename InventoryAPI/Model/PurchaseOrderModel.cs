namespace InventoryAPI.Model
{


    //// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    //public class Address
    //{
    //    public string line1 { get; set; }
    //    public string line2 { get; set; }
    //    public string line3 { get; set; }
    //    public string line4 { get; set; }
    //    public string line5 { get; set; }
    //    public string city { get; set; }
    //    public string stateProvince { get; set; }
    //    public string postalCode { get; set; }
    //    public string country { get; set; }
    //}

    //public class AssignedToUser
    //{
    //    public int id { get; set; }
    //    public string name { get; set; }
    //}

    //public class Billing
    //{
    //    public string company { get; set; }
    //    public string contact { get; set; }
    //    public string phone { get; set; }
    //    public string email { get; set; }
    //    public string addressName { get; set; }
    //    public string addressType { get; set; }
    //    public Address address { get; set; }
    //}

    //public class Currency
    //{
    //    public int id { get; set; }
    //    public string name { get; set; }
    //}

    //public class CustomField
    //{
    //    public int id { get; set; }
    //    public string name { get; set; }
    //    public string value { get; set; }
    //    public string dataType { get; set; }
    //}

    //public class Datum
    //{
    //    public int id { get; set; }
    //    public int starred { get; set; }
    //    public int syncToken { get; set; }
    //    public string number { get; set; }
    //    public DateTime date { get; set; }
    //    public Vendor vendor { get; set; }
    //    public object customer { get; set; }
    //    public Location location { get; set; }
    //    public Billing billing { get; set; }
    //    public Shipping shipping { get; set; }
    //    public Terms terms { get; set; }
    //    public object department { get; set; }
    //    public AssignedToUser assignedToUser { get; set; }
    //    public Currency currency { get; set; }
    //    public object taxCode { get; set; }
    //    public ShippingMethod shippingMethod { get; set; }
    //    public string trackingNumber { get; set; }
    //    public object linkedTransaction { get; set; }
    //    public List<object> linkedReceipts { get; set; }
    //    public string receivedStatus { get; set; }
    //    public double exchangeRate { get; set; }
    //    public string vendorMessage { get; set; }
    //    public string comment { get; set; }
    //    public string vendorNotes { get; set; }
    //    public DateTime expectedDate { get; set; }
    //    public DateTime expectedShip { get; set; }
    //    public List<CustomField> customFields { get; set; }
    //    public double depositPercent { get; set; }
    //    public double depositAmount { get; set; }
    //    public double subTotal { get; set; }
    //    public double taxAmount { get; set; }
    //    public double total { get; set; }
    //    public double openAmount { get; set; }
    //    public bool dropShip { get; set; }
    //    public bool blanketPO { get; set; }
    //    public bool contractManufacturing { get; set; }
    //    public bool confirmed { get; set; }
    //    public bool closed { get; set; }
    //    public bool archived { get; set; }
    //    public bool pendingApproval { get; set; }
    //    public bool summaryOnly { get; set; }
    //    public bool hasSignature { get; set; }
    //    public bool updateDefaultCosts { get; set; }
    //    public object syncMessage { get; set; }
    //    public string lastSync { get; set; }
    //    public object keys { get; set; }
    //    public object values { get; set; }
    //    public List<Line> lines { get; set; }
    //}

    //public class Item
    //{
    //    public int id { get; set; }
    //    public string name { get; set; }
    //}

    //public class Line
    //{
    //    public int id { get; set; }
    //    public int lineNumber { get; set; }
    //    public Item item { get; set; }
    //    public string vendorPartNumber { get; set; }
    //    public object @class { get; set; }
    //    public object job { get; set; }
    //    public object workcenter { get; set; }
    //    public string customer { get; set; }
    //    public Tax tax { get; set; }
    //    public object linkedTransaction { get; set; }
    //    public string description { get; set; }
    //    public double quantity { get; set; }
    //    public double weight { get; set; }
    //    public double volume { get; set; }
    //    public string weightunit { get; set; }
    //    public string volumeunit { get; set; }
    //    public double unitprice { get; set; }
    //    public double amount { get; set; }
    //    public double received { get; set; }
    //    public object uom { get; set; }
    //    public object bin { get; set; }
    //    public object lot { get; set; }
    //    public object lotExpiration { get; set; }
    //    public object serials { get; set; }
    //}

    //public class Location
    //{
    //    public int id { get; set; }
    //    public string name { get; set; }
    //}

    //public class PurchaseOrderModel
    //{
    //    public int count { get; set; }
    //    public int totalCount { get; set; }
    //    public List<Datum> data { get; set; }
    //    public string status { get; set; }
    //    public string message { get; set; }
    //}

    //public class Shipping
    //{
    //    public string company { get; set; }
    //    public string contact { get; set; }
    //    public string phone { get; set; }
    //    public string email { get; set; }
    //    public string addressName { get; set; }
    //    public string addressType { get; set; }
    //    public Address address { get; set; }
    //}

    //public class ShippingMethod
    //{
    //    public int id { get; set; }
    //    public string name { get; set; }
    //}

    //public class Tax
    //{
    //    public bool taxable { get; set; }
    //    public object taxCode { get; set; }
    //}

    //public class Terms
    //{
    //    public int id { get; set; }
    //    public string name { get; set; }
    //}

    //public class Vendor
    //{
    //    public int id { get; set; }
    //    public string name { get; set; }
    //}

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Address
    {
        public string line1 { get; set; }
        public string line2 { get; set; }
        public object line3 { get; set; }
        public object line4 { get; set; }
        public object line5 { get; set; }
        public string city { get; set; }
        public string stateProvince { get; set; }
        public string postalCode { get; set; }
        public string country { get; set; }
    }

    public class Billing
    {
        public string company { get; set; }
        public string contact { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string addressName { get; set; }
        public string addressType { get; set; }
        public Address address { get; set; }
    }

    public class Customer
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class Datum
    {
        public int id { get; set; }
        public int starred { get; set; }
        public int syncToken { get; set; }
        public string number { get; set; }
        public DateTime date { get; set; }
        public Vendor vendor { get; set; }
        public object customer { get; set; }
        public Location location { get; set; }
        public Billing billing { get; set; }
        public Shipping shipping { get; set; }
        public Terms terms { get; set; }
        public Department department { get; set; }
        public object assignedToUser { get; set; }
        public object currency { get; set; }
        public object taxCode { get; set; }
        public ShippingMethod shippingMethod { get; set; }
        public object trackingNumber { get; set; }
        public object linkedTransaction { get; set; }
        public List<LinkedReceipt> linkedReceipts { get; set; }
        public string receivedStatus { get; set; }
        public double exchangeRate { get; set; }
        public string vendorMessage { get; set; }
        public string comment { get; set; }
        public string vendorNotes { get; set; }
        public DateTime? expectedDate { get; set; }
        public DateTime? expectedShip { get; set; }
        public object customFields { get; set; }
        public double depositPercent { get; set; }
        public double depositAmount { get; set; }
        public double subTotal { get; set; }
        public double taxAmount { get; set; }
        public double total { get; set; }
        public double openAmount { get; set; }
        public bool dropShip { get; set; }
        public bool blanketPO { get; set; }
        public bool contractManufacturing { get; set; }
        public bool confirmed { get; set; }
        public bool closed { get; set; }
        public bool archived { get; set; }
        public bool pendingApproval { get; set; }
        public bool summaryOnly { get; set; }
        public bool hasSignature { get; set; }
        public bool updateDefaultCosts { get; set; }
        public object syncMessage { get; set; }
        public string lastSync { get; set; }
        public object keys { get; set; }
        public object values { get; set; }
        public List<Line> lines { get; set; }
    }

    public class Department
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class Item
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class Line
    {
        public int id { get; set; }
        public int lineNumber { get; set; }
        public Item item { get; set; }
        public string vendorPartNumber { get; set; }
        public object @class { get; set; }
        public object job { get; set; }
        public object workcenter { get; set; }
        public Customer customer { get; set; }
        public Tax tax { get; set; }
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
        public object serials { get; set; }
    }

    public class LinkedReceipt
    {
        public int id { get; set; }
        public string transactionType { get; set; }
        public string refNumber { get; set; }
        public int lineNumber { get; set; }
    }

    public class Location
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class PurchaseOrderModel
    {
        public int count { get; set; }
        public int totalCount { get; set; }
        public List<Datum> data { get; set; }
        public string status { get; set; }
        public string message { get; set; }
    }

    public class Shipping
    {
        public string company { get; set; }
        public string contact { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string addressName { get; set; }
        public string addressType { get; set; }
        public Address address { get; set; }
    }

    public class ShippingMethod
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class Tax
    {
        public bool taxable { get; set; }
        public object taxCode { get; set; }
    }

    public class Terms
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class Vendor
    {
        public int id { get; set; }
        public string name { get; set; }
    }




}
