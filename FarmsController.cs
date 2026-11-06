
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AgriStreamIoT.web.Models;
using AgriStreamIoT.web.Data;

public class FarmsController : Controller
{
    private readonly AgriStreamDbContext _context;

    public FarmsController(AgriStreamDbContext context)
    {
        _context = context;
    }

    // GET: FARMS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Farms.ToListAsync());
    }

    // GET: FARMS/Details/5
    public async Task<IActionResult> Details(int? farmid)
    {
        if (farmid == null)
        {
            return NotFound();
        }

        var farm = await _context.Farms
            .FirstOrDefaultAsync(m => m.FarmId == farmid);
        if (farm == null)
        {
            return NotFound();
        }

        return View(farm);
    }

    // GET: FARMS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: FARMS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
  //  public async Task<IActionResult> Create([Bind("FarmId,UserId,FarmName,Location,SizeHectares,CreatedAt,Crops,Recommendations,SensorReadings,User")] Farm farm)
  public async Task<IActionResult> Create(Farm farm)
    {
        ModelState.Remove(nameof(Farm.User));
        if (ModelState.IsValid)
        {
            _context.Add(farm);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(farm);
    }

    // GET: FARMS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var farm = await _context.Farms.FindAsync(id);
        if (farm == null)
        {
            return NotFound();
        }
        return View(farm);
    }

    // POST: FARMS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("FarmId,UserId,FarmName,Location,SizeHectares,CreatedAt,Crops,Recommendations,SensorReadings,User")] Farm farm)
    {
        if (id != farm.FarmId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(farm);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FarmExists(farm.FarmId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(farm);
    }

    // GET: FARMS/Delete/5
    public async Task<IActionResult> Delete(int? farmid)
    {
        if (farmid == null)
        {
            return NotFound();
        }

        var farm = await _context.Farms
            .FirstOrDefaultAsync(m => m.FarmId == farmid);
        if (farm == null)
        {
            return NotFound();
        }

        return View(farm);
    }

    // POST: FARMS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? farmid)
    {
        var farm = await _context.Farms.FindAsync(farmid);
        if (farm != null)
        {
            _context.Farms.Remove(farm);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool FarmExists(int? farmid)
    {
        return _context.Farms.Any(e => e.FarmId == farmid);
    }
}
