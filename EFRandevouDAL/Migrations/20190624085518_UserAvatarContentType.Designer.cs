﻿// <auto-generated />
using System;
using EFRandevouDAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EFRandevouDAL.Migrations
{
    [DbContext(typeof(RandevouBusinessDbContext))]
    [Migration("20190624085518_UserAvatarContentType")]
    partial class UserAvatarContentType
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024");

            modelBuilder.Entity("RandevouData.Messages.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("FromUserId");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("MessageContent")
                        .IsRequired();

                    b.Property<DateTime?>("ReadDate");

                    b.Property<DateTime>("SendDate");

                    b.Property<int>("ToUserId");

                    b.HasKey("Id");

                    b.HasIndex("FromUserId");

                    b.HasIndex("ToUserId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("RandevouData.Users.Details.UserDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AvatarContentType");

                    b.Property<byte[]>("AvatarImage");

                    b.Property<string>("City");

                    b.Property<int>("Heigth");

                    b.Property<string>("Region");

                    b.Property<int>("Tattos");

                    b.Property<int>("UserId");

                    b.Property<int>("Width");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("UsersDetails");
                });

            modelBuilder.Entity("RandevouData.Users.Details.UserDetailsDictionaryItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DetailsType")
                        .IsRequired();

                    b.Property<string>("DisplayName");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("ObjectType")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("UserDetailsDictionary");

                    b.HasData(
                        new { Id = 1, DetailsType = "EyesColor", DisplayName = "Brązowe", IsDeleted = false, Name = "Brown", ObjectType = "boolean" },
                        new { Id = 2, DetailsType = "EyesColor", DisplayName = "Niebieskie", IsDeleted = false, Name = "Blue", ObjectType = "boolean" },
                        new { Id = 3, DetailsType = "EyesColor", DisplayName = "zielone", IsDeleted = false, Name = "Green", ObjectType = "boolean" },
                        new { Id = 4, DetailsType = "HairColor", DisplayName = "ciemne", IsDeleted = false, Name = "HairDark", ObjectType = "boolean" },
                        new { Id = 5, DetailsType = "HairColor", DisplayName = "jasne", IsDeleted = false, Name = "HairLight", ObjectType = "boolean" },
                        new { Id = 6, DetailsType = "Interests", DisplayName = "piłka nożna", IsDeleted = false, Name = "Football", ObjectType = "boolean" },
                        new { Id = 7, DetailsType = "Interests", DisplayName = "koszykówka", IsDeleted = false, Name = "Basketball", ObjectType = "boolean" },
                        new { Id = 8, DetailsType = "Interests", DisplayName = "szachy", IsDeleted = false, Name = "Chess", ObjectType = "boolean" }
                    );
                });

            modelBuilder.Entity("RandevouData.Users.Details.UsersDetailsItemsValues", b =>
                {
                    b.Property<int>("UserDetailsId");

                    b.Property<int>("UserDetailsDictionaryItemId");

                    b.Property<bool>("Value");

                    b.HasKey("UserDetailsId", "UserDetailsDictionaryItemId");

                    b.HasAlternateKey("UserDetailsDictionaryItemId", "UserDetailsId");

                    b.ToTable("UsersDetailsItemsValues");
                });

            modelBuilder.Entity("RandevouData.Users.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("BirthDate");

                    b.Property<string>("DisplayName");

                    b.Property<char>("Gender");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("RandevouData.Users.UsersFriendship", b =>
                {
                    b.Property<int>("User1Id");

                    b.Property<int>("User2Id");

                    b.Property<int>("Id");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("RelationStatus");

                    b.HasKey("User1Id", "User2Id");

                    b.HasIndex("User2Id");

                    b.ToTable("Friendships");
                });

            modelBuilder.Entity("RandevouData.Messages.Message", b =>
                {
                    b.HasOne("RandevouData.Users.User", "FromUser")
                        .WithMany()
                        .HasForeignKey("FromUserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("RandevouData.Users.User", "ToUser")
                        .WithMany()
                        .HasForeignKey("ToUserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RandevouData.Users.Details.UserDetails", b =>
                {
                    b.HasOne("RandevouData.Users.User", "User")
                        .WithOne("UserDetails")
                        .HasForeignKey("RandevouData.Users.Details.UserDetails", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RandevouData.Users.Details.UsersDetailsItemsValues", b =>
                {
                    b.HasOne("RandevouData.Users.Details.UserDetails")
                        .WithMany("DetailsItemsValues")
                        .HasForeignKey("UserDetailsId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RandevouData.Users.UsersFriendship", b =>
                {
                    b.HasOne("RandevouData.Users.User", "User1")
                        .WithMany()
                        .HasForeignKey("User1Id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("RandevouData.Users.User", "User2")
                        .WithMany()
                        .HasForeignKey("User2Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}