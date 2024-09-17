using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace webIti.Models;

public class instructor
{
    public int  id { get; set; } 
    // by def this is a primary key, but if it has another name you must set an attribute on it 
    
    public string  Name  { get; set; }
    [Display(Name = "Instructor Salary")]
    //[DataType(DataType.Password)]    // affect where you use editor for
    // [DataType(DataType.EmailAddress)]
    // [DataType(DataType.PhoneNumber)]
   // [Range(10000, 130000, ErrorMessage = "The Salary Must Between 10K and 130k")]
    [Remote("checkSalary", "Instructor", ErrorMessage = "Must more than 12k")]
    public int Salary { get; set; }
    public string?  Address { get; set; }
    public string  Image  { get; set; }

    [ForeignKey("Department")]
    public int  DeptId { get; set; }
    public virtual newDepartment?  Department { get; set; }

    [ForeignKey("course")]
    public int  CourseId { get; set; }
    public virtual Course? Course { get; set; }
}