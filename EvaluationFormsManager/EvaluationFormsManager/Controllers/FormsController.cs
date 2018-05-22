using EvaluationFormsManager.Domain;
using EvaluationFormsManager.Extensions;
using EvaluationFormsManager.Models;
using EvaluationFormsManager.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

            return View(employeeForms);
        }

        // GET: Forms/Details/5
        public IActionResult Details(int? id)
        {
            return NotFound();
        }

        // GET: Forms/Create
        [Route("Form/Create", Name = "FormCreate")]
        public IActionResult Create()
        {
            List<Status> statuses = formService.GetAllStatuses().ToList();
            List<Importance> importances = formService.GetAllImportances().ToList();

            FormEditVM formCreate = new FormEditVM
            {
                ImportanceList = importances,
                StatusList = statuses,
                ImportanceColors = new List<string>
                {
                    "text-danger",
                    "text-info"
                },
                StatusColors = new List<string>
                {
                    "text-success",
                    "text-default"
                },
                Sections = new List<Section>()
            };

            HttpContext.Session.SetObjectAsJson("Form", formCreate);

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

            createdForm = new Form {
                Name = form.Name,
                Description = form.Description,
                Importance = importances.Find(importance => importance.Id == form.ImportanceId),
                Status = statuses.Find(status => status.Id == form.StatusId),
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                CreatedBy = DEFAULT_USER_ID,
                ModifiedBy = DEFAULT_USER_ID
            };

            formService.AddForm(createdForm);

            return RedirectToAction("Index");
        }

        // GET: Forms/Edit/5
        [Route("Form/{id}/Edit", Name = "FormEdit")]
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
        [ValidateAntiForgeryToken]
        [Route("Form/{id}/Edit", Name = "FormEdit")]
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
        [Route("Forms/{formId}/Sections/Create")]
        public IActionResult CreateSection(int formId)
        {
            CreateSectionVM model = new CreateSectionVM()
            {
                UserId = DEFAULT_USER_ID,
                Criteria = new List<Criteria>()
            };

            List<Importance> importances = formService.GetAllImportances().ToList();
            List<SelectListItem> importanceSelectList = new List<SelectListItem>();
            importances.ForEach(importance => importanceSelectList.Add(new SelectListItem
            {
                Value = importance.Id.ToString(),
                Text = importance.Name
            }));
            model.ImportanceList = importanceSelectList;

            return View(model);
        }

        [HttpPost]
        [Route("Forms/{formId}/Sections/Create")]
        public IActionResult CreateSection(int formId, CreateSectionVM sectionModel)
        {


            return NotFound();
        }

        private bool FormExists(int id)
        {
            return true;
        }
    }
}
