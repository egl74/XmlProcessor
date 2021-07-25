using System;
using System.Collections.Generic;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using XmlProcessorServer.Models;

namespace XmlProcessorServer
{
    public class FileAnalyticsContext : DbContext
    {
        public DbSet<FileAnalyticsModel> FileAnalytics { get; set; }
        public string DbPath { get; private set; }

        public FileAnalyticsContext(IConfiguration config)
        {
            DbPath = config.GetValue<string>("ConnectionStrings:ProcessorStorage");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FileAnalyticsModel>()
            .Property(m => m.Duplications)
            .HasConversion(
                duplications => JsonSerializer.Serialize(duplications, null),
                duplications => JsonSerializer.Deserialize<Dictionary<string, int>>(duplications, null));
        }
    }
}
