using Intuit.Ipp.Data;
using InventoryAPI.Model;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SaleforceMetadata;
using System.Data;
using System.Net.Http.Headers;
using System.Reflection;
using System.Web;
using static AutoMapper.Internal.ExpressionFactory;

namespace InventoryAPI.Repository
{
    public class CommonRepository
    {
        //string connectionString = "Server=DESKTOP-MG67Q84;Database=Inventory;User=sa;Password=sys@123;TrustServerCertificate=True";
        public int InsertErrorLog(string connectionString, string MethodName, string Error_code, string Error_message)
        {
            int ID = 0;
            try
            {
                using (SqlConnection connection1 = new SqlConnection(connectionString))
                {
                    connection1.Open();
                    using (SqlCommand command1 = new SqlCommand("USPInsertErrorLog", connection1))
                    {
                        command1.CommandType = CommandType.StoredProcedure;
                        command1.Parameters.AddWithValue("@MethodName", MethodName);
                        command1.Parameters.AddWithValue("@Error_code", Error_code);
                        command1.Parameters.AddWithValue("@Error_message", Error_message);
                        try
                        {
                            ID = command1.ExecuteNonQuery();
                        }
                        catch (SqlException ex)
                        {
                            Console.WriteLine("Error: " + ex.Message);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return ID;
        }
        public void GetTimesheetAPI(string connectionString, salesforceTokenModel objsalesforce)
        {
            try
            {
                if (!string.IsNullOrEmpty(objsalesforce.access_token))
                {
                    DateTimeOffset currentDateTime = DateTimeOffset.UtcNow.AddDays(-3);
                    string formattedDateTime = currentDateTime.ToString("yyyy-MM-ddTHH:mm:sszzz");
                    string deString = HttpUtility.UrlEncode(formattedDateTime);
                    string restQuery = $"https://automaticequipment.my.salesforce.com//services/data/v54.0/query?q=SELECT+Assigned_Personnel__c,CreatedById,CreatedDate,Elepsed_Time__c,End_Work__c,Id,IsDeleted,LastModifiedById,LastModifiedDate,LatitudeCpt__c,LatitudeTrv__c,LatitudeWrk__c,LongitudeCpt__c,LongitudeTrv__c,LongitudeWrk__c,MALatitude__c,MALongitude__c,MAQuality__c,MASimilarity__c,MASkipGeocoding__c,MAVerifiedLatitude__c,MAVerifiedLongitude__c,Name,Start_Travel__c,Start_Work__c,StatusToday__c,Status__c,SystemModstamp,Travel_Time__c,Visit__c,Work_Order__c,Work_Time__c+FROM+TimesheetWO__c+where+CreatedDate%3E%3D" + deString + " ORDER BY CreatedDate DESC NULLS FIRST";
                    var client1 = new HttpClient();
                    HttpRequestMessage request1 = new HttpRequestMessage(HttpMethod.Get, restQuery);
                    request1.Headers.Add("Authorization", "Bearer " + objsalesforce.access_token);
                    request1.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response1 = client1.SendAsync(request1).Result;
                    var data = response1.Content.ReadAsStringAsync().Result;
                    TimesheetModel objtimesheet = JsonConvert.DeserializeObject<TimesheetModel>(data);
                    if (objtimesheet != null)
                    {
                        for (int i = 0; i < objtimesheet.records.Count; i++)
                        {
                            using (SqlConnection connection1 = new SqlConnection(connectionString))
                            {
                                connection1.Open();
                                using (SqlCommand command1 = new SqlCommand("USPInsertTimesheetMasterData", connection1))
                                {
                                    command1.CommandType = CommandType.StoredProcedure;
                                    command1.Parameters.AddWithValue("@Id", objtimesheet.records[i].Id);
                                    command1.Parameters.AddWithValue("@Name", objtimesheet.records[i].Name);
                                    command1.Parameters.AddWithValue("@StartTravelc", objtimesheet.records[i].Start_Travel__c);
                                    command1.Parameters.AddWithValue("@StartWorkc", objtimesheet.records[i].Start_Work__c);
                                    command1.Parameters.AddWithValue("@EndWorkc", objtimesheet.records[i].End_Work__c);
                                    command1.Parameters.AddWithValue("@TravelTimec", objtimesheet.records[i].Travel_Time__c);
                                    command1.Parameters.AddWithValue("@ElepsedTimec", objtimesheet.records[i].Elepsed_Time__c);
                                    command1.Parameters.AddWithValue("@WorkTimec", objtimesheet.records[i].Work_Time__c);
                                    command1.Parameters.AddWithValue("@Statusc", objtimesheet.records[i].Status__c);
                                    command1.Parameters.AddWithValue("@AssignedPersonnelc", objtimesheet.records[i].Assigned_Personnel__c);
                                    command1.Parameters.AddWithValue("@CreatedDate", objtimesheet.records[i].CreatedDate);
                                    command1.Parameters.AddWithValue("@CreatedById", objtimesheet.records[i].CreatedById);
                                    command1.Parameters.AddWithValue("@LastModifiedDate", objtimesheet.records[i].LastModifiedDate);
                                    command1.Parameters.AddWithValue("@LastModifiedById", objtimesheet.records[i].LastModifiedById);
                                    command1.Parameters.AddWithValue("@IsDeleted", objtimesheet.records[i].IsDeleted);
                                    command1.Parameters.AddWithValue("@Pagec", objtimesheet.records[i].Page__c);
                                    command1.Parameters.AddWithValue("@StatusTodayc", objtimesheet.records[i].StatusToday__c);
                                    try
                                    {
                                        var id = command1.ExecuteNonQuery();
                                    }
                                    catch (SqlException ex)
                                    {
                                        InsertErrorLog(connectionString, "USPInsertTimesheetMasterData", "", ex.Message);
                                        Console.WriteLine("Error: " + ex.Message);
                                    }
                                }
                            }

                        }
                    }
                }
                else
                {
                    Console.WriteLine("Failed to obtain access token.");
                    InsertErrorLog(connectionString, "GetTimesheetDataAPI", "", "Failed to obtain access token is null");
                }
            }
            catch (Exception)
            {

            }
        }
        public void GetWorkOrderAPI(string connectionString, salesforceTokenModel objsalesforce)
        {
            try
            {
                if (!string.IsNullOrEmpty(objsalesforce.access_token))
                {
                    DateTimeOffset currentDateTime = DateTimeOffset.UtcNow.AddDays(-3);
                    string formattedDateTime = currentDateTime.ToString("yyyy-MM-ddTHH:mm:sszzz");
                    string deString = HttpUtility.UrlEncode(formattedDateTime);
                    string restQuery = $"https://automaticequipment.my.salesforce.com//services/data/v54.0/query?q=SELECT+Account_Credit_Status__c,Account_Estatus__c,Account__c,Amount_Invoiced__c,Balance__c,Case__c,Company__c,Contact__c,Contrato_Mant__c,Control__c,CreatedById,CreatedDate,CreatedDate_Formula__c,Created_Date__c,Create_Refer_Opportunity__c,Credit__c,Cubierto_Por__c,Customer_Notes__c,Id,Initial_Order__c,Inspection_Completed__c,Internal_Comments__c,Invoice_Status__c,IsDeleted,Job_Date__c,job_Description__c,Labor_Costs__c,LastActivityDate,LastModifiedById,LastModifiedDate,Longitude__c,Name,Net_Amount_Invoiced_Excluding_Tax__c,Next_Maintenance_Created__c,Other_Costs__c,OwnerId,Payment_Terms__c,Payment_Term_Details__c,Pending_Materials__c,percent_Invoiced__c,PO_Number__c,Profitability_percent__c,Sales_Person__c,Signatory_Email__c,Signatory_Name__c,SignatureId__c,Signature_Date__c,Signature_Image__c,Source__c,Status__c,Subtype__c,SystemModstamp,Tax_Amount_After_Credit__c,Tax_Amount_Billed__c,Tax_Credit__c,Tenico_Referido__c,Total_Amount_Invoiced_To_Customer__c,Total_Invoiced_to_Customer__c,Total_Job_Amount__c,Total_material_Costs__c,Total_Price_Materials_Calc__c,Total_WO_Material_Costs__c,Type__c,T_cnico_Asociado__c,User_for_Reports__c,WorkOrder_Net_Income__c,Work_Performed__c+FROM+WorkOrder__c+where+CreatedDate%3E%3D" + deString + "ORDER BY CreatedDate DESC NULLS FIRST";
                    var client1 = new HttpClient();
                    HttpRequestMessage request1 = new HttpRequestMessage(HttpMethod.Get, restQuery);
                    request1.Headers.Add("Authorization", "Bearer " + objsalesforce.access_token);
                    request1.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response1 = client1.SendAsync(request1).Result;
                    var data = response1.Content.ReadAsStringAsync().Result;
                    WorkOrderModel objWorkOrder = JsonConvert.DeserializeObject<WorkOrderModel>(data);
                    if (objWorkOrder != null)
                    {
                        for (int i = 0; i < objWorkOrder.records.Count; i++)
                        {
                            using (SqlConnection connection1 = new SqlConnection(connectionString))
                            {
                                connection1.Open();
                                using (SqlCommand command1 = new SqlCommand("USPInsertWorkOrderMasterData", connection1))
                                {
                                    command1.CommandType = CommandType.StoredProcedure;
                                    command1.Parameters.AddWithValue("@Id", objWorkOrder.records[i].Id);
                                    command1.Parameters.AddWithValue("@Name", objWorkOrder.records[i].Name);
                                    command1.Parameters.AddWithValue("@Account_Credit_Status__c", objWorkOrder.records[i].Account_Credit_Status__c);
                                    command1.Parameters.AddWithValue("@Account_Estatus__c", objWorkOrder.records[i].Account_Estatus__c);
                                    command1.Parameters.AddWithValue("@Account__c", objWorkOrder.records[i].Account__c);
                                    command1.Parameters.AddWithValue("@Amount_Invoiced__c", objWorkOrder.records[i].Amount_Invoiced__c);
                                    command1.Parameters.AddWithValue("@Balance__c", objWorkOrder.records[i].Balance__c);
                                    command1.Parameters.AddWithValue("@Case__c", objWorkOrder.records[i].Case__c == null ? "" : objWorkOrder.records[i].Case__c);
                                    command1.Parameters.AddWithValue("@Company__c", objWorkOrder.records[i].Company__c == null ? "" : objWorkOrder.records[i].Company__c);
                                    command1.Parameters.AddWithValue("@Contact__c", objWorkOrder.records[i].Contact__c == null ? "" : objWorkOrder.records[i].Contact__c);
                                    command1.Parameters.AddWithValue("@Contrato_Mant__c", objWorkOrder.records[i].Contrato_Mant__c == null ? "" : objWorkOrder.records[i].Contrato_Mant__c);
                                    command1.Parameters.AddWithValue("@Control__c", objWorkOrder.records[i].Control__c);
                                    command1.Parameters.AddWithValue("@CreatedById", objWorkOrder.records[i].CreatedById);
                                    command1.Parameters.AddWithValue("@CreatedDate", objWorkOrder.records[i].CreatedDate);
                                    command1.Parameters.AddWithValue("@CreatedDate_Formula__c", objWorkOrder.records[i].CreatedDate_Formula__c);
                                    command1.Parameters.AddWithValue("@Created_Date__c", objWorkOrder.records[i].Created_Date__c);
                                    command1.Parameters.AddWithValue("@Create_Refer_Opportunity__c", objWorkOrder.records[i].Create_Refer_Opportunity__c);
                                    command1.Parameters.AddWithValue("@Credit__c", objWorkOrder.records[i].Credit__c);
                                    command1.Parameters.AddWithValue("@Cubierto_Por__c", objWorkOrder.records[i].Cubierto_Por__c);
                                    command1.Parameters.AddWithValue("@Customer_Notes__c", objWorkOrder.records[i].Customer_Notes__c == null ? "" : objWorkOrder.records[i].Customer_Notes__c);
                                    command1.Parameters.AddWithValue("@Initial_Order__c", objWorkOrder.records[i].Initial_Order__c);
                                    command1.Parameters.AddWithValue("@Inspection_Completed__c", objWorkOrder.records[i].Inspection_Completed__c);
                                    command1.Parameters.AddWithValue("@InternalCommentsc", objWorkOrder.records[i].Internal_Comments__c == null ? "" : objWorkOrder.records[i].Internal_Comments__c);
                                    command1.Parameters.AddWithValue("@Invoice_Status__c", objWorkOrder.records[i].Invoice_Status__c == null ? "" : objWorkOrder.records[i].Invoice_Status__c);
                                    command1.Parameters.AddWithValue("@IsDeleted", objWorkOrder.records[i].IsDeleted);
                                    command1.Parameters.AddWithValue("@Job_Date__c", objWorkOrder.records[i].Job_Date__c);
                                    command1.Parameters.AddWithValue("@job_Description__c", objWorkOrder.records[i].job_Description__c);
                                    command1.Parameters.AddWithValue("@Labor_Costs__c", objWorkOrder.records[i].Labor_Costs__c);
                                    command1.Parameters.AddWithValue("@LastActivityDate", objWorkOrder.records[i].LastActivityDate == null ? "" : objWorkOrder.records[i].LastActivityDate);
                                    command1.Parameters.AddWithValue("@LastModifiedById", objWorkOrder.records[i].LastModifiedById == null ? "" : objWorkOrder.records[i].LastModifiedById);
                                    command1.Parameters.AddWithValue("@LastModifiedDate", objWorkOrder.records[i].LastModifiedDate);
                                    command1.Parameters.AddWithValue("@Longitude__c", objWorkOrder.records[i].Longitude__c == null ? "" : objWorkOrder.records[i].Longitude__c);
                                    command1.Parameters.AddWithValue("@Net_Amount_Invoiced_Excluding_Tax__c", objWorkOrder.records[i].Net_Amount_Invoiced_Excluding_Tax__c);
                                    command1.Parameters.AddWithValue("@Next_Maintenance_Created__c", objWorkOrder.records[i].Next_Maintenance_Created__c);
                                    command1.Parameters.AddWithValue("@Other_Costs__c", objWorkOrder.records[i].Other_Costs__c);
                                    command1.Parameters.AddWithValue("@OwnerId", objWorkOrder.records[i].OwnerId);
                                    command1.Parameters.AddWithValue("@Payment_Terms__c", objWorkOrder.records[i].Payment_Terms__c == null ? "" : objWorkOrder.records[i].Payment_Terms__c);
                                    command1.Parameters.AddWithValue("@Payment_Term_Details__c", objWorkOrder.records[i].Payment_Term_Details__c == null ? "" : objWorkOrder.records[i].Payment_Term_Details__c);
                                    command1.Parameters.AddWithValue("@Pending_Materials__c", objWorkOrder.records[i].Pending_Materials__c);
                                    command1.Parameters.AddWithValue("@percent_Invoiced__c", objWorkOrder.records[i].percent_Invoiced__c == null ? "" : objWorkOrder.records[i].percent_Invoiced__c);
                                    command1.Parameters.AddWithValue("@PO_Number__c", objWorkOrder.records[i].PO_Number__c == null ? "" : objWorkOrder.records[i].PO_Number__c);
                                    command1.Parameters.AddWithValue("@Profitability_percent__c", objWorkOrder.records[i].Profitability_percent__c);
                                    command1.Parameters.AddWithValue("@Sales_Person__c", objWorkOrder.records[i].Sales_Person__c == null ? "" : objWorkOrder.records[i].Sales_Person__c);
                                    command1.Parameters.AddWithValue("@Signatory_Email__c", objWorkOrder.records[i].Signatory_Email__c == null ? "" : objWorkOrder.records[i].Signatory_Email__c);
                                    command1.Parameters.AddWithValue("@Signatory_Name__c", objWorkOrder.records[i].Signatory_Name__c == null ? "" : objWorkOrder.records[i].Signatory_Name__c);
                                    command1.Parameters.AddWithValue("@SignatureId__c", objWorkOrder.records[i].SignatureId__c == null ? "" : objWorkOrder.records[i].SignatureId__c);
                                    command1.Parameters.AddWithValue("@Signature_Date__c", objWorkOrder.records[i].Signature_Date__c == null ? "" : objWorkOrder.records[i].Signature_Date__c);
                                    command1.Parameters.AddWithValue("@Signature_Image__c", objWorkOrder.records[i].Signature_Image__c);
                                    command1.Parameters.AddWithValue("@Source__c", objWorkOrder.records[i].Source__c == null ? "" : objWorkOrder.records[i].Source__c);
                                    command1.Parameters.AddWithValue("@Status__c", objWorkOrder.records[i].Status__c);
                                    command1.Parameters.AddWithValue("@Subtype__c", objWorkOrder.records[i].Subtype__c);
                                    command1.Parameters.AddWithValue("@SystemModstamp", objWorkOrder.records[i].SystemModstamp);
                                    command1.Parameters.AddWithValue("@Tax_Amount_After_Credit__c", objWorkOrder.records[i].Tax_Amount_After_Credit__c);
                                    command1.Parameters.AddWithValue("@Tax_Amount_Billed__c", objWorkOrder.records[i].Tax_Amount_Billed__c);
                                    command1.Parameters.AddWithValue("@Tax_Credit__c", objWorkOrder.records[i].Tax_Credit__c);
                                    command1.Parameters.AddWithValue("@Tenico_Referido__c", objWorkOrder.records[i].Tenico_Referido__c == null ? "" : objWorkOrder.records[i].Tenico_Referido__c);
                                    command1.Parameters.AddWithValue("@Total_Amount_Invoiced_To_Customer__c", objWorkOrder.records[i].Total_Amount_Invoiced_To_Customer__c);
                                    command1.Parameters.AddWithValue("@Total_Invoiced_to_Customer__c", objWorkOrder.records[i].Total_Invoiced_to_Customer__c);
                                    command1.Parameters.AddWithValue("@Total_Job_Amount__c", objWorkOrder.records[i].Total_Job_Amount__c == null ? "" : objWorkOrder.records[i].Total_Job_Amount__c);
                                    command1.Parameters.AddWithValue("@Total_material_Costs__c", objWorkOrder.records[i].Total_material_Costs__c);
                                    command1.Parameters.AddWithValue("@Total_Price_Materials_Calc__c", objWorkOrder.records[i].Total_Price_Materials_Calc__c);
                                    command1.Parameters.AddWithValue("@Total_WO_Material_Costs__c", objWorkOrder.records[i].Total_WO_Material_Costs__c);
                                    command1.Parameters.AddWithValue("@Type__c", objWorkOrder.records[i].Type__c == null ? "" : objWorkOrder.records[i].Type__c);
                                    command1.Parameters.AddWithValue("@T_cnico_Asociado__c", objWorkOrder.records[i].T_cnico_Asociado__c);
                                    command1.Parameters.AddWithValue("@User_for_Reports__c", objWorkOrder.records[i].User_for_Reports__c);
                                    command1.Parameters.AddWithValue("@WorkOrder_Net_Income__c", objWorkOrder.records[i].WorkOrder_Net_Income__c);
                                    command1.Parameters.AddWithValue("@Work_Performed__c", objWorkOrder.records[i].Work_Performed__c == null ? "" : objWorkOrder.records[i].Work_Performed__c);
                                    try
                                    {
                                        var id = command1.ExecuteNonQuery();
                                    }
                                    catch (SqlException ex)
                                    {
                                        InsertErrorLog(connectionString, "USPInsertWorkOrderMasterData", "", ex.Message);
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Failed to obtain access token.");
                    InsertErrorLog(connectionString, "GetWorkOrderData", "", "Failed to obtain access token is null");
                }
            }
            catch (Exception)
            {

            }
        }
        public void EmployeeWoCData(string connectionString, salesforceTokenModel objsalesforce)
        {
            try
            {
                if (!string.IsNullOrEmpty(objsalesforce.access_token))
                {
                    DateTimeOffset currentDateTime = DateTimeOffset.UtcNow.AddDays(-30);
                    string formattedDateTime = currentDateTime.ToString("yyyy-MM-ddTHH:mm:sszzz");
                    string deString = HttpUtility.UrlEncode(formattedDateTime);
                    string restQuery = $"https://automaticequipment.my.salesforce.com//services/data/v54.0/query?q=SELECT+CreatedById,CreatedDate,Hourly_Rate__c,Id,IsDeleted,LastModifiedById,LastModifiedDate,LastReferencedDate,LastViewedDate,Name,OwnerId,SystemModstamp,User__c+FROM+EmployeeWO__c+where+CreatedDate%3E%3D" + deString + "ORDER BY CreatedDate DESC NULLS FIRST";
                    var client1 = new HttpClient();
                    HttpRequestMessage request1 = new HttpRequestMessage(HttpMethod.Get, restQuery);
                    request1.Headers.Add("Authorization", "Bearer " + objsalesforce.access_token);
                    request1.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response1 = client1.SendAsync(request1).Result;
                    var data = response1.Content.ReadAsStringAsync().Result;
                    EmployeeWOCModel objEmployeeWO = JsonConvert.DeserializeObject<EmployeeWOCModel>(data);
                    if (objEmployeeWO != null)
                    {
                        for (int i = 0; i < objEmployeeWO.records.Count; i++)
                        {
                            using (SqlConnection connection1 = new SqlConnection(connectionString))
                            {
                                connection1.Open();
                                using (SqlCommand command1 = new SqlCommand("USP_EmployeeWOCDataSave", connection1))
                                {
                                    command1.CommandType = CommandType.StoredProcedure;
                                    command1.Parameters.AddWithValue("@Id", objEmployeeWO.records[i].Id);
                                    command1.Parameters.AddWithValue("@Name", objEmployeeWO.records[i].Name);
                                    command1.Parameters.AddWithValue("@Hourly_Rate__c", objEmployeeWO.records[i].Hourly_Rate__c);
                                    command1.Parameters.AddWithValue("@OwnerId", objEmployeeWO.records[i].OwnerId);
                                    command1.Parameters.AddWithValue("@User__c", objEmployeeWO.records[i].User__c);
                                    command1.Parameters.AddWithValue("@CreatedById", objEmployeeWO.records[i].CreatedById);
                                    command1.Parameters.AddWithValue("@CreatedDate", objEmployeeWO.records[i].CreatedDate);
                                    command1.Parameters.AddWithValue("@LastModifiedById", objEmployeeWO.records[i].LastModifiedById == null ? "" : objEmployeeWO.records[i].LastModifiedById);
                                    command1.Parameters.AddWithValue("@LastModifiedDate", objEmployeeWO.records[i].LastModifiedDate == null ? "" : objEmployeeWO.records[i].LastModifiedDate);
                                    command1.Parameters.AddWithValue("@LastReferencedDate", objEmployeeWO.records[i].LastReferencedDate == null ? "" : objEmployeeWO.records[i].LastReferencedDate);
                                    command1.Parameters.AddWithValue("@LastViewedDate", objEmployeeWO.records[i].LastViewedDate == null ? "" : objEmployeeWO.records[i].LastViewedDate);
                                    command1.Parameters.AddWithValue("@IsDeleted", objEmployeeWO.records[i].IsDeleted == null ? "" : objEmployeeWO.records[i].IsDeleted);
                                    command1.Parameters.AddWithValue("@SystemModstamp", objEmployeeWO.records[i].SystemModstamp == null ? "" : objEmployeeWO.records[i].SystemModstamp);
                                    try
                                    {
                                        var id = command1.ExecuteNonQuery();
                                    }
                                    catch (SqlException ex)
                                    {
                                        InsertErrorLog(connectionString, "USP_EmployeeWOCDataSave", "", ex.Message);
                                        Console.WriteLine("Error: " + ex.Message);
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Failed to obtain access token.");
                    InsertErrorLog(connectionString, "GetEmployeeWoCData", "", "Failed to obtain access token is null");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<string> InsertPaymentData(string connectionString, PaymentModel obj, string CompanyName)
        {
            if (obj != null)
            {
                var paymentData = obj.IntuitResponse.QueryResponse.Payment;
                for (int i = 0; i < paymentData.Count; i++)
                {
                    using (SqlConnection connection1 = new SqlConnection(connectionString))
                    {
                        connection1.Open();
                        using (SqlCommand command1 = new SqlCommand("USPInsertPymentMasterData", connection1))
                        {
                            command1.CommandType = CommandType.StoredProcedure;
                            command1.Parameters.AddWithValue("@Id", paymentData[i].Id);
                            command1.Parameters.AddWithValue("@CustomerRef", paymentData[i].CustomerRef.text);
                            command1.Parameters.AddWithValue("@DepositToAccountRef", paymentData[i].CurrencyRef.name);
                            command1.Parameters.AddWithValue("@TotalAmt", paymentData[i].TotalAmt);
                            command1.Parameters.AddWithValue("@UnappliedAmt", paymentData[i].UnappliedAmt);
                            command1.Parameters.AddWithValue("@ProcessPayment", paymentData[i].ProcessPayment);
                            command1.Parameters.AddWithValue("@TxnDate", paymentData[i].TxnDate);
                            command1.Parameters.AddWithValue("@CurrencyRef", paymentData[i].CurrencyRef.text);
                            command1.Parameters.AddWithValue("@Company", CompanyName);
                            try
                            {
                                var id = command1.ExecuteNonQuery();
                            }
                            catch (SqlException ex)
                            {
                                InsertErrorLog(connectionString, "USPInsertPymentMasterData", "", ex.Message);
                                Console.WriteLine("Error: " + ex.Message);
                            }
                        }
                    }
                }
            }
            return "successfully";
        }
        public async Task<string> InsertPurchaseorder(string connectionString, PurchaseOrderModel objPO, string CompanyName)
        {
            try
            {
                if (objPO != null)
                {
                    for (int i = 0; i < objPO.data.Count; i++)
                    {
                        string SOSPurchaseOrderID = objPO.data[i].id.ToString();
                        string SOSPurchaseOrderNumber = objPO.data[i].number.ToString();

                        string POTotalAmount = objPO.data[i].total.ToString();
                        DateTime Date = objPO.data[i].date;

                        int PurchaseOrderID = 0;

                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();
                            using (SqlCommand command = new SqlCommand("USP_PurchaseOrderDataSave", connection))
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.AddWithValue("@SOSPurchaseOrderID", SOSPurchaseOrderID);
                                command.Parameters.AddWithValue("@SOSPurchaseOrderNumber", SOSPurchaseOrderNumber);
                                command.Parameters.AddWithValue("@POTotalAmount", POTotalAmount);
                                command.Parameters.AddWithValue("@Date", Date);
                                command.Parameters.AddWithValue("@Company", CompanyName);
                                try
                                {
                                    PurchaseOrderID = Convert.ToInt32(command.ExecuteScalar());
                                    Console.WriteLine("Data inserted successfully.");
                                }
                                catch (SqlException ex)
                                {
                                    InsertErrorLog(connectionString, "USP_PurchaseOrderDataSave", "", ex.Message);
                                    Console.WriteLine("Error: " + ex.Message);
                                }
                            }
                        }

                        for (int j = 0; j < objPO.data[i].lines.Count; j++)
                        {
                            string SOSItemDescription = objPO.data[i].lines[j].description;
                            if (SOSItemDescription == null)
                            {
                                SOSItemDescription = "";
                            }

                            InventoryAPI.Model.Customer objc = objPO.data[i].lines[j].customer;
                            string SOSWorkOrderNumber = "";
                            if (objc == null)
                            {
                                SOSWorkOrderNumber = "";
                            }
                            else
                            {
                                SOSWorkOrderNumber = objc.name;
                            }

                            double SOSItemQuantity = objPO.data[i].lines[j].quantity;
                            double SOSItemUnitPrice = objPO.data[i].lines[j].unitprice;

                            if (PurchaseOrderID > 0)
                            {
                                using (SqlConnection connection1 = new SqlConnection(connectionString))
                                {
                                    connection1.Open();
                                    using (SqlCommand command1 = new SqlCommand("USP_TblPurchaseOrderDetailDataSave", connection1))
                                    {
                                        command1.CommandType = CommandType.StoredProcedure;
                                        command1.Parameters.AddWithValue("@PurchaseOrderID", PurchaseOrderID);
                                        command1.Parameters.AddWithValue("@SOSWorkOrderNumber", SOSWorkOrderNumber);
                                        command1.Parameters.AddWithValue("@SOSItemDescription", SOSItemDescription);
                                        command1.Parameters.AddWithValue("@SOSItemQuantity", SOSItemQuantity);
                                        command1.Parameters.AddWithValue("@SOSItemUnitPrice", SOSItemUnitPrice);
                                        try
                                        {
                                            var id = command1.ExecuteNonQuery();
                                        }
                                        catch (SqlException ex)
                                        {
                                            InsertErrorLog(connectionString, "USP_TblPurchaseOrderDetailDataSave", "", ex.Message);
                                            Console.WriteLine("Error: " + ex.Message);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string code = ((HttpRequestException)ex).StatusCode.ToString();
                InsertErrorLog(connectionString, "GetPurchaseOrder", code, ex.Message);
            }

            return "successfully";
        }
        public async Task<string> InsertItemReceipt(string connectionString, ItemReceiptModel1 obj, string CompanyName)
        {
            try
            {
                if (obj != null)
                {
                    for (int i = 0; i < obj.data.Count; i++)
                    {
                        try
                        {
                            int Id = obj.data[i].id;
                            DateTime date = obj.data[i].date;
                            string number = obj.data[i].number;

                            int currencyid = 0;
                            if (obj.data[i].currency != null)
                            {
                                currencyid = obj.data[i].currency.id;
                            }

                            int VenderId = 0;
                            if (obj.data[i].vendor != null)
                            {
                                currencyid = obj.data[i].vendor.id;
                            }

                            string VenderName = "";
                            if (obj.data[i].vendor != null)
                            {
                                VenderName = obj.data[i].vendor.name;
                            }

                            string currencyName = "";
                            if (obj.data[i].currency != null)
                            {
                                currencyName = obj.data[i].currency.name;
                            }

                            string comment = obj.data[i].comment;
                            string VendorMessage = obj.data[i].vendorMessage;
                            string VendorNotes = obj.data[i].vendorNotes;
                            double SubTotal = obj.data[i].subTotal;
                            double TaxAmount = obj.data[i].taxAmount;
                            double Total = obj.data[i].total;

                            int ItemReceiptID = 0;
                            using (SqlConnection connection = new SqlConnection(connectionString))
                            {
                                connection.Open();
                                using (SqlCommand command = new SqlCommand("USP_ItemReceiptDataSave", connection))
                                {
                                    command.CommandType = CommandType.StoredProcedure;
                                    command.Parameters.AddWithValue("@Id", Id);
                                    command.Parameters.AddWithValue("@Date", date);
                                    command.Parameters.AddWithValue("@Number", number);
                                    command.Parameters.AddWithValue("@CurrencyID", currencyid);
                                    command.Parameters.AddWithValue("@CurrencyName", currencyName);
                                    command.Parameters.AddWithValue("@comment", comment == null ? "" : comment);
                                    command.Parameters.AddWithValue("@VenderId", VenderId);
                                    command.Parameters.AddWithValue("@VenderName", VenderName == null ? "" : VenderName);
                                    command.Parameters.AddWithValue("@VendorMessage", VendorMessage == null ? "" : VendorMessage);
                                    command.Parameters.AddWithValue("@VendorNotes", VendorNotes == null ? "" : VendorNotes);
                                    command.Parameters.AddWithValue("@SubTotal", SubTotal);
                                    command.Parameters.AddWithValue("@TaxAmount", TaxAmount);
                                    command.Parameters.AddWithValue("@Total", Total);
                                    command.Parameters.AddWithValue("@Company", CompanyName);
                                    try
                                    {
                                        ItemReceiptID = Convert.ToInt32(command.ExecuteScalar());
                                    }
                                    catch (SqlException ex)
                                    {
                                        InsertErrorLog(connectionString, "USP_ItemReceiptDataSave", "", ex.Message);
                                        Console.WriteLine("Error: " + ex.Message);
                                    }
                                }
                            }

                            for (int j = 0; j < obj.data[i].lines.Count; j++)
                            {
                                int linesid = obj.data[i].lines[j].id;
                                int lineNumber = obj.data[i].lines[j].lineNumber;
                                string linedescription = obj.data[i].lines[j].description == null ? "" : obj.data[i].lines[j].description;
                                double lineweight = obj.data[i].lines[j].weight;
                                string linevolume = obj.data[i].lines[j].volume.ToString() == null ? "" : obj.data[i].lines[j].volume.ToString();
                                double linequantity = obj.data[i].lines[j].quantity;
                                double lineunitprice = obj.data[i].lines[j].unitprice;
                                double lineamount = obj.data[i].lines[j].amount;
                                string customer = obj.data[i].lines[j].customer == null ? "" : obj.data[i].lines[j].customer.ToString();

                                if (ItemReceiptID > 0)
                                {
                                    using (SqlConnection connection1 = new SqlConnection(connectionString))
                                    {
                                        connection1.Open();
                                        using (SqlCommand command1 = new SqlCommand("USP_ItemReceiptDetailDataSave", connection1))
                                        {
                                            command1.CommandType = CommandType.StoredProcedure;
                                            command1.Parameters.AddWithValue("@ItemReceiptID", ItemReceiptID);
                                            command1.Parameters.AddWithValue("@Id", linesid);
                                            command1.Parameters.AddWithValue("@LineNumber", lineNumber);
                                            command1.Parameters.AddWithValue("@description", linedescription == null ? "" : linedescription);
                                            command1.Parameters.AddWithValue("@weight", lineweight);
                                            command1.Parameters.AddWithValue("@volume", linevolume);
                                            command1.Parameters.AddWithValue("@quantity", linequantity);
                                            command1.Parameters.AddWithValue("@unitprice", lineunitprice);
                                            command1.Parameters.AddWithValue("@amount", lineamount);
                                            command1.Parameters.AddWithValue("@customer", customer);
                                            try
                                            {
                                                var itemdetailid = command1.ExecuteNonQuery();
                                            }
                                            catch (SqlException ex)
                                            {
                                                InsertErrorLog(connectionString, "USP_ItemReceiptDetailDataSave", "", ex.Message);
                                                Console.WriteLine("Error: " + ex.Message);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return "successfully";
        }
        public async Task<string> InsertInvoiceData(string connectionString, InvoiceModel Invoiceobj, string CompanyName)
        {
            if (Invoiceobj != null)
            {
                for (int i = 0; i < Invoiceobj.IntuitResponse.QueryResponse.Invoice.Count; i++)
                {
                    int InvoiceID = 0;
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        string workorder = "";
                        for (int k = 0; k < Invoiceobj.IntuitResponse.QueryResponse.Invoice[i].CustomField.Count; k++)
                        {
                            if (Invoiceobj.IntuitResponse.QueryResponse.Invoice[i].CustomField[k].Name == "Work Order")
                            {
                                workorder = Invoiceobj.IntuitResponse.QueryResponse.Invoice[i].CustomField[k].StringValue == null ? "" : Invoiceobj.IntuitResponse.QueryResponse.Invoice[i].CustomField[k].StringValue;
                            }
                        }

                        connection.Open();
                        using (SqlCommand command = new SqlCommand("USP_InvoiceDataSave", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@Id", Invoiceobj.IntuitResponse.QueryResponse.Invoice[i].Id);
                            command.Parameters.AddWithValue("@DocNumber", Invoiceobj.IntuitResponse.QueryResponse.Invoice[i].DocNumber);
                            command.Parameters.AddWithValue("@TxnDate", Invoiceobj.IntuitResponse.QueryResponse.Invoice[i].TxnDate);
                            command.Parameters.AddWithValue("@TotalAmt", Invoiceobj.IntuitResponse.QueryResponse.Invoice[i].TotalAmt);
                            command.Parameters.AddWithValue("@Balance", Invoiceobj.IntuitResponse.QueryResponse.Invoice[i].Balance);
                            command.Parameters.AddWithValue("@WorkOrder", workorder);
                            command.Parameters.AddWithValue("@Company", CompanyName);

                            try
                            {
                                InvoiceID = Convert.ToInt32(command.ExecuteScalar());
                            }
                            catch (SqlException ex)
                            {
                                InsertErrorLog(connectionString, "USP_InvoiceDataSave", "", ex.Message);
                            }
                        }

                        
                        for (int j = 0; j < Invoiceobj.IntuitResponse.QueryResponse.Invoice[i].Line.Count; j++)
                        {
                            Double Qty = 0;
                            Double UnitPrice = 0;
                            if (Invoiceobj.IntuitResponse.QueryResponse.Invoice[i].Line[j].SalesItemLineDetail != null)
                            {
                                Qty= Convert.ToDouble(Invoiceobj.IntuitResponse.QueryResponse.Invoice[i].Line[j].SalesItemLineDetail.Qty);
                                UnitPrice = Convert.ToDouble(Invoiceobj.IntuitResponse.QueryResponse.Invoice[i].Line[j].SalesItemLineDetail.UnitPrice);
                            }

                            string linesid = Invoiceobj.IntuitResponse.QueryResponse.Invoice[i].Line[j].Id == null ? "0" : Invoiceobj.IntuitResponse.QueryResponse.Invoice[i].Line[j].Id;
                            string Description = Invoiceobj.IntuitResponse.QueryResponse.Invoice[i].Line[j].Description == null ? "" : Invoiceobj.IntuitResponse.QueryResponse.Invoice[i].Line[j].Description;
                            double Amount = Convert.ToDouble(Invoiceobj.IntuitResponse.QueryResponse.Invoice[i].Line[j].Amount);

                            if (InvoiceID > 0)
                            {
                                using (SqlConnection connection1 = new SqlConnection(connectionString))
                                {
                                    connection1.Open();
                                    using (SqlCommand command1 = new SqlCommand("USP_InvoiceDetailDataSave", connection1))
                                    {
                                        command1.CommandType = CommandType.StoredProcedure;
                                        command1.Parameters.AddWithValue("@InvoiceId", InvoiceID);
                                        command1.Parameters.AddWithValue("@LineNum", linesid);
                                        command1.Parameters.AddWithValue("@Description", Description);
                                        command1.Parameters.AddWithValue("@UnitPrice", UnitPrice);
                                        command1.Parameters.AddWithValue("@Qty", Qty);
                                        command1.Parameters.AddWithValue("@Amount", Amount);
                                        try
                                        {
                                            var itemdetailid = command1.ExecuteNonQuery();
                                        }
                                        catch (SqlException ex)
                                        {
                                            InsertErrorLog(connectionString, "USP_InvoiceDetailDataSave", "", ex.Message);
                                        }
                                    }
                                }
                            }
                        }
                    }

                }
            }
           return "successfully";
        }
    }
}
