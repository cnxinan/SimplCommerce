using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimplCommerce.Module.Core.Services;
using CoreModels = SimplCommerce.Module.Core.Models;

namespace SimplCommerce.Module.ProjectZeroApi.Areas.User.Controllers
{
    [Area("PZAUser")]
    [Route("api/pza/user")]
    [Authorize]
    public class UserApiController : Controller
    {
        private readonly UserManager<CoreModels.User> _userManager;
        private readonly ITokenService _tokenService;

        public UserApiController(UserManager<CoreModels.User> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [HttpGet("userinfo")]
        public async Task<IActionResult> GetUserInfo()
        {
            var authHeader = HttpContext.Request.Headers["Authorization"];
            var token = authHeader.ToString().Replace("Bearer ", "");
            var principal = _tokenService.GetPrincipalFromExpiredToken(token);
            if (principal == null)
            {
                return BadRequest(new { Error = "Invalid token" });
            }

            var user = await _userManager.GetUserAsync(principal);
            var userRoles = await _userManager.GetRolesAsync(user);
            return Ok(new { eamil = user.Email, roles = userRoles });
        }
    }
}
