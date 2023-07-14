using BLL.DTO;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using NibulonTestTask.Models;
using NibulonTestTask.Models.ViewModels;
using System.Diagnostics;

namespace NibulonTestTask.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGrainService _grainService;

        public HomeController(ILogger<HomeController> logger, IGrainService grainService)
        {
            _logger = logger;
            this._grainService = grainService;
        }

        public IActionResult Index()
        {
            IndexViewModel viewModel = new IndexViewModel();
            TableViewModel tableViewModel = new TableViewModel();

            tableViewModel.TableName = "Таблиця 1";
            tableViewModel.IsReadOnly = false;
            tableViewModel.Grains = _grainService.GetAll();

            viewModel.Table = tableViewModel;
            viewModel.Dates= viewModel.Table.Grains
                .Select(x => x.RecordDate.ToString("dd.MM.yyyy"))
                .Distinct()
                .ToArray();
            
            return View(viewModel);
        }
        public IActionResult Table1()
        {
            TableViewModel tableViewModel = new TableViewModel();
            tableViewModel.TableName = "Таблиця 1";
            tableViewModel.IsReadOnly = false;
            tableViewModel.Grains = _grainService.GetAll();

            return PartialView("Table",tableViewModel);
        }
        public IActionResult Table2(string startDate,string endDate)
        {
            DateTime start=DateTime.Parse(startDate);
            DateTime end=DateTime.Parse(endDate);

            var grouped=_grainService.GroupByDate(start, end);

            TableViewModel tableViewModel = new TableViewModel();
            tableViewModel.TableName = "Таблиця 2";
            tableViewModel.IsReadOnly = true;
            tableViewModel.Grains = grouped;

            return PartialView("Table",tableViewModel);
        }
        public IActionResult Table3(string startDate, string endDate)
        {
            DateTime start = DateTime.Parse(startDate);
            DateTime end = DateTime.Parse(endDate);

            var grouped= _grainService.GroupByDate(start, end);
            var res=_grainService.GroupByDate(grouped,start,end);

            TableViewModel tableViewModel = new TableViewModel();
            tableViewModel.TableName = "Таблиця 3";
            tableViewModel.IsReadOnly = true;
            tableViewModel.Grains = res;
            tableViewModel.Message = Request.HttpContext.Session.GetString("info");
            return PartialView("Table", tableViewModel);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}