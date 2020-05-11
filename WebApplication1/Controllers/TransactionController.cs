using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.Services;

namespace WebApplication1.Controllers
{
    [Route("api")]
    [ApiController]
    public class TransactionController : Controller
    {
        // GET: api/Transaction
        [HttpPost]
        [Route("accounts/addfunds")]
        public IActionResult AddFunds([FromBody]Transaction transaction)
        {
            TransactionService transactionService = new TransactionService();
            transactionService.AddFunds(transaction);
            return Ok();
        }

        // GET: api/Transaction/5
        [HttpGet]
        [Route("accounts/{id}")]
        public IActionResult GetAccount(int id)
        {
            TransactionService transactionService = new TransactionService();
            var accounts = transactionService.GetBalance(id);
            return Json(accounts);
        }

        // POST: api/Transaction
        [HttpPost]
        [Route("accounts/movefunds")]
        public IActionResult Post([FromBody] Transaction transaction)
        {
            TransactionService transactionService = new TransactionService();
            transactionService.MoveFunds(transaction);
            return Ok();
        }

        // PUT: api/Transaction/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
