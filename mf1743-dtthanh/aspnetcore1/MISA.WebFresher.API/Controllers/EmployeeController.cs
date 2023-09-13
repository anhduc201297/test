using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher.Application;
using MISA.WebFresher.Domain;

namespace MISA.WebFresher.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeeController : BaseController<Employee>
    {
        private readonly IEmployeeRepository employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository) : base(employeeRepository)
        {
            this.employeeRepository = employeeRepository;   
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployeeAsync(Employee employee)
        {
            var result = await employeeRepository.CreateEmployeeAsync(employee);
            return Ok(result);
        }


        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateEmployeeAsync(Employee employee, Guid id)
        {
            var result = await employeeRepository.UpdateEmployeeAsync(employee, id);
            return Ok(result);
        }

    }
}
