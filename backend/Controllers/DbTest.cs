using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Models; // Zakładając, że Twoje modele są w namespace backend.Models

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DbTest : ControllerBase
    {
        private readonly DataContext _context;

        public DbTest(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pracownik>>> GetEmployees()
        {
            // Pobierz wszystkich pracowników z bazy danych
            var employees = await _context.Pracownicy.ToListAsync();

            if (employees == null || employees.Count == 0)
            {
                // Jeśli nie ma pracowników w bazie danych, zwróć odpowiedni komunikat
                return NotFound("Brak pracowników w bazie danych.");
            }

            // Jeśli są pracownicy, zwróć ich listę
            return Ok(employees);
        }
    }
}
