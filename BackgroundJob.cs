namespace ContractMonthlyClaimsSystemWeb
{
    public class BackgroundJob
    {
        private readonly ApplicationDbContext _context;

        public BackgroundJob(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Run()
        {
            var claims = _context.Claims.Where(c => c.IsVerified == false).ToList();

            foreach (var claim in claims)
            {
                VerifyClaim(claim);
            }
        }

        private void VerifyClaim(Claim claim)
        {
            var lecturer = _context.Lecturers.FindAsync(claim.LecturerId).Result;

            if (lecturer != null)
            {
                var lecturerClaims = _context.Claims.Where(c => c.LecturerId == claim.LecturerId).ToList();

                if (lecturerClaims.Any(c => c.IsApproved))
                {
                    // Verify if the lecturer has already been approved
                    return;
                }
                else
                {
                    // Verify if the claim is valid (e.g., hours worked is within limits)
                    if (claim.HoursWorked > 40)
                    {
                        // Reject the claim if it's invalid
                        claim.IsVerified = false;
                        _context.SaveChanges();
                        return;
                    }
                    else
                    {
                        // Automatically approve the claim if it's valid
                        claim.IsApproved = true;
                        _context.SaveChanges();
                        return;


                    }
                }
            }
        }
    }
}

git add .
git commit -m "Added Hangfire background job for automated claim verification and approval process"