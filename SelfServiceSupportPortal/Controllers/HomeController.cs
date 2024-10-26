using AadharVerify.Data.Core.DataRepository.RepositoryWrapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SelfServiceSupportPortal.Models;
using System.Diagnostics;
using System.Globalization;

namespace SelfServiceSupportPortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;   
        private readonly IDataRepositoryWrapper _dataRepositoryWrapper;

        public HomeController(IDataRepositoryWrapper dataRepositoryWrapper, ILogger<HomeController> logger)
        {
            _dataRepositoryWrapper = dataRepositoryWrapper;
            _logger = logger;
        }
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        public List<object> GetChartDetails(int Type)
        {
            var compplineData = _dataRepositoryWrapper.RegisterComplainRepository.GetAsQueryable();
            List<object> data = new List<object>();

            List<string> labels;
            List<int> counts;

            switch (Type)
            {
                case 1:
                    var groupedDataAsDate = compplineData.GroupBy(x => x.RegisterdDate.Date);

                    labels = groupedDataAsDate.Select(x => x.Key.ToString("dd-MM-yyyy")).ToList();
                    counts = groupedDataAsDate.Select(x => x.Count()).ToList();
                    break;
                case 2:
                    var dataForWeek = compplineData.ToList();
                    var groupedDataAsWeek = dataForWeek
                    .GroupBy(item => CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(item.RegisterdDate, CalendarWeekRule.FirstDay, DayOfWeek.Monday))
                    .Select(group => new
                    {
                        Week = group.Key,
                        StartDate = group.Min(item => item.RegisterdDate),
                        EndDate = group.Max(item => item.RegisterdDate),
                        Items = group.ToList()
                    })
                    .ToList();

                    labels = groupedDataAsWeek
                        .Select(x => $"{x.StartDate:dd} - {x.EndDate:dd} {x.StartDate.ToString("MMM")}")
                        .ToList();
                    counts = groupedDataAsWeek.Select(x => x.Items.Count()).ToList();
                    break;
                case 3:
                    var groupedDataAsMonthYear = compplineData
                        .GroupBy(x => new { Year = x.RegisterdDate.Year, Month = x.RegisterdDate.Month });

                    labels = groupedDataAsMonthYear
                        .Select(x => $"{x.Key.Month}/{x.Key.Year}")
                        .ToList();

                    counts = groupedDataAsMonthYear.Select(x => x.Count()).ToList();
                    break;
                case 4:
                    var groupedDataAsYear = compplineData.GroupBy(x => x.RegisterdDate.Year);

                    labels = groupedDataAsYear.Select(x => x.Key.ToString()).ToList();
                    counts = groupedDataAsYear.Select(x => x.Count()).ToList();
                    break;
                default:
                    var groupedData = compplineData.GroupBy(x => x.RegisterdDate.Date);

                    labels = groupedData.Select(x => x.Key.ToString("dd-MM-yyyy")).ToList();
                    counts = groupedData.Select(x => x.Count()).ToList();
                    break;
            }

            data.Add(labels);
            data.Add(counts);

            return data;
        }

        public List<object> GetTopComplains(int Type)
        {
            var complineData = _dataRepositoryWrapper.ComplainsRepository.GetComplains(null);
            List<object> data = new List<object>();

            switch (Type)
            {
                case 1:
                    complineData = complineData.Where(x => x.RegisterdDate.Month == DateTime.Now.Month);
                    break;
                case 2:
                    complineData = complineData.Where(x => x.RegisterdDate.Year == DateTime.Now.Year);
                    break;
                case 3:
                    complineData = complineData.Where(x => x.RegisterdDate.Year == DateTime.Now.AddYears(-1).Year);
                    break;
            }


            var top5Complaints = complineData
            .GroupBy(x => x.ComplainId)
            .OrderByDescending(group => group.Count())
            .Take(5)
            .Select(group => new
            {
                Name = group.Select(x=>x.Name).Distinct().ToList(),
                Count = group.Count()
            })
            .ToList();

            List<string?> labels = top5Complaints.SelectMany(x=>x.Name).ToList();
            List<int> counts = top5Complaints.Select(x => x.Count).ToList();

            data.Add(labels);
            data.Add(counts);
            return data;
        }


        public List<object> GetBranchModelOnComplain(int Type)
        {
            var registerComplainWithProductBranch = _dataRepositoryWrapper.RegisterComplainRepository.GetRegisterComplainWithProductBranch();
            List<object> data = new List<object>();

            switch (Type)
            {
                case 1:
                    registerComplainWithProductBranch = registerComplainWithProductBranch.Where(x => x.RegisterdDate.Month == DateTime.Now.Month);
                    break;
                case 2:
                    registerComplainWithProductBranch = registerComplainWithProductBranch.Where(x => x.RegisterdDate.Year == DateTime.Now.Year);
                    break;
                case 3:
                    registerComplainWithProductBranch = registerComplainWithProductBranch.Where(x => x.RegisterdDate.Year == DateTime.Now.AddYears(-1).Year);
                    break;
            }

            var groupedData = registerComplainWithProductBranch.GroupBy(x => x.BrandModuleNo);
            var labels = groupedData.Select(x => x.Key).ToList();
            var counts = groupedData.Select(x => x.Count()).ToList();

            data.Add(labels);
            data.Add(counts);

            return data;

        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Login()
        {
            return Redirect("/Identity/Account/Login");
        }

        public IActionResult ProcessQRCode()
        {
            // Process the qrCodeData as needed
            return View();
        }

        [HttpPost]
        public IActionResult ProcessQRCode([FromBody] string qrCodeData)
        {
            // Process the qrCodeData as needed
            return Ok("QR Code Data Processed: " + qrCodeData);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}