﻿// <auto-generated />
using System;
using BTZ_Transports.Dados.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BTZ_Transports.Dados.Migrations
{
    [DbContext(typeof(BTZContext))]
    partial class BTZContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BTZ_Transports.Negocio.Models.Combustivel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<decimal>("Preco")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Combustiveis", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Nome = "Gasolina",
                            Preco = 4.29m
                        },
                        new
                        {
                            Id = 2,
                            Nome = "Etanol",
                            Preco = 2.99m
                        },
                        new
                        {
                            Id = 3,
                            Nome = "Diesel",
                            Preco = 2.09m
                        });
                });

            modelBuilder.Entity("BTZ_Transports.Negocio.Models.Motorista", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasColumnType("VARCHAR(20)");

                    b.Property<string>("CategoriaCNH")
                        .IsRequired()
                        .HasColumnType("VARCHAR(3)");

                    b.Property<DateTime>("DataDeNascimento")
                        .HasColumnType("DATETIME");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("VARCHAR(512)");

                    b.Property<string>("NumeroCNH")
                        .IsRequired()
                        .HasColumnType("VARCHAR(20)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Motoristas", (string)null);
                });

            modelBuilder.Entity("BTZ_Transports.Negocio.Models.RegistroAbastecimento", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CombustivelId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Data")
                        .HasColumnType("DATETIME");

                    b.Property<Guid>("MotoristaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("QuantidadeAbastecida")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ValorTotal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("VeiculoId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CombustivelId");

                    b.HasIndex("MotoristaId");

                    b.HasIndex("VeiculoId");

                    b.ToTable("RegistrosAbastecimento", (string)null);
                });

            modelBuilder.Entity("BTZ_Transports.Negocio.Models.Veiculo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AnoDeFabricacao")
                        .HasColumnType("int");

                    b.Property<decimal>("CapacidadeMaximaDoTanque")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("CombustivelId")
                        .HasColumnType("int");

                    b.Property<string>("Fabricante")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("VARCHAR(512)");

                    b.Property<string>("Observacao")
                        .HasColumnType("VARCHAR(1028)");

                    b.HasKey("Id");

                    b.HasIndex("CombustivelId");

                    b.ToTable("Veiculos", (string)null);
                });

            modelBuilder.Entity("BTZ_Transports.Negocio.Models.RegistroAbastecimento", b =>
                {
                    b.HasOne("BTZ_Transports.Negocio.Models.Combustivel", "Combustivel")
                        .WithMany("RegistroAbastecimentos")
                        .HasForeignKey("CombustivelId")
                        .OnDelete(DeleteBehavior.ClientNoAction)
                        .IsRequired();

                    b.HasOne("BTZ_Transports.Negocio.Models.Motorista", "Motorista")
                        .WithMany("RegistroAbastecimentos")
                        .HasForeignKey("MotoristaId")
                        .OnDelete(DeleteBehavior.ClientNoAction)
                        .IsRequired();

                    b.HasOne("BTZ_Transports.Negocio.Models.Veiculo", "Veiculo")
                        .WithMany("RegistroAbastecimentos")
                        .HasForeignKey("VeiculoId")
                        .OnDelete(DeleteBehavior.ClientNoAction)
                        .IsRequired();

                    b.Navigation("Combustivel");

                    b.Navigation("Motorista");

                    b.Navigation("Veiculo");
                });

            modelBuilder.Entity("BTZ_Transports.Negocio.Models.Veiculo", b =>
                {
                    b.HasOne("BTZ_Transports.Negocio.Models.Combustivel", "Combustivel")
                        .WithMany("Veiculos")
                        .HasForeignKey("CombustivelId")
                        .OnDelete(DeleteBehavior.ClientNoAction)
                        .IsRequired();

                    b.Navigation("Combustivel");
                });

            modelBuilder.Entity("BTZ_Transports.Negocio.Models.Combustivel", b =>
                {
                    b.Navigation("RegistroAbastecimentos");

                    b.Navigation("Veiculos");
                });

            modelBuilder.Entity("BTZ_Transports.Negocio.Models.Motorista", b =>
                {
                    b.Navigation("RegistroAbastecimentos");
                });

            modelBuilder.Entity("BTZ_Transports.Negocio.Models.Veiculo", b =>
                {
                    b.Navigation("RegistroAbastecimentos");
                });
#pragma warning restore 612, 618
        }
    }
}
