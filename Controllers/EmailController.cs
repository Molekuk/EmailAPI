using EmailAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using EmailAPI.Models;

namespace EmailAPI.Controllers
{
    [ApiController]
    [Route("api/")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;
        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("mails")]
        public async Task<IActionResult> Mails(Message message)
        {
            if (ModelState.IsValid)
            {
                var result = await _emailService.SendEmailAsync(message);
                if(!result.Succeed)
                {
                    string msg = "Errors:\n";
                    foreach (var err in result.Errors)
                    {
                        msg += err + "\n";
                    }
                    return StatusCode(500,msg);
                }


            }
            return Ok("Сообщения успешно отправлены");
        }

        [HttpGet("mails")]
        public async Task<IActionResult> Mails()
        {
                IEnumerable<SentMessage> messages;
                try
                {
                    messages  =  await _emailService.GetSentMessagesAsync();
                }
                catch (Exception ex)
                {
                    return StatusCode(500,"Error: "+ex.Message);
                }
            return Ok(messages);
        }
    }
}
