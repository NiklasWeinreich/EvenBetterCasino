using Backend.DTO.BalanceDTO;
using Backend.Interfaces.IBalance;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers.BalanceController
{
    [Route("api/[controller]")]
    [ApiController]
    public class BalanceController : ControllerBase
    {

        private readonly IBalanceService _balanceService;

        public BalanceController(IBalanceService balanceService)
        {
            _balanceService = balanceService;
        }

        [HttpGet("{userId}/get")]
        public async Task<IActionResult> GetBalanceByUserId([FromRoute] int userId)
        {
            try
            {
                var currentBalance = await _balanceService.GetBalanceAsync(userId);

                BalanceResponse response = new BalanceResponse { Balance = currentBalance };

                if (response == null) 
                { 
                    return NotFound(); 
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost("{userId}/deposit")]
        public async Task<IActionResult> Deposit(int userId, [FromBody] BalanceRequest balanceRequest)
        {
            try
            {
                var newBalance = await _balanceService.DepositAsync(userId, balanceRequest.Amount);

                BalanceResponse response = new BalanceResponse { Balance = newBalance };

                if (response == null)
                { 
                    return NotFound(); 
                }

                return Ok(response);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);

            }
        }

        [HttpPost("{userId}/withdraw")]
        public async Task<IActionResult> Withdraw(int userId, [FromBody] BalanceRequest balanceRequest)
        {
            try
            {
                var newBalance = await _balanceService.WithdrawAsync(userId, balanceRequest.Amount);

                BalanceResponse response = new BalanceResponse { Balance = newBalance };

                if (response == null)
                { 
                    return NotFound(); 
                }

                return Ok(response);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{userId}/bet")]
        public async Task<IActionResult> PlaceBet(int userId, [FromBody] BalanceRequest balanceRequest)
        {
            try
            {

                var newBalance = await _balanceService.PlaceBetAsync(userId, balanceRequest.Amount);

                BalanceResponse response = new BalanceResponse { Balance = newBalance };

                if(response == null)
                {
                    return NotFound();
                }

                return Ok(response);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{userId}/win")]
        public async Task<IActionResult> Win(int userId, [FromBody] BalanceRequest balanceRequest)
        {
            try
            {

                var newBalance = await _balanceService.PlaceBetAsync(userId, balanceRequest.Amount);

                BalanceResponse response = new BalanceResponse { Balance = newBalance };

                if (response == null)
                {
                    return NotFound();
                }

                return Ok(response);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

    }
}
