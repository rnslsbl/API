using Client.Models;
using Client.Repositories.Data;
using Client.Repositories.Interface;
using Client.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository repository;

        public EmployeeController(IEmployeeRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var result = await repository.Get();
            var employees = new List<Employee>();

            if (result.Data != null)
            {
                employees = result.Data?.Select(e => new Employee
                {
                    Guid = e.Guid,
                    NIK = e.NIK,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    BirthDate = e.BirthDate,
                    Gender= e.Gender,
                    HiringDate = e.HiringDate,
                    Email=e.Email,
                    PhoneNumber = e.PhoneNumber,
                }).ToList();
            }

            return View(employees);
        }

        public async Task<IActionResult> GetAllEmployee()
        {
            var result = await repository.GetAllEmployee();
            var getAllEmployee = new List<GetAllEmployeeVM>();

            if (result.Data != null)
            {
                getAllEmployee = result.Data.Select(e => new GetAllEmployeeVM
                {
                    guid = e.guid,
                    NIK = e.NIK,
                    FullName = e.FullName,
                    BirthDate = e.BirthDate,
                    Gender = e.Gender,
                    HiringDate = e.HiringDate,
                    Email = e.Email,
                    PhoneNumber = e.PhoneNumber,
                    Major = e.Major,
                    Degree = e.Degree,
                    GPA = e.GPA,
                    UniversityName = e.UniversityName,
                }).ToList();
            }
            return View(getAllEmployee);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        /*[ValidateAntiForgeryToken]*/
        public async Task<IActionResult> Create(Employee employee)
        {
            /*if (ModelState.IsValid)
            {*/
            //daaru general repo
            var result = await repository.Post(employee);
            if (result.StatusCode == 200)
            {
                return RedirectToAction(nameof(Index));
            }
            else if (result.StatusCode == 409)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View();
                /* }*/
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        /*[ValidateAntiForgeryToken]*/
        public async Task<IActionResult> Edit(Employee employee)
        {
            /*if (ModelState.IsValid)
            {*/
            var result = await repository.Put(employee);
            if (result.StatusCode == 200)
            {
                return RedirectToAction(nameof(Index));
            }
            else if (result.StatusCode == 409)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View();
                /*}*/
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Guid Guid)
        {
            var result = await repository.Get(Guid);
            var employee= new Employee();
            if (result.Data?.Guid is null)
            {
                return View(employee);
            }
            else
            {
                employee.Guid = result.Data.Guid;
                employee.NIK = result.Data.NIK;
                employee.FirstName = result.Data.FirstName;
                employee.LastName = result.Data.LastName;
                employee.BirthDate = result.Data.BirthDate;
                employee.Gender = result.Data.Gender;
                employee.HiringDate = result.Data.HiringDate;
                employee.Email = result.Data.Email;
                employee.PhoneNumber = result.Data.PhoneNumber;
            }

            return View(employee);
        }

        public async Task<IActionResult> Deletes(Guid Guid)
        {
            var result = await repository.Get(Guid);
            var employee = new Employee();
            if (result.Data?.Guid is null)
            {
                return View(employee);
            }
            else
            {
                employee.Guid = result.Data.Guid;
                employee.NIK = result.Data.NIK;
                employee.FirstName = result.Data.FirstName;
                employee.LastName = result.Data.LastName;
                employee.BirthDate = result.Data.BirthDate;
                employee.Gender = result.Data.Gender;
                employee.HiringDate = result.Data.HiringDate;
                employee.Email = result.Data.Email;
                employee.PhoneNumber = result.Data.PhoneNumber;
            }
            return View(employee);
        }
        [HttpPost]
        /* [ValidateAntiForgeryToken]*/
        public async Task<IActionResult> Remove(Guid Guid)
        {
            var result = await repository.Deletes(Guid);
            if (result.StatusCode == 200)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
