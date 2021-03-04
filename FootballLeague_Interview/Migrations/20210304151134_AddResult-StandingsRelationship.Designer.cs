﻿// <auto-generated />
using System;
using FootballLeague_Interview.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FootballLeague_Interview.Migrations
{
    [DbContext(typeof(FootballLeagueDbContext))]
    [Migration("20210304151134_AddResult-StandingsRelationship")]
    partial class AddResultStandingsRelationship
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FootballLeague_Interview.DAL.Entities.DomesticLeague", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Name");

                    b.ToTable("Leagues");
                });

            modelBuilder.Entity("FootballLeague_Interview.DAL.Entities.Result", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AwayTeamId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("GoalsScoredAwayTeam")
                        .HasColumnType("int");

                    b.Property<int>("GoalsScoredHomeTeam")
                        .HasColumnType("int");

                    b.Property<string>("HomeTeamId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LeagueId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("SeasonId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("AwayTeamId");

                    b.HasIndex("HomeTeamId");

                    b.HasIndex("LeagueId");

                    b.HasIndex("SeasonId", "LeagueId");

                    b.ToTable("Results");
                });

            modelBuilder.Entity("FootballLeague_Interview.DAL.Entities.Season", b =>
                {
                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("YearEndOfSeason")
                        .HasColumnType("int");

                    b.Property<int>("YearStartOfSeason")
                        .HasColumnType("int");

                    b.HasKey("FullName");

                    b.ToTable("Seasons");
                });

            modelBuilder.Entity("FootballLeague_Interview.DAL.Entities.StandingRow", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Draws")
                        .HasColumnType("int");

                    b.Property<int>("GoalsConceded")
                        .HasColumnType("int");

                    b.Property<int>("GoalsScored")
                        .HasColumnType("int");

                    b.Property<int>("Losses")
                        .HasColumnType("int");

                    b.Property<int>("Played")
                        .HasColumnType("int");

                    b.Property<int>("Points")
                        .HasColumnType("int");

                    b.Property<string>("StandingsLeagueId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("StandingsSeasonId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TeamId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Wins")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.HasIndex("StandingsSeasonId", "StandingsLeagueId");

                    b.ToTable("StandingRow");
                });

            modelBuilder.Entity("FootballLeague_Interview.DAL.Entities.Standings", b =>
                {
                    b.Property<string>("SeasonId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LeagueId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("SeasonId", "LeagueId");

                    b.HasIndex("LeagueId");

                    b.ToTable("Standings");
                });

            modelBuilder.Entity("FootballLeague_Interview.DAL.Entities.Team", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DomesticLeagueName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("DomesticLeagueName");

                    b.HasIndex("Name", "DomesticLeagueName")
                        .IsUnique();

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("FootballLeague_Interview.DAL.Entities.Result", b =>
                {
                    b.HasOne("FootballLeague_Interview.DAL.Entities.Team", "AwayTeam")
                        .WithMany("AwayResults")
                        .HasForeignKey("AwayTeamId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("FootballLeague_Interview.DAL.Entities.Team", "HomeTeam")
                        .WithMany("HomeResults")
                        .HasForeignKey("HomeTeamId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("FootballLeague_Interview.DAL.Entities.DomesticLeague", "League")
                        .WithMany()
                        .HasForeignKey("LeagueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FootballLeague_Interview.DAL.Entities.Season", "Season")
                        .WithMany()
                        .HasForeignKey("SeasonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FootballLeague_Interview.DAL.Entities.Standings", "OfStandings")
                        .WithMany("ResultsDuringTheSeason")
                        .HasForeignKey("SeasonId", "LeagueId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("AwayTeam");

                    b.Navigation("HomeTeam");

                    b.Navigation("League");

                    b.Navigation("OfStandings");

                    b.Navigation("Season");
                });

            modelBuilder.Entity("FootballLeague_Interview.DAL.Entities.StandingRow", b =>
                {
                    b.HasOne("FootballLeague_Interview.DAL.Entities.Team", "Team")
                        .WithMany()
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FootballLeague_Interview.DAL.Entities.Standings", null)
                        .WithMany("StandingRows")
                        .HasForeignKey("StandingsSeasonId", "StandingsLeagueId");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("FootballLeague_Interview.DAL.Entities.Standings", b =>
                {
                    b.HasOne("FootballLeague_Interview.DAL.Entities.DomesticLeague", "League")
                        .WithMany()
                        .HasForeignKey("LeagueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FootballLeague_Interview.DAL.Entities.Season", "Season")
                        .WithMany()
                        .HasForeignKey("SeasonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("League");

                    b.Navigation("Season");
                });

            modelBuilder.Entity("FootballLeague_Interview.DAL.Entities.Team", b =>
                {
                    b.HasOne("FootballLeague_Interview.DAL.Entities.DomesticLeague", "DomesticLeague")
                        .WithMany("Teams")
                        .HasForeignKey("DomesticLeagueName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DomesticLeague");
                });

            modelBuilder.Entity("FootballLeague_Interview.DAL.Entities.DomesticLeague", b =>
                {
                    b.Navigation("Teams");
                });

            modelBuilder.Entity("FootballLeague_Interview.DAL.Entities.Standings", b =>
                {
                    b.Navigation("ResultsDuringTheSeason");

                    b.Navigation("StandingRows");
                });

            modelBuilder.Entity("FootballLeague_Interview.DAL.Entities.Team", b =>
                {
                    b.Navigation("AwayResults");

                    b.Navigation("HomeResults");
                });
#pragma warning restore 612, 618
        }
    }
}
