﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProSMan.Backend.Infrastructure;

namespace ProSMan.Backend.Infrastructure.Migrations
{
    [DbContext(typeof(ProSManContext))]
    partial class ProSManContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ProSMan.Backend.Model.Category", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name");

                    b.Property<Guid>("ProjectId");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("ProSMan.Backend.Model.Project", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("ProSMan.Backend.Model.Sprint", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<DateTime>("FromDate");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsFinished");

                    b.Property<string>("Name");

                    b.Property<Guid>("ProjectId");

                    b.Property<DateTime>("ToDate");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("Sprints");
                });

            modelBuilder.Entity("ProSMan.Backend.Model.Task", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ActualSpentTime");

                    b.Property<Guid?>("CategoryId");

                    b.Property<string>("Description");

                    b.Property<bool>("IsFinished");

                    b.Property<string>("Name");

                    b.Property<string>("Priority")
                        .IsRequired();

                    b.Property<Guid>("ProjectId");

                    b.Property<Guid>("SprintId");

                    b.Property<int>("TimeEstimate");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("SprintId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("ProSMan.Backend.Model.Category", b =>
                {
                    b.HasOne("ProSMan.Backend.Model.Project", "Project")
                        .WithMany("Categories")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("ProSMan.Backend.Model.Sprint", b =>
                {
                    b.HasOne("ProSMan.Backend.Model.Project", "Project")
                        .WithMany("Sprints")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ProSMan.Backend.Model.Task", b =>
                {
                    b.HasOne("ProSMan.Backend.Model.Category", "Category")
                        .WithMany("Tasks")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ProSMan.Backend.Model.Project", "Project")
                        .WithMany("Tasks")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ProSMan.Backend.Model.Sprint", "Sprint")
                        .WithMany("Tasks")
                        .HasForeignKey("SprintId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
