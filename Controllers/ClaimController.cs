using Microsoft.AspNetCore.Mvc;
using ContractMonthlyClaimsSystemWeb.Models;
using System.IO;
using Microsoft.AspNetCore.Http;
using System;

namespace ContractMonthlyClaimsSystemWeb.Controllers
{
    public class ClaimController : Controller
    {
        private static List<Claim> claims = new List<Claim>();  // Simulate a database

        // GET: /Claim/Index
        public IActionResult Index()
        {
            return View(claims);  // Pass all claims to the view
        }

        // GET: /Claim/Submit
        public IActionResult Submit()
        {
            return View();
        }

        // POST: /Claim/Submit
        [HttpPost]
        public IActionResult Submit(Claim claim, IFormFile document)
        {
            if (document != null && document.Length > 0)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", document.FileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    document.CopyTo(fileStream);
                }
                claim.DocumentFilePath = filePath;  // Save the file path to the claim model
            }

            claim.Status = ClaimStatus.Pending;
            claim.DateSubmitted = DateTime.Now;
            claims.Add(claim);  // Save claim to the list (or database)

            return RedirectToAction("Index");
        }

        // GET: /Claim/Track
        public IActionResult Track(int id)
        {
            var claim = claims.FirstOrDefault(c => c.Id == id);
            return claim != null ? View(claim) : NotFound();
        }

        // POST: /Claim/Approve
        [HttpPost]
        public IActionResult Approve(int id)
        {
            var claim = claims.FirstOrDefault(c => c.Id == id);
            if (claim != null)
            {
                claim.Status = ClaimStatus.Approved;
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        // POST: /Claim/Reject
        [HttpPost]
        public IActionResult Reject(int id)
        {
            var claim = claims.FirstOrDefault(c => c.Id == id);
            if (claim != null)
            {
                claim.Status = ClaimStatus.Rejected;
                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}

it add .
git commit -m "Set up Claims and Lecturers controllers with basic CRUD operations and automated verification process"