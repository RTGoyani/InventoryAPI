using InventoryAPI.Model;
using InventoryAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using Salesforce.Common;
using Salesforce.Common.Models.Json;
using Salesforce.Force;
using System.Data;
using System.Net.Http.Headers;
using System.Text;
using System.Web;

namespace InventoryAPI.Controllers
{
    public class SalesforceController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public SalesforceController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //[HttpGet]
        //[Route("SalesforceToken")]
        //public async Task<IActionResult> SalesforceToken()
        //{
        //    CommonRepository commonRepository = new CommonRepository();
        //    salesforceTokenModel objsalesforce = new salesforceTokenModel();
        //    try
        //    {
        //        var client = new HttpClient();
        //        var request = new HttpRequestMessage(HttpMethod.Post, "https://app-data-1445.my.salesforce.com//services/oauth2/token?client_id=3MVG9pRzvMkjMb6k3iXFkTMrRELNgVP5.puzNbakMZFXCkV07U5KUD2I_l6DARsuFcBNXp.eN6XO.ETKOET3M&client_secret=EDD267EC9F2F4389EF5FC5266E440952C2CD7F83572AB25D479EDD20DB719FAA&grant_type=client_credentials");
        //        var response = await client.SendAsync(request);
        //        response.EnsureSuccessStatusCode();
        //        var data = await response.Content.ReadAsStringAsync();
        //        objsalesforce = JsonConvert.DeserializeObject<salesforceTokenModel>(data);
        //    }
        //    catch (Exception ex)
        //    {
        //        string Message = ex.Message;
        //        string code = ((HttpRequestException)ex).StatusCode.ToString();
        //        commonRepository.InsertErrorLog("SalesforceToken", code, Message);
        //        throw;
        //    }
        //    return Ok(objsalesforce);
        //}


        [HttpGet]
        [Route("GetTimesheetandWorkOrderAPI")]
        public async Task<IActionResult> GetTimesheetandWorkOrderAPI()
        {
            salesforceTokenModel objsalesforce = new salesforceTokenModel();
            CommonRepository commonRepository = new CommonRepository();
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            try
            {
                string clientid = _configuration["AppSettings:SalesforceClientid"];
                string clientsecret = _configuration["AppSettings:SalesforceClientSecret"];
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://automaticequipment.my.salesforce.com//services/oauth2/token?client_id="+ clientid + "&client_secret="+ clientsecret + "&grant_type=client_credentials");
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var data1 = await response.Content.ReadAsStringAsync();
                objsalesforce = JsonConvert.DeserializeObject<salesforceTokenModel>(data1);
                if (objsalesforce != null)
                {
                    commonRepository.GetTimesheetAPI(connectionString,objsalesforce);
                    commonRepository.GetWorkOrderAPI(connectionString,objsalesforce);
                    commonRepository.EmployeeWoCData(connectionString,objsalesforce);
                }
                else
                {
                    commonRepository.InsertErrorLog(connectionString, "GetTimesheetDataAPI", "", "Failed to obtain access token ,in objsalesforce is null");
                }
            }
            catch (Exception ex)
            {
                commonRepository.InsertErrorLog(connectionString, "GetTimesheetDataAPI", "", ex.Message.ToString());
            }
            return Ok("successfully");
        }



