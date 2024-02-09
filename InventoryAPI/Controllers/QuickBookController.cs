using Intuit.Ipp.Data;
using Intuit.Ipp.OAuth2PlatformClient;
using InventoryAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using RestSharp;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Net.Http;
using System.Web;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Xml;
using static Intuit.Ipp.OAuth2PlatformClient.OidcConstants;
using InventoryAPI.Repository;
using System.Text;
using System.Net.Http.Headers;

namespace InventoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuickBookController : ControllerBase
    {

        public static string clientid = "ABAOg9vBhPu1r8ftjc8QKu4kGS6G2mp2TKbXwDqxvsXYkePjyG";  //Local
        public static string clientsecret = "CV7ubvqhPHoLTS6mTjxJu1MWjTXtnmuuw29GqdKY";          //Local


        public static string redirectUrl = "https://localhost:7084/api/QuickBook/callbackMethod";
        public static string environment = "production"; // "sandbox";
        string code = "";

        private readonly IConfiguration _configuration;

        public QuickBookController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static OAuth2Client auth2Client = new OAuth2Client(clientid, clientsecret, redirectUrl, environment);

      



        [HttpGet]
        [Route("GetPaymentAPI1")]
        public async Task<string> GetPaymentAPI1()
        {
            try
            {
                string CompanyName1 = _configuration["AppSettings:QuickBookCompanyName1"];
                string CompanyCode1 = _configuration["AppSettings:QuickBookCompanyCode1"];
                var res1= await GetPayment(CompanyName1, CompanyCode1);

            }
            catch (Exception)
            {
                throw;
            }
            return "successfully";
        }

        [HttpGet]
        [Route("GetPaymentAPI2")]
        public async Task<string> GetPaymentAPI2()
        {
            try
            {
                string CompanyName2 = _configuration["AppSettings:QuickBookCompanyName2"];
                string CompanyCode2 = _configuration["AppSettings:QuickBookCompanyCode2"];
                var res2 = await GetPayment(CompanyName2, CompanyCode2);
            }
            catch (Exception ex)
            {

            }
            return "successfully";
        }


        [NonAction]
        public async Task<string> GetPayment(string CompanyName,string CompanyCode)
        {
            CommonRepository commonRepository = new CommonRepository();
            PaymentModel obj = new PaymentModel();
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            try
            {
                string Username = _configuration["AppSettings:QuickBookUsername"];
                string Password = _configuration["AppSettings:QuickBookPassword"];
                //string Company = _configuration["AppSettings:QuickBookCompany"];

                List<OidcScopes> scopes = new List<OidcScopes>();
                scopes.Add(OidcScopes.Accounting);
                string authorizeUrl = auth2Client.GetAuthorizationURL(scopes);

                IWebDriver driver1 = new ChromeDriver();
                driver1.Navigate().GoToUrl(authorizeUrl);
                await System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(4));
                IWebElement usernameField = driver1.FindElement(By.Id("iux-identifier-first-international-email-user-id-input"));
                usernameField.SendKeys(Username);
                IWebElement button1 = driver1.FindElement(By.XPath("//button[@type='submit']"));
                button1.Click();

                await System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(4));
                IWebElement passwordField = driver1.FindElement(By.Id("iux-password-confirmation-password"));
                passwordField.SendKeys(Password);
                IWebElement buttonpass = driver1.FindElement(By.XPath("//button[@type='submit']"));
                buttonpass.Click();

                await System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(15));

                IWebElement companyField = driver1.FindElement(By.Id("idsDropdownTypeaheadTextField2"));
                companyField.SendKeys(CompanyName);
                IWebElement companyField1 = driver1.FindElement(By.ClassName("company-list-item"));
                companyField1.Click();


                await System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(2));

                IWebElement button2 = driver1.FindElement(By.ClassName("btn-next"));
                button2.Click();

                await System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(5));
                var decodedUrl = driver1.Url;

                NameValueCollection queryString = HttpUtility.ParseQueryString(new Uri(decodedUrl).Query);
                var codeval = queryString["code"];

                if (!string.IsNullOrEmpty(codeval))
                {
                    code = queryString["code"];
                }
                driver1.Quit();
            }
            catch (Exception ex)
            {
                string code = ((HttpRequestException)ex).StatusCode.ToString();
                commonRepository.InsertErrorLog(connectionString, "QuickBookAuth", code, ex.Message);
                throw;
            }

            var objtoken = await QuickBookController.auth2Client.GetBearerTokenAsync(code);
            var token =objtoken.AccessToken;

            try
            {
                DateTime twoDaysAgo = DateTime.UtcNow.AddDays(-40);
                string formattedDate = twoDaysAgo.ToString("yyyy-MM-ddTHH:mm:ssZ");

                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, "https://quickbooks.api.intuit.com/v3/company/"+ CompanyCode + "/query?query= select * from Payment where MetaData.CreateTime >= '" + formattedDate + "'&minorversion=69");
                request.Headers.Add("Authorization", "Bearer " + token + "");
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var tokenResponse = await response.Content.ReadAsStringAsync();
                string jsonString = ConvertXmlToJson(tokenResponse);
                obj = JsonConvert.DeserializeObject<PaymentModel>(jsonString);
                await commonRepository.InsertPaymentData(connectionString, obj, CompanyName);


                var client1 = new HttpClient();
                var request1 = new HttpRequestMessage(HttpMethod.Get, "https://quickbooks.api.intuit.com/v3/company/" + CompanyCode + "/query?query= select * from invoice where MetaData.CreateTime >= '" + formattedDate + "'&minorversion=69");
                request1.Headers.Add("Authorization", "Bearer " + token + "");
                var response1 = await client1.SendAsync(request1);
                response1.EnsureSuccessStatusCode();
                var tokenResponse1 = await response1.Content.ReadAsStringAsync();
                string jsonString1 = ConvertXmlToJson(tokenResponse1);
                InvoiceModel objInvoice = JsonConvert.DeserializeObject<InvoiceModel>(jsonString1);
                await commonRepository.InsertInvoiceData(connectionString, objInvoice, CompanyName);
            }
            catch (Exception ex)
            {
                string code = ((HttpRequestException)ex).StatusCode.ToString();
                commonRepository.InsertErrorLog(connectionString, "GetPayment", code, ex.Message);
                throw;
            }
            return "successfully";
        }

        static string ConvertXmlToJson(string xmlString)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlString);
            string jsonString = JsonConvert.SerializeXmlNode(xmlDoc, Newtonsoft.Json.Formatting.Indented);
            return jsonString;
        }


        [HttpGet]
        [Route("callbackMethod")]
        public string callbackMethod()
        {
            string data = "test";
            code = HttpContext.Request.Query["code"];
            string realmId = HttpContext.Request.Query["realmId"];
            return data;
        }

    }
}
