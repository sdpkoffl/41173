using GSMS.Interface;
using GSMS.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace GSMS.Controllers
{
    public class UserController : Controller
    {
        private readonly IUser _user;
        private readonly IPerson _person;
        public UserController(IUser tempUser , IPerson tempPerson)
        {
            this._user = tempUser;
            this._person = tempPerson;
            //gridMvcHelper = new GridMVCAjaxDemo.Helpers.GridMvcHelper();
        }

        public ActionResult AddUser()
        {
            List<User> _out = new List<User>();
            HttpResponseMessage result = _user.getUser();

            if (result.IsSuccessStatusCode)
            {
                var _collegeResponse = result.Content.ReadAsStringAsync().Result;
                _out = JsonConvert.DeserializeObject<List<User>>(_collegeResponse);
            }
            List<PersonType> _out1 = new List<PersonType>();
            List<Person> _outPersons = new List<Person>();
            

            HttpResponseMessage result2 = _person.getPerson();

            if (result2.IsSuccessStatusCode)
            {
                var _collegeResponse = result2.Content.ReadAsStringAsync().Result;
                _out1 = JsonConvert.DeserializeObject<List<PersonType>>(_collegeResponse);
            }
            if (_out1 != null && _out1.Count > 0)
            {
                foreach(var item in _out1)
                {
                    Person _outPerson = new Person();
                    _outPerson = item.Person;
                    _outPersons.Add(_outPerson);
                }

                ViewBag.PersonId = _out1;
                ViewBag.Person = _outPersons;

            }
            return View(_out);
        }

        public JsonResult SaveUser(User data)
        {
            try
            {
                var cc = _user.PostUser(data);
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public JsonResult DeleteUser(User data)
        {
            try
            {
                var cc = _user.Delete((int)data.UserId);
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw;
            }

        }

       
    }
}