        //[HttpGet]
        //[Route("GetWorkOrderData")]
        //public async Task<IActionResult> GetWorkOrderData()
        //{
        //    salesforceTokenModel objsalesforce = new salesforceTokenModel();
        //    CommonRepository commonRepository = new CommonRepository();
        //    string connectionString = _configuration.GetConnectionString("DefaultConnection");
        //    var data = "";
        //    try
        //    {
        //        string clientid = _configuration["AppSettings:SalesforceClientid"];
        //        string clientsecret = _configuration["AppSettings:SalesforceClientSecret"];
        //        var client = new HttpClient();
        //        var request = new HttpRequestMessage(HttpMethod.Post, "https://automaticequipment.my.salesforce.com//services/oauth2/token?client_id="+ clientid + "&client_secret="+ clientsecret + "&grant_type=client_credentials");
        //        var response = await client.SendAsync(request);
        //        response.EnsureSuccessStatusCode();
        //        var data1 = await response.Content.ReadAsStringAsync();
        //        objsalesforce = JsonConvert.DeserializeObject<salesforceTokenModel>(data1);
        //        if (objsalesforce != null)
        //        {
        //            if (!string.IsNullOrEmpty(objsalesforce.access_token))
        //            {
        //                DateTimeOffset currentDateTime = DateTimeOffset.UtcNow.AddDays(-3);
        //                string formattedDateTime = currentDateTime.ToString("yyyy-MM-ddTHH:mm:sszzz");
        //                string deString = HttpUtility.UrlEncode(formattedDateTime);
        //                string restQuery = $"https://automaticequipment.my.salesforce.com//services/data/v54.0/query?q=SELECT+Account_Credit_Status__c,Account_Estatus__c,Account__c,Amount_Invoiced__c,Balance__c,Case__c,Company__c,Contact__c,Contrato_Mant__c,Control__c,CreatedById,CreatedDate,CreatedDate_Formula__c,Created_Date__c,Create_Refer_Opportunity__c,Credit__c,Cubierto_Por__c,Customer_Notes__c,Id,Initial_Order__c,Inspection_Completed__c,Internal_Comments__c,Invoice_Status__c,IsDeleted,Job_Date__c,job_Description__c,Labor_Costs__c,LastActivityDate,LastModifiedById,LastModifiedDate,Longitude__c,Name,Net_Amount_Invoiced_Excluding_Tax__c,Next_Maintenance_Created__c,Other_Costs__c,OwnerId,Payment_Terms__c,Payment_Term_Details__c,Pending_Materials__c,percent_Invoiced__c,PO_Number__c,Profitability_percent__c,Sales_Person__c,Signatory_Email__c,Signatory_Name__c,SignatureId__c,Signature_Date__c,Signature_Image__c,Source__c,Status__c,Subtype__c,SystemModstamp,Tax_Amount_After_Credit__c,Tax_Amount_Billed__c,Tax_Credit__c,Tenico_Referido__c,Total_Amount_Invoiced_To_Customer__c,Total_Invoiced_to_Customer__c,Total_Job_Amount__c,Total_material_Costs__c,Total_Price_Materials_Calc__c,Total_WO_Material_Costs__c,Type__c,T_cnico_Asociado__c,User_for_Reports__c,WorkOrder_Net_Income__c,Work_Performed__c+FROM+WorkOrder__c+where+CreatedDate%3E%3D" + deString + "ORDER BY CreatedDate DESC NULLS FIRST";
        //                var client1 = new HttpClient();
        //                HttpRequestMessage request1 = new HttpRequestMessage(HttpMethod.Get, restQuery);
        //                request1.Headers.Add("Authorization", "Bearer " + objsalesforce.access_token);
        //                request1.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //                HttpResponseMessage response1 = client1.SendAsync(request1).Result;
        //                data = response1.Content.ReadAsStringAsync().Result;
        //                WorkOrderModel objWorkOrder = JsonConvert.DeserializeObject<WorkOrderModel>(data);
        //                if (objWorkOrder != null)
        //                {
        //                    for (int i = 0; i < objWorkOrder.records.Count; i++)
        //                    {
        //                        using (SqlConnection connection1 = new SqlConnection(connectionString))
        //                        {
        //                            connection1.Open();
        //                            using (SqlCommand command1 = new SqlCommand("USPInsertWorkOrderMasterData", connection1))
        //                            {
        //                                command1.CommandType = CommandType.StoredProcedure;
        //                                command1.Parameters.AddWithValue("@Id", objWorkOrder.records[i].Id);
        //                                command1.Parameters.AddWithValue("@Name", objWorkOrder.records[i].Name);
        //                                command1.Parameters.AddWithValue("@Account_Credit_Status__c", objWorkOrder.records[i].Account_Credit_Status__c);
        //                                command1.Parameters.AddWithValue("@Account_Estatus__c", objWorkOrder.records[i].Account_Estatus__c);
        //                                command1.Parameters.AddWithValue("@Account__c", objWorkOrder.records[i].Account__c);
        //                                command1.Parameters.AddWithValue("@Amount_Invoiced__c", objWorkOrder.records[i].Amount_Invoiced__c);
        //                                command1.Parameters.AddWithValue("@Balance__c", objWorkOrder.records[i].Balance__c);
        //                                command1.Parameters.AddWithValue("@Case__c", objWorkOrder.records[i].Case__c == null ? "" : objWorkOrder.records[i].Case__c);
        //                                command1.Parameters.AddWithValue("@Company__c", objWorkOrder.records[i].Company__c == null ? "" : objWorkOrder.records[i].Company__c);
        //                                command1.Parameters.AddWithValue("@Contact__c", objWorkOrder.records[i].Contact__c == null ? "" : objWorkOrder.records[i].Contact__c);
        //                                command1.Parameters.AddWithValue("@Contrato_Mant__c", objWorkOrder.records[i].Contrato_Mant__c == null ? "" : objWorkOrder.records[i].Contrato_Mant__c);
        //                                command1.Parameters.AddWithValue("@Control__c", objWorkOrder.records[i].Control__c);
        //                                command1.Parameters.AddWithValue("@CreatedById", objWorkOrder.records[i].CreatedById);
        //                                command1.Parameters.AddWithValue("@CreatedDate", objWorkOrder.records[i].CreatedDate);
        //                                command1.Parameters.AddWithValue("@CreatedDate_Formula__c", objWorkOrder.records[i].CreatedDate_Formula__c);
        //                                command1.Parameters.AddWithValue("@Created_Date__c", objWorkOrder.records[i].Created_Date__c);
        //                                command1.Parameters.AddWithValue("@Create_Refer_Opportunity__c", objWorkOrder.records[i].Create_Refer_Opportunity__c);
        //                                command1.Parameters.AddWithValue("@Credit__c", objWorkOrder.records[i].Credit__c);
        //                                command1.Parameters.AddWithValue("@Cubierto_Por__c", objWorkOrder.records[i].Cubierto_Por__c);
        //                                command1.Parameters.AddWithValue("@Customer_Notes__c", objWorkOrder.records[i].Customer_Notes__c  == null ? "" : objWorkOrder.records[i].Customer_Notes__c);
        //                                command1.Parameters.AddWithValue("@Initial_Order__c", objWorkOrder.records[i].Initial_Order__c);
        //                                command1.Parameters.AddWithValue("@Inspection_Completed__c", objWorkOrder.records[i].Inspection_Completed__c);
        //                                command1.Parameters.AddWithValue("@InternalCommentsc", objWorkOrder.records[i].Internal_Comments__c == null ? "" : objWorkOrder.records[i].Internal_Comments__c);
        //                                command1.Parameters.AddWithValue("@Invoice_Status__c", objWorkOrder.records[i].Invoice_Status__c == null ? "" : objWorkOrder.records[i].Invoice_Status__c);
        //                                command1.Parameters.AddWithValue("@IsDeleted", objWorkOrder.records[i].IsDeleted);
        //                                command1.Parameters.AddWithValue("@Job_Date__c", objWorkOrder.records[i].Job_Date__c);
        //                                command1.Parameters.AddWithValue("@job_Description__c", objWorkOrder.records[i].job_Description__c);
        //                                command1.Parameters.AddWithValue("@Labor_Costs__c", objWorkOrder.records[i].Labor_Costs__c);
        //                                command1.Parameters.AddWithValue("@LastActivityDate", objWorkOrder.records[i].LastActivityDate == null ? "" : objWorkOrder.records[i].LastActivityDate);
        //                                command1.Parameters.AddWithValue("@LastModifiedById", objWorkOrder.records[i].LastModifiedById == null ? "" : objWorkOrder.records[i].LastModifiedById);
        //                                command1.Parameters.AddWithValue("@LastModifiedDate", objWorkOrder.records[i].LastModifiedDate);
        //                                command1.Parameters.AddWithValue("@Longitude__c", objWorkOrder.records[i].Longitude__c == null ? "" : objWorkOrder.records[i].Longitude__c);
        //                                command1.Parameters.AddWithValue("@Net_Amount_Invoiced_Excluding_Tax__c", objWorkOrder.records[i].Net_Amount_Invoiced_Excluding_Tax__c);
        //                                command1.Parameters.AddWithValue("@Next_Maintenance_Created__c", objWorkOrder.records[i].Next_Maintenance_Created__c);
        //                                command1.Parameters.AddWithValue("@Other_Costs__c", objWorkOrder.records[i].Other_Costs__c);
        //                                command1.Parameters.AddWithValue("@OwnerId", objWorkOrder.records[i].OwnerId);
        //                                command1.Parameters.AddWithValue("@Payment_Terms__c", objWorkOrder.records[i].Payment_Terms__c == null ? "" : objWorkOrder.records[i].Payment_Terms__c);
        //                                command1.Parameters.AddWithValue("@Payment_Term_Details__c", objWorkOrder.records[i].Payment_Term_Details__c == null ? "" : objWorkOrder.records[i].Payment_Term_Details__c);
        //                                command1.Parameters.AddWithValue("@Pending_Materials__c", objWorkOrder.records[i].Pending_Materials__c);
        //                                command1.Parameters.AddWithValue("@percent_Invoiced__c", objWorkOrder.records[i].percent_Invoiced__c == null ? "" : objWorkOrder.records[i].percent_Invoiced__c);
        //                                command1.Parameters.AddWithValue("@PO_Number__c", objWorkOrder.records[i].PO_Number__c == null ? "" : objWorkOrder.records[i].PO_Number__c);
        //                                command1.Parameters.AddWithValue("@Profitability_percent__c", objWorkOrder.records[i].Profitability_percent__c);
        //                                command1.Parameters.AddWithValue("@Sales_Person__c", objWorkOrder.records[i].Sales_Person__c == null ? "" : objWorkOrder.records[i].Sales_Person__c);
        //                                command1.Parameters.AddWithValue("@Signatory_Email__c", objWorkOrder.records[i].Signatory_Email__c == null ? "" : objWorkOrder.records[i].Signatory_Email__c);
        //                                command1.Parameters.AddWithValue("@Signatory_Name__c", objWorkOrder.records[i].Signatory_Name__c == null ? "" : objWorkOrder.records[i].Signatory_Name__c);
        //                                command1.Parameters.AddWithValue("@SignatureId__c", objWorkOrder.records[i].SignatureId__c == null ? "" : objWorkOrder.records[i].SignatureId__c);
        //                                command1.Parameters.AddWithValue("@Signature_Date__c", objWorkOrder.records[i].Signature_Date__c == null ? "" : objWorkOrder.records[i].Signature_Date__c);
        //                                command1.Parameters.AddWithValue("@Signature_Image__c", objWorkOrder.records[i].Signature_Image__c);
        //                                command1.Parameters.AddWithValue("@Source__c", objWorkOrder.records[i].Source__c == null ? "" : objWorkOrder.records[i].Source__c);
        //                                command1.Parameters.AddWithValue("@Status__c", objWorkOrder.records[i].Status__c);
        //                                command1.Parameters.AddWithValue("@Subtype__c", objWorkOrder.records[i].Subtype__c);
        //                                command1.Parameters.AddWithValue("@SystemModstamp", objWorkOrder.records[i].SystemModstamp);
        //                                command1.Parameters.AddWithValue("@Tax_Amount_After_Credit__c", objWorkOrder.records[i].Tax_Amount_After_Credit__c);
        //                                command1.Parameters.AddWithValue("@Tax_Amount_Billed__c", objWorkOrder.records[i].Tax_Amount_Billed__c);
        //                                command1.Parameters.AddWithValue("@Tax_Credit__c", objWorkOrder.records[i].Tax_Credit__c);
        //                                command1.Parameters.AddWithValue("@Tenico_Referido__c", objWorkOrder.records[i].Tenico_Referido__c == null ? "" : objWorkOrder.records[i].Tenico_Referido__c);
        //                                command1.Parameters.AddWithValue("@Total_Amount_Invoiced_To_Customer__c", objWorkOrder.records[i].Total_Amount_Invoiced_To_Customer__c);
        //                                command1.Parameters.AddWithValue("@Total_Invoiced_to_Customer__c", objWorkOrder.records[i].Total_Invoiced_to_Customer__c);
        //                                command1.Parameters.AddWithValue("@Total_Job_Amount__c", objWorkOrder.records[i].Total_Job_Amount__c == null ? "" : objWorkOrder.records[i].Total_Job_Amount__c);
        //                                command1.Parameters.AddWithValue("@Total_material_Costs__c", objWorkOrder.records[i].Total_material_Costs__c);
        //                                command1.Parameters.AddWithValue("@Total_Price_Materials_Calc__c", objWorkOrder.records[i].Total_Price_Materials_Calc__c);
        //                                command1.Parameters.AddWithValue("@Total_WO_Material_Costs__c", objWorkOrder.records[i].Total_WO_Material_Costs__c);
        //                                command1.Parameters.AddWithValue("@Type__c", objWorkOrder.records[i].Type__c);
        //                                command1.Parameters.AddWithValue("@T_cnico_Asociado__c", objWorkOrder.records[i].T_cnico_Asociado__c);
        //                                command1.Parameters.AddWithValue("@User_for_Reports__c", objWorkOrder.records[i].User_for_Reports__c);
        //                                command1.Parameters.AddWithValue("@WorkOrder_Net_Income__c", objWorkOrder.records[i].WorkOrder_Net_Income__c);
        //                                command1.Parameters.AddWithValue("@Work_Performed__c", objWorkOrder.records[i].Work_Performed__c == null ? "" : objWorkOrder.records[i].Work_Performed__c);
        //                                try
        //                                {
        //                                    var id = command1.ExecuteNonQuery();
        //                                }
        //                                catch (SqlException ex)
        //                                {
        //                                    commonRepository.InsertErrorLog("USPInsertWorkOrderMasterData", "", ex.Message);
        //                                    Console.WriteLine("Error: " + ex.Message);
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                Console.WriteLine("Failed to obtain access token.");
        //                commonRepository.InsertErrorLog("GetWorkOrderData", "", "Failed to obtain access token is null");
        //            }
        //        }
        //        else
        //        {
        //            commonRepository.InsertErrorLog("GetWorkOrderData", "", "Failed to obtain access token ,in objsalesforce is null");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        commonRepository.InsertErrorLog("GetWorkOrderData", "", ex.Message.ToString());
        //    }
        //    return Ok("successfully");
        //}




