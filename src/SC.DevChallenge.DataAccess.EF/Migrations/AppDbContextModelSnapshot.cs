﻿// <auto-generated />

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SC.DevChallenge.DataAccess.EF.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SC.DevChallenge.DataAccess.Abstractions.Entities.Instrument", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Instruments","dbo");
                });

            modelBuilder.Entity("SC.DevChallenge.DataAccess.Abstractions.Entities.Owner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Owners","dbo");
                });

            modelBuilder.Entity("SC.DevChallenge.DataAccess.Abstractions.Entities.OwnerInstrument", b =>
                {
                    b.Property<int>("OwnerId");

                    b.Property<int>("InstrumentId");

                    b.HasKey("OwnerId", "InstrumentId");

                    b.HasIndex("InstrumentId");

                    b.ToTable("OwnerInstruments","dbo");
                });

            modelBuilder.Entity("SC.DevChallenge.DataAccess.Abstractions.Entities.OwnerPortfolio", b =>
                {
                    b.Property<int>("OwnerId");

                    b.Property<int>("PortfolioId");

                    b.HasKey("OwnerId", "PortfolioId");

                    b.HasIndex("PortfolioId");

                    b.ToTable("OwnerPortfolios","dbo");
                });

            modelBuilder.Entity("SC.DevChallenge.DataAccess.Abstractions.Entities.Portfolio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Portfolios","dbo");
                });

            modelBuilder.Entity("SC.DevChallenge.DataAccess.Abstractions.Entities.Price", b =>
                {
                    b.Property<int>("PortfolioId");

                    b.Property<int>("OwnerId");

                    b.Property<int>("InstrumentId");

                    b.Property<DateTime>("Date");

                    b.Property<int>("Timeslot");

                    b.Property<double>("Value");

                    b.HasKey("PortfolioId", "OwnerId", "InstrumentId", "Date");

                    b.HasIndex("InstrumentId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Prices","dbo");
                });

            modelBuilder.Entity("SC.DevChallenge.DataAccess.Abstractions.Entities.OwnerInstrument", b =>
                {
                    b.HasOne("SC.DevChallenge.DataAccess.Abstractions.Entities.Instrument", "Instrument")
                        .WithMany("OwnerInstruments")
                        .HasForeignKey("InstrumentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SC.DevChallenge.DataAccess.Abstractions.Entities.Owner", "Owner")
                        .WithMany("OwnerInstruments")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SC.DevChallenge.DataAccess.Abstractions.Entities.OwnerPortfolio", b =>
                {
                    b.HasOne("SC.DevChallenge.DataAccess.Abstractions.Entities.Owner", "Owner")
                        .WithMany("OwnerPortfolios")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SC.DevChallenge.DataAccess.Abstractions.Entities.Portfolio", "Portfolio")
                        .WithMany("OwnerPortfolios")
                        .HasForeignKey("PortfolioId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SC.DevChallenge.DataAccess.Abstractions.Entities.Price", b =>
                {
                    b.HasOne("SC.DevChallenge.DataAccess.Abstractions.Entities.Instrument", "Instrument")
                        .WithMany()
                        .HasForeignKey("InstrumentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SC.DevChallenge.DataAccess.Abstractions.Entities.Owner", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SC.DevChallenge.DataAccess.Abstractions.Entities.Portfolio", "Portfolio")
                        .WithMany()
                        .HasForeignKey("PortfolioId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
