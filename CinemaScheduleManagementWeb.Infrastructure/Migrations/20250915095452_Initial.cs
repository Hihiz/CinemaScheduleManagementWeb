using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CinemaScheduleManagementWeb.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:FilmStatusEnum", "Active,InActive")
                .Annotation("Npgsql:Enum:SessionStatusEnum", "Active,Canceled");

            migrationBuilder.CreateTable(
                name: "CinemaSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, comment: "PK.")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OpenTime = table.Column<TimeSpan>(type: "interval", nullable: false, comment: "Время открытия кинотеатра."),
                    CloseTime = table.Column<TimeSpan>(type: "interval", nullable: false, comment: "Время закрытия кинотеатра.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("CinemaSettins_pkey", x => x.Id);
                },
                comment: "Таблица настроек работы кинотеатра.");

            migrationBuilder.CreateTable(
                name: "Films",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, comment: "PK.")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false, comment: "Наименование фильма."),
                    Duration = table.Column<int>(type: "integer", nullable: false, comment: "Продолжительность фильма (в минутах)."),
                    AgeLimit = table.Column<short>(type: "smallint", nullable: false, comment: "Возрастное ограничение фильма."),
                    Status = table.Column<string>(type: "text", nullable: false, defaultValueSql: "'Active'::\"FilmStatusEnum\""),
                    PosterUrl = table.Column<string>(type: "text", nullable: true, comment: "Постер фильма.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("Films_pkey", x => x.Id);
                },
                comment: "Таблица фильмов.");

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, comment: "PK.")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false, comment: "Наименование жанра.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("Genres_pkey", x => x.Id);
                },
                comment: "Таблица жанров.");

            migrationBuilder.CreateTable(
                name: "Halls",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, comment: "PK.")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false, comment: "Наименование зала."),
                    TotalSeat = table.Column<int>(type: "integer", nullable: false, comment: "Количество мест зала."),
                    TechBreak = table.Column<int>(type: "integer", nullable: false, comment: "Время технического перерыва (в минутах).")
                },
                constraints: table =>
                {
                    table.PrimaryKey("Halls_pkey", x => x.Id);
                },
                comment: "Таблица залов.");

            migrationBuilder.CreateTable(
                name: "FilmGenres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, comment: "PK.")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FilmId = table.Column<int>(type: "integer", nullable: false, comment: "FK на Id фильма."),
                    GenreId = table.Column<int>(type: "integer", nullable: false, comment: "FK на Id жанра.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("FilmGenres_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "Fk_Films_Id",
                        column: x => x.FilmId,
                        principalTable: "Films",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "Fk_Genres_Id",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id");
                },
                comment: "Таблица жанров фильма.");

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, comment: "PK.")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FilmId = table.Column<int>(type: "integer", nullable: false, comment: "FK на Id фильма."),
                    HallId = table.Column<int>(type: "integer", nullable: false, comment: "FK на Id зала."),
                    SessionStart = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Время начала сеанса."),
                    SessionEnd = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Время окончания сеанса."),
                    Price = table.Column<decimal>(type: "numeric", nullable: false, comment: "Цена сеанса."),
                    Status = table.Column<string>(type: "text", nullable: true, defaultValueSql: "'Active'::\"SessionStatusEnum\"")
                },
                constraints: table =>
                {
                    table.PrimaryKey("Sessions_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "Fk_Films_Id",
                        column: x => x.FilmId,
                        principalTable: "Films",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "Fk_Halls_Id",
                        column: x => x.HallId,
                        principalTable: "Halls",
                        principalColumn: "Id");
                },
                comment: "Таблица сеансов.");

            migrationBuilder.CreateIndex(
                name: "IX_FilmGenres_FilmId",
                table: "FilmGenres",
                column: "FilmId");

            migrationBuilder.CreateIndex(
                name: "IX_FilmGenres_GenreId",
                table: "FilmGenres",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_FilmId",
                table: "Sessions",
                column: "FilmId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_HallId",
                table: "Sessions",
                column: "HallId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CinemaSettings");

            migrationBuilder.DropTable(
                name: "FilmGenres");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Films");

            migrationBuilder.DropTable(
                name: "Halls");
        }
    }
}
