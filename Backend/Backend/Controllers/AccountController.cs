using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        // localhost:44374/api/Account

        private readonly BankingContext _db;

        public AccountController(BankingContext context)
        {
            _db = context;
        }

        
        [HttpPost]
        public IActionResult CreateAccount(Account account)
        {
            _db.Accounts.Add(account);
            _db.SaveChanges();

            return Ok(account);
        }

        [HttpGet]
        public IActionResult GetAccountByID(int id)
        {
            //Account account = _db.Accounts.SingleOrDefault(account => account.ID == id);
            Account account = _db.Accounts.Find(id);
            
            if(account == null)
            {
                return NotFound();
            }

            return Ok(account);
        }

        [HttpPut]

        public IActionResult UpdateAccount(Account account)
        {
            Account accountFromDb = _db.Accounts.Find(account.ID);

            if (accountFromDb == null)
            {
                return NotFound();
            }

            accountFromDb.firstName = account.firstName;
            accountFromDb.lastName = account.lastName;  
            accountFromDb.balance = account.balance;

            _db.SaveChanges();

            return Ok("Updated Account successfully");
        }

        [HttpDelete]
        public IActionResult DeleteAccount(int id)
        {
            Account accountFromDb = _db.Accounts.Find(id);

            if (accountFromDb == null)
            {
                return NotFound();
            }

            _db.Accounts.Remove(accountFromDb); 
            _db.SaveChanges();

            return Ok("Account deleted successfully");
        }
    }
}
