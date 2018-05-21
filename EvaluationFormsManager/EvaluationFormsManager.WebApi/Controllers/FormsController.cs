using EvaluationFormsManager.Domain;
using EvaluationFormsManager.Shared;
using EvaluationFormsManager.WebApi.Middleware;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

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

        // GET: api/forms?userId=1
        [HttpGet]
        [ValidateUserId]
        public IActionResult Get([FromQuery]string userId)
        {
            List<Form> forms = formService.GetAllForms().ToList();

            return Ok(forms);
        }

        // GET: api/forms/1?userId=1
        [HttpGet("{formId}")]
        [ValidateUserId]
        public IActionResult Get([FromQuery]string userId, string formId)
        {
            if (!int.TryParse(formId, out int internalFormId))
            {
                return BadRequest(new { status = 400, error = "No valid Form Identifier provided." });
            }

            Form forms = formService.GetForm(internalFormId);

            return Ok(forms);
        }
    }
}
