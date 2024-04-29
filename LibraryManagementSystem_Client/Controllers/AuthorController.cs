using LibraryManagementSystem_Client.Helper;
using LibraryManagementSystem_Client.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace LibraryManagementSystem_Client.Controllers
{
    [Authorize]
    public class AuthorController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7118/api/");
        private readonly HttpClient _client;

        public AuthorController(HttpClient httpClient)
        {
            _client = httpClient;
            _client.BaseAddress = baseAddress;
        }
        [HttpGet]
        public IActionResult Index()
        {
            ServiceResponse<IEnumerable<Author>> authorList = new ServiceResponse<IEnumerable<Author>>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "Author/GetAll").Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                authorList = JsonConvert.DeserializeObject<ServiceResponse<IEnumerable<Author>>>(result);
            }
            return View(authorList.Data);
        }
        [HttpGet]
        public IActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Author author)
        {
            string result = JsonConvert.SerializeObject(author);
            StringContent content = new StringContent(result, Encoding.UTF8, "application/json");
            HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "Author/Create", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ServiceResponse<Author> author = new ServiceResponse<Author>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "Author/GetById/get/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                author = JsonConvert.DeserializeObject<ServiceResponse<Author>>(result);
            }
            return View(author.Data);

        }
        [HttpPost]
        public IActionResult Edit(Author author)
        {
            string result = JsonConvert.SerializeObject(author);
            StringContent content = new StringContent(result, Encoding.UTF8, "application/json");
            HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "Author/Update", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            ServiceResponse<Author> author = new ServiceResponse<Author>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "Author/GetById/get/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                author = JsonConvert.DeserializeObject<ServiceResponse<Author>>(result);
            }
            return View(author.Data);
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "Author/Delete/Delete/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
