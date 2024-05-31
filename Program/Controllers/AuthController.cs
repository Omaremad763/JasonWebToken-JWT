using jwt__dev_Creed.model;
using jwt__dev_Creed.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace jwt__dev_Creed.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuth_service _auth_Service;


        public AuthController(IAuth_service auth_Service)
        {
            _auth_Service = auth_Service;
        }


        [HttpPost(template: "register")]

        public async Task<IActionResult> registeration([FromBody]  register_model Rmodel)
        {
           if(!ModelState.IsValid) {

                return BadRequest(ModelState);}
       
            var result=await _auth_Service.register_async(Rmodel);

            if (!result.is_auth)
                return BadRequest(result.message);

            return Ok(result);
                
                }
        
        [HttpPost(template: "token")]

        public async Task<IActionResult> Get_token([FromBody] TokenRequest_model Tmodel)
        {
            if (!ModelState.IsValid)
            {

                return BadRequest(ModelState);
            }

            var result = await _auth_Service.Gettoken_async(Tmodel);

            if (!result.is_auth)
                return BadRequest(result.message);

            return Ok(result);

        }



    }
}