        //[HttpGet]
        //[Route("GetEmployeeWoCData")]
        //public async Task<IActionResult> GetEmployeeWoCData()
        //{
        //    salesforceTokenModel objsalesforce = new salesforceTokenModel();
        //    CommonRepository commonRepository = new CommonRepository();
        //    string connectionString = _configuration.GetConnectionString("DefaultConnection");
        //    var data = "";
        //    try
        //    {
        //        string clientid = _configuration["AppSettings:SalesforceClientid"];
        //        string clientsecret = _configuration["AppSettings:SalesforceClientSecret"];
        //        var client = new HttpClient();
        //        var request = new HttpRequestMessage(HttpMethod.Post, "https://automaticequipment.my.salesforce.com//services/oauth2/token?client_id="+ clientid + "&client_secret="+ clientsecret + "&grant_type=client_credentials");
        //        var response = await client.SendAsync(request);
        //        response.EnsureSuccessStatusCode();
        //        var data1 = await response.Content.ReadAsStringAsync();
        //        objsalesforce = JsonConvert.DeserializeObject<salesforceTokenModel>(data1);
        //        if (objsalesforce != null)
        //        {
        //            if (!string.IsNullOrEmpty(objsalesforce.access_token))
        //            {
        //                DateTimeOffset currentDateTime = DateTimeOffset.UtcNow.AddDays(-30);
        //                string formattedDateTime = currentDateTime.ToString("yyyy-MM-ddTHH:mm:sszzz");
        //                string deString = HttpUtility.UrlEncode(formattedDateTime);
        //                string restQuery = $"https://automaticequipment.my.salesforce.com//services/data/v54.0/query?q=SELECT+CreatedById,CreatedDate,Hourly_Rate__c,Id,IsDeleted,LastModifiedById,LastModifiedDate,LastReferencedDate,LastViewedDate,Name,OwnerId,SystemModstamp,User__c+FROM+EmployeeWO__c+where+CreatedDate%3E%3D" + deString + "ORDER BY CreatedDate DESC NULLS FIRST";
        //                var client1 = new HttpClient();
        //                HttpRequestMessage request1 = new HttpRequestMessage(HttpMethod.Get, restQuery);
        //                request1.Headers.Add("Authorization", "Bearer " + objsalesforce.access_token);
        //                request1.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //                HttpResponseMessage response1 = client1.SendAsync(request1).Result;
        //                data = response1.Content.ReadAsStringAsync().Result;
        //                EmployeeWOCModel objEmployeeWO = JsonConvert.DeserializeObject<EmployeeWOCModel>(data);
        //                if (objEmployeeWO != null)
        //                {
        //                    for (int i = 0; i < objEmployeeWO.records.Count; i++)
        //                    {
        //                        using (SqlConnection connection1 = new SqlConnection(connectionString))
        //                        {
        //                            connection1.Open();
        //                            using (SqlCommand command1 = new SqlCommand("USP_EmployeeWOCDataSave", connection1))
        //                            {
        //                                command1.CommandType = CommandType.StoredProcedure;
        //                                command1.Parameters.AddWithValue("@Id", objEmployeeWO.records[i].Id);
        //                                command1.Parameters.AddWithValue("@Name", objEmployeeWO.records[i].Name);
        //                                command1.Parameters.AddWithValue("@Hourly_Rate__c", objEmployeeWO.records[i].Hourly_Rate__c);
        //                                command1.Parameters.AddWithValue("@OwnerId", objEmployeeWO.records[i].OwnerId);
        //                                command1.Parameters.AddWithValue("@User__c", objEmployeeWO.records[i].User__c);
        //                                command1.Parameters.AddWithValue("@CreatedById", objEmployeeWO.records[i].CreatedById);
        //                                command1.Parameters.AddWithValue("@CreatedDate", objEmployeeWO.records[i].CreatedDate);
        //                                command1.Parameters.AddWithValue("@LastModifiedById", objEmployeeWO.records[i].LastModifiedById == null ? "" : objEmployeeWO.records[i].LastModifiedById);
        //                                command1.Parameters.AddWithValue("@LastModifiedDate", objEmployeeWO.records[i].LastModifiedDate == null ? "" : objEmployeeWO.records[i].LastModifiedDate);
        //                                command1.Parameters.AddWithValue("@LastReferencedDate", objEmployeeWO.records[i].LastReferencedDate == null ? "" : objEmployeeWO.records[i].LastReferencedDate);
        //                                command1.Parameters.AddWithValue("@LastViewedDate", objEmployeeWO.records[i].LastViewedDate == null ? "" : objEmployeeWO.records[i].LastViewedDate);
        //                                command1.Parameters.AddWithValue("@IsDeleted", objEmployeeWO.records[i].IsDeleted == null ? "" : objEmployeeWO.records[i].IsDeleted);
        //                                command1.Parameters.AddWithValue("@SystemModstamp", objEmployeeWO.records[i].SystemModstamp == null ? "" : objEmployeeWO.records[i].SystemModstamp);
        //                                try
        //                                {
        //                                    var id = command1.ExecuteNonQuery();
        //                                }
        //                                catch (SqlException ex)
        //                                {
        //                                    commonRepository.InsertErrorLog(connectionString, "USP_EmployeeWOCDataSave", "", ex.Message);
        //                                    Console.WriteLine("Error: " + ex.Message);
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                Console.WriteLine("Failed to obtain access token.");
        //                commonRepository.InsertErrorLog(connectionString, "GetEmployeeWoCData", "", "Failed to obtain access token is null");
        //            }
        //        }
        //        else
        //        {
        //            commonRepository.InsertErrorLog(connectionString, "GetEmployeeWoCData", "", "Failed to obtain access token ,in objsalesforce is null");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        commonRepository.InsertErrorLog(connectionString, "GetEmployeeWoCData", "", ex.Message.ToString());
        //    }
        //    return Ok("successfully");
        //}

    }
}
