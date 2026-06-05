using FoodOrdering.Common.Models;
using FoodOrdering.API.Utilities;
using FoodOrdering.Service.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace FoodOrdering.API.Controllers
{
    /// <summary>
    /// Handles user authentication and authorization operations.
    /// Provides APIs for user login and retrieving current user details.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtHelper _jwtHelper;
        private readonly IUserService _userService;

        /// <summary>
        /// Initializes a new instance of the AuthController class.
        /// </summary>
        /// <param name="jwtHelper">JWT helper used for token generation.</param>
        /// <param name="userService">User service used for user-related operations.</param>
        public AuthController(
            JwtHelper jwtHelper,
            IUserService userService)
        {
            _jwtHelper = jwtHelper;
            _userService = userService;
        }

        /// <summary>
        /// Authenticates the user using email and password.
        /// Generates and returns a JWT token upon successful login.
        /// </summary>
        /// <param name="request">Login request containing email and password.</param>
        /// <returns>JWT token and user details if authentication is successful.</returns>
        [HttpPost]
        [Route("LoginAsync")]
        public async Task<IActionResult> LoginAsync(
            LoginRequest request)
        {
            try
            {
                var user =
                    await _userService.GetUserByEmail(
                        request.Email);

                if (user == null)
                {
                    return Unauthorized(
                        new ApiResponse<object>
                        {
                            StatusCode = 401,
                            Message = "Invalid Credentials",
                            Data = null
                        });
                }

                bool isValidPassword =
                    BCrypt.Net.BCrypt.Verify(
                        request.Password,
                        user.PasswordHash);

                if (!isValidPassword)
                {
                    return Unauthorized(
                        new ApiResponse<object>
                        {
                            StatusCode = 401,
                            Message = "Invalid Credentials",
                            Data = null
                        });
                }

                string role =
                    user.RoleId == 1
                    ? "Admin"
                    : "User";

                var token =
                    _jwtHelper.GenerateToken(
                        user.UserGuid,
                        role);

                return Ok(new ApiResponse<object>
                {
                    StatusCode = 200,
                    Message = "Login Success",
                    Data = new
                    {
                        UserGuid = user.UserGuid,
                        UserName = user.UserName,
                        Email = user.Email,
                        Role = role,
                        Token = token
                    }
                });
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Auth Error - LoginAsync: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves details of the currently authenticated user.
        /// Information is extracted from JWT token claims.
        /// </summary>
        /// <returns>Current user's UserGuid and Role.</returns>
        [Authorize]
        [HttpGet]
        [Route("CurrentUser")]
        public IActionResult CurrentUser()
        {
            return Ok(new ApiResponse<object>
            {
                StatusCode = 200,
                Message = "User Details Retrieved Successfully",
                Data = new
                {
                    UserGuid = User.FindFirst("UserGuid")?.Value,
                    Role = User.FindFirst(ClaimTypes.Role)?.Value
                }
            });
        }
    }
}