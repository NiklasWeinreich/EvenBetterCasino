using Backend.Interfaces.IUser;
using Backend.Services.UserService;

namespace Backend.Authentication
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUserRepository userRepository, IJwtUtils jwtUtils)
        {
            string token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            int? Id = jwtUtils.ValidateJwtToken(token);
            if (Id is not null)
            {
                //Attach account to context on succesful jwt validation
                var user = await userRepository.GetUserByIdAsync(Id.Value);
                if (user != null)
                {
                    context.Items["User"] = UserService.MapEntityToResponse(user);
                }
            }

            await _next(context);
        }
    }
}
