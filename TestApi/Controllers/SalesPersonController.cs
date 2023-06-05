using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestApi.Contracts;
using TestApi.Models;

namespace TestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SalesPersonController : ControllerBase
    {
        private readonly ISalesPersonsRepository _salesPersonsRepository;
        public SalesPersonController(ISalesPersonsRepository salesPersonsRepository)
        {
            _salesPersonsRepository = salesPersonsRepository;
        }


        // GET: api/SalesPerson
        [HttpGet]
        public IEnumerable<SalesPerson> GetSalesPersons()
        {
            return  _salesPersonsRepository.GetAll();
        }

        // GET: api/SalesPerson/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SalesPerson>> GetSalesPerson(int id)
        {
            var salesPerson = await _salesPersonsRepository.Find(id);

            if (salesPerson == null)
            {
                return NotFound();
            }

            return salesPerson;
        }

        // PUT: api/SalesPerson/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSalesPerson(int id, SalesPerson salesPerson)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != salesPerson.SalesPersonId)
            {
                return BadRequest();
            }

            await _salesPersonsRepository.Update(salesPerson);

            return NoContent();
        }

        // POST: api/SalesPerson
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SalesPerson>> PostSalesPerson(SalesPerson salesPerson)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _salesPersonsRepository.Add(salesPerson);

            return CreatedAtAction("GetSalesPerson", new { id = salesPerson.SalesPersonId }, salesPerson);
        }

        // DELETE: api/SalesPerson/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalesPerson(int id)
        {


            var salesPerson = await _salesPersonsRepository.Find(id);
            if (salesPerson == null)
            {
                return NotFound();
            }

            await _salesPersonsRepository.Remove(id);

            return NoContent();
        }

        private async Task<bool> SalesPersonExists(int id)
        {
            return await _salesPersonsRepository.IsExists(id);
        }
    }
}
