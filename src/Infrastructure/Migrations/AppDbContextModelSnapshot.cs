﻿// <auto-generated />
using ApplicationCore.Values;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ApplicationCore.Entities.Demo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Demo");
                });

            modelBuilder.Entity("ApplicationCore.Entities.Permission", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Action")
                        .IsRequired();

                    b.Property<string>("Controller")
                        .IsRequired();

                    b.Property<int>("HttpMethod");

                    b.Property<Guid?>("RoleId");

                    b.Property<Guid?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("Permission");
                });

            modelBuilder.Entity("ApplicationCore.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("NickName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("ApplicationCore.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<string>("NickName")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<string>("Pwd")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.Property<Guid?>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("ApplicationCore.Entities.Permission", b =>
                {
                    b.HasOne("ApplicationCore.Entities.Role")
                        .WithMany("Permissions")
                        .HasForeignKey("RoleId");

                    b.HasOne("ApplicationCore.Entities.User")
                        .WithMany("Permissions")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("ApplicationCore.Entities.User", b =>
                {
                    b.HasOne("ApplicationCore.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.SetNull);
                });
#pragma warning restore 612, 618
        }
    }
}
