using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProniaHomeTask.Helpers;

namespace ProniaHomeTask.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = AppRoles.Admin)]
    public abstract class AdminController : Controller
    {
    }
}
