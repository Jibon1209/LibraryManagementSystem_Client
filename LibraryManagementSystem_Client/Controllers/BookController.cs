using LibraryManagementSystem_Client.Helper;
using LibraryManagementSystem_Client.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace LibraryManagementSystem_Client.Controllers
{
    [Authorize]
    public class BookController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7118/api/");
        private readonly HttpClient _client;

        public BookController(HttpClient httpClient)
        {
            _client = httpClient;
            _client.BaseAddress = baseAddress;
        }
        [HttpGet]
        public IActionResult Index()
        {
            ServiceResponse<IEnumerable<Book>> bookList = new ServiceResponse<IEnumerable<Book>>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "Book/GetAll").Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                bookList = JsonConvert.DeserializeObject<ServiceResponse<IEnumerable<Book>>>(result);
            }
            return View(bookList.Data);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ComboBox();

            return View();
        }
        [HttpPost]
        public IActionResult Create(Book book)
        {
            string result = JsonConvert.SerializeObject(book);
            StringContent content = new StringContent(result, Encoding.UTF8, "application/json");
            HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "Book/Create", content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View();
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ComboBox();
            ServiceResponse<Book> book = new ServiceResponse<Book>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "Book/GetById/get/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                book = JsonConvert.DeserializeObject<ServiceResponse<Book>>(result);
            }
            return View(book.Data);

        }
        [HttpPost]
        public IActionResult Edit(Book book)
        {
            string result = JsonConvert.SerializeObject(book);
            StringContent content = new StringContent(result, Encoding.UTF8, "application/json");
            HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "Book/Update", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            ServiceResponse<Book> book = new ServiceResponse<Book>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "Book/GetById/get/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                book = JsonConvert.DeserializeObject<ServiceResponse<Book>>(result);
            }
            return View(book.Data);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "Book/Delete/Delete/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        public void ComboBox()
        {
            ServiceResponse<IEnumerable<Author>> authorList = new ServiceResponse<IEnumerable<Author>>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "Author/GetAll").Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                authorList = JsonConvert.DeserializeObject<ServiceResponse<IEnumerable<Author>>>(result);
            }
            ViewBag.Authors = authorList?.Data ?? new List<Author>();
        }
    }
}
