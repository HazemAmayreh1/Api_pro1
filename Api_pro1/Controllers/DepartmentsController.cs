using Api_pro1.Data;
using Api_pro1.DTOs;
using Api_pro1.DTOs.Departments;
using Api_pro1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_pro1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public DepartmentsController (ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var dep = context.departments.Select(
                x=>new DepartmentGetAllDot()
                {
                    Id=x.Id,
                    Name=x.Name,
                }
                );
           

            return Ok(dep);
        }
        
        [HttpGet("GetDetails")]
        public IActionResult GetById(int id)
        {
            var dep = context.departments
            .Where(x => x.Id == id)
            .Select(x => new GetDepartmentByIdDot
            {
             Id = x.Id,
             Name = x.Name
            })
            .FirstOrDefault();

            if (dep == null)
            {
                return NotFound("Department not found!!!");
            }
            return Ok(dep);
        }

        [HttpPost("Create")]
        public IActionResult create(CreateDepartmentDot depDto)
        {
            Department dep = new Department()
            {
                Name = depDto.Name
            };
            context.departments.Add(dep);
            context.SaveChanges();
            return Ok(dep);
        }

        [HttpPut("update")]
        public IActionResult update(UpdateDepartmentDto depDto, int id)
        {
            var cureent = context.departments.Find(id);
            if (cureent is null)
            {
                return NotFound("departments is not found!!");
            }
            cureent.Name = depDto.Name;

            context.SaveChanges();

            return Ok();
        }

        [HttpDelete("Remove")]
        public IActionResult Remove(int id)
        {
            var dep = context.departments.Find(id);
            if (dep == null)
            {
                return NotFound("departments is not found!!");
            }
            context.Remove(dep);
            context.SaveChanges();

            return Ok();
        }
    }
}
