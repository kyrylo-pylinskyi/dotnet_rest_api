using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using My_Rest_API.Domain;
using My_Rest_API.Repository;

namespace My_Rest_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly DataContext _context;

        public PersonController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Person>>> GetPersons()
        {
            return Ok(await _context.Persons.ToListAsync());
        }

        [HttpGet("top")]
        public async Task<ActionResult<List<Person>>> GetTop()
        {
            return Ok(_context.Persons.Take(10));
        }

        [HttpGet("id/{id}")]
        public async Task<ActionResult<Person>> GetPersonById(int id)
        {
            var person = await _context.Persons.FindAsync(id);
            if (person == null)
                return BadRequest("Person not found.");
            return Ok(person);
        }

        [HttpGet("firstname/{firstName}")]
        public async Task<ActionResult<List<Person>>> GetPersonByFirstName(string firstName)
        {
            var persons = await _context.Persons.Where(p => p.FirstName.Contains(firstName)).ToListAsync();

            if (persons == null)
                return BadRequest("Person not found.");
            return Ok(persons);
        }

        [HttpGet("lastname/{lastName}")]
        public async Task<ActionResult<List<Person>>> GetPersonByLastName(string lastName)
        {
            var persons = await _context.Persons.Where(p => p.LastName.Contains(lastName)).ToListAsync();

            if (persons == null)
                return BadRequest("Person not found.");
            return Ok(persons);
        }

        [HttpPost]
        public async Task<ActionResult<List<Person>>> AddPerson(Person person)
        {
            _context.Persons.Add(person);
            await _context.SaveChangesAsync();

            return Ok(await _context.Persons.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Person>>> UpdatePerson(Person request)
        {
            var person = await _context.Persons.FindAsync(request.Id);
            if (person == null)
                return BadRequest("Person not found.");

            person.FirstName = request.FirstName;
            person.FirstName = request.FirstName;
            person.LastName = request.LastName;
            person.BirthDate = request.BirthDate;

            await _context.SaveChangesAsync();

            return Ok(await _context.Persons.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Person>>> DeletePerson(int id)
        {
            var dbHero = await _context.Persons.FindAsync(id);
            if (dbHero == null)
                return BadRequest("Person not found.");

            _context.Persons.Remove(dbHero);
            await _context.SaveChangesAsync();

            return Ok(await _context.Persons.ToListAsync());
        }

    }
}