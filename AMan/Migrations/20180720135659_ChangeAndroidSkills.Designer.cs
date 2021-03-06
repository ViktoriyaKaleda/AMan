﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using AMan.Models;

namespace AMan.Migrations
{
    [DbContext(typeof(AManJobContext))]
    [Migration("20180720135659_ChangeAndroidSkills")]
    partial class ChangeAndroidSkills
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AMan.Models.Android", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AvatarPath");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(16);

                    b.Property<int>("Reliability");

                    b.Property<bool>("Status");

                    b.HasKey("Id");

                    b.ToTable("Android");
                });

            modelBuilder.Entity("AMan.Models.Job", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Complexity");

                    b.Property<string>("Description")
                        .HasMaxLength(255);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(16);

                    b.HasKey("Id");

                    b.ToTable("Job");
                });
        }
    }
}
