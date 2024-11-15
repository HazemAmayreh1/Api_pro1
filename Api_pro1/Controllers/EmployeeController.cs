using Api_pro1.Data;
using Api_pro1.DTOs.Employees;
using Api_pro1.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_pro1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public EmployeeController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var emp = context.employees.ToList();
            var response = emp.Adapt<IEnumerable<GetAllEmployeeDot>>();

            return Ok(response);
        }

        [HttpGet("GetDetails")]
        public IActionResult GetById(int id)
        {
            var emp = context.employees.Find(id);
            if (emp == null)
            {
                return NotFound("employee not found!!!");
            }
            var empDto = emp.Adapt<GetEmployeeByIdDto>();

            return Ok(empDto);
        }

        [HttpPost("Create")]
        public IActionResult Create(CreateEmployeeDto empDto)
        {
            var employee = empDto.Adapt<Employee>();
            context.employees.Add(employee);
            context.SaveChanges();
            return Ok();
        }

        [HttpPut("update")]
        public IActionResult update(updateEmployeesDto empDto,int id)
        {
            var cureent = context.employees.Find(id);
            if (cureent is null) 
            { 
                return NotFound("employee is not found!!");
            }
            cureent.Name= empDto.Name;
            cureent.Description= empDto.Description;

            context.SaveChanges();

            return Ok();
        }

        [HttpDelete("Remove")]
        public IActionResult Remove( int id)
        {
            var emp = context.employees.Find(id);
            if (emp == null)
            {
                return NotFound("employee is not found!!");
            }
            context.Remove(emp);
            context.SaveChanges();

            return Ok();
        }


    }
}
