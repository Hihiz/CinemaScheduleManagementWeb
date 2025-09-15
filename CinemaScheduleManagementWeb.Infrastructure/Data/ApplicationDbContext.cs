using CinemaScheduleManagementWeb.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CinemaScheduleManagementWeb.Infrastructure.Data;

/// <summary>
/// Класс контекста БД.
/// </summary>
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<FilmEntity> Films { get; set; }

    public DbSet<GenreEntity> Genres { get; set; }

    public DbSet<FilmGenreEntity> FilmGenres { get; set; }

    public DbSet<HallEntity> Halls { get; set; }

    public DbSet<SessionEntity> Sessions { get; set; }

    public DbSet<CinemaSettingEntity> CinemaSettings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasPostgresEnum("SessionStatusEnum", new[] { "Active", "Canceled" });
        modelBuilder.HasPostgresEnum("FilmStatusEnum", new[] { "Active", "InActive" });

        modelBuilder.Entity<FilmEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Films_pkey");

            entity.ToTable(tb => tb.HasComment("Таблица фильмов."));

            entity.Property(e => e.Id).HasComment("PK.");
            entity.Property(e => e.AgeLimit).HasComment("Возрастное ограничение фильма.");
            entity.Property(e => e.Duration).HasComment("Продолжительность фильма (в минутах).");
            entity.Property(e => e.PosterUrl).HasComment("Постер фильма.");
            entity.Property(e => e.Status).HasDefaultValueSql("'Active'::\"FilmStatusEnum\"");
            entity.Property(e => e.Title).HasComment("Наименование фильма.");
        });

        modelBuilder.Entity<GenreEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Genres_pkey");

            entity.ToTable(tb => tb.HasComment("Таблица жанров."));

            entity.Property(e => e.Id).HasComment("PK.");
            entity.Property(e => e.Title).HasComment("Наименование жанра.");
        });

        modelBuilder.Entity<FilmGenreEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("FilmGenres_pkey");

            entity.ToTable(tb => tb.HasComment("Таблица жанров фильма."));

            entity.HasIndex(e => e.FilmId, "IX_FilmGenres_FilmId");

            entity.HasIndex(e => e.GenreId, "IX_FilmGenres_GenreId");

            entity.Property(e => e.Id).HasComment("PK.");
            entity.Property(e => e.FilmId).HasComment("FK на Id фильма.");
            entity.Property(e => e.GenreId).HasComment("FK на Id жанра.");

            entity.HasOne(d => d.FilmEntity).WithMany(p => p.FilmGenresEntity)
                .HasForeignKey(d => d.FilmId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_Films_Id");

            entity.HasOne(d => d.GenreEntity).WithMany(p => p.FilmGenresEntity)
                .HasForeignKey(d => d.GenreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_Genres_Id");
        });

        modelBuilder.Entity<HallEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Halls_pkey");

            entity.ToTable(tb => tb.HasComment("Таблица залов."));

            entity.Property(e => e.Id).HasComment("PK.");
            entity.Property(e => e.TechBreak).HasComment("Время технического перерыва (в минутах).");
            entity.Property(e => e.Title).HasComment("Наименование зала.");
            entity.Property(e => e.TotalSeat).HasComment("Количество мест зала.");
        });

        modelBuilder.Entity<SessionEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Sessions_pkey");

            entity.ToTable(tb => tb.HasComment("Таблица сеансов."));

            entity.HasIndex(e => e.FilmId, "IX_Sessions_FilmId");

            entity.HasIndex(e => e.HallId, "IX_Sessions_HallId");

            entity.Property(e => e.Id).HasComment("PK.");
            entity.Property(e => e.FilmId).HasComment("FK на Id фильма.");
            entity.Property(e => e.HallId).HasComment("FK на Id зала.");
            entity.Property(e => e.Price).HasComment("Цена сеанса.");
            entity.Property(e => e.SessionEnd).HasComment("Время окончания сеанса.");
            entity.Property(e => e.SessionStart).HasComment("Время начала сеанса.");
            entity.Property(e => e.Status).HasDefaultValueSql("'Active'::\"SessionStatusEnum\"");

            entity.HasOne(d => d.FilmEntity).WithMany(p => p.SessionsEntity)
                .HasForeignKey(d => d.FilmId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_Films_Id");

            entity.HasOne(d => d.HallEntity).WithMany(p => p.SessionsEntity)
                .HasForeignKey(d => d.HallId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_Halls_Id");
        });

        modelBuilder.Entity<CinemaSettingEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("CinemaSettins_pkey");

            entity.ToTable(tb => tb.HasComment("Таблица настроек работы кинотеатра."));

            entity.Property(e => e.Id).HasComment("PK.");
            entity.Property(e => e.OpenTime).HasComment("Время открытия кинотеатра.");
            entity.Property(e => e.CloseTime).HasComment("Время закрытия кинотеатра.");
        });
    }
}
