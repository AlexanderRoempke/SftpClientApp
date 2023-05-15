﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SftpClientApp.Database;

#nullable disable

namespace SftpClientApp.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SftpClientApp.Database.Entities.Certificate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CertificateData")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Certificates");
                });

            modelBuilder.Entity("SftpClientApp.Database.Entities.FileFilter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pattern")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("FileFilters");
                });

            modelBuilder.Entity("SftpClientApp.Database.Entities.LogEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("LogLevelId")
                        .HasColumnType("int");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SftpConfigurationId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("LogLevelId");

                    b.HasIndex("SftpConfigurationId");

                    b.ToTable("LogEntries");
                });

            modelBuilder.Entity("SftpClientApp.Database.Entities.LogLevel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("LogLevels");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "All"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Info"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Warn"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Error"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Fatal"
                        });
                });

            modelBuilder.Entity("SftpClientApp.Database.Entities.SftpConfiguration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CertificateId")
                        .HasColumnType("int");

                    b.Property<bool>("DeleteAfterTransfer")
                        .HasColumnType("bit");

                    b.Property<string>("Host")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IntervalInMinutes")
                        .HasColumnType("int");

                    b.Property<int>("LogLevelId")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Port")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Workstationname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CertificateId");

                    b.HasIndex("LogLevelId");

                    b.ToTable("SftpConfigurations");
                });

            modelBuilder.Entity("SftpClientApp.Database.Entities.SftpTask", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("DestinationPath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("FileFilterId")
                        .HasColumnType("int");

                    b.Property<int>("SftpConfigurationId")
                        .HasColumnType("int");

                    b.Property<string>("SourcePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FileFilterId");

                    b.HasIndex("SftpConfigurationId");

                    b.ToTable("SftpTasks");
                });

            modelBuilder.Entity("SftpClientApp.Database.Entities.LogEntry", b =>
                {
                    b.HasOne("SftpClientApp.Database.Entities.LogLevel", "LogLevel")
                        .WithMany("LogEntries")
                        .HasForeignKey("LogLevelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SftpClientApp.Database.Entities.SftpConfiguration", "SftpConfiguration")
                        .WithMany()
                        .HasForeignKey("SftpConfigurationId");

                    b.Navigation("LogLevel");

                    b.Navigation("SftpConfiguration");
                });

            modelBuilder.Entity("SftpClientApp.Database.Entities.SftpConfiguration", b =>
                {
                    b.HasOne("SftpClientApp.Database.Entities.Certificate", "Certificate")
                        .WithMany()
                        .HasForeignKey("CertificateId");

                    b.HasOne("SftpClientApp.Database.Entities.LogLevel", "LogLevel")
                        .WithMany()
                        .HasForeignKey("LogLevelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Certificate");

                    b.Navigation("LogLevel");
                });

            modelBuilder.Entity("SftpClientApp.Database.Entities.SftpTask", b =>
                {
                    b.HasOne("SftpClientApp.Database.Entities.FileFilter", "FileFilter")
                        .WithMany()
                        .HasForeignKey("FileFilterId");

                    b.HasOne("SftpClientApp.Database.Entities.SftpConfiguration", "SftpConfiguration")
                        .WithMany("SftpTasks")
                        .HasForeignKey("SftpConfigurationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FileFilter");

                    b.Navigation("SftpConfiguration");
                });

            modelBuilder.Entity("SftpClientApp.Database.Entities.LogLevel", b =>
                {
                    b.Navigation("LogEntries");
                });

            modelBuilder.Entity("SftpClientApp.Database.Entities.SftpConfiguration", b =>
                {
                    b.Navigation("SftpTasks");
                });
#pragma warning restore 612, 618
        }
    }
}