using EvaluationFormsManager.Domain;
using EvaluationFormsManager.ErrorHandling;
using EvaluationFormsManager.Extensions;
using EvaluationFormsManager.Models;
using EvaluationFormsManager.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
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

        private void ClearSession()
        {
            HttpContext.Session.Clear();
        }

        private List<FormBriefVM> CreateFormsVMs(List<Form> forms)
        {
            if (forms == null)
                return new List<FormBriefVM>();

            List<FormBriefVM> formModels = new List<FormBriefVM>();
            forms.ForEach(form => formModels.Add(new FormBriefVM
            {
                Id = form.Id,
                Name = form.Name,
                Description = form.Description,
                ImportanceLevel = form.Importance.Level,
                Status = form.Status,
                CreatedDate = form.CreatedDate,
                ModifiedDate = form.ModifiedDate
            }));

            return formModels;
        }

        // GET: Forms
        [HttpGet]
        public ActionResult Index()
        {
            List<Form> forms = formService.GetAllForms().ToList();
            List<FormBriefVM> employeeForms = CreateFormsVMs(forms);

            ClearSession();

            return View(employeeForms);
        }

        // GET: Forms/Shared
        [HttpGet]
        [Route("Shared")]
        public IActionResult Shared()
        {
            List<Form> sharedForms = formService.GetSharedForms(DEFAULT_USER_ID).ToList();
            List<FormBriefVM> formsToDisplay = CreateFormsVMs(sharedForms);

            return View(formsToDisplay);
        }

        private FormEditVM CreateFormVM(Form form)
        {
            List<Status> statuses = formService.GetAllStatuses().ToList();
            List<Importance> importances = formService.GetAllImportances().ToList();

            FormEditVM formModel = new FormEditVM
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
                if (form.Id != 0)
                    formModel.Id = form.Id;

                if (form.Name != null)
                    formModel.Name = form.Name;

                if (form.Description != null)
                    formModel.Description = form.Description;

                if (form.Importance != null)
                    formModel.ImportanceId = form.Importance.Id;

                if (form.Status != null)
                    formModel.StatusId = form.Status.Id;

                if (form.Sections != null)
                    formModel.Sections = form.Sections;
            }

            return formModel;
        }

        // GET: Forms/Create
        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            Form form = null;
            if(HttpContext.Session.GetString("Action") != null)
            {
                if (HttpContext.Session.GetString("Action") == "Create")
                    form = HttpContext.Session.GetObjectFromJson<Form>("Form");
            }
            if (form == null)
                form = new Form();

            HttpContext.Session.SetObjectAsJson("Form", form);
            HttpContext.Session.SetString("Action", "Create");

            FormEditVM formCreate = CreateFormVM(form);
            return View(formCreate);
        }

        private Form UpdateFormFromVM(FormEditVM formModel)
        {
            List<Status> statuses = formService.GetAllStatuses().ToList();
            List<Importance> importances = formService.GetAllImportances().ToList();
            Form formFromSession = HttpContext.Session.GetObjectFromJson<Form>("Form");

            Form form = new Form
            {
                Name = formModel.Name,
                Description = formModel.Description,
                Importance = importances.Find(importance => importance.Id == formModel.ImportanceId),
                Status = statuses.Find(status => status.Id == formModel.StatusId),
                ModifiedDate = DateTime.Now,
                ModifiedBy = DEFAULT_USER_ID
            };

            if(formFromSession != null)
            {
                if (formFromSession.Id != 0)
                    form.Id = formFromSession.Id;

                form.Sections = formFromSession.Sections ?? new List<Section>();
                form.CreatedBy = formFromSession.CreatedBy ?? DEFAULT_USER_ID;
                form.CreatedDate = formFromSession.CreatedDate != null ? formFromSession.CreatedDate : DateTime.Now;
            }

            return form;
        }

        // POST: Forms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public IActionResult Create(FormEditVM formModel)
        {
            Form createdForm = UpdateFormFromVM(formModel);
            formService.AddForm(createdForm);

            ClearSession();
            return RedirectToAction("Index");
        }

        private void UpdateSessionFromVM(FormEditVM formModel)
        {
            List<Status> statuses = formService.GetAllStatuses().ToList();
            List<Importance> importances = formService.GetAllImportances().ToList();

            Form createdForm = HttpContext.Session.GetObjectFromJson<Form>("Form");
            ICollection<Section> formSections = createdForm != null ? createdForm.Sections : null;
            int formId = createdForm != null ? createdForm.Id : -1;

            createdForm = new Form()
            {
                Name = formModel.Name,
                Description = formModel.Description,
                Importance = importances.Find(importance => importance.Id == formModel.ImportanceId),
                Status = statuses.Find(status => status.Id == formModel.StatusId),
                Sections = formSections,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                CreatedBy = DEFAULT_USER_ID,
                ModifiedBy = DEFAULT_USER_ID
            };

            if (formId != -1)
                createdForm.Id = formId;

            HttpContext.Session.SetObjectAsJson("Form", createdForm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Session/Create")]
        public IActionResult UpdateSessionCreate(FormEditVM formModel)
        {
            UpdateSessionFromVM(formModel);
            return RedirectToAction("CreateSection");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Session/Edit")]
        public IActionResult UpdateSessionEdit(FormEditVM formModel, int index)
        {
            UpdateSessionFromVM(formModel);
            return RedirectToAction("EditSection", new { index = index });
        }

        // GET: Forms/Edit/5
        [HttpGet]
        [Route("{id}/Edit", Name = "Edit")]
        public IActionResult Edit(int id)
        {
            Form form = null;
            if (HttpContext.Session.GetString("Action") != null)
            {
                if (HttpContext.Session.GetString("Action") == "Edit")
                    form = HttpContext.Session.GetObjectFromJson<Form>("Form");
            }
            else
            {
                form = formService.GetForm(id);
            }

            if (form == null)
                return NotFound();

            FormEditVM formEdit = CreateFormVM(form);

            HttpContext.Session.SetObjectAsJson("Form", form);
            HttpContext.Session.SetString("Action", "Edit");

            return View(formEdit);
        }

        // POST: Forms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("{id}/Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, FormEditVM formModel)
        {
            Form editedForm = UpdateFormFromVM(formModel);
            formService.UpdateForm(editedForm);

            ClearSession();
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

        private Section UpdateSectionFromVM(CreateSectionVM sectionModel)
        {
            if (sectionModel == null)
                return new Section();

            Section section = new Section()
            {
                Name = sectionModel.Name,
                Description = sectionModel.Description,
                EvaluationScale = sectionModel.EvaluationScale,
                ModifiedDate = DateTime.Now,
                ModifiedBy = DEFAULT_USER_ID
            };

            Section sectionFromSession = HttpContext.Session.GetObjectFromJson<Section>("Section");
            if(sectionFromSession != null)
            {
                if (sectionFromSession.Id != 0)
                    section.Id = sectionFromSession.Id;

                section.Criteria = sectionFromSession.Criteria ?? new List<Criteria>();
                section.CreatedBy = sectionFromSession.CreatedBy ?? DEFAULT_USER_ID;
            }

            return section;
        }

        [HttpPost]
        [Route("Sections/Create")]
        public IActionResult CreateSection(CreateSectionVM sectionModel)
        {
            Section section = UpdateSectionFromVM(sectionModel);

            Form form = HttpContext.Session.GetObjectFromJson<Form>("Form");
            if (form.Sections == null)
                form.Sections = new List<Section>();

            form.Sections.Add(section);
            HttpContext.Session.SetObjectAsJson("Form", form);

            return RedirectToForm();
        }

        [HttpGet]
        [Route("Sections/{index}/Edit")]
        public IActionResult EditSection(int index)
        {
            Form form = HttpContext.Session.GetObjectFromJson<Form>("Form");
            Section section = form.Sections.ElementAt(index);
            HttpContext.Session.SetObjectAsJson("Section", section);

            CreateSectionVM model = new CreateSectionVM()
            {
                UserId = DEFAULT_USER_ID,
                Name = section.Name,
                Description = section.Description,
                EvaluationScale = section.EvaluationScale
            };

            return View(model);
        }

        [HttpPost]
        [Route("Sections/{index}/Edit")]
        public IActionResult EditSection(int index, CreateSectionVM sectionModel)
        {
            Form form = HttpContext.Session.GetObjectFromJson<Form>("Form");
            if (form.Sections != null)
                form.Sections.ToList()[index] = UpdateSectionFromVM(sectionModel);

            HttpContext.Session.SetObjectAsJson("Form", form);
            return RedirectToForm();
        }

        private IActionResult RedirectToForm()
        {
            string action = HttpContext.Session.GetString("Action");

            if (action == null)
                return RedirectToAction("Index");

            if (action == "Edit")
            {
                Form form = HttpContext.Session.GetObjectFromJson<Form>("Form");
                if (form != null)
                    return RedirectToAction(action, new { id = form.Id.ToString() });

                return RedirectToAction("Index");
            }

            return RedirectToAction(action);
        }

        [HttpPost]
        [Route("Sections/Delete")]
        public IActionResult DeleteSection(FormEditVM formModel,int index)
        {
            UpdateSessionFromVM(formModel);

            Form form = HttpContext.Session.GetObjectFromJson<Form>("Form");
            if(form != null)
            {
                if (form.Sections != null)
                {
                    List<Section> formSections = form.Sections.ToList();
                    if (index >= 0 && index < formSections.Count)
                    {
                        formSections.RemoveAt(index);
                        form.Sections = formSections;
                    }

                    HttpContext.Session.SetObjectAsJson("Form", form);
                }
            }

            return RedirectToForm();
        }

        [HttpGet]
        [Route("Sections/Cancel")]
        public IActionResult CancelSection()
        {
            return RedirectToForm();
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
    }
}
