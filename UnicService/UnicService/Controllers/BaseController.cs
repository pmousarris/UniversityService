using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ModelUtil;
using ModelUtil.Logger;
using System.Data;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using UnicService.ViewModels.Login;

namespace UnicService.Controllers
{
    public abstract class BaseController : Controller
    {
        private IHttpContextAccessor _httpContextAccessor = null;
        private readonly IUniLogger _logger;

        protected BaseController(IUniLogger logger)
        {
            _logger = logger;
        }

        protected BaseController(IHttpContextAccessor httpContextAccessor, IUniLogger logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        protected void sendError(string message = null, Exception exception = null, Dictionary<string, string> parameters = null, Priority priority = Priority.Error, [CallerMemberName] string caller = null)
        {
            _logger.send(message: message, exception: exception, parameters: parameters, priority: priority, caller: caller);
        }

        protected UserClaim GetUserClaim()
        {
            return Util.GetUserClaim(_httpContextAccessor?.HttpContext?.User);
        }
    }
}
