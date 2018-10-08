﻿// <auto-generated />
using System;
using EFRandevouDAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EFRandevouDAL.Migrations
{
    [DbContext(typeof(RandevouDbContext))]
    partial class RandevouDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024");

            modelBuilder.Entity("RandevouData.Messages.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("FromUserId");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime>("ReadDate");

                    b.Property<DateTime>("SendDate");

                    b.Property<int?>("ToUserId");

                    b.HasKey("Id");

                    b.HasIndex("FromUserId");

                    b.HasIndex("ToUserId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("RandevouData.Users.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("BirthDate");

                    b.Property<string>("DisplayName");

                    b.Property<char>("Gender");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("RandevouData.Messages.Message", b =>
                {
                    b.HasOne("RandevouData.Users.User", "FromUser")
                        .WithMany()
                        .HasForeignKey("FromUserId");

                    b.HasOne("RandevouData.Users.User", "ToUser")
                        .WithMany()
                        .HasForeignKey("ToUserId");
                });
#pragma warning restore 612, 618
        }
    }
}
