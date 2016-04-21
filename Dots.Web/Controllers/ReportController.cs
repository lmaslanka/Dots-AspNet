namespace Dots.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.IO;
    using System.Linq;
    using System.Web.Mvc;
    using CsvHelper;
    using dots.database;
    using dots.viewModels;

    public class ReportController : BaseController
    {
        public ActionResult Index()
        {
            var firstDayOfTheMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            var lastDayOfTheMonth = firstDayOfTheMonth.AddMonths(1).AddDays(-1);

            return View(GetOutbreakReportData(firstDayOfTheMonth, lastDayOfTheMonth));
        }

        [HttpPost]
        public ActionResult Index(OutbreakReportParameters parameterValues)
        {
            return View(GetOutbreakReportData(parameterValues.StartDate, parameterValues.EndDate));
        }

        private List<OutbreakListReportItem> GetOutbreakReportData(DateTime startDate, DateTime endDate)
        {
            using (var db = new DotsContext())
            {
                GetUser(db);

                var counties = db.Outbreaks.Include("County").Select(o => o.County.Name).Distinct().ToList();

                var groups = counties.Select(county => new OutbreakListReportItem
                {
                    County = county,
                    Outbreaks = new List<OutbreakReportItem>()
                }).ToList();

                foreach (var group in groups)
                {
                    string sql = @"SELECT
                                        Facility = o.Facility + ' (' + o.OutbreakLocation + ')',
                                        o.OutbreakDeclaredDate,
                                        o.OutbreakDeclaredOverDate,
                                        o.IsOutbreakDeclaredOver
                                    FROM [dbo].[Outbreaks] As o
                                    JOIN [dbo].[Counties] As c ON o.CountyId = c.RecordId
                                    WHERE (o.OutbreakDeclaredDate >= @StartDate AND o.OutbreakDeclaredDate <= @EndDate) AND c.Name = @CountyName
                                    ORDER BY o.Facility ASC";

                    object[] parameters = new object[3];

                    parameters[0] = new SqlParameter("@CountyName", group.County);
                    parameters[1] = new SqlParameter("@StartDate", startDate);
                    parameters[2] = new SqlParameter("@EndDate", endDate);

                    group.Outbreaks = db.Database.SqlQuery<OutbreakReportItem>(sql, parameters).ToList();
                }

                return groups;
            }
        }

        public FileResult Download(string start, string end)
        {
            var memoryStream = new MemoryStream();
            var tw = new StreamWriter(memoryStream);
            var csv = new CsvWriter(tw);

            var convertedStartDate = start.Replace('-', '/');
            var convertedEndDate = end.Replace('-', '/');

            DateTime startDate;
            DateTime endDate;

            try
            {
                startDate = DateTime.Parse(convertedStartDate);
                endDate = DateTime.Parse(convertedEndDate);
            }
            catch (Exception)
            {
                startDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                endDate = startDate.AddMonths(1).AddDays(-1);
            }

            var outbreaks = GetOutbreakReportData(startDate, endDate);

            foreach (var county in outbreaks)
            {
                csv.WriteField(county.County);
                csv.NextRecord();

                foreach (var outbreak in county.Outbreaks)
                {
                    csv.WriteField(outbreak.Facility);
                    csv.WriteField(outbreak.OutbreakDeclaredDate.Date);
                    if (outbreak.OutbreakDeclaredOverDate == DateTime.MinValue)
                    {
                        csv.WriteField("-");
                    }
                    else
                    {
                        csv.WriteField(outbreak.OutbreakDeclaredOverDate.Date);
                    }
                    csv.NextRecord();
                }

                csv.NextRecord();
            }

            tw.Flush();

            return File(memoryStream.ToArray(), "application/csv", "outbreakReport.csv");
        }
    }
}