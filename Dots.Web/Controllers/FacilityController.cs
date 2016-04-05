namespace Dots.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper;
    using dots.database;
    using dots.models;
    using dots.viewModels;

    public class FacilityController : BaseController
    {
        #region Facility List

        public ActionResult Index()
        {
            using (var db = new DotsContext())
            {
                GetUser(db);

                var facilitiesFromDb = db.Facilities.OrderBy(f => f.Name).ToList();
                var facilitiesViewModel = this.mapper.Map<List<FacilityItemViewModel>>(facilitiesFromDb);

                return View(facilitiesViewModel);
            }
        }

        #endregion

        #region New Facility

        public ActionResult New()
        {
            using (var db = new DotsContext())
            {
                GetUser(db);
                var facilityVm = new FacilityItemViewModel();

                return View(facilityVm);
            }
        }

        [HttpPost]
        public ActionResult New(FacilityItemViewModel facilityVm)
        {
            using (var db = new DotsContext())
            {
                var user = GetUser(db);

                if (this.ModelState.IsValid)
                {
                    var currentDateTime = DateTime.Now;
                    var facilityDb = this.mapper.Map<Facility>(facilityVm);

                    facilityDb.ModifiedBy = user.Username;
                    facilityDb.ModifiedOn = currentDateTime;
                    facilityDb.CreatedBy = user.Username;
                    facilityDb.CreatedOn = currentDateTime;

                    db.Facilities.Add(facilityDb);

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }

                return View(facilityVm);
            }
        }

        #endregion

        #region Edit Facility

        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return View("NotFound");
            }

            using (var db = new DotsContext())
            {
                GetUser(db);
                var facilityFromDb = db.Facilities.FirstOrDefault(u => u.RecordId == id);

                if (facilityFromDb == null)
                {
                    return View("NotFound");
                }

                var facilityVm = this.mapper.Map<FacilityItemViewModel>(facilityFromDb);
                
                return View(facilityVm);
            }
        }

        [HttpPost]
        public ActionResult Edit(FacilityItemViewModel facilityVm)
        {
            using (var db = new DotsContext())
            {
                var user = GetUser(db);

                if (this.ModelState.IsValid)
                {

                    var currentDateTime = DateTime.Now;
                    var facilityDb = db.Facilities.FirstOrDefault(u => u.RecordId == facilityVm.RecordId);

                    facilityDb.Name = facilityVm.Name;
                    facilityDb.ModifiedBy = user.Username;
                    facilityDb.ModifiedOn = currentDateTime;

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }

                return View(facilityVm);
            }
        }

        #endregion

        #region Delete Facility

        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return View("NotFound");
            }

            using (var db = new DotsContext())
            {
                GetUser(db);
                var facilityFromDb = db.Facilities.FirstOrDefault(u => u.RecordId == id);

                if (facilityFromDb == null)
                {
                    return View("NotFound");
                }

                var facilityVm = this.mapper.Map<FacilityItemViewModel>(facilityFromDb);
                
                return View(facilityVm);
            }
        }

        [HttpPost]
        public ActionResult Delete(FacilityItemViewModel facilityVm)
        {
            using (var db = new DotsContext())
            {
                GetUser(db);
                var facilityDb = db.Facilities.FirstOrDefault(f => f.RecordId == facilityVm.RecordId);

                db.Facilities.Remove(facilityDb);

                db.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        #endregion

        public FacilityController()
        {
            var automapperConfiguration = new MapperConfiguration(cfg => {
                cfg.CreateMap<Facility, FacilityItemViewModel>();
                cfg.CreateMap<FacilityItemViewModel, Facility>();
            });

            this.mapper = automapperConfiguration.CreateMapper();
        }

        private readonly IMapper mapper;
    }
}