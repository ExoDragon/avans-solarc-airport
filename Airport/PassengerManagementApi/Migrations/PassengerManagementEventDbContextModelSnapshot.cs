﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PassengerManagementApi.DBContext;

namespace PassengerManagementApi.Migrations
{
    [DbContext(typeof(PassengerManagementEventDbContext))]
    partial class PassengerManagementEventDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PassengerManagementApi.Domain.Aggregates.PassengerAggregate", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("CurrentVersion")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Passenger");
                });

            modelBuilder.Entity("PassengerManagementApi.Domain.Aggregates.TicketAggregate", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("CurrentVersion")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Ticket");
                });

            modelBuilder.Entity("PassengerManagementApi.Domain.Core.Event", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("EventData")
                        .HasColumnType("text");

                    b.Property<string>("MessageType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PassengerAggregateId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TicketAggregateId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<int>("Version")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PassengerAggregateId");

                    b.HasIndex("TicketAggregateId");

                    b.ToTable("Event");
                });

            modelBuilder.Entity("PassengerManagementApi.Domain.Core.Event", b =>
                {
                    b.HasOne("PassengerManagementApi.Domain.Aggregates.PassengerAggregate", null)
                        .WithMany("Events")
                        .HasForeignKey("PassengerAggregateId");

                    b.HasOne("PassengerManagementApi.Domain.Aggregates.TicketAggregate", null)
                        .WithMany("Events")
                        .HasForeignKey("TicketAggregateId");
                });

            modelBuilder.Entity("PassengerManagementApi.Domain.Aggregates.PassengerAggregate", b =>
                {
                    b.Navigation("Events");
                });

            modelBuilder.Entity("PassengerManagementApi.Domain.Aggregates.TicketAggregate", b =>
                {
                    b.Navigation("Events");
                });
#pragma warning restore 612, 618
        }
    }
}
