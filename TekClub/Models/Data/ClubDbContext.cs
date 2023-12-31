﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TekClub.Models.Data
{
    public class ClubDbContext : IdentityDbContext<ApplicationUser>
    {
        public ClubDbContext(DbContextOptions<ClubDbContext> options) : base(options)
        {
        }

        public DbSet<Club> Clubs { get; set; }
        public DbSet<Activité> Activités { get; set; }
        public DbSet<Evenement> Evenements { get; set; }
    }
}
