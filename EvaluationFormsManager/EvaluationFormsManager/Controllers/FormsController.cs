using EvaluationFormsManager.Domain;
using EvaluationFormsManager.Models;
using EvaluationFormsManager.Shared;
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
        private const int DEFAULT_USER_ID = 1;

        public FormsController(IFormService formService)
        {
            this.formService = formService;
        }

        // GET: Forms
        public ActionResult Index()
        {
            List<Form> forms = formService.GetAllFormsCreatedBy(DEFAULT_USER_ID).ToList();
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
        [Route("Form/Create", Name = "Create")]
        public IActionResult Create()
        {
            List<Status> statuses = formService.GetAllStatuses().ToList();
            List<Importance> importances = formService.GetAllImportances().ToList();

            FormCreateVM formCreate = new FormCreateVM
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

            return View(formCreate);
        }

        // POST: Forms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,Description,Status,CreatedBy,ModifiedBy,CreatedDate,ModifiedDate")] Form form)
        {
            return NotFound(form);
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
        public IActionResult Delete(int? id)
        {
            return NotFound();
        }

        // POST: Forms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            return RedirectToAction(nameof(Index));
        }

        private bool FormExists(int id)
        {
            return true;
        }
    }
}
