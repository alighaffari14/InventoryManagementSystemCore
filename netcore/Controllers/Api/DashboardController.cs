using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using netcore.Data;

namespace netcore.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/Dashboard")]
    public class DashboardController : Controller
    {

        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetPieData")]
        public IActionResult GetPieData()
        {
            var productTypes = Enum.GetValues(typeof(Models.Invent.ProductType));
            List<PieData> pieDatas = new List<PieData>();
            List<string> colors = new List<string>()
            {
                "#f56954", "#00a65a", "#f39c12", "#00c0ef", "#3c8dbc", "#d2d6de"
            };

            int i = 0;
            foreach (var item in productTypes)
            {
                int count = _context.Product.Where(x => x.productType.Equals(item)).Count();
                PieData pieData = new PieData();
                pieData.value = count;
                pieData.label = item.ToString();
                pieData.color = colors[i];
                pieDatas.Add(pieData);
                i++;
                if (i > colors.Count - 1) i = 0;
            }

            return Json(pieDatas);
        }

        [HttpGet("GetBarData")]
        public IActionResult GetBarData()
        {

            List<string> labels = new List<string>()
            {
                "January", "February", "March", "April", "May", "June", "July"
            };

            List<BarDataItem> datasets = new List<BarDataItem>()
            {
                new BarDataItem{
                    label = "Electronics",
                    fillColor = "rgba(210, 214, 222, 1)",
                    strokeColor = "rgba(210, 214, 222, 1)",
                    pointColor = "rgba(210, 214, 222, 1)",
                    pointStrokeColor = "#c1c7d1",
                    pointHighlightFill = "#fff",
                    pointHighlightStroke = "rgba(220,220,220,1)",
                    data = new List<int>()
                    {
                        65, 59, 80, 81, 56, 55, 40
                    }
                },
                 new BarDataItem{
                    label = "Digital Goods",
                    fillColor = "rgba(60,141,188,0.9)",
                    strokeColor = "rgba(60,141,188,0.8)",
                    pointColor = "#3b8bba",
                    pointStrokeColor = "rgba(60,141,188,1)",
                    pointHighlightFill = "#fff",
                    pointHighlightStroke = "rgba(60,141,188,1)",
                    data = new List<int>()
                    {
                        28, 48, 40, 19, 86, 27, 90
                    }
                },
            };

            BarData barData = new BarData();
            barData.labels = labels;
            barData.datasets = datasets;

            return Json(barData);
        }
    }

    public class PieData
    {
        public int value { get; set; }
        public string color { get; set; }
        public string highlight { get; set; }
        public string label { get; set; }
    }

    public class BarData
    {
        public List<string> labels { get; set; }
        public List<BarDataItem> datasets { get; set; }
    }

    public class BarDataItem
    {
        public string label { get; set; }
        public string fillColor { get; set; }
        public string strokeColor { get; set; }
        public string pointColor { get; set; }
        public string pointStrokeColor { get; set; }
        public string pointHighlightFill { get; set; }
        public string pointHighlightStroke { get; set; }
        public List<int> data { get; set; } = new List<int>();
    }
}