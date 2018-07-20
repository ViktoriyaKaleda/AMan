using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using AMan.Models;

namespace AMan.Migrations
{
    [DbContext(typeof(AManJobContext))]
    [Migration("20180720201521_AddJobFieldToAndroid")]
    partial class AddJobFieldToAndroid
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

                    b.Property<int>("CurrentJobId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(16);

                    b.Property<int>("Reliability");

                    b.Property<string>("SkillsTags");

                    b.Property<bool>("Status");

                    b.HasKey("Id");

                    b.HasIndex("CurrentJobId");

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

            modelBuilder.Entity("AMan.Models.Android", b =>
                {
                    b.HasOne("AMan.Models.Job", "CurrentJob")
                        .WithMany()
                        .HasForeignKey("CurrentJobId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
