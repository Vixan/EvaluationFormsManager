using EvaluationFormsManager.Domain;
using EvaluationFormsManager.Models;
using EvaluationFormsManager.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace EvaluationFormsManager.Controllers
{
    public class FormsController : Controller
    {
        private readonly IFormService formService;

        public FormsController(IFormService formService)
        {
            this.formService = formService;
        }

        // GET: Forms
        public ActionResult Index()
        {
            const int DEFAULT_USER_ID = 1;
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
        public IActionResult Create()
        {
            return NotFound();
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
        public IActionResult Edit(int? id)
        {
            return NotFound();
        }

        // POST: Forms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Description,Status,CreatedBy,ModifiedBy,CreatedDate,ModifiedDate")] Form form)
        {
            return NotFound(form);
        }

        // GET: Forms/Delete/5
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
