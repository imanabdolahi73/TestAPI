using Newtonsoft.Json;
using NuGet.Common;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace WebClient.Models
{
    public class CustomerRepository
    {
        private string apiUrl = "http://localhost:34978/Api/Customers";
        private HttpClient _client;
        
        public CustomerRepository()
        {
            _client = new HttpClient();
            
        }

        public List<Customer> GetAllCustomer(string token) 
        {
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",token);
            var result = _client.GetStringAsync(apiUrl).Result;

            List<Customer> customers = JsonConvert.DeserializeObject<List<Customer>>(result);

            return customers;
        }

        public Customer GetCustomerById(int id , string token)
        {
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var result = _client.GetStringAsync(apiUrl + "/" + id).Result;
            Customer customer = JsonConvert.DeserializeObject<Customer>(result);

            return customer;
        }

        public void AddCustomer(Customer customer , string token)
        {
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            string jsonCustomer = JsonConvert.SerializeObject(customer);
            StringContent content = new StringContent(jsonCustomer, Encoding.UTF8, "application/json");

            var result = _client.PostAsync(apiUrl, content).Result;
        }

        public void UpdateCustomer(Customer customer, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            string jsonCustomer = JsonConvert.SerializeObject(customer);
            StringContent content = new StringContent(jsonCustomer, Encoding.UTF8, "application/json");

            var result = _client.PutAsync(apiUrl+"/"+customer.CustomerId, content).Result;
        }

        public void DeleteCustomer(int id, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var result = _client.DeleteAsync(apiUrl + "/" + id).Result;
        }
    }

    public class Customer
    {
        public int CustomerId { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? Address { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? ZipCode { get; set; }

    }
}
