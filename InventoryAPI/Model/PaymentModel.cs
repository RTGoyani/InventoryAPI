using Newtonsoft.Json;

namespace InventoryAPI.Model
{

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class CurrencyRef
    {
        [JsonProperty("@name")]
        public string name { get; set; }

        [JsonProperty("#text")]
        public string text { get; set; }
    }

    public class CustomerRef
    {
        [JsonProperty("@name")]
        public string name { get; set; }

        [JsonProperty("#text")]
        public string text { get; set; }
    }

    public class IntuitResponse
    {
        [JsonProperty("@xmlns")]
        public string xmlns { get; set; }

        [JsonProperty("@time")]
        public DateTime time { get; set; }
        public QueryResponse QueryResponse { get; set; }
    }

    public class LinkedTxn
    {
        public string TxnId { get; set; }
        public string TxnType { get; set; }
    }

    public class MetaData
    {
        public DateTime CreateTime { get; set; }
        public DateTime LastUpdatedTime { get; set; }
    }

    public class Payment
    {
        [JsonProperty("@domain")]
        public string domain { get; set; }

        [JsonProperty("@sparse")]
        public string sparse { get; set; }
        public string Id { get; set; }
        public string SyncToken { get; set; }
        public MetaData MetaData { get; set; }
        public string TxnDate { get; set; }
        public CurrencyRef CurrencyRef { get; set; }
        public object Line { get; set; }
        public CustomerRef CustomerRef { get; set; }
        public string DepositToAccountRef { get; set; }
        public string TotalAmt { get; set; }
        public string UnappliedAmt { get; set; }
        public string ProcessPayment { get; set; }
        public LinkedTxn LinkedTxn { get; set; }
        public string PaymentMethodRef { get; set; }
        public string PaymentRefNum { get; set; }
        public string PrivateNote { get; set; }
    }

    public class QueryResponse
    {
        [JsonProperty("@startPosition")]
        public string startPosition { get; set; }

        [JsonProperty("@maxResults")]
        public string maxResults { get; set; }
        public List<Payment> Payment { get; set; }
    }

    public class PaymentModel
    {
        [JsonProperty("?xml")]
        public Xml xml { get; set; }
        public IntuitResponse IntuitResponse { get; set; }
    }

    public class Xml
    {
        [JsonProperty("@version")]
        public string version { get; set; }

        [JsonProperty("@encoding")]
        public string encoding { get; set; }

        [JsonProperty("@standalone")]
        public string standalone { get; set; }
    }




}
