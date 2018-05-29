using EvaluationFormsManager.Domain;
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

        // GET: Forms
        public ActionResult Index()
        {
            List<Form> forms = formService.GetAllForms().ToList();
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

            ClearSession();

            return View(employeeForms);
        }

        // GET: Forms/Details/5
        public IActionResult Details(int? id)
        {
            return NotFound();
        }

        // GET: Forms/Create
        [HttpGet]
        [Route("Form/Create", Name = "FormCreate")]
        public IActionResult Create()
        {
            Form form = null;

            if(HttpContext.Session.GetString("Action") != null)
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

            if(form != null)
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

            if (form == null)
                form = new Form();

            HttpContext.Session.SetObjectAsJson("Form", form);
            HttpContext.Session.SetString("Action", "Create");

            return View(formCreate);
        }

        // POST: Forms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Form/Create", Name = "FormCreate")]
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
        [Route("Form/Session/Create")]
        public IActionResult UpdateSessionCreate(FormEditVM formModel)
        {
            UpdateSessionFromVM(formModel);

            return RedirectToAction("CreateSection");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Form/Session/Edit")]
        public IActionResult UpdateSessionEdit(FormEditVM formModel, int index)
        {
            UpdateSessionFromVM(formModel);

            return RedirectToAction("EditSection", new { index = index });
        }

        // GET: Forms/Edit/5
        [HttpGet]
        [Route("Form/{id}/Edit", Name = "FormEdit")]
        public IActionResult Edit(int id)
        {
            Form form = null;

            if (HttpContext.Session.GetString("Action") != null)
            {
                if (HttpContext.Session.GetString("Action") == "Edit")
                    form = HttpContext.Session.GetObjectFromJson<Form>("Form");
            }
            else
                form = formService.GetForm(id);

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

            HttpContext.Session.SetObjectAsJson("Form", form);
            HttpContext.Session.SetString("Action", "Edit");

            return View(formEdit);
        }

        // POST: Forms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Form/{id}/Edit", Name = "FormEdit")]
        public IActionResult Edit(int id, FormEditVM form)
        {
            List<Status> statuses = formService.GetAllStatuses().ToList();
            List<Importance> importances = formService.GetAllImportances().ToList();

            Form createdForm = HttpContext.Session.GetObjectFromJson<Form>("Form");
            Form formToEdit = formService.GetForm(id);

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

            if (createdForm != null)
            {
                editedForm.Sections = createdForm.Sections;
                editedForm.CreatedBy = createdForm.CreatedBy;
                editedForm.CreatedDate = createdForm.CreatedDate;
            }

            formService.UpdateForm(editedForm);

            ClearSession();

            return RedirectToAction("Index");
        }

        // GET: Forms/Delete/5
        [Route("Form/{id}/Delete", Name = "FormDelete")]
        public IActionResult Delete(int id)
        {
            Form formToDelete = formService.GetForm(id);
            formService.DeleteForm(formToDelete);

            return RedirectToAction("Index");
        }

        // POST: Forms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Form/{id}/Delete", Name = "FormDelete")]
        public IActionResult DeleteConfirmed(int id)
        {
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("Forms/Sections/Cancel")]
        public IActionResult CancelSection()
        {
            string action = HttpContext.Session.GetString("Action");

            if (action == null)
                return RedirectToAction("Index");

            if(action == "Edit")
            {
                Form form = HttpContext.Session.GetObjectFromJson<Form>("Form");
                if (form != null)
                    return RedirectToAction(action, new { id = form.Id.ToString() });

                return RedirectToAction("Index");
            }

            return RedirectToAction(action);
        }

        [HttpGet]
        [Route("Forms/Sections/Create")]
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
        [Route("Forms/Sections/Create")]
        public IActionResult CreateSection(CreateSectionVM sectionModel)
        {
            Section section = HttpContext.Session.GetObjectFromJson<Section>("Section");

            section = new Section()
            {
                Name = sectionModel.Name,
                Description = sectionModel.Description,
                Criteria = section.Criteria,
                EvaluationScale = sectionModel.EvaluationScale,
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
            {
                string action = HttpContext.Session.GetString("Action");

                if (action == "Edit")
                    return RedirectToAction(action, new { id = form.Id.ToString() });
                else
                    return RedirectToAction(action);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("Forms/Sections/{index}/Edit")]
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
        [Route("Forms/Sections/{index}/Edit")]
        public IActionResult EditSection(int index, CreateSectionVM model)
        {
            Form form = HttpContext.Session.GetObjectFromJson<Form>("Form");
            List<Section> formSections = form.Sections.ToList();

            Section section = HttpContext.Session.GetObjectFromJson<Section>("Section");
            section = new Section
            {
                Id = section.Id,
                Name = model.Name,
                Description = model.Description,
                Criteria = section.Criteria,
                EvaluationScale = model.EvaluationScale,
                CreatedBy = section.CreatedBy,
                ModifiedBy = DEFAULT_USER_ID,
                ModifiedDate = DateTime.Now,
            };

            formSections[index] = section;
            form.Sections = formSections;

            HttpContext.Session.SetObjectAsJson("Form", form);

            if (HttpContext.Session.GetString("Action") != null)
            {
                string action = HttpContext.Session.GetString("Action");

                if (action == "Edit")
                    return RedirectToAction(action, new { id = form.Id.ToString() });
                else
                    return RedirectToAction(action);
            }

            // If we got here, something failed
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("Forms/Section/Criteria")]
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
        [Route("Forms/Section/Criteria/Create")]
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
        [Route("Forms/Section/Criteria/Delete")]
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
        [Route("Forms/Section/Criteria/Edit")]
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
    }
}
