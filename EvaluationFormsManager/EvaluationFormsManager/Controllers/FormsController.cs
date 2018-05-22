﻿using EvaluationFormsManager.Domain;
using EvaluationFormsManager.Extensions;
using EvaluationFormsManager.Models;
using EvaluationFormsManager.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;


namespace EvaluationFormsManager.Controllers
{
    public class FormsController : Controller
    {
        private readonly IFormService formService;
        private static List<Criteria> formCriteria = new List<Criteria>();

        // TODO: Remove DEFAULT_USER_ID
        private const int DEFAULT_USER_ID = 1;

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
        [Route("Forms/{formId}/Sections/Create")]
        public IActionResult CreateSection(int formId, CreateSectionVM sectionModel)
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
            form.Sections.Add(section);

            HttpContext.Session.SetObjectAsJson("Form", form);

            return Redirect("/Form/Create");
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
            if (index >= formCriteria.Count && index < 0)
                return false;

            Section section = HttpContext.Session.GetObjectFromJson<Section>("Section");

            var criteria = section.Criteria.ToList();
            criteria.RemoveAt(index);
            section.Criteria = criteria;

            HttpContext.Session.SetObjectAsJson("Section", section);

            return true;
        }

        [HttpPost]
        [Route("Forms/Section/Criteria/Edit")]
        public bool EditCriteria(int index, string name)
        {
            if (index >= formCriteria.Count && index < 0)
                return false;

            Section section = HttpContext.Session.GetObjectFromJson<Section>("Section");

            Criteria criteria = section.Criteria.ElementAt(index);
            criteria.Name = name;
            criteria.ModifiedBy = DEFAULT_USER_ID;
            criteria.ModifiedDate = DateTime.Now;

            HttpContext.Session.SetObjectAsJson("Section", section);

            return true;
        }

        private bool FormExists(int id)
        {
            return true;
        }
    }
}
