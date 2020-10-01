using DotnetcorePoc.Models;
using DotnetcorePoc.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.IO;

namespace DotnetcorePoc.Controllers
{   // homecontroller inheritted from controller class to return json or view data
    public class HomeController : Controller
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IHostingEnvironment hostingEnvironment;

        public HomeController(IMemberRepository memberRepository, 
                                IHostingEnvironment hostingEnvironment)
        {
            _memberRepository = memberRepository;
            this.hostingEnvironment = hostingEnvironment;
        }

        // Displays the index/home page 
        public ViewResult Index()
        {
            return View();
        }

        // Displays Contact detail of the author 
        public ViewResult Contact()
        {
            return View();
        }

        // Displays the list of members
        public ViewResult List()
        {
            var model = _memberRepository.GetMembers();
            return View(model);
        }

        // Displays the detail info of a member
        public ViewResult Details(int Id)
        {
            // throw new Exception("Error in Details view!");
            Member member = _memberRepository.GetMember(Id);
            if(member == null)
            {
                Response.StatusCode = 404;
                // Calling ErrorController using relative file path
                return View("../Error/MemberNotFound", Id);
            }

            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Member = member,
                PageTitle = "Member Details"
            };
            return View(homeDetailsViewModel); 
        }
        
        // Displays legal matters related to the site
        public IActionResult Privacy()
        {
            return View();
        }

        // Displays the details of a member to be edited
        [HttpGet]
        public ViewResult Edit(int id)
        {
            Member member = _memberRepository.GetMember(id);
            MemberEditViewModel memberEditViewModel = new MemberEditViewModel
            {
                Id = member.Id,
                Name = member.Name,
                Email = member.Email,
                Department = member.Department,
                ExistingPhotoPath = member.PhotoPath
            };
            return View(memberEditViewModel);
        }

        // Posts the edited details of the member to the member repository
        [HttpPost]
        public IActionResult Edit(MemberEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Member member = _memberRepository.GetMember(model.Id);
                member.Name = model.Name;
                member.Email = model.Email;
                member.Department = model.Department;
                // checks if the photo has been changed
                if(model.Photo != null)
                {
                    // Deleting existing photo if exists
                    if (model.ExistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(hostingEnvironment.WebRootPath, "images", model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    member.PhotoPath = processUploadedFile(model);
                }
               
                _memberRepository.Update(member);
                return RedirectToAction("list");
            }
            return View();
        }

        /* Custom method to get the physical path of the image folder and its steps;
            // uploading the file in images folder in wwwroot folder
            // using Ihosting environment service of core to get the physical path of the wwwroot folder
            // Guid is used for unique filename
            // CopyTo method of IFormFile property of the photo
        */
        private string processUploadedFile(MemberRegisterViewModel model)
        {
            string uniqueFileName = null;
            if (model.Photo != null)
            {
                string uploadfolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(uploadfolder, uniqueFileName);
                // with "Using", "fileStream" will be disposed after executing the statements inside the braces
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        // Gets and Displays the member registration form
        [HttpGet]
        public ViewResult Register()
        {
            return View();
        }

        // Posts the new member details to the member repository
        // Both ViewResult and RedirectToActionResult implements the interface, IActionResult
        [HttpPost]
        public IActionResult Register(MemberRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = processUploadedFile(model);
                
                // create new member object to copy the properties from incoming object to new member. 
                Member newMember = new Member
                {
                    Name = model.Name,
                    Email = model.Email,
                    Department = model.Department,
                    PhotoPath = uniqueFileName
                };

                _memberRepository.Add(newMember);
                return RedirectToAction("details", new { id = newMember.Id });
            }
            return View();
        }

        /*
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        */
    }
}
