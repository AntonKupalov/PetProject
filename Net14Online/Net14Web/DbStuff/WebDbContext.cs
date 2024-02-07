﻿using Microsoft.EntityFrameworkCore;
using Net14Web.DbStuff.Models;
using Net14Web.DbStuff.Models.BookingWeb;
using Net14Web.DbStuff.Models.GameShop;
using Net14Web.DbStuff.Models.InvestPort;
using Net14Web.DbStuff.Models.LifeScore;
using Net14Web.DbStuff.Models.Movies;
using Net14Web.DbStuff.Models.RetroConsoles;
using Net14Web.DbStuff.Models.TaskTracker;
using Net14Web.DbStuff.Models.Bonds;
namespace Net14Web.DbStuff
{
    public class WebDbContext : DbContext
    {
        public DbSet<Hero> Heroes { get; set; }
        public DbSet<Weapon> Weapons { get; set; }
        public DbSet<UsersPcShop> UserPcShop { get; set; }
        public DbSet<Search> Searches { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<GameComment> GameComments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Dividend> Dividend { get; set; }
        public DbSet<TaskInfo> TaskInfos { get; set; }
        public DbSet<RetroUser> RetroUsers { get; set; }
        public DbSet<LoginBooking> LoginsBooking { get; set; }
        public DbSet<Bond> Bonds { get; set; }
        // LifeScore
        public DbSet<SportGame> SportGames { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }

        public WebDbContext(DbContextOptions<WebDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder
                .Entity<Hero>()
                .HasOne(hero => hero.FavoriteWeapon)
                .WithMany(weapon => weapon.HeroesWhoLikeTheWeapon)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Hero>()
                .HasMany(hero => hero.KnowedWeapons)
                .WithMany(weapon => weapon.HeroesWhoKnowsTheWeapon);

            builder.Entity<User>()
                .HasMany(user => user.Comments)
                .WithOne(comment => comment.User);

            builder.Entity<Movie>()
                .HasMany(movie => movie.Comments)
                .WithOne(comment => comment.Movie);

            builder
                .Entity<Game>()
                .HasMany(game => game.Comments)
                .WithOne(comment => comment.CommentedGame)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<LoginBooking>()
                .HasMany(loginBooking => loginBooking.Searches)
                .WithOne(search => search.LoginBooking);


            builder.Entity<Team>()
                .HasMany(team => team.Games)
                .WithMany(nextGame => nextGame.Teams);

            builder.Entity<Player>()
                .HasOne(player => player.Team)
                .WithMany(team => team.Players);
        }
    }
}
