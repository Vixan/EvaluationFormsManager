using EvaluationFormsManager.Domain;
using EvaluationFormsManager.ErrorHandling;
using EvaluationFormsManager.Extensions;
using EvaluationFormsManager.Models;
using EvaluationFormsManager.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;


namespace EvaluationFormsManager.Controllers
{
    [Route("[controller]")]
    public class FormsController : Controller
    {
        private readonly IFormService formService;

        // TODO: Remove DEFAULT_USER_ID
        private const string DEFAULT_USER_ID = "userId";

        public FormsController(IFormService formService)
        {
            this.formService = formService;
        }

        // GET: Forms
        [HttpGet]
        public IActionResult Index()
        {
            List<Form> forms = formService.GetOwnedForms(DEFAULT_USER_ID).ToList();
            List<FormBriefVM> employeeForms = new List<FormBriefVM>();

            forms.ForEach(form => employeeForms.Add(new FormBriefVM
            {
                Id = form.Id,
                Name = form.Name,
                Description = form.Description,
                ImportanceLevel = form.Importance.Level,
                Status = form.Status,
                CreatedDate = form.CreatedDate,
                ModifiedDate = form.ModifiedDate
            }));

            return View(employeeForms);
        }

        // GET: Forms/Shared
        [HttpGet]
        [Route("Shared")]
        public IActionResult Shared()
        {
            List<Form> sharedForms = formService.GetSharedForms(DEFAULT_USER_ID).ToList();
            List<FormBriefVM> formsToDisplay = new List<FormBriefVM>();

            sharedForms.ForEach(form => formsToDisplay.Add(new FormBriefVM
            {
                Id = form.Id,
                Name = form.Name,
                Description = form.Description,
                ImportanceLevel = form.Importance.Level,
                Status = form.Status,
                CreatedDate = form.CreatedDate,
                ModifiedDate = form.ModifiedDate
            }));

            return View(formsToDisplay);
        }

        // GET: Forms/Create
        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            Form form = null;

            if (HttpContext.Session.GetString("Action") != null)
            {
                if (HttpContext.Session.GetString("Action") == "Create")
                    form = HttpContext.Session.GetObjectFromJson<Form>("Form");
            }

            List<Status> statuses = formService.GetAllStatuses().ToList();
            List<Importance> importances = formService.GetAllImportances().ToList();

            FormEditVM formCreate = new FormEditVM
            {
                ImportanceList = importances,
                StatusList = statuses,
                ImportanceColors = new List<string>
                {
                    "text-info",
                    "text-danger"
                },
                StatusColors = new List<string>
                {
                    "text-success",
                    "text-default"
                },
                Sections = new List<Section>()
            };

            if (form != null)
            {
                if (form.Name != null)
                    formCreate.Name = form.Name;

                if (form.Description != null)
                    formCreate.Description = form.Description;

                if (form.Importance != null)
                    formCreate.ImportanceId = form.Importance.Id;

                if (form.Status != null)
                    formCreate.StatusId = form.Status.Id;

                if (form.Sections != null)
                    formCreate.Sections = form.Sections;
            }

            HttpContext.Session.SetString("Action", "Create");
            HttpContext.Session.SetObjectAsJson("Form", formCreate);

            return View(formCreate);
        }

