﻿using FootballLeague_Interview.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeague_Interview.DAL
{
    public class FootballLeagueDbContext : DbContext
    {
        public FootballLeagueDbContext(DbContextOptions<FootballLeagueDbContext> options)
            : base(options)
        {
        }

        public DbSet<Team> Teams { get; set; }
        public DbSet<DomesticLeague> Leagues { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<Standings> Standings { get; set; }
        public DbSet<Season> Seasons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // We create an index instead of using it as a primary key simply because it's more convenient to have a one-field key
            // for an entity in Entity Framework.
            modelBuilder.Entity<Team>()
                .HasIndex(t => new { t.Name, t.DomesticLeagueName })
                .IsUnique();

            modelBuilder.Entity<Result>()
                .HasOne(r => r.HomeTeam)
                .WithMany(ht => ht.HomeResults)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Result>()
                .HasOne(r => r.AwayTeam)
                .WithMany(at => at.AwayResults)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Result>()
                .HasOne(r => r.OfStandings)
                .WithMany(s => s.ResultsDuringTheSeason)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Standings>()
                .HasKey(s => new { s.SeasonId, s.LeagueId });
        }
    }
}
