using Backend.DTO.EmailDTO;
using Backend.Interfaces.IEmail;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers.EmailController
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        public IActionResult sendEmail(EmailResponse emailData)
        {
            try
            {
                _emailService.SendEmail(emailData);

                return Ok(new { message = "Email Sent!" });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { message = "Failed to send Email", error = ex.Message });
            }
        }
    }
}
