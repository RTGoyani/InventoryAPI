using Newtonsoft.Json;

namespace InventoryAPI.Model
{
 

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class BillAddr
    {
        public string Id { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string CountrySubDivisionCode { get; set; }
        public string PostalCode { get; set; }
        public string Line3 { get; set; }
        public string Line4 { get; set; }
        public string Line5 { get; set; }

    }

    public class BillEmail
    {
        public string Address { get; set; }
    }

    public class BillEmailBcc
    {
        public string Address { get; set; }
    }

    public class BillEmailCc
    {
        public string Address { get; set; }
    }

    public class ClassRef1
    {
        [JsonProperty("@name")]
        public string name { get; set; }

        [JsonProperty("#text")]
        public string text { get; set; }
    }

    public class CurrencyRef1
    {
        [JsonProperty("@name")]
        public string name { get; set; }

        [JsonProperty("#text")]
        public string text { get; set; }
    }

    public class CustomerRef1
    {
        [JsonProperty("@name")]
        public string name { get; set; }

        [JsonProperty("#text")]
        public string text { get; set; }
    }

    public class CustomField
    {
        public string DefinitionId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string StringValue { get; set; }
    }

    public class DeliveryInfo
    {
        public string DeliveryType { get; set; }
        public DateTime DeliveryTime { get; set; }
        public string DeliveryErrorType { get; set; }
    }

    public class DepartmentRef
    {
        [JsonProperty("@name")]
        public string name { get; set; }

        [JsonProperty("#text")]
        public string text { get; set; }
    }

    public class GroupItemRef
    {
        [JsonProperty("@name")]
        public string name { get; set; }

        [JsonProperty("#text")]
        public string text { get; set; }
    }

    public class GroupLineDetail
    {
        public GroupItemRef GroupItemRef { get; set; }
        public string Quantity { get; set; }
        public List<Line2> Line { get; set; }
    }

    public class IntuitResponse1
    {
        [JsonProperty("@xmlns")]
        public string xmlns { get; set; }

        [JsonProperty("@time")]
        public DateTime time { get; set; }
        public QueryResponse1 QueryResponse { get; set; }
    }

    public class Invoice
    {
        [JsonProperty("@domain")]
        public string domain { get; set; }

        [JsonProperty("@sparse")]
        public string sparse { get; set; }
        public object LinkedTxn { get; set; }
        public string Id { get; set; }
        public string SyncToken { get; set; }
        public MetaData1 MetaData { get; set; }
        public List<CustomField> CustomField { get; set; }
        public string DocNumber { get; set; }
        public string TxnDate { get; set; }
        public DepartmentRef DepartmentRef { get; set; }
        public CurrencyRef1 CurrencyRef { get; set; }
        public List<Line2> Line { get; set; }
        public TxnTaxDetail TxnTaxDetail { get; set; }
        public string RecurDataRef { get; set; }
        public CustomerRef1 CustomerRef { get; set; }
        public string CustomerMemo { get; set; }
        public BillAddr BillAddr { get; set; }
        public ShipAddr ShipAddr { get; set; }
        public string FreeFormAddress { get; set; }
        public ShipFromAddr ShipFromAddr { get; set; }
        public SalesTermRef SalesTermRef { get; set; }
        public string DueDate { get; set; }
        public string TotalAmt { get; set; }
        public string ApplyTaxAfterDiscount { get; set; }
        public string PrintStatus { get; set; }
        public string EmailStatus { get; set; }
        public BillEmail BillEmail { get; set; }
        public BillEmailCc BillEmailCc { get; set; }
        public BillEmailBcc BillEmailBcc { get; set; }
        public string Balance { get; set; }
        public DeliveryInfo DeliveryInfo { get; set; }
        public object TaxExemptionRef { get; set; }
        public string AllowIPNPayment { get; set; }
        public string AllowOnlinePayment { get; set; }
        public string AllowOnlineCreditCardPayment { get; set; }
        public string AllowOnlineACHPayment { get; set; }
        public string EInvoiceStatus { get; set; }
        public string ProjectRef { get; set; }
        public string PrivateNote { get; set; }
    }

    public class ItemAccountRef
    {
        [JsonProperty("@name")]
        public string name { get; set; }

        [JsonProperty("#text")]
        public string text { get; set; }
    }

    public class ItemRef
    {
        [JsonProperty("@name")]
        public string name { get; set; }

        [JsonProperty("#text")]
        public string text { get; set; }
    }

    public class Line2
    {
        public string Id { get; set; }
        public string LineNum { get; set; }
        public string Description { get; set; }
        public string Amount { get; set; }
        public string DetailType { get; set; }
        public SalesItemLineDetail SalesItemLineDetail { get; set; }
        public object DescriptionLineDetail { get; set; }
        public object SubTotalLineDetail { get; set; }
        public GroupLineDetail GroupLineDetail { get; set; }
    }

    public class MetaData1
    {
        public DateTime CreateTime { get; set; }
        public string LastModifiedByRef { get; set; }
        public DateTime LastUpdatedTime { get; set; }
    }

    public class QueryResponse1
    {
        [JsonProperty("@startPosition")]
        public string startPosition { get; set; }

        [JsonProperty("@maxResults")]
        public string maxResults { get; set; }

        [JsonProperty("@totalCount")]
        public string totalCount { get; set; }
        public List<Invoice> Invoice { get; set; }
    }

    public class InvoiceModel
    {
        [JsonProperty("?xml")]
        public Xml1 xml { get; set; }
        public IntuitResponse1 IntuitResponse { get; set; }
    }

    public class SalesItemLineDetail
    {
        public ItemRef ItemRef { get; set; }
        public ClassRef1 ClassRef { get; set; }
        public string UnitPrice { get; set; }
        public string Qty { get; set; }
        public ItemAccountRef ItemAccountRef { get; set; }
        public string TaxCodeRef { get; set; }
        public string TaxClassificationRef { get; set; }
    }

    public class SalesTermRef
    {
        [JsonProperty("@name")]
        public string name { get; set; }

        [JsonProperty("#text")]
        public string text { get; set; }
    }

    public class ShipAddr
    {
        public string Id { get; set; }
        public string Line1 { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string CountrySubDivisionCode { get; set; }
        public string PostalCode { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }
        public string Line4 { get; set; }
    }

    public class ShipFromAddr
    {
        public string Id { get; set; }
        public string Line2 { get; set; }
    }


    public class TaxLine
    {
        public string Amount { get; set; }
        public string DetailType { get; set; }
        public TaxLineDetail TaxLineDetail { get; set; }
    }


    public class TaxLineDetail
    {
        public string TaxRateRef { get; set; }
        public string PercentBased { get; set; }
        public string TaxPercent { get; set; }
        public string NetAmountTaxable { get; set; }
    }

    public class TxnTaxDetail
    {
        public object TxnTaxCodeRef { get; set; }
        public string TotalTax { get; set; }
        public Object TaxLine { get; set; }
        public string TaxReviewStatus { get; set; }
    }

    public class Xml1
    {
        [JsonProperty("@version")]
        public string version { get; set; }

        [JsonProperty("@encoding")]
        public string encoding { get; set; }

        [JsonProperty("@standalone")]
        public string standalone { get; set; }
    }


}
