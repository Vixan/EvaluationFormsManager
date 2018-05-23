using EvaluationFormsManager.Domain;
using EvaluationFormsManager.ErrorHandling;
using EvaluationFormsManager.Shared;
using EvaluationFormsManager.WebApi.Middleware;
using EvaluationFormsManager.WebApi.Models;
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
                return BadRequest(ErrorsDictionary.GetResultObject(ErrorCodes.ERR_FORM_ID_INVALID));
            }

            Form forms = formService.GetForm(internalFormId);

            return Ok(forms);
        }

        // GET: api/forms/owned?userId=1
        [HttpGet]
        [Route("Owned")]
        [ValidateUserId]
        public IActionResult GetOwned([FromQuery]string userId)
        {
            List<Form> forms = formService.GetOwnedForms(userId).ToList();

            return Ok(forms);
        }

        // GET: api/forms/shared?userId=1
        [HttpGet]
        [Route("Shared")]
        [ValidateUserId]
        public IActionResult GetShared([FromQuery]string userId)
        {
            List<Form> forms = formService.GetSharedForms(userId).ToList();

            return Ok(forms);
        }

        // GET: api/forms/1/sections?userId=1
        [HttpGet("{sectionId}")]
        [Route("Sections")]
        [ValidateUserId]
        public IActionResult GetSection([FromQuery]string userId, int sectionId)
        {
            Section section = formService.GetSection(sectionId);

            if (section == null)
            {
                return NotFound(ErrorsDictionary.GetResultObject(ErrorCodes.ERR_FORM_NOT_FOUND));
            }

            return Ok(section);
        }

        // GET: api/forms/importances?userId=1
        [HttpGet]
        [Route("Importances")]
        [ValidateUserId]
        public IActionResult GetImportances([FromQuery]string userId)
        {
            IEnumerable<Importance> importances = formService.GetAllImportances();

            return Ok(importances);
        }

        // GET: api/forms/statuses?userId=1
        [HttpGet]
        [Route("Statuses")]
        [ValidateUserId]
        public IActionResult GetStatuses([FromQuery]string userId)
        {
            IEnumerable<Status> statuses = formService.GetAllStatuses();

            return Ok(statuses);
        }

        // POST: api/forms?userId=1
        [HttpPost]
        [ValidateUserId]
        public IActionResult Post([FromQuery]string userId, [FromBody]FormEditVM form)
        {
            if (form == null)
            {
                return BadRequest(ErrorsDictionary.GetResultObject(ErrorCodes.ERR_FORM_OBJ_EMPTY));
            }

            List<Status> statuses = formService.GetAllStatuses().ToList();
            List<Importance> importances = formService.GetAllImportances().ToList();

            Form formToCreate = new Form
            {
                Name = form.Name,
                Description = form.Description,
                Importance = importances.Find(importance => importance.Id == form.ImportanceId),
                Status = statuses.Find(status => status.Id == form.StatusId),
                Sections = form.Sections,
                CreatedBy = userId,
                ModifiedBy = userId,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
            };

            formService.AddForm(formToCreate);

            return Created(HttpContext.Request.Path, formToCreate);
        }

        // PUT: api/forms/1?userId=1
        [HttpPut("{formId}")]
        [ValidateUserId]
        public IActionResult Put([FromQuery]string userId, string formId, [FromBody]FormEditVM form)
        {
            if (!int.TryParse(formId, out int internalFormId))
            {
                return BadRequest(ErrorsDictionary.GetResultObject(ErrorCodes.ERR_FORM_ID_INVALID));
            }

            Form formToDelete = formService.GetForm(internalFormId);

            if (formToDelete == null)
            {
                return NotFound(ErrorsDictionary.GetResultObject(ErrorCodes.ERR_FORM_NOT_FOUND));
            }

            List<Status> statuses = formService.GetAllStatuses().ToList();
            List<Importance> importances = formService.GetAllImportances().ToList();

            Form formToUpdate = new Form
            {
                Id = internalFormId,
                Name = form.Name,
                Description = form.Description,
                Importance = importances.Find(importance => importance.Id == form.ImportanceId),
                Status = statuses.Find(status => status.Id == form.StatusId),
                Sections = form.Sections,
                ModifiedBy = userId,
                ModifiedDate = DateTime.Now,
            };

            formService.UpdateForm(formToUpdate);

            return Ok();
        }

        // DELETE: api/forms/1?userId=1
        [HttpDelete("{formId}")]
        [ValidateUserId]
        public IActionResult Delete([FromQuery]string userId, string formId)
        {
            if (!int.TryParse(formId, out int internalFormId))
            {
                return BadRequest(ErrorsDictionary.GetResultObject(ErrorCodes.ERR_FORM_ID_INVALID));
            }

            Form formToDelete = formService.GetForm(internalFormId);

            if (formToDelete == null)
            {
                return NotFound(ErrorsDictionary.GetResultObject(ErrorCodes.ERR_FORM_NOT_FOUND));
            }

            formService.DeleteForm(formToDelete);

            return NoContent();
        }
    }
}
