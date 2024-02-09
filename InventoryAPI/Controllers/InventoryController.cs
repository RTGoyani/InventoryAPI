using HtmlAgilityPack;
using Intuit.Ipp.Core.Configuration;
using Intuit.Ipp.Data;
using InventoryAPI.Model;
using InventoryAPI.Repository;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.ComponentModel.Design;
using System.Data;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Web;
using System.Xml;

namespace InventoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public InventoryController(IConfiguration configuration)
        {
            _configuration = configuration;
        }



        //[HttpPost]
        //[Route("InventoryTokenGeneration")]
        //public async Task<IActionResult> InventoryTokenGeneration()
        //{
        //    string connectionString = _configuration.GetConnectionString("DefaultConnection");
        //    CommonRepository commonRepository = new CommonRepository();
        //    TokenModel objtoken = new TokenModel();
        //    var responseContent = "";
        //    try
        //    {
        //        string clientid = _configuration["AppSettings:InventoryClientid"];
        //        string clientsecret = _configuration["AppSettings:InventoryClientSecret"];
        //        string username = _configuration["AppSettings:InventoryUsername"];
        //        string password = _configuration["AppSettings:InventoryPassword"];
        //        string code = "";
        //        IWebDriver driver1 = new ChromeDriver();
        //        driver1.Navigate().GoToUrl("https://api.sosinventory.com/oauth2/authorize?response_type=code&client_id="+ clientid + "&redirect_uri=");
        //        IWebElement usernameField = driver1.FindElement(By.Id("Email"));
        //        IWebElement passwordField = driver1.FindElement(By.Id("Password"));
        //        usernameField.SendKeys(username);
        //        passwordField.SendKeys(password);
        //        IWebElement button1 = driver1.FindElement(By.XPath("//button[@type='submit']")); // Replace with the actual button's ID or another suitable selector
        //        button1.Click();
        //        var url = driver1.Url;

        //        //IWebElement CompanyId = driver1.FindElement(By.Id("CompanyId"));
        //        //CompanyId.Click();


        //        string radioValue = "33923";
        //        IWebElement radioButton = driver1.FindElement(By.XPath($"//input[@type='radio' and @value='{radioValue}']"));

        //        // Check if the radio button is not already selected
        //        if (!radioButton.Selected)
        //        {
        //            // Click the radio button to select it
        //            radioButton.Click();
        //        }

        //        IWebElement button2 = driver1.FindElement(By.XPath("//button[@type='submit']"));
        //        button2.Click();
        //        var url1 = driver1.Url;

        //        var decodedUrl = Uri.UnescapeDataString(url1);
        //        var splitedUrl = decodedUrl.Split("&");
        //        if (splitedUrl.Length > 1)
        //        {
        //            code = splitedUrl[1].Replace("code=", "").Trim();
        //        }
        //        driver1.Quit();

        //        using (HttpClient client = new HttpClient())
        //        {
        //            client.DefaultRequestHeaders.Add("Host", "api.sosinventory.com");
        //            client.DefaultRequestHeaders.Add("Cookie", "ASP.NET_SessionId=m4prq5uxykksdfwdwtx5bf3x; __RequestVerificationToken=JFmYZ6NFW4Hciby46gAWnOX4EwWyTMe3cvrUwgX7HcfLEgCZJUv7Pno5fFoyb7ted3GFcgL9Ze_6nYnlovLXOppdFXA1");

        //            var content = new FormUrlEncodedContent(new[]
        //            {
        //                        new KeyValuePair<string, string>("grant_type", "authorization_code"),
        //                        new KeyValuePair<string, string>("client_id",clientid),
        //                        new KeyValuePair<string, string>("client_secret", clientsecret),
        //                        new KeyValuePair<string, string>("code", code),
        //                        new KeyValuePair<string, string>("redirect_uri", "")
        //            });

        //            try
        //            {
        //                var response = await client.PostAsync("https://api.sosinventory.com/oauth2/token", content);

        //                if (response.IsSuccessStatusCode)
        //                {
        //                    responseContent = await response.Content.ReadAsStringAsync();
        //                    objtoken = JsonConvert.DeserializeObject<TokenModel>(responseContent);
        //                }
        //                else
        //                {
        //                    Console.WriteLine("Error: " + response.StatusCode);
        //                    commonRepository.InsertErrorLog(connectionString,"InventoryTokenGeneration", "", "An error occurred: IsSuccessStatusCode false ");
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                commonRepository.InsertErrorLog(connectionString,"InventoryTokenGeneration", "", ex.Message);
        //                Console.WriteLine("An error occurred: " + ex.Message);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string code = ((HttpRequestException)ex).StatusCode.ToString();
        //        commonRepository.InsertErrorLog(connectionString,"InventoryTokenGeneration", code, ex.Message);
        //        return BadRequest(ex.Message);
        //    }
        //    return Ok(responseContent);
        //}


        [HttpGet]
        [Route("GetPurchaseOrderAndItemReceiptAPI1")]
        public async Task<IActionResult> GetPurchaseOrderAndItemReceiptAPI1()
        {
            try
            {
                string CompanyId = _configuration["AppSettings:InventoryCompany1"];
                var res1 = await GetPurchaseOrder(CompanyId);
            }
            catch (Exception ex)
            {

            }
            return Ok("successfully");
        }



        [HttpGet]
        [Route("GetPurchaseOrderAndItemReceiptAPI2")]
        public async Task<IActionResult> GetPurchaseOrderAndItemReceiptAPI2()
        {
            try
            {
                string CompanyId = _configuration["AppSettings:InventoryCompany2"];
                var res1 = await GetPurchaseOrder(CompanyId);
            }
            catch (Exception ex)
            {

            }
            return Ok("successfully");
        }



        [NonAction]
        public async Task<IActionResult> GetPurchaseOrder(string CompanyId)
        {
            var CompanyName = "";
            CommonRepository commonRepository = new CommonRepository();
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            PurchaseOrderModel objPO = new PurchaseOrderModel();
            var responseData = "";

            TokenModel objtoken = new TokenModel();
            var responseContent = "";
            try
            {
                string clientid = _configuration["AppSettings:InventoryClientid"];
                string clientsecret = _configuration["AppSettings:InventoryClientSecret"];
                string username = _configuration["AppSettings:InventoryUsername"];
                string password = _configuration["AppSettings:InventoryPassword"];

                string code = "";
                IWebDriver driver1 = new ChromeDriver();
                driver1.Navigate().GoToUrl("https://api.sosinventory.com/oauth2/authorize?response_type=code&client_id=" + clientid + "&redirect_uri=");
                IWebElement usernameField = driver1.FindElement(By.Id("Email"));
                IWebElement passwordField = driver1.FindElement(By.Id("Password"));
                usernameField.SendKeys(username);
                passwordField.SendKeys(password);
                IWebElement button1 = driver1.FindElement(By.XPath("//button[@type='submit']")); // Replace with the actual button's ID or another suitable selector
                button1.Click();
                var url = driver1.Url;


                //IWebElement CompanyId = driver1.FindElement(By.Id("CompanyId"));
                //CompanyId.Click();

                // string radioValue = "33923"; company 2

                string radioValue = CompanyId.ToString();
                IWebElement radioButton = driver1.FindElement(By.XPath($"//input[@type='radio' and @value='{radioValue}']"));

                if (!radioButton.Selected)
                {
                    radioButton.Click();
                }


                if(CompanyId == "33637")
                {
                    CompanyName = "Automatic Control Technology";
                }
                else
                {
                    CompanyName = "Automatic Equipment, Inc.";
                }

                IWebElement button2 = driver1.FindElement(By.XPath("//button[@type='submit']"));
                button2.Click();
                var url1 = driver1.Url;

                var decodedUrl = Uri.UnescapeDataString(url1);
                var splitedUrl = decodedUrl.Split("&");
                if (splitedUrl.Length > 1)
                {
                    code = splitedUrl[1].Replace("code=", "").Trim();
                }
                driver1.Quit();

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Host", "api.sosinventory.com");
                    client.DefaultRequestHeaders.Add("Cookie", "ASP.NET_SessionId=m4prq5uxykksdfwdwtx5bf3x; __RequestVerificationToken=JFmYZ6NFW4Hciby46gAWnOX4EwWyTMe3cvrUwgX7HcfLEgCZJUv7Pno5fFoyb7ted3GFcgL9Ze_6nYnlovLXOppdFXA1");

                    var content = new FormUrlEncodedContent(new[]
                    {
                                new KeyValuePair<string, string>("grant_type", "authorization_code"),
                                new KeyValuePair<string, string>("client_id",clientid),
                                new KeyValuePair<string, string>("client_secret", clientsecret),
                                new KeyValuePair<string, string>("code", code),
                                new KeyValuePair<string, string>("redirect_uri", "")
                    });

                    try
                    {
                        var response = await client.PostAsync("https://api.sosinventory.com/oauth2/token", content);

                        if (response.IsSuccessStatusCode)
                        {
                            responseContent = await response.Content.ReadAsStringAsync();
                            objtoken = JsonConvert.DeserializeObject<TokenModel>(responseContent);
                        }
                        else
                        {
                            Console.WriteLine("Error: " + response.StatusCode);
                            commonRepository.InsertErrorLog(connectionString, "InventoryTokenGeneration", "", "An error occurred: IsSuccessStatusCode false ");
                        }
                    }
                    catch (Exception ex)
                    {
                        commonRepository.InsertErrorLog(connectionString, "InventoryTokenGeneration", "", ex.Message);
                        Console.WriteLine("An error occurred: " + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                string code = ((HttpRequestException)ex).StatusCode.ToString();
                commonRepository.InsertErrorLog(connectionString, "InventoryTokenGeneration", code, ex.Message);
                return BadRequest(ex.Message);
            }


            try
            {
                var client = new HttpClient();
                DateTimeOffset fromDateTime = DateTimeOffset.UtcNow.AddDays(-60);
                string fromDateTimeDateTime = fromDateTime.ToString("yyyy-MM-ddTHH:mm:sszzz");
                string fromDateString = HttpUtility.UrlEncode(fromDateTimeDateTime);


                DateTimeOffset currentDateTime = DateTimeOffset.UtcNow;
                string formattedDateTime = currentDateTime.ToString("yyyy-MM-ddTHH:mm:sszzz");
                string toDateString = HttpUtility.UrlEncode(formattedDateTime);


                var request = new HttpRequestMessage(HttpMethod.Get, "https://api.sosinventory.com/api/v2/purchaseorder?start=1&maxresults=200&from="+ fromDateString + "&to="+ toDateString + "");
                request.Headers.Add("Authorization", "Bearer "+ objtoken.access_token+ "");
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                await response.Content.ReadAsStringAsync();
                responseData = await response.Content.ReadAsStringAsync();
                objPO = JsonConvert.DeserializeObject<PurchaseOrderModel>(responseData);
                await commonRepository.InsertPurchaseorder(connectionString, objPO, CompanyName);


                var client1 = new HttpClient();
                var request1 = new HttpRequestMessage(HttpMethod.Get, "https://api.sosinventory.com/api/v2/itemreceipt?start=1&maxresults=200&from=" + fromDateString + "&to=" + toDateString + "");
                request1.Headers.Add("Authorization", "Bearer " + objtoken.access_token + "");
                var response1 = await client.SendAsync(request1);
                response1.EnsureSuccessStatusCode();
                await response1.Content.ReadAsStringAsync();
                var responseData1 = await response1.Content.ReadAsStringAsync();
                ItemReceiptModel1 obj = JsonConvert.DeserializeObject<ItemReceiptModel1>(responseData1);
                await commonRepository.InsertItemReceipt(connectionString, obj, CompanyName);

            }
            catch (Exception ex)
            {
                string code = ((HttpRequestException)ex).StatusCode.ToString();
                commonRepository.InsertErrorLog(connectionString, "GetPurchaseOrder", code, ex.Message);
                return BadRequest(ex.Message);
            }
            return Ok("successfully");
        }


        //[HttpGet]
        //[Route("GetItemReceipt")]
        //public async Task<IActionResult> GetItemReceipt()
        //{
        //    string connectionString = _configuration.GetConnectionString("DefaultConnection");
        //    CommonRepository commonRepository = new CommonRepository();
        //    var responseData = "";
        //    TokenModel objtoken = new TokenModel();
        //    var responseContent = "";
          
        //    try
        //    {
        //        string clientid = _configuration["AppSettings:InventoryClientid"];
        //        string clientsecret = _configuration["AppSettings:InventoryClientSecret"];
        //        string username = _configuration["AppSettings:InventoryUsername"];
        //        string password = _configuration["AppSettings:InventoryPassword"];
        //        string code = "";
        //        IWebDriver driver1 = new ChromeDriver();
        //        driver1.Navigate().GoToUrl("https://api.sosinventory.com/oauth2/authorize?response_type=code&client_id=" + clientid + "&redirect_uri=");
        //        IWebElement usernameField = driver1.FindElement(By.Id("Email"));
        //        IWebElement passwordField = driver1.FindElement(By.Id("Password"));
        //        usernameField.SendKeys(username);
        //        passwordField.SendKeys(password);
        //        IWebElement button1 = driver1.FindElement(By.XPath("//button[@type='submit']")); // Replace with the actual button's ID or another suitable selector
        //        button1.Click();
        //        var url = driver1.Url;


        //        //IWebElement CompanyId = driver1.FindElement(By.Id("CompanyId"));
        //        //CompanyId.Click();

        //        // string radioValue = "33923"; company 2
        //        string radioValue = "33637";
        //        IWebElement radioButton = driver1.FindElement(By.XPath($"//input[@type='radio' and @value='{radioValue}']"));
        //        if (!radioButton.Selected)
        //        {
        //            radioButton.Click();
        //        }


        //        IWebElement button2 = driver1.FindElement(By.XPath("//button[@type='submit']"));
        //        button2.Click();
        //        var url1 = driver1.Url;

        //        var decodedUrl = Uri.UnescapeDataString(url1);
        //        var splitedUrl = decodedUrl.Split("&");
        //        if (splitedUrl.Length > 1)
        //        {
        //            code = splitedUrl[1].Replace("code=", "").Trim();
        //        }

        //        driver1.Quit();

        //        using (HttpClient client = new HttpClient())
        //        {
        //            client.DefaultRequestHeaders.Add("Host", "api.sosinventory.com");
        //            client.DefaultRequestHeaders.Add("Cookie", "ASP.NET_SessionId=m4prq5uxykksdfwdwtx5bf3x; __RequestVerificationToken=JFmYZ6NFW4Hciby46gAWnOX4EwWyTMe3cvrUwgX7HcfLEgCZJUv7Pno5fFoyb7ted3GFcgL9Ze_6nYnlovLXOppdFXA1");

        //            var content = new FormUrlEncodedContent(new[]
        //            {
        //                        new KeyValuePair<string, string>("grant_type", "authorization_code"),
        //                        new KeyValuePair<string, string>("client_id",clientid),
        //                        new KeyValuePair<string, string>("client_secret", clientsecret),
        //                        new KeyValuePair<string, string>("code", code),
        //                        new KeyValuePair<string, string>("redirect_uri", "")
        //            });

        //            try
        //            {
        //                var response = await client.PostAsync("https://api.sosinventory.com/oauth2/token", content);

        //                if (response.IsSuccessStatusCode)
        //                {
        //                    responseContent = await response.Content.ReadAsStringAsync();
        //                    objtoken = JsonConvert.DeserializeObject<TokenModel>(responseContent);
        //                }
        //                else
        //                {
        //                    Console.WriteLine("Error: " + response.StatusCode);
        //                    commonRepository.InsertErrorLog(connectionString,"InventoryTokenGeneration", "", "An error occurred: IsSuccessStatusCode false ");
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                commonRepository.InsertErrorLog(connectionString, "InventoryTokenGeneration", "", ex.Message);
        //                Console.WriteLine("An error occurred: " + ex.Message);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string code = ((HttpRequestException)ex).StatusCode.ToString();
        //        commonRepository.InsertErrorLog(connectionString , "InventoryTokenGeneration", code, ex.Message);
        //    }

        //    try
        //    {

        //        DateTimeOffset fromDateTime = DateTimeOffset.UtcNow.AddDays(-60);
        //        string fromDateTimeDateTime = fromDateTime.ToString("yyyy-MM-ddTHH:mm:sszzz");
        //        string fromDateString = HttpUtility.UrlEncode(fromDateTimeDateTime);


        //        DateTimeOffset currentDateTime = DateTimeOffset.UtcNow;
        //        string formattedDateTime = currentDateTime.ToString("yyyy-MM-ddTHH:mm:sszzz");
        //        string toDateString = HttpUtility.UrlEncode(formattedDateTime);
        //        var client = new HttpClient();
        //        var request = new HttpRequestMessage(HttpMethod.Get, "https://api.sosinventory.com/api/v2/itemreceipt?start=1&maxresults=200&from="+ fromDateString + "&to="+ toDateString + "");
        //        request.Headers.Add("Authorization", "Bearer " + objtoken.access_token + "");
        //        var response = await client.SendAsync(request);
        //        response.EnsureSuccessStatusCode();
        //        await response.Content.ReadAsStringAsync();
        //        responseData = await response.Content.ReadAsStringAsync();
        //        ItemReceiptModel1 obj = JsonConvert.DeserializeObject<ItemReceiptModel1>(responseData);
              

        //        if (obj != null)
        //        {
        //            for (int i = 0; i < obj.data.Count; i++)
        //            {
        //                try
        //                {
        //                    int Id = obj.data[i].id;
        //                    DateTime date = obj.data[i].date;
        //                    string number = obj.data[i].number;

        //                    int currencyid = 0;
        //                    if (obj.data[i].currency != null)
        //                    {
        //                        currencyid = obj.data[i].currency.id;
        //                    }

        //                    int VenderId = 0;
        //                    if (obj.data[i].vendor != null)
        //                    {
        //                        currencyid = obj.data[i].vendor.id;
        //                    }

        //                    string VenderName = "";
        //                    if (obj.data[i].vendor != null)
        //                    {
        //                        VenderName = obj.data[i].vendor.name;
        //                    }

        //                    string currencyName = "";
        //                    if (obj.data[i].currency != null)
        //                    {
        //                        currencyName = obj.data[i].currency.name;
        //                    }

        //                    string comment = obj.data[i].comment;
        //                    string VendorMessage = obj.data[i].vendorMessage;
        //                    string VendorNotes = obj.data[i].vendorNotes;
        //                    double SubTotal = obj.data[i].subTotal;
        //                    double TaxAmount = obj.data[i].taxAmount;
        //                    double Total = obj.data[i].total;

        //                    int ItemReceiptID = 0;
        //                    using (SqlConnection connection = new SqlConnection(connectionString))
        //                    {
        //                        connection.Open();
        //                        using (SqlCommand command = new SqlCommand("USP_ItemReceiptDataSave", connection))
        //                        {
        //                            command.CommandType = CommandType.StoredProcedure;
        //                            command.Parameters.AddWithValue("@Id", Id);
        //                            command.Parameters.AddWithValue("@Date", date);
        //                            command.Parameters.AddWithValue("@Number", number);
        //                            command.Parameters.AddWithValue("@CurrencyID", currencyid);
        //                            command.Parameters.AddWithValue("@CurrencyName", currencyName);
        //                            command.Parameters.AddWithValue("@comment", comment == null ? "" : comment);
        //                            command.Parameters.AddWithValue("@VenderId", VenderId);
        //                            command.Parameters.AddWithValue("@VenderName", VenderName == null ? "" : VenderName);
        //                            command.Parameters.AddWithValue("@VendorMessage", VendorMessage == null ? "" : VendorMessage);
        //                            command.Parameters.AddWithValue("@VendorNotes", VendorNotes == null ? "" : VendorNotes);
        //                            command.Parameters.AddWithValue("@SubTotal", SubTotal);
        //                            command.Parameters.AddWithValue("@TaxAmount", TaxAmount);
        //                            command.Parameters.AddWithValue("@Total", Total);

        //                            try
        //                            {
        //                                ItemReceiptID = Convert.ToInt32(command.ExecuteScalar());
        //                            }
        //                            catch (SqlException ex)
        //                            {
        //                                commonRepository.InsertErrorLog(connectionString, "USP_ItemReceiptDataSave", "", ex.Message);
        //                                Console.WriteLine("Error: " + ex.Message);
        //                            }
        //                        }
        //                    }

        //                    for (int j = 0; j < obj.data[i].lines.Count; j++)
        //                    {
        //                        int linesid = obj.data[i].lines[j].id;
        //                        int lineNumber = obj.data[i].lines[j].lineNumber;
        //                        string linedescription = obj.data[i].lines[j].description == null ? "" : obj.data[i].lines[j].description;
        //                        double lineweight = obj.data[i].lines[j].weight;
        //                        string linevolume = obj.data[i].lines[j].volume.ToString() == null ? "" : obj.data[i].lines[j].volume.ToString();
        //                        double linequantity = obj.data[i].lines[j].quantity;
        //                        double lineunitprice = obj.data[i].lines[j].unitprice;
        //                        double lineamount = obj.data[i].lines[j].amount;
        //                        string customer = obj.data[i].lines[j].customer == null ? "" : obj.data[i].lines[j].customer.ToString();

        //                        if (ItemReceiptID > 0)
        //                        {
        //                            using (SqlConnection connection1 = new SqlConnection(connectionString))
        //                            {
        //                                connection1.Open();
        //                                using (SqlCommand command1 = new SqlCommand("USP_ItemReceiptDetailDataSave", connection1))
        //                                {
        //                                    command1.CommandType = CommandType.StoredProcedure;
        //                                    command1.Parameters.AddWithValue("@ItemReceiptID", ItemReceiptID);
        //                                    command1.Parameters.AddWithValue("@Id", linesid);
        //                                    command1.Parameters.AddWithValue("@LineNumber", lineNumber);
        //                                    command1.Parameters.AddWithValue("@description", linedescription == null ? "" : linedescription);
        //                                    command1.Parameters.AddWithValue("@weight", lineweight);
        //                                    command1.Parameters.AddWithValue("@volume", linevolume);
        //                                    command1.Parameters.AddWithValue("@quantity", linequantity);
        //                                    command1.Parameters.AddWithValue("@unitprice", lineunitprice);
        //                                    command1.Parameters.AddWithValue("@amount", lineamount);
        //                                    command1.Parameters.AddWithValue("@customer", customer);
        //                                    try
        //                                    {
        //                                        var itemdetailid = command1.ExecuteNonQuery();
        //                                    }
        //                                    catch (SqlException ex)
        //                                    {
        //                                        commonRepository.InsertErrorLog(connectionString, "USP_ItemReceiptDetailDataSave", "", ex.Message);
        //                                        Console.WriteLine("Error: " + ex.Message);
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //                catch (Exception ex)
        //                {

        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string code = ((HttpRequestException)ex).StatusCode.ToString();
        //        commonRepository.InsertErrorLog(connectionString, "GetItemReceipt", code, ex.Message);
        //        return BadRequest(ex.Message);
        //    }
        //    return Ok("successfully");
        //}

    }
}
