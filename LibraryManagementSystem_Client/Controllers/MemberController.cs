using LibraryManagementSystem_Client.Helper;
using LibraryManagementSystem_Client.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace LibraryManagementSystem_Client.Controllers
{
    [Authorize]
    public class MemberController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7118/api/");
        private readonly HttpClient _client;

        public MemberController(HttpClient httpClient)
        {
            _client = httpClient;
            _client.BaseAddress = baseAddress;
        }
        [HttpGet]
        public IActionResult Index()
        {
            ServiceResponse<IEnumerable<Member>> memberList = new ServiceResponse<IEnumerable<Member>>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "Member/GetAll").Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                memberList = JsonConvert.DeserializeObject<ServiceResponse<IEnumerable<Member>>>(result);
            }
            return View(memberList.Data);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Member member)
        {
            string result = JsonConvert.SerializeObject(member);
            StringContent content = new StringContent(result, Encoding.UTF8, "application/json");
            HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "Member/Create", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ServiceResponse<Member> member = new ServiceResponse<Member>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "Member/GetById/get/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                member = JsonConvert.DeserializeObject<ServiceResponse<Member>>(result);
            }
            return View(member.Data);

        }
        [HttpPost]
        public IActionResult Edit(Member member)
        {
            string result = JsonConvert.SerializeObject(member);
            StringContent content = new StringContent(result, Encoding.UTF8, "application/json");
            HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "Member/Update", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            ServiceResponse<Member> member = new ServiceResponse<Member>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "Member/GetById/get/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                member = JsonConvert.DeserializeObject<ServiceResponse<Member>>(result);
            }
            return View(member.Data);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "Member/Delete/Delete/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
