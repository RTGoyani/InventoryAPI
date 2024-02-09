namespace InventoryAPI.Model
{
    public class WorkOrderModel
    {
        public int totalSize { get; set; }
        public bool done { get; set; }
        public List<Record1> records { get; set; }
    }
    public class Attributes1
    {
        public string type { get; set; }
        public string url { get; set; }
    }

    public class Record1
    {
        public Attributes1 attributes { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Account_Credit_Status__c { get; set; }
        public string Account_Estatus__c { get; set; }
        public string Account__c { get; set; }
        public double Amount_Invoiced__c { get; set; }
        public double Balance__c { get; set; }
        public string Case__c { get; set; }
        public string Company__c { get; set; }
        public string Contact__c { get; set; }
        public string Contrato_Mant__c { get; set; }
        public bool Control__c { get; set; }
        public string CreatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedDate_Formula__c { get; set; }
        public DateTime Created_Date__c { get; set; }
        public bool Create_Refer_Opportunity__c { get; set; }
        public double Credit__c { get; set; }
        public string Cubierto_Por__c { get; set; }
        public string Customer_Notes__c { get; set; }
     
        public bool Initial_Order__c { get; set; }
        public bool Inspection_Completed__c { get; set; }
        public object Internal_Comments__c { get; set; }
        public string Invoice_Status__c { get; set; }
        public bool IsDeleted { get; set; }
        public string Job_Date__c { get; set; }
        public string job_Description__c { get; set; }
        public double Labor_Costs__c { get; set; }
        public object LastActivityDate { get; set; }
        public string LastModifiedById { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public object Longitude__c { get; set; }
       
        public double Net_Amount_Invoiced_Excluding_Tax__c { get; set; }
        public bool Next_Maintenance_Created__c { get; set; }
        public double Other_Costs__c { get; set; }
        public string OwnerId { get; set; }
        public string Payment_Terms__c { get; set; }
        public string Payment_Term_Details__c { get; set; }
        public double Pending_Materials__c { get; set; }
        public double? percent_Invoiced__c { get; set; }
        public string PO_Number__c { get; set; }
        public double Profitability_percent__c { get; set; }
        public string Sales_Person__c { get; set; }
        public string Signatory_Email__c { get; set; }
        public object Signatory_Name__c { get; set; }
        public object SignatureId__c { get; set; }
        public object Signature_Date__c { get; set; }
        public string Signature_Image__c { get; set; }
        public object Source__c { get; set; }
        public string Status__c { get; set; }
        public string Subtype__c { get; set; }
        public DateTime SystemModstamp { get; set; }
        public double Tax_Amount_After_Credit__c { get; set; }
        public double Tax_Amount_Billed__c { get; set; }
        public double Tax_Credit__c { get; set; }
        public string Tenico_Referido__c { get; set; }
        public double Total_Amount_Invoiced_To_Customer__c { get; set; }
        public double Total_Invoiced_to_Customer__c { get; set; }
        public double? Total_Job_Amount__c { get; set; }
        public double Total_material_Costs__c { get; set; }
        public double Total_Price_Materials_Calc__c { get; set; }
        public double Total_WO_Material_Costs__c { get; set; }
        public string Type__c { get; set; }
        public string T_cnico_Asociado__c { get; set; }
        public double User_for_Reports__c { get; set; }
        public double WorkOrder_Net_Income__c { get; set; }
        public string Work_Performed__c { get; set; }
    }
}
