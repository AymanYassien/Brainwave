using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using webIti.Models;

namespace webIti.Controllers;

public class InstructorController : Controller
{
    public IActionResult checkSalary(int Salary)
    {
        if (Salary > 1200)
            return Json(true);
        return Json(false);
    }  // Remote attribute
    
    private SchoolDBContext context = new SchoolDBContext();
    
    public IActionResult Index()
    {
        List<instructor> instructorsModel = context.Instructors.ToList();
        return View(instructorsModel);
    }
    
    [HttpGet]
    public IActionResult New ()// open empty form 
    {
        // call in anchor tag from index => open new form 
        ViewData["deptList"] = context.Departments.ToList();
        ViewData["courseList"] = context.Courses.ToList();
        return View("new");
        //return View(new instructor());
    }
    // submit button
    [HttpPost]
    //[ValidateAntiForgeryToken] // tag helper
    public IActionResult saveNew(instructor inst)
    {
        
        if (ModelState.IsValid)
        {
            try
            {
                context.Instructors.Add(inst);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ModelState.AddModelError(String.Empty, e.Message);
                return View("new", inst);
            }
            
            
            
            //return View("index");// error null ref, you must send model to this page
            /*
             * how we can solve this ?
             *
             * can write code of index here again , but this lead to redundancy.
             * 
             * can call index()  , but this through error , this branch not return
             * view - response on request, just create new one- .
             *
             * 
             */

            
            // also can call actions in another view
            // can pass parameter here
        }
        
        ViewData["deptList"] = context.Departments.ToList();
        ViewData["courseList"] = context.Courses.ToList();
        return View("new", inst);
    }
    /*
     * to avoid any one try hack you through send data through url while you assign post to form method
     * add attribute on your action to serve only determined in form,
     * but be careful the attribute must match attribute in form
     *
     * can send post through form only,
     * and send get through form or tag - equal from url directly-
     */
    public IActionResult Details(int id )
    {

        instructor instructorModel = context.Instructors.FirstOrDefault(d => d.id == id);
        return View(instructorModel);
    }
    public IActionResult Edit(int id) // anchor tag => get inst data
    {
        instructor instModel = context.Instructors.FirstOrDefault(i => i.id == id);
        
        ViewData["deptList"] = context.Departments.ToList();
        ViewData["courseList"] = context.Courses.ToList();
        
        return View(instModel);

    }
    [HttpPost]
    public IActionResult saveEdit(int id, instructor newInst) // prefer send id separated
    {
        if (newInst.Name != null  && newInst.CourseId > 0 && newInst.DeptId > 0 )
        {

            instructor oldInstructor = context.Instructors.FirstOrDefault(i => i.id == id);

            if (oldInstructor != null)
            {
                oldInstructor.Name = newInst.Name;
                oldInstructor.Salary = newInst.Salary;
                oldInstructor.Image = newInst.Image;
                oldInstructor.Address = newInst.Address;
                oldInstructor.DeptId = newInst.DeptId;
                oldInstructor.CourseId = newInst.CourseId;
                
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            

            return RedirectToAction("Index");
            
        }

        ViewData["deptList"] = context.Departments.ToList();
        ViewData["courseList"] = context.Courses.ToList();
        return View("Edit", newInst);
            
    }
    public IActionResult Delete(int id) // anchor tag => get inst data
    {
        instructor instructor = context.Instructors.FirstOrDefault(i => i.id == id);
        context.Instructors.Remove(instructor);
        context.SaveChanges();
        
        return RedirectToAction("Index");

    }


    public IActionResult DetailsWithPartialView(int id)
    {
        instructor inst = context.Instructors.FirstOrDefault(d => d.id == id);
        return PartialView("Details", inst); // details with out layout 
    }
}