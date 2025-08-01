using Microsoft.AspNetCore.Mvc;

namespace EmployeeApi.Controllers
{
    public class BaseController : ControllerBase
    {
        protected IActionResult HandleResponse<T>(T result)
        {
            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