        // POST: Forms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public IActionResult Create(FormEditVM form)
        {
            List<Status> statuses = formService.GetAllStatuses().ToList();
            List<Importance> importances = formService.GetAllImportances().ToList();

            Form createdForm = HttpContext.Session.GetObjectFromJson<Form>("Form");
            ICollection<Section> formSections = createdForm != null ? createdForm.Sections : null;

            createdForm = new Form {
                Name = form.Name,
                Description = form.Description,
                Importance = importances.Find(importance => importance.Id == form.ImportanceId),
                Status = statuses.Find(status => status.Id == form.StatusId),
                Sections = formSections,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                CreatedBy = DEFAULT_USER_ID,
                ModifiedBy = DEFAULT_USER_ID
            };

            formService.AddForm(createdForm);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create/Session")]
        public IActionResult FormCreateToSession(FormEditVM formModel)
        {
            List<Status> statuses = formService.GetAllStatuses().ToList();
            List<Importance> importances = formService.GetAllImportances().ToList();

            Form createdForm = HttpContext.Session.GetObjectFromJson<Form>("Form");
            ICollection<Section> formSections = createdForm != null ? createdForm.Sections : null;

            createdForm = new Form
            {
                Name = formModel.Name,
                Description = formModel.Description,
                Importance = importances.Find(importance => importance.Id == formModel.ImportanceId),
                Status = statuses.Find(status => status.Id == formModel.StatusId),
                Sections = createdForm.Sections,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                CreatedBy = DEFAULT_USER_ID,
                ModifiedBy = DEFAULT_USER_ID
            };

            HttpContext.Session.SetObjectAsJson("Form", createdForm);

            return RedirectToAction("CreateSection");
        }

        // GET: Forms/5/Edit
        [HttpGet]
        [Route("{id}/Edit", Name = "Edit")]
        public IActionResult Edit(int id)
        {
            Form form = formService.GetForm(id);
            List<Status> statuses = formService.GetAllStatuses().ToList();
            List<Importance> importances = formService.GetAllImportances().ToList();

            if (form == null)
            {
                return NotFound();
            }

            FormEditVM formEdit = new FormEditVM
            {
                Id = form.Id,
                Name = form.Name,
                Description = form.Description,
                StatusId = form.Status.Id,
                ImportanceId = form.Importance.Id,
                Sections = form.Sections,
                StatusList = statuses,
                ImportanceList = importances,
                ImportanceColors = new List<string>
                {
                    "text-danger",
                    "text-info"
                },
                StatusColors = new List<string>
                {
                    "text-success",
                    "text-default"
                }
            };

            return View(formEdit);
        }

        // POST: Forms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("{id}/Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, FormEditVM form)
        {
            List<Status> statuses = formService.GetAllStatuses().ToList();
            List<Importance> importances = formService.GetAllImportances().ToList();

            Form editedForm = new Form
            {
                Id = form.Id,
                Name = form.Name,
                Description = form.Description,
                Importance = importances.Find(importance => importance.Id == form.ImportanceId),
                Status = statuses.Find(status => status.Id == form.StatusId),
                ModifiedDate = DateTime.Now,
                ModifiedBy = DEFAULT_USER_ID
            };

            formService.UpdateForm(editedForm);

            return RedirectToAction("Index");
        }

        // DELETE: Forms/5/Delete
        [HttpDelete]
        [Route("{formId}/Delete")]
        public IActionResult Delete(int formId)
        {
            Form formToDelete = formService.GetForm(formId);

            if (formToDelete == null)
            {
                return BadRequest(ErrorsDictionary.GetResultObject(ErrorCodes.ERR_FORM_NOT_FOUND));
            }

            formService.DeleteForm(formToDelete);

            return NoContent();
        }

        [HttpGet]
        [Route("Sections/Create")]
        public IActionResult CreateSection()
        {
            Section section = new Section()
            {
                Criteria = new List<Criteria>()
            };

            CreateSectionVM model = new CreateSectionVM()
            {
                UserId = DEFAULT_USER_ID
            };

            HttpContext.Session.SetObjectAsJson("Section", section);

            return View(model);
        }

        [HttpPost]
        [Route("Sections/Create")]
        public IActionResult CreateSection(CreateSectionVM sectionModel)
        {
            Section section = HttpContext.Session.GetObjectFromJson<Section>("Section");

            section = new Section()
            {
                Name = sectionModel.Name,
                Description = sectionModel.Description,
                Criteria = section.Criteria,
                EvaluationScale = (EvaluationScale)Int32.Parse(sectionModel.EvaluationScale),
                ModifiedDate = DateTime.Now,
                CreatedBy = DEFAULT_USER_ID,
                ModifiedBy = DEFAULT_USER_ID
            };

            Form form = HttpContext.Session.GetObjectFromJson<Form>("Form");
            if (form.Sections == null)
                form.Sections = new List<Section>();

            form.Sections.Add(section);

            HttpContext.Session.SetObjectAsJson("Form", form);

            if (HttpContext.Session.GetString("Action") != null)
                return RedirectToAction(HttpContext.Session.GetString("Action"));

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("Section/Criteria")]
        public string GetCriteria()
        {
            ICollection<Criteria> sectionCriteria = HttpContext.Session.GetObjectFromJson<Section>("Section").Criteria;

            List<CriteriaVM> criteriaModels = new List<CriteriaVM>();
            for (int i = 0; i < sectionCriteria.Count; i++)
            {
                criteriaModels.Add(new CriteriaVM()
                {
                    Index = i + 1,
                    Name = sectionCriteria.ElementAt(i).Name,
                    ModifiedBy = sectionCriteria.ElementAt(i).ModifiedBy,
                    ModifiedDate = DateTime.Now.ToString("dd MMM hh:mm tt")
                });
            }

            return JsonConvert.SerializeObject(criteriaModels);
        }

        [HttpPost]
        [Route("Section/Criteria/Create")]
        public bool CreateCriteria(string name)
        {
            if (name == null)
                return false;

            Criteria criteria = new Criteria()
            {
                Name = name,
                CreatedBy = DEFAULT_USER_ID,
                ModifiedBy = DEFAULT_USER_ID,
                ModifiedDate = DateTime.Now
            };

            Section section = HttpContext.Session.GetObjectFromJson<Section>("Section");
            section.Criteria.Add(criteria);

            HttpContext.Session.SetObjectAsJson("Section", section);

            return true;
        }

        [HttpPost]
        [Route("Section/Criteria/Delete")]
        public bool DeleteCriteria(int index)
        {
            Section section = HttpContext.Session.GetObjectFromJson<Section>("Section");
            var criteria = section.Criteria.ToList();

            if (index >= criteria.Count && index < 0)
                return false;

            criteria.RemoveAt(index);
            section.Criteria = criteria;

            HttpContext.Session.SetObjectAsJson("Section", section);

            return true;
        }

        [HttpPost]
        [Route("Section/Criteria/Edit")]
        public bool EditCriteria(int index, string name)
        {
            Section section = HttpContext.Session.GetObjectFromJson<Section>("Section");

            if (index >= section.Criteria.Count && index < 0)
                return false;


            Criteria criteria = section.Criteria.ElementAt(index);
            criteria.Name = name;
            criteria.ModifiedBy = DEFAULT_USER_ID;
            criteria.ModifiedDate = DateTime.Now;

            HttpContext.Session.SetObjectAsJson("Section", section);

            return true;
        }

        [HttpDelete]
        [Route("Shared/{formId}/Unshare")]
        public IActionResult Unshare(int formId)
        {
            Form formToUnshare = formService.GetForm(formId);

            if (formToUnshare == null)
            {
                return BadRequest(ErrorsDictionary.GetResultObject(ErrorCodes.ERR_FORM_NOT_FOUND));
            }

            IEnumerable<string> usersToUnshareWith = new List<string> { DEFAULT_USER_ID };

            formService.UnshareForm(formToUnshare, usersToUnshareWith);

            return NoContent();
        }

        private bool FormExists(int id)
        {
            return true;
        }
    }
}
