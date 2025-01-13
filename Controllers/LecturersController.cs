using Microsoft.AspNetCore.Mvc;

public class LecturersController : Controller
{
    private readonly ApplicationDbContext _context;

    public LecturersController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Lecturers/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Lecturers/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Lecturer lecturer)
    {
        if (ModelState.IsValid)
        {
            _context.Lecturers.Add(lecturer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index)); // Redirect to a list page
        }
        return View(lecturer);
    }

    // GET: Lecturers
    public async Task<IActionResult> Index()
    {
        return View(await _context.Lecturers.ToListAsync());
    }

    // GET: Lecturers/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var lecturer = await _context.Lecturers.FindAsync(id);
        if (lecturer != null)
        {
            return View(lecturer);
        }

        return NotFound();
    }

    // POST: Lecturers/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Lecturer lecturer)
    {
        if (id != lecturer.Id)
        {
            return NotFound();
        }

        try
        {
            _context.Update(lecturer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!LecturerExists(lecturer.Id))
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

    private bool LecturerExists(int id)
    {
        return _context.Lecturers.Any(e => e.Id == id);
    }
}