using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GameAPI.Models;

public partial class GameOfBasketballContext : DbContext
{
    public GameOfBasketballContext()
    {
    }

    public GameOfBasketballContext(DbContextOptions<GameOfBasketballContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AllRawStat> AllRawStats { get; set; }

    public virtual DbSet<Game> Games { get; set; }

    public virtual DbSet<Player> Players { get; set; }

    public virtual DbSet<PlayerTeam> PlayerTeams { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<SeasonStat> SeasonStats { get; set; }

    public virtual DbSet<Stat> Stats { get; set; }

    public virtual DbSet<StatType> StatTypes { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<TeamRoster> TeamRosters { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AllRawStat>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("AllRawStats");

            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.StatName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.StatNameAbr).HasMaxLength(5);
            entity.Property(e => e.StatValue).HasColumnType("decimal(8, 4)");
        });

        modelBuilder.Entity<Game>(entity =>
        {
            entity.Property(e => e.Location)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.AwayTeam).WithMany(p => p.GameAwayTeams)
                .HasForeignKey(d => d.AwayTeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AwayTeamId_TeamId");

            entity.HasOne(d => d.HomeTeam).WithMany(p => p.GameHomeTeams)
                .HasForeignKey(d => d.HomeTeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HomeTeamId_TeamId");
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PlayerTeam>(entity =>
        {
            entity.HasIndex(e => new { e.PlayerId, e.TeamId }, "IX_PlayerTeam_Unique").IsUnique();

            entity.HasOne(d => d.Player).WithMany(p => p.PlayerTeams)
                .HasForeignKey(d => d.PlayerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PlayerTeams_Players");

            entity.HasOne(d => d.Team).WithMany(p => p.PlayerTeams)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PlayerTeams_Teams");
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Schedule");

            entity.Property(e => e.AwayTeam)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Away Team");
            entity.Property(e => e.HomeTeam)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Home Team");
            entity.Property(e => e.Location)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SeasonStat>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("SeasonStats");

            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Total2PtsMade).HasColumnType("decimal(38, 4)");
            entity.Property(e => e.Total2PtsMissed).HasColumnType("decimal(38, 4)");
            entity.Property(e => e.Total3PtsMade).HasColumnType("decimal(38, 4)");
            entity.Property(e => e.Total3PtsMissed).HasColumnType("decimal(38, 4)");
            entity.Property(e => e.TotalAssists).HasColumnType("decimal(38, 4)");
            entity.Property(e => e.TotalBlocks).HasColumnType("decimal(38, 4)");
            entity.Property(e => e.TotalDreb)
                .HasColumnType("decimal(38, 4)")
                .HasColumnName("TotalDREB");
            entity.Property(e => e.TotalFouls).HasColumnType("decimal(38, 4)");
            entity.Property(e => e.TotalFtmade)
                .HasColumnType("decimal(38, 4)")
                .HasColumnName("TotalFTMade");
            entity.Property(e => e.TotalFtmissed)
                .HasColumnType("decimal(38, 4)")
                .HasColumnName("TotalFTMissed");
            entity.Property(e => e.TotalOreb)
                .HasColumnType("decimal(38, 4)")
                .HasColumnName("TotalOREB");
            entity.Property(e => e.TotalSteals).HasColumnType("decimal(38, 4)");
            entity.Property(e => e.TotalTurnovers).HasColumnType("decimal(38, 4)");
        });

        modelBuilder.Entity<Stat>(entity =>
        {
            entity.Property(e => e.StatValue).HasColumnType("decimal(8, 4)");

            entity.HasOne(d => d.Game).WithMany(p => p.Stats)
                .HasForeignKey(d => d.GameId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Stats_Games");

            entity.HasOne(d => d.PlayerTeam).WithMany(p => p.Stats)
                .HasForeignKey(d => d.PlayerTeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Stats_PlayerTeams");

            entity.HasOne(d => d.StatType).WithMany(p => p.Stats)
                .HasForeignKey(d => d.StatTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Stats_StatTypes");
        });

        modelBuilder.Entity<StatType>(entity =>
        {
            entity.Property(e => e.StatName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.StatNameAbr).HasMaxLength(5);
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.Property(e => e.TeamName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TeamRoster>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("TeamRosters");

            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TeamName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
