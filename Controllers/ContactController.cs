using ContactProject.Models;
using ContactProject.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    public class ContactController : Controller
    {
        private readonly IContactInterface _contact;
        private readonly IUserInterface _userRepo;
        // GET: ContactController

        public ContactController(IContactInterface contact)
        {
            _contact = contact;
        }

        // public async Task<ActionResult> List()
        // {
        //     // if(HttpContext.Session.GetInt32("UserId")  != null)
        //     // {
        //     //     List<t_Contact> contacts = await _contact.GetAllByUser(HttpContext.Session.GetInt32("UserId").ToString());
        //     //     return View(contacts);
        //     // }
        //      List<t_Contact> contacts = await _contact.GetAllByUser(HttpContext.Session.GetInt32("UserId").ToString());
        //        return View(contacts);
        //     // else
        //     // {
        //     //   //  return View(contacts);
        //     //       return RedirectToAction("Index", "Home");
        //     // }
        // }

        public async Task<ActionResult> List()
        {
            var contacts = await _contact.GetAll();
            return View(contacts);
        }
        // public async Task< ActionResult> Index()
        // {
        //     return RedirectToAction();
        // }

        [HttpGet]
        public async Task<ActionResult> Create(string id = "")
        {
            t_Contact contact = new t_Contact();
            if (id != "")
            {
                contact = await _contact.GetOne(id);

            }
            return View(contact);
        }

        [HttpPost]

        public async Task<ActionResult> Create(t_Contact contact)
        {
            if (ModelState.IsValid)
            {
                if (HttpContext.Session.GetInt32("UserId") != null)
                {

                }
                    contact.c_UserId = 0;
                    var result = 0;

                    if (contact.c_ContactId == 0)
                    {
                        result = await _contact.Add(contact);
                    }
                    else
                    {
                        result = await _contact.Update(contact);
                    }

                    if (result == 0)
                    {
                        TempData["Message"] = "There is some error while adding or updating the contact";
                        return RedirectToAction("List", "Contact");
                    }
                    else
                    {
                        return RedirectToAction("List", "Contact");
                    }
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            return View();
        }
        
        [HttpGet]
        public async Task<ActionResult> Delete(string id)
        {
            int status = await _contact.Delete(id);

            if (status == 1)
            {
                ViewData["Message"] = "Contact Added Succesfully";
                return RedirectToAction("List");
            }
            else
            {
                ViewData["Message"] = "There is Some Error while Delete Contact";
                return RedirectToAction("List");
            }
        }
        
        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("List");
            }

            var contact = await _contact.GetOne(id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }


        [HttpPost]
        public async Task<ActionResult> Edit(t_Contact contact)
        {
            if (ModelState.IsValid)
            {
                var existingContact = await _contact.GetOne(contact.c_ContactId.ToString());

                if (existingContact != null)
                {
                    existingContact.c_ContactName = contact.c_ContactName;
                    existingContact.c_Email = contact.c_Email;
                    existingContact.c_Group = contact.c_Group;
                    existingContact.c_Address = contact.c_Address;
                    existingContact.c_Mobile = contact.c_Mobile;
                    existingContact.c_Status = contact.c_Status;

                    int result = await _contact.Update(existingContact);
                    if (result == 1)
                    {
                        TempData["Message"] = "Contact updated successfully!";
                        return RedirectToAction("List");
                    }
                }
                TempData["Message"] = "Error updating contact!";
            }
            return View(contact);
        }



    }

}
