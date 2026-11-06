using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AgriStreamIoT.web.Data;


namespace AgriStreamIoT.web.Controllers
{
    public class DashboardController : Controller
    {
        private readonly AgriStreamDbContext _context;

        public DashboardController(AgriStreamDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["TotalFarms"] = await _context.Farms.CountAsync();
            ViewData["TotalCrops"] = await _context.Crops.CountAsync();
            ViewData["TotalReadings"] = await _context.SensorReadings.CountAsync();
            ViewData["TotalRecommendations"] = await _context.Recommendations.CountAsync(); 

            var latest=await _context.SensorReadings
                .OrderByDescending(r => r.RecordedAt)
                .FirstOrDefaultAsync();

            ViewBag.SoilMoisture = latest?.SoilMoisture;
            ViewBag.Temperature = latest?.Temparature;
            ViewBag.Humidity = latest?.Humidity;
            return View();
        }
      
    }
}
