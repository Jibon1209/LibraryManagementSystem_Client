using LibraryManagementSystem_Client.Helper;
using LibraryManagementSystem_Client.Models;
using LibraryManagementSystem_Client.Models.DTOS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace LibraryManagementSystem_Client.Controllers
{
    [Authorize]
    public class BorrowedBookController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7118/api/");
        private readonly HttpClient _client;

        public BorrowedBookController(HttpClient httpClient)
        {
            _client = httpClient;
            _client.BaseAddress = baseAddress;
        }
        [HttpGet]
        public IActionResult Index()
        {
            ServiceResponse<IEnumerable<BorrowedBook>> bookList = new ServiceResponse<IEnumerable<BorrowedBook>>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "BorrowedBook/GetAll").Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                bookList = JsonConvert.DeserializeObject<ServiceResponse<IEnumerable<BorrowedBook>>>(result);
            }
            return View(bookList.Data);
        }
        [HttpGet]
        public IActionResult Create()
        {
            BookList();
            MemberList();

            return View();
        }
        [HttpPost]
        public IActionResult Create(BorrowedBook borrowedBook)
        {
            string result = JsonConvert.SerializeObject(borrowedBook);
            StringContent content = new StringContent(result, Encoding.UTF8, "application/json");
            HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "BorrowedBook/Create", content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View();
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            BookList();
            MemberList();
            ServiceResponse<BorrowedBook> book = new ServiceResponse<BorrowedBook>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "BorrowedBook/GetById/get/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                book = JsonConvert.DeserializeObject<ServiceResponse<BorrowedBook>>(result);
            }
            return View(book.Data);

        }
        [HttpPost]
        public IActionResult Edit(BorrowedBook borrowedBook)
        {
            string result = JsonConvert.SerializeObject(borrowedBook);
            StringContent content = new StringContent(result, Encoding.UTF8, "application/json");
            HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "BorrowedBook/Update", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            ServiceResponse<BorrowedBook> book = new ServiceResponse<BorrowedBook>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "BorrowedBook/GetById/get/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                book = JsonConvert.DeserializeObject<ServiceResponse<BorrowedBook>>(result);
            }
            return View(book.Data);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "BorrowedBook/Delete/Delete/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        public void BookList()
        {
            ServiceResponse<IEnumerable<BookDTO>> bookList = new ServiceResponse<IEnumerable<BookDTO>>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "Book/GetBookList").Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine("Response result: " + result);
                bookList = JsonConvert.DeserializeObject<ServiceResponse<IEnumerable<BookDTO>>>(result);
            }
            ViewBag.Books = bookList?.Data ?? new List<BookDTO>();
        }
        public void MemberList()
        {
            ServiceResponse<IEnumerable<MemberDTO>> memberList = new ServiceResponse<IEnumerable<MemberDTO>>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "Member/GetMemberList").Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine("Response result: " + result);
                memberList = JsonConvert.DeserializeObject<ServiceResponse<IEnumerable<MemberDTO>>>(result);
            }
            ViewBag.Members = memberList?.Data ?? new List<MemberDTO>();
        }
    }
}
