﻿using Asp.net_School.Data;
using Asp.net_School.Models;
using Asp.net_School.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Asp.net_School.Controllers
{
    public class Students : Controller
    {
        private readonly ApplicationDbContext appDbContext; //depandency injection

        public Students(ApplicationDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var students = await appDbContext.students.ToListAsync();
            return View(students);

        }

        [HttpGet]

        public IActionResult Add()
        {
            return View();
        }
        //ajax
        [HttpPost]
        public async Task<IActionResult> Add(AddStudentViewModel addStudentRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // return validation errors
            }

            var student = new Student()
            {
                Id = addStudentRequest.Id,
                Name = addStudentRequest.Name,
                Department = addStudentRequest.Department,
            };
            await appDbContext.students.AddAsync(student);
            await appDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> view(int id)
        {
            var student = await appDbContext.students.FirstOrDefaultAsync(x => x.Id == id);
            if ( student != null) 
            {
                var viewModel = new UpdateStudentViewModel()
                {
                    Id = student.Id,
                    Name = student.Name,
                    Department = student.Department

                };
                return await Task.Run(()=> View("View",viewModel));
            }
            
            return RedirectToAction("Index");

        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateStudentViewModel model)
        {
            var student = await appDbContext.students.FindAsync(model.Id);
            if (student != null)
            {
                student.Name = model.Name;
                student.Department = model.Department;
                await appDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        //[HttpPost]
        //public IActionResult Update(UpdateStudentViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Update student in database
        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        var errors = new Dictionary<string, string[]>();
        //        foreach (var key in ModelState.Keys)
        //        {
        //            var errorsList = ModelState[key].Errors.Select(e => e.ErrorMessage).ToArray();
        //            if (errorsList.Length > 0)
        //            {
        //                errors.Add(key, errorsList);
        //            }
        //        }
        //        return new JsonResult(errors) { StatusCode = 400 };
        //    }
        //}

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateStudentViewModel model)
        {
            var student =  await appDbContext.students.FindAsync(model.Id);
            if (student != null)
            {
                appDbContext.students.Remove(student);
                await appDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
