using Microsoft.AspNetCore.Mvc;
using backend.Models; 

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PracownikController : ControllerBase
    {
        private readonly DataContext _context;

        public PracownikController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pracownik>>> GetEmployees()
        {

            var employees = await _context.Pracownicy.ToListAsync();
            if (employees == null || employees.Count == 0)
            {
                return NotFound("Brak pracowników w bazie danych.");
            }
            return Ok(employees);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Pracownik>> GetEmployee(int id)
        {
            var employee = await _context.Pracownicy.FindAsync(id);

            if (employee == null)
            {
                return NotFound("Nie znaleziono pracownika o podanym ID.");
            }

            return Ok(employee);
        }

        
        [HttpGet("Exists/{id}")]
        public bool EmployeeExists(int id)
        {
            return _context.Pracownicy.Any(e => e.Id == id);
        }

        [HttpPost]
        public async Task<ActionResult<Pracownik>> AddEmployee([FromBody] Pracownik employee)
        {
            if (employee == null)
            {
                return BadRequest("Nieprawidłowe dane pracownika.");
            }

            _context.Pracownicy.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee);
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.Pracownicy.FindAsync(id);
            if (employee == null)
            {
                return NotFound("Nie znaleziono pracownika o podanym ID.");
            }

            _context.Pracownicy.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}
