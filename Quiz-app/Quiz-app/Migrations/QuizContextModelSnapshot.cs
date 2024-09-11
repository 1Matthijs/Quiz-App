﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Quiz_app.Data;

#nullable disable

namespace Quiz_app.Migrations
{
    [DbContext(typeof(QuizContext))]
    partial class QuizContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Quiz_app.Model.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AnswerA")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("AnswerB")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("AnswerC")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("CorrectAnswer")
                        .IsRequired()
                        .HasColumnType("varchar(1)");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("Quiz_app.Model.QuizResult", b =>
                {
                    b.Property<int>("QuizResultId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("QuizResultId"));

                    b.Property<int>("CorrectAnswers")
                        .HasColumnType("int");

                    b.Property<string>("Feedback")
                        .HasColumnType("longtext");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("QuizResultId");

                    b.HasIndex("UserId");

                    b.ToTable("QuizResults");
                });

            modelBuilder.Entity("Quiz_app.Model.StudentAnswer", b =>
                {
                    b.Property<int>("StudentAnswerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("StudentAnswerId"));

                    b.Property<string>("AnswerGiven")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Feedback")
                        .HasColumnType("longtext");

                    b.Property<bool>("IsCorrect")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.Property<int>("QuizResultId")
                        .HasColumnType("int");

                    b.HasKey("StudentAnswerId");

                    b.HasIndex("QuestionId");

                    b.HasIndex("QuizResultId");

                    b.ToTable("StudentAnswers");
                });

            modelBuilder.Entity("Quiz_app.Model.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("UserId"));

                    b.Property<bool>("IsTeacher")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Quiz_app.Model.QuizResult", b =>
                {
                    b.HasOne("Quiz_app.Model.User", "User")
                        .WithMany("QuizResults")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Quiz_app.Model.StudentAnswer", b =>
                {
                    b.HasOne("Quiz_app.Model.Question", "Question")
                        .WithMany()
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Quiz_app.Model.QuizResult", "QuizResult")
                        .WithMany()
                        .HasForeignKey("QuizResultId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");

                    b.Navigation("QuizResult");
                });

            modelBuilder.Entity("Quiz_app.Model.User", b =>
                {
                    b.Navigation("QuizResults");
                });
#pragma warning restore 612, 618
        }
    }
}
