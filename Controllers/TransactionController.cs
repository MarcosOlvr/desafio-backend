using desafio_backend.Models;
using desafio_backend.Repository.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace desafio_backend.Controllers
{
    [Route("api")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepo;

        public TransactionController(ITransactionRepository transactionRepository)
        {
            _transactionRepo = transactionRepository;
        }

        [HttpGet("transactions")]
        public ActionResult AllTransactions()
        {
            var transaction = _transactionRepo.GetAllTransactions();

            return Ok(transaction);
        }
        
        [HttpGet("transaction/{id:int}")]
        public ActionResult GetTransaction(int id)
        {
            var transaction = _transactionRepo.GetTransaction(id);

            if (transaction == null)
            {
                return NotFound();
            }

            return Ok(transaction);
        }

        [HttpPost("transaction")]
        public ActionResult CreateTransaction(Transaction model) 
        {
            if (!ModelState.IsValid)
                return BadRequest(model);

            var transaction = _transactionRepo.AddTransaction(model);

            return StatusCode(201, transaction);
        }
    }
}
