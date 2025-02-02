﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Sweeft;

#nullable disable

namespace Sweeft.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250202162716_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Sweeft.Pupil", b =>
                {
                    b.Property<int>("PupilId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("PupilId"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Grade")
                        .HasColumnType("integer");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("PupilId");

                    b.ToTable("Pupils");
                });

            modelBuilder.Entity("Sweeft.Teacher", b =>
                {
                    b.Property<int>("TeacherId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("TeacherId"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("TeacherId");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("Sweeft.TeacherPupil", b =>
                {
                    b.Property<int>("TeacherId")
                        .HasColumnType("integer");

                    b.Property<int>("PupilId")
                        .HasColumnType("integer");

                    b.HasKey("TeacherId", "PupilId");

                    b.HasIndex("PupilId");

                    b.ToTable("TeacherPupils");
                });

            modelBuilder.Entity("Sweeft.TeacherPupil", b =>
                {
                    b.HasOne("Sweeft.Pupil", "Pupil")
                        .WithMany("TeacherPupils")
                        .HasForeignKey("PupilId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sweeft.Teacher", "Teacher")
                        .WithMany("TeacherPupils")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pupil");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("Sweeft.Pupil", b =>
                {
                    b.Navigation("TeacherPupils");
                });

            modelBuilder.Entity("Sweeft.Teacher", b =>
                {
                    b.Navigation("TeacherPupils");
                });
#pragma warning restore 612, 618
        }
    }
}
