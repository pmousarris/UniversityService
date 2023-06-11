using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using ModelUtil;
using UnicService.Controllers;
using UnicService.ViewModels.Login;

namespace UnicService.Filters
{
    public class ExcludeFilterAttribute : Attribute
    {
    }

    public class AccessActionFilter : IAsyncActionFilter
    {
        private static Dictionary<UserRole, List<string>> UserRoleAccessDic = new Dictionary<UserRole, List<string>>
            {
                { UserRole.Student, new List<string> { nameof(StudentController), nameof(ClassController) } },
                { UserRole.Lecturer, new List<string> { nameof(StudentController), nameof(ClassController) } }
            };

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Check if the action has ExcludeFilterAttribute.
            if (context.ActionDescriptor.EndpointMetadata.Any(meta => meta is ExcludeFilterAttribute))
            {
                await next();
                return;
            }

            // Execute filter logic.
            UserClaim userClaim = Util.GetUserClaim(context.HttpContext.User);
            var controllerName = (string)context.RouteData.Values["controller"] + "Controller";

            if (!HasAccess(userClaim.UserRole, controllerName))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            await next();
        }

        private bool HasAccess(UserRole role, string controllerName)
        {
            if (UserRoleAccessDic.ContainsKey(role))
            {
                return UserRoleAccessDic[role].Contains(controllerName);
            }

            return false;
        }
    }
}
