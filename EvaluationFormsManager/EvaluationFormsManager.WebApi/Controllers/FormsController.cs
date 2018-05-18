using EvaluationFormsManager.Shared;
using Microsoft.AspNetCore.Mvc;
using System;

namespace EvaluationFormsManager.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class FormsController : Controller
    {
        private readonly IFormService formService;

        public FormsController(IFormService formService)
        {
            this.formService = formService ?? throw new ArgumentNullException(nameof(formService));
        }

        // GET: api/forms?id=1
        [HttpGet(Name = "GetAllForms")]
        public JsonResult Get(string id)
        {
            if (id == null)
            {
                return Json(new { status = 400, error = "No User identifier provided." });
            }

            int userId = int.Parse(id);

            return Json(formService.GetAllFormsCreatedBy(userId));
        }

        // GET: api/forms/5?id=1
        [HttpGet("{formId}", Name = "GetForm")]
        public JsonResult Get(int formId, string id)
        {
            if (id == null)
            {
                return Json(new { status = 400, error = "No User identifier provided." });
            }

            int userId = int.Parse(id);

            var result = formService.GetForm(formId);

            if (result == null)
            {
                return Json(new { status = 404, error = "Form not found." });
            }

            return Json(result);
        }
    }
}
