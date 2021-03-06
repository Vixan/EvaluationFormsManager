﻿using EvaluationFormsManager.DataTransferObjects;
using EvaluationFormsManager.Domain;
using EvaluationFormsManager.ErrorHandling;
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
            List<EvaluationFormDTO> formsToSend = new List<EvaluationFormDTO>();

            formService.GetAllForms().ToList().ForEach(form => formsToSend.Add(new EvaluationFormDTO
            {
                Id = form.Id,
                Name = form.Name,
                Description = form.Description,
                Importance = form.Importance,
                Status = form.Status,
                Sections = form.Sections,
                CreatedDate = form.CreatedDate,
                ModifiedDate = form.ModifiedDate
            }));


            return Ok(formsToSend);
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

            Form form = formService.GetForm(internalFormId);

            EvaluationFormDTO formToSend = new EvaluationFormDTO
            {
                Id = form.Id,
                Name = form.Name,
                Description = form.Description,
                Importance = form.Importance,
                Status = form.Status,
                Sections = form.Sections,
                CreatedDate = form.CreatedDate,
                ModifiedDate = form.ModifiedDate
            };

            return Ok(formToSend);
        }

        // GET: api/forms/owned?userId=1
        [HttpGet]
        [Route("Owned")]
        [ValidateUserId]
        public IActionResult GetOwned([FromQuery]string userId)
        {
            List<EvaluationFormDTO> formsToSend = new List<EvaluationFormDTO>();

            formService.GetOwnedForms(userId).ToList().ForEach(form => formsToSend.Add(new EvaluationFormDTO
            {
                Id = form.Id,
                Name = form.Name,
                Description = form.Description,
                Importance = form.Importance,
                Status = form.Status,
                Sections = form.Sections,
                CreatedDate = form.CreatedDate,
                ModifiedDate = form.ModifiedDate
            }));

            return Ok(formsToSend);
        }

        // GET: api/forms/shared?userId=1
        [HttpGet]
        [Route("Shared")]
        [ValidateUserId]
        public IActionResult GetShared([FromQuery]string userId)
        {
            List<EvaluationFormDTO> formsToSend = new List<EvaluationFormDTO>();

            formService.GetSharedForms(userId).ToList().ForEach(form => formsToSend.Add(new EvaluationFormDTO
            {
                Id = form.Id,
                Name = form.Name,
                Description = form.Description,
                Importance = form.Importance,
                Status = form.Status,
                Sections = form.Sections,
                CreatedDate = form.CreatedDate,
                ModifiedDate = form.ModifiedDate
            }));

            return Ok(formsToSend);
        }

        // GET: api/forms/sections/2?userId=1
        [HttpGet]
        [Route("Sections/{sectionId}")]
        [ValidateUserId]
        public IActionResult GetSection([FromQuery]string userId, int sectionId)
        {
            Section section = formService.GetSection(sectionId);

            if (section == null)
            {
                return NotFound(ErrorsDictionary.GetResultObject(ErrorCodes.ERR_SECTION_NOT_FOUND));
            }

            SectionDTO sectionToSend = new SectionDTO
            {
                Id = section.Id,
                Name = section.Name,
                Description = section.Description,
                EvaluationScale = (EvaluationScaleDTO)section.EvaluationScale,
                Criteria = section.Criteria
            };

            return Ok(sectionToSend);
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
        public IActionResult Post([FromQuery]string userId, [FromBody]EditFormDTO form)
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
                Sections = form.Sections.Select(section => new Section {
                    Name = section.Name,
                    Description = section.Description,
                    EvaluationScale = section.EvaluationScale,
                    Criteria = section.Criteria.Select(criteria => new Criteria
                    {
                        Name = criteria.Name,
                        ModifiedBy = userId,
                        CreatedBy = userId,
                        ModifiedDate = DateTime.Now
                    }).ToList(),
                    CreatedBy = userId,
                    ModifiedBy = userId,
                    ModifiedDate = DateTime.Now
                }).ToList(),
                CreatedBy = userId,
                ModifiedBy = userId,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };

            formService.AddForm(formToCreate);

            return Created(HttpContext.Request.Path, formToCreate);
        }

        // PUT: api/forms/1?userId=1
        [HttpPut("{formId}")]
        [ValidateUserId]
        public IActionResult Put([FromQuery]string userId, string formId, [FromBody]EditFormDTO form)
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
                Sections = form.Sections.Select(section => new Section
                {
                    Name = section.Name,
                    Description = section.Description,
                    EvaluationScale = section.EvaluationScale,
                    Criteria = section.Criteria.Select(criteria => new Criteria
                    {
                        Name = criteria.Name,
                        ModifiedBy = userId,
                        CreatedBy = userId,
                        ModifiedDate = DateTime.Now
                    }).ToList(),
                    CreatedBy = userId,
                    ModifiedBy = userId,
                    ModifiedDate = DateTime.Now
                }).ToList(),
                ModifiedBy = userId,
                ModifiedDate = DateTime.Now
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

        // POST: api/forms/1/share?userId=1
        [HttpPost("{formId}/Share")]
        [Route("Share")]
        [ValidateUserId]
        public IActionResult Share([FromQuery]string userId, string formId, [FromBody]ShareFormDTO shareObject)
        {
            IEnumerable<string> shareList = shareObject.UsersList;

            if (!int.TryParse(formId, out int internalFormId))
            {
                return BadRequest(ErrorsDictionary.GetResultObject(ErrorCodes.ERR_FORM_ID_INVALID));
            }

            if (shareList == null || shareList.Count() == 0)
            {
                return BadRequest(ErrorsDictionary.GetResultObject(ErrorCodes.ERR_USERLIST_SHARE_INVALID));
            }

            Form formToShare = formService.GetForm(internalFormId);

            if (formToShare == null)
            {
                return BadRequest(ErrorsDictionary.GetResultObject(ErrorCodes.ERR_FORM_NOT_FOUND));
            }

            formService.ShareForm(formToShare, shareList);

            return Created(HttpContext.Request.Path, formToShare);
        }

        // DELETE: api/forms/1/unshare?userId=1
        [HttpDelete("{formId}/Unshare")]
        [Route("Unshare")]
        [ValidateUserId]
        public IActionResult Unshare([FromQuery]string userId, string formId, [FromBody]ShareFormDTO unshareObject)
        {
            IEnumerable<string> shareList = unshareObject.UsersList;

            if (!int.TryParse(formId, out int internalFormId))
            {
                return BadRequest(ErrorsDictionary.GetResultObject(ErrorCodes.ERR_FORM_ID_INVALID));
            }

            if (shareList == null || shareList.Count() == 0)
            {
                return BadRequest(ErrorsDictionary.GetResultObject(ErrorCodes.ERR_USERLIST_UNSHARE_INVALID));
            }

            Form formToUnshare = formService.GetForm(internalFormId);

            if (formToUnshare == null)
            {
                return BadRequest(ErrorsDictionary.GetResultObject(ErrorCodes.ERR_FORM_NOT_FOUND));
            }

            formService.UnshareForm(formToUnshare, shareList);

            return NoContent();
        }
    }
}
