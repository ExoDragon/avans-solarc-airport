﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ZoneManagementApi.DBContext;

namespace ZoneManagementApi.Migrations
{
    [DbContext(typeof(ZoneManagementEventDbContext))]
    partial class ZoneManagementEventDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ZoneManagementApi.Domain.Aggregate.LeaseAggregate", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("CurrentVersion")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Lease");
                });

            modelBuilder.Entity("ZoneManagementApi.Domain.Aggregate.ZoneAggregate", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("CurrentVersion")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Zone");
                });

            modelBuilder.Entity("ZoneManagementApi.Domain.Core.Event", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("EventData")
                        .HasColumnType("text");

                    b.Property<string>("LeaseAggregateId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("MessageType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<int>("Version")
                        .HasColumnType("int");

                    b.Property<string>("ZoneAggregateId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("LeaseAggregateId");

                    b.HasIndex("ZoneAggregateId");

                    b.ToTable("Event");
                });

            modelBuilder.Entity("ZoneManagementApi.Domain.Core.Event", b =>
                {
                    b.HasOne("ZoneManagementApi.Domain.Aggregate.LeaseAggregate", null)
                        .WithMany("Events")
                        .HasForeignKey("LeaseAggregateId");

                    b.HasOne("ZoneManagementApi.Domain.Aggregate.ZoneAggregate", null)
                        .WithMany("Events")
                        .HasForeignKey("ZoneAggregateId");
                });

            modelBuilder.Entity("ZoneManagementApi.Domain.Aggregate.LeaseAggregate", b =>
                {
                    b.Navigation("Events");
                });

            modelBuilder.Entity("ZoneManagementApi.Domain.Aggregate.ZoneAggregate", b =>
                {
                    b.Navigation("Events");
                });
#pragma warning restore 612, 618
        }
    }
}