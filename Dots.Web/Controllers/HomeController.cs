namespace Dots.Web.Controllers
{
    using AutoMapper;
    using dots.database;
    using dots.models;
    using dots.viewModels;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Web.Mvc;

    public class HomeController : BaseController
    {
        #region Main List

        public ActionResult Index()
        {
            using (var db = new DotsContext())
            {
                GetUser(db);
                var counties = db.Outbreaks.Include("County").Select(o => o.County.Name).Distinct().ToList();
                var groups = new List<OutbreakListItemViewModel>();

                foreach (var county in counties)
                {
                    groups.Add(new OutbreakListItemViewModel
                    {
                        County = county,
                        Outbreaks = new List<OutbreakItemViewModel>()
                    });
                }

                foreach (var group in groups)
                {
                    string sql = @"SELECT	
                                    o.RecordId, 
                                    o.Facility, 
                                    o.OutbreakLocation, 
                                    ft.Name As FacilityType, 
                                    CONVERT(nvarchar(20), o.OutbreakDeclaredDate, 107) As OutbreakDeclaredDate,
                                    CONVERT(nvarchar(20), o.OutbreakDeclaredOverDate, 107) As OutbreakDeclaredOverDate,
                                    o.IsOutbreakDeclared,
                                    o.IsOutbreakDeclaredOver,
                                    CONVERT(nvarchar(20), o.AdmissionsOpenDate, 107) As AdmissionsOpenDate,
                                    CONVERT(nvarchar(20), o.AdmissionsCloseDate, 107) As AdmissionsCloseDate,
                                    o.IsAdmissionsClosed,
                                    o.IsAdmissionsOpened,
                                    o.Pathogen,
                                    c.Name As County,
                                    OutbreakLevel = 
                                    CASE
	                                    WHEN o.IsOutbreakDeclaredOver = 1 THEN 'resolved'
	                                    WHEN (CASE o.IsOutbreakDeclaredOver
			                                    WHEN 0 THEN DATEDIFF(dd, o.OutbreakDeclaredDate, GETDATE())
			                                    ELSE DATEDIFF(dd, o.OutbreakDeclaredDate, o.OutbreakDeclaredOverDate)
		                                    END) <= 15 THEN 'low'
	                                    WHEN (CASE o.IsOutbreakDeclaredOver
			                                    WHEN 0 THEN DATEDIFF(dd, o.OutbreakDeclaredDate, GETDATE())
			                                    ELSE DATEDIFF(dd, o.OutbreakDeclaredDate, o.OutbreakDeclaredOverDate)
		                                    END) > 15 
		                                    AND
		                                    (CASE o.IsOutbreakDeclaredOver
			                                    WHEN 0 THEN DATEDIFF(dd, o.OutbreakDeclaredDate, GETDATE())
			                                    ELSE DATEDIFF(dd, o.OutbreakDeclaredDate, o.OutbreakDeclaredOverDate)
		                                    END) <= 30 THEN 'medium'
	                                    ELSE 'high'
                                    END,
                                    OutbreakDurationInDays = 
                                    CASE o.IsOutbreakDeclaredOver
	                                    WHEN 0 THEN DATEDIFF(dd, o.OutbreakDeclaredDate, GETDATE())
	                                    ELSE DATEDIFF(dd, o.OutbreakDeclaredDate, o.OutbreakDeclaredOverDate)
                                    END,
                                    IsCommentPresent = 
	                                    CASE
                                            WHEN LEN(o.Comment) IS NULL THEN CAST(0 As BIT)
		                                    WHEN LEN(o.Comment) = 0 THEN CAST(0 As BIT)
		                                    ELSE CAST(1 As BIT) 
	                                END,
	                                LastUpdated = 
	                                CASE
		                                WHEN DATEDIFF(ss, o.ModifiedOn, GETDATE()) <= 45 THEN 'updated a few seconds ago'
		                                WHEN DATEDIFF(ss, o.ModifiedOn, GETDATE()) > 45 AND DATEDIFF(ss, o.ModifiedOn, GETDATE()) <= 90 THEN 'updated a minute ago'
		                                WHEN DATEDIFF(ss, o.ModifiedOn, GETDATE()) > 90 AND DATEDIFF(mi, o.ModifiedOn, GETDATE()) <= 45 THEN 'updated ' + CAST(DATEDIFF(mi, o.ModifiedOn, GETDATE()) As varchar(20)) + ' minutes ago'
		                                WHEN DATEDIFF(mi, o.ModifiedOn, GETDATE()) > 45 AND DATEDIFF(mi, o.ModifiedOn, GETDATE()) <= 90 THEN 'updated an hour ago'
		                                WHEN DATEDIFF(mi, o.ModifiedOn, GETDATE()) > 90 AND DATEDIFF(hh, o.ModifiedOn, GETDATE()) <= 22 THEN 'updated ' + CAST(DATEDIFF(hh, o.ModifiedOn, GETDATE()) As varchar(20)) + ' hours ago'
		                                WHEN DATEDIFF(hh, o.ModifiedOn, GETDATE()) > 22 AND DATEDIFF(hh, o.ModifiedOn, GETDATE()) <= 36 THEN 'updated a day ago'
		                                WHEN DATEDIFF(hh, o.ModifiedOn, GETDATE()) > 36 AND DATEDIFF(dd, o.ModifiedOn, GETDATE()) <= 25 THEN 'updated ' + CAST(DATEDIFF(dd, o.ModifiedOn, GETDATE()) As varchar(20)) + ' days ago'
		                                WHEN DATEDIFF(dd, o.ModifiedOn, GETDATE()) > 25 AND DATEDIFF(dd, o.ModifiedOn, GETDATE()) <= 45 THEN 'updated a month ago'
		                                WHEN DATEDIFF(dd, o.ModifiedOn, GETDATE()) > 45 AND DATEDIFF(dd, o.ModifiedOn, GETDATE()) <= 345 THEN 'updated ' + CAST(DATEDIFF(mm, o.ModifiedOn, GETDATE()) As varchar(20)) + ' months ago'
		                                WHEN DATEDIFF(dd, o.ModifiedOn, GETDATE()) > 345 AND DATEDIFF(dd, o.ModifiedOn, GETDATE()) <= 545 THEN 'updated a year ago'
		                                WHEN DATEDIFF(dd, o.ModifiedOn, GETDATE()) > 545 THEN 'updated ' + CAST(DATEDIFF(yy, o.ModifiedOn, GETDATE()) As varchar(20)) + ' years ago'
	                                END
                                    FROM [dbo].[Outbreaks] As o
                                    JOIN [dbo].[FacilityTypes] As ft
                                    ON o.FacilityTypeId = ft.RecordId
                                    JOIN [dbo].[Counties] As c
                                    ON o.CountyId = c.RecordId
                                    WHERE (o.IsOutbreakDeclaredOver = 0 OR DATEDIFF(dd, o.OutbreakDeclaredOverDate, GETDATE()) <= 7)
                                    AND c.Name = @CountyName
                                    ORDER BY o.Facility ASC";

                    group.Outbreaks = db.Database.SqlQuery<OutbreakItemViewModel>(sql, new SqlParameter("@CountyName", group.County)).ToList();
                }

                return View(groups);
            }
        }

        #endregion

        #region New Outbreak

        public ActionResult New()
        {
            using (var db = new DotsContext())
            {
                GetUser(db);
                var model = new OutbreakItemEditViewModel();

                model.OutbreakDeclaredDate = DateTime.Now;

                model.Facilities = GetFacilities(db);
                model.OutbreakLocations = GetOutbreakLocations(db);
                model.FacilityTypes = GetFacilityTypes(db);
                model.Pathogens = GetPathogens(db);
                model.Counties = GetCounties(db);

                return View(model);
            }
        }

        [HttpPost]
        public ActionResult New(OutbreakItemEditViewModel outbreak)
        {
            if (this.ModelState.IsValid)
            {
                var currentDateTime = DateTime.Now;

                using (var db = new DotsContext())
                {
                    var user = GetUser(db);
                    var outbreakItem = this.mapper.Map<Outbreak>(outbreak);

                    outbreakItem.IsOutbreakDeclared = true;
                    outbreakItem.ModifiedBy = user.Username;
                    outbreakItem.ModifiedOn = currentDateTime;
                    outbreakItem.CreatedBy = user.Username;
                    outbreakItem.CreatedOn = currentDateTime;
                    
                    db.Outbreaks.Add(outbreakItem);
                    db.SaveChanges();

                    UpdateFacility(db, outbreak.Facility, user.Username, currentDateTime);
                    UpdateOutbreakLocation(db, outbreak.OutbreakLocation, user.Username, currentDateTime);
                    UpdatePathogen(db, outbreak.Pathogen, user.Username, currentDateTime);
                }

                return RedirectToAction("Index");
            }

            return View(outbreak);
        }

        #endregion

        #region Edit Outbreak

        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return View("NotFound");
            }

            using (var db = new DotsContext())
            {
                var user = GetUser(db);
                OutbreakItemEditViewModel model = null;
                var outbreak = db.Outbreaks.FirstOrDefault(o => o.RecordId == id);

                if (outbreak == null)
                {
                    return View("NotFound");
                }
                else
                {
                    model = this.mapper.Map<OutbreakItemEditViewModel>(outbreak);

                    model.Facilities = GetFacilities(db);
                    model.OutbreakLocations = GetOutbreakLocations(db);
                    model.FacilityTypes = GetFacilityTypes(db);
                    model.Pathogens = GetPathogens(db);
                    model.Counties = GetCounties(db);
                }

                return View(model);
            }
        }

        [HttpPost]
        public ActionResult Edit(OutbreakItemEditViewModel outbreak)
        {
            if (this.ModelState.IsValid)
            {
                var currentDateTime = DateTime.Now;

                using (var db = new DotsContext())
                {
                    var user = GetUser(db);
                    var outbreakItem = db.Outbreaks.FirstOrDefault(o => o.RecordId == outbreak.RecordId);

                    if (outbreakItem != null)
                    {
                        outbreakItem.FacilityTypeId = outbreak.FacilityTypeId;
                        outbreakItem.CountyId = outbreak.CountyId;
                        outbreakItem.Facility = outbreak.Facility;
                        outbreakItem.OutbreakLocation = outbreak.OutbreakLocation;
                        outbreakItem.IsOutbreakDeclared = outbreak.IsOutbreakDeclared;
                        outbreakItem.IsOutbreakDeclaredOver = outbreak.IsOutbreakDeclaredOver;
                        outbreakItem.OutbreakDeclaredDate = outbreak.OutbreakDeclaredDate;
                        outbreakItem.OutbreakDeclaredOverDate = outbreak.OutbreakDeclaredOverDate;
                        outbreakItem.IsAdmissionsClosed = outbreak.IsAdmissionsClosed;
                        outbreakItem.IsAdmissionsOpened = outbreak.IsAdmissionsOpened;
                        outbreakItem.AdmissionsCloseDate = outbreak.AdmissionsCloseDate;
                        outbreakItem.AdmissionsOpenDate = outbreak.AdmissionsOpenDate;
                        outbreakItem.Pathogen = outbreak.Pathogen;
                        outbreakItem.Comment = outbreak.Comment;
                        outbreakItem.ModifiedBy = user.Username;
                        outbreakItem.ModifiedOn = currentDateTime;

                        db.SaveChanges();

                        UpdateFacility(db, outbreak.Facility, user.Username, currentDateTime);
                        UpdateOutbreakLocation(db, outbreak.OutbreakLocation, user.Username, currentDateTime);
                        UpdatePathogen(db, outbreak.Pathogen, user.Username, currentDateTime);
                    }
                }

                return RedirectToAction("Index");
            }

            return View(outbreak);
        }

        #endregion

        #region Get Dropdown Values Helpers

        private List<CountyItemViewModel> GetCounties(DotsContext db)
        {
            var counties = db.Counties.OrderBy(p => p.Name).ToList();
            var countyVm = new List<CountyItemViewModel>();
            foreach (var county in counties)
            {
                countyVm.Add(new CountyItemViewModel
                {
                    RecordId = county.RecordId,
                    Name = county.Name
                });
            }

            return countyVm;
        }

        private List<FacilityTypeItemViewModel> GetFacilityTypes(DotsContext db)
        {
            var facilityTypes = db.FacilityTypes.OrderBy(p => p.Name).ToList();
            var facilityTypeVm = new List<FacilityTypeItemViewModel>();
            foreach (var facilityType in facilityTypes)
            {
                facilityTypeVm.Add(new FacilityTypeItemViewModel
                {
                    RecordId = facilityType.RecordId,
                    Name = facilityType.Name
                });
            }

            return facilityTypeVm;
        }

        private List<FacilityItemViewModel> GetFacilities(DotsContext db)
        {
            var facilities = db.Facilities.OrderBy(p => p.Name).ToList();
            var facilityVm = new List<FacilityItemViewModel>();
            foreach (var facility in facilities)
            {
                facilityVm.Add(new FacilityItemViewModel
                {
                    RecordId = facility.RecordId,
                    Name = facility.Name
                });
            }

            return facilityVm;
        }

        private List<OutbreakLocationItemViewModel> GetOutbreakLocations(DotsContext db)
        {
            var outbreakLocations = db.OutbreakLocations.OrderBy(p => p.Name).ToList();
            var outbreakLocationsVm = new List<OutbreakLocationItemViewModel>();
            foreach (var outbreakLocation in outbreakLocations)
            {
                outbreakLocationsVm.Add(new OutbreakLocationItemViewModel
                {
                    RecordId = outbreakLocation.RecordId,
                    Name = outbreakLocation.Name
                });
            }

            return outbreakLocationsVm;
        }

        private List<PathogenItemViewModel> GetPathogens(DotsContext db)
        {
            var pathogens = db.Pathogens.OrderBy(p => p.Name).ToList();
            var pathogenVm = new List<PathogenItemViewModel>();
            foreach (var pathogen in pathogens)
            {
                pathogenVm.Add(new PathogenItemViewModel
                {
                    Name = pathogen.Name
                });
            }

            return pathogenVm;
        }

        #endregion

        #region Update Dropdown Values Helper

        private void UpdateFacility(DotsContext db, string name, string username, DateTime currentDateTime)
        {
            var cleanName = name?.Trim();

            if (!string.IsNullOrEmpty(cleanName))
            {
                var facilities = db.Facilities.FirstOrDefault(f => f.Name.ToLower() == name.ToLower());

                if (facilities == null)
                {
                    var facility = new Facility
                    {
                        Name = name,
                        ModifiedBy = username,
                        ModifiedOn = currentDateTime,
                        CreatedBy = username,
                        CreatedOn = currentDateTime
                    };

                    db.Facilities.Add(facility);
                    db.SaveChanges();
                }
            }
        }

        private void UpdateOutbreakLocation(DotsContext db, string name, string username, DateTime currentDateTime)
        {
            var cleanName = name?.Trim();

            if (!string.IsNullOrEmpty(cleanName))
            {
                var locations = db.OutbreakLocations.FirstOrDefault(f => f.Name.ToLower() == name.ToLower());

                if (locations == null)
                {
                    var location = new OutbreakLocation
                    {
                        Name = name,
                        ModifiedBy = username,
                        ModifiedOn = currentDateTime,
                        CreatedBy = username,
                        CreatedOn = currentDateTime
                    };

                    db.OutbreakLocations.Add(location);
                    db.SaveChanges();
                }
            }
        }

        private void UpdatePathogen(DotsContext db, string name, string username, DateTime currentDateTime)
        {
            var cleanName = name?.Trim();

            if (!string.IsNullOrEmpty(cleanName))
            {
                var pathogenClean = name.Trim().ToLower();
                var pathogenFromDatabase = db.Pathogens.FirstOrDefault(f => f.Name.ToLower() == pathogenClean);

                if (pathogenFromDatabase == null)
                {
                    var pathogenToDatabase = new Pathogen
                    {
                        Name = pathogenClean,
                        ModifiedBy = username,
                        ModifiedOn = currentDateTime,
                        CreatedBy = username,
                        CreatedOn = currentDateTime
                    };

                    db.Pathogens.Add(pathogenToDatabase);
                    db.SaveChanges();
                }
            }
        }

        #endregion

        public HomeController()
        {
            var automapperConfiguration = new MapperConfiguration(cfg => {
                cfg.CreateMap<Outbreak, OutbreakItemEditViewModel>();
                cfg.CreateMap<OutbreakItemEditViewModel, Outbreak>();
                cfg.CreateMap<OutbreakListItemViewModel, Outbreak>();
            });

            this.mapper = automapperConfiguration.CreateMapper();
        }

        private readonly IMapper mapper;
    }
}