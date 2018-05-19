using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace netcore.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/Dashboard")]
    public class DashboardController : Controller
    {
        [HttpGet("GetPieData")]
        public IActionResult GetPieData()
        {
            List<PieData> pieDate = new List<PieData>()
            {
                new PieData{value = 70, color = "#f56954", highlight = "#f56954", label = "Chrome1"},
                new PieData{value = 80, color = "#00a65a", highlight = "#00a65a", label = "Chrome2"},
                new PieData{value = 90, color = "#f39c12", highlight = "#f39c12", label = "Chrome3"},
                new PieData{value = 20, color = "#00c0ef", highlight = "#00c0ef", label = "Chrome4"},
                new PieData{value = 50, color = "#3c8dbc", highlight = "#3c8dbc", label = "Chrome5"},
                new PieData{value = 200, color = "#d2d6de", highlight = "#d2d6de", label = "Chrome6"}
            };

            return Json(pieDate);
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