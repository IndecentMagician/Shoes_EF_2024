﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shoes_EF_2024.Datos;

#nullable disable

namespace Shoes_EF_2024.Datos.Migrations
{
    [DbContext(typeof(ShoesDbContext))]
    [Migration("20240906034433_SeedersPopulandoDB")]
    partial class SeedersPopulandoDB
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Shoes_EF_2024.Entidades.Brands", b =>
                {
                    b.Property<int>("BrandId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BrandId"));

                    b.Property<bool>("Active")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<string>("BrandName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("BrandId");

                    b.HasIndex("BrandName")
                        .IsUnique();

                    b.HasIndex(new[] { "BrandName" }, "IX_Brands_Name");

                    b.ToTable("Brands", (string)null);

                    b.HasData(
                        new
                        {
                            BrandId = 1,
                            Active = true,
                            BrandName = "OSIRIS"
                        },
                        new
                        {
                            BrandId = 2,
                            Active = true,
                            BrandName = "DC"
                        });
                });

            modelBuilder.Entity("Shoes_EF_2024.Entidades.Colors", b =>
                {
                    b.Property<int>("ColorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ColorId"));

                    b.Property<bool>("Active")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<string>("ColorName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ColorId");

                    b.HasIndex("ColorName")
                        .IsUnique();

                    b.HasIndex(new[] { "ColorName" }, "IX_Color_Name");

                    b.ToTable("Colors", (string)null);

                    b.HasData(
                        new
                        {
                            ColorId = 1,
                            Active = true,
                            ColorName = "Niggz"
                        },
                        new
                        {
                            ColorId = 2,
                            Active = true,
                            ColorName = "Rojo"
                        });
                });

            modelBuilder.Entity("Shoes_EF_2024.Entidades.Genre", b =>
                {
                    b.Property<int>("GenreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GenreId"));

                    b.Property<string>("GenreName")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("GenreId");

                    b.HasIndex("GenreName")
                        .IsUnique();

                    b.HasIndex(new[] { "GenreName" }, "IX_Genre_Name");

                    b.ToTable("Genre", (string)null);

                    b.HasData(
                        new
                        {
                            GenreId = 1,
                            GenreName = "Masculino"
                        },
                        new
                        {
                            GenreId = 2,
                            GenreName = "Femenino"
                        },
                        new
                        {
                            GenreId = 3,
                            GenreName = "x"
                        });
                });

            modelBuilder.Entity("Shoes_EF_2024.Entidades.ShoeSize", b =>
                {
                    b.Property<int>("ShoeSizeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ShoeSizeId"));

                    b.Property<int>("QuantityInStock")
                        .HasColumnType("int");

                    b.Property<int>("ShoeId")
                        .HasColumnType("int");

                    b.Property<int>("SizeId")
                        .HasColumnType("int");

                    b.HasKey("ShoeSizeId");

                    b.HasIndex("ShoeId");

                    b.HasIndex("SizeId");

                    b.HasIndex(new[] { "QuantityInStock" }, "IX_QuantityInStock");

                    b.ToTable("ShoeSizes", (string)null);
                });

            modelBuilder.Entity("Shoes_EF_2024.Entidades.Shoes", b =>
                {
                    b.Property<int>("ShoeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ShoeId"));

                    b.Property<bool>("Active")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<int>("BrandId")
                        .HasColumnType("int");

                    b.Property<int>("ColorID")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("GenreId")
                        .HasColumnType("int");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<int>("SportId")
                        .HasColumnType("int");

                    b.HasKey("ShoeId");

                    b.HasIndex("ColorID");

                    b.HasIndex(new[] { "BrandId" }, "FK_Shoes_BrandId");

                    b.HasIndex(new[] { "GenreId" }, "FK_Shoes_GenreId");

                    b.HasIndex(new[] { "SportId" }, "FK_Shoes_SportId");

                    b.ToTable("Shoes", (string)null);

                    b.HasData(
                        new
                        {
                            ShoeId = 1,
                            Active = true,
                            BrandId = 1,
                            ColorID = 1,
                            Description = "Vans",
                            GenreId = 2,
                            Model = "Calle",
                            Price = 15m,
                            SportId = 3
                        },
                        new
                        {
                            ShoeId = 2,
                            Active = true,
                            BrandId = 2,
                            ColorID = 1,
                            Description = "Botas de Lluvia ",
                            GenreId = 1,
                            Model = "Botas",
                            Price = 20m,
                            SportId = 1
                        });
                });

            modelBuilder.Entity("Shoes_EF_2024.Entidades.Sizes", b =>
                {
                    b.Property<int>("SizeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SizeId"));

                    b.Property<decimal>("sizeNumber")
                        .HasPrecision(3, 1)
                        .HasColumnType("decimal (3, 1)");

                    b.HasKey("SizeId");

                    b.HasIndex("sizeNumber")
                        .IsUnique();

                    b.HasIndex(new[] { "SizeId" }, "IX_Size");

                    b.ToTable("Sizes", (string)null);

                    b.HasData(
                        new
                        {
                            SizeId = 1,
                            sizeNumber = 34m
                        },
                        new
                        {
                            SizeId = 2,
                            sizeNumber = 34.5m
                        },
                        new
                        {
                            SizeId = 3,
                            sizeNumber = 35.0m
                        },
                        new
                        {
                            SizeId = 4,
                            sizeNumber = 35.5m
                        },
                        new
                        {
                            SizeId = 5,
                            sizeNumber = 36.0m
                        },
                        new
                        {
                            SizeId = 6,
                            sizeNumber = 36.5m
                        },
                        new
                        {
                            SizeId = 7,
                            sizeNumber = 37.0m
                        },
                        new
                        {
                            SizeId = 8,
                            sizeNumber = 37.5m
                        },
                        new
                        {
                            SizeId = 9,
                            sizeNumber = 38.0m
                        },
                        new
                        {
                            SizeId = 10,
                            sizeNumber = 38.5m
                        },
                        new
                        {
                            SizeId = 11,
                            sizeNumber = 39.0m
                        },
                        new
                        {
                            SizeId = 12,
                            sizeNumber = 39.5m
                        },
                        new
                        {
                            SizeId = 13,
                            sizeNumber = 40.0m
                        },
                        new
                        {
                            SizeId = 14,
                            sizeNumber = 40.5m
                        },
                        new
                        {
                            SizeId = 15,
                            sizeNumber = 41.0m
                        },
                        new
                        {
                            SizeId = 16,
                            sizeNumber = 41.5m
                        },
                        new
                        {
                            SizeId = 17,
                            sizeNumber = 42.0m
                        },
                        new
                        {
                            SizeId = 18,
                            sizeNumber = 42.5m
                        },
                        new
                        {
                            SizeId = 19,
                            sizeNumber = 43.0m
                        },
                        new
                        {
                            SizeId = 20,
                            sizeNumber = 43.5m
                        },
                        new
                        {
                            SizeId = 21,
                            sizeNumber = 44.0m
                        },
                        new
                        {
                            SizeId = 22,
                            sizeNumber = 44.5m
                        },
                        new
                        {
                            SizeId = 23,
                            sizeNumber = 45.0m
                        },
                        new
                        {
                            SizeId = 24,
                            sizeNumber = 45.5m
                        },
                        new
                        {
                            SizeId = 25,
                            sizeNumber = 46.0m
                        },
                        new
                        {
                            SizeId = 26,
                            sizeNumber = 46.5m
                        },
                        new
                        {
                            SizeId = 27,
                            sizeNumber = 47.0m
                        });
                });

            modelBuilder.Entity("Shoes_EF_2024.Entidades.Sports", b =>
                {
                    b.Property<int>("SportId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SportId"));

                    b.Property<bool>("Active")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<string>("SportName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("SportId");

                    b.HasIndex(new[] { "SportName" }, "IX_Sport_Name");

                    b.ToTable("Sports", (string)null);

                    b.HasData(
                        new
                        {
                            SportId = 1,
                            Active = true,
                            SportName = "Basquet"
                        },
                        new
                        {
                            SportId = 2,
                            Active = true,
                            SportName = "bocha internacional"
                        },
                        new
                        {
                            SportId = 3,
                            Active = true,
                            SportName = "air guittar"
                        });
                });

            modelBuilder.Entity("Shoes_EF_2024.Entidades.ShoeSize", b =>
                {
                    b.HasOne("Shoes_EF_2024.Entidades.Shoes", "Shoe")
                        .WithMany("shoesize")
                        .HasForeignKey("ShoeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Shoes_EF_2024.Entidades.Sizes", "Size")
                        .WithMany("shoesize")
                        .HasForeignKey("SizeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Shoe");

                    b.Navigation("Size");
                });

            modelBuilder.Entity("Shoes_EF_2024.Entidades.Shoes", b =>
                {
                    b.HasOne("Shoes_EF_2024.Entidades.Brands", "brand")
                        .WithMany("shoes")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Shoes_BrandId");

                    b.HasOne("Shoes_EF_2024.Entidades.Colors", "color")
                        .WithMany()
                        .HasForeignKey("ColorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Shoes_EF_2024.Entidades.Genre", "genre")
                        .WithMany("shoes")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Shoes_GenreId");

                    b.HasOne("Shoes_EF_2024.Entidades.Sports", "sport")
                        .WithMany("shoes")
                        .HasForeignKey("SportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Shoes_SportId");

                    b.Navigation("brand");

                    b.Navigation("color");

                    b.Navigation("genre");

                    b.Navigation("sport");
                });

            modelBuilder.Entity("Shoes_EF_2024.Entidades.Brands", b =>
                {
                    b.Navigation("shoes");
                });

            modelBuilder.Entity("Shoes_EF_2024.Entidades.Genre", b =>
                {
                    b.Navigation("shoes");
                });

            modelBuilder.Entity("Shoes_EF_2024.Entidades.Shoes", b =>
                {
                    b.Navigation("shoesize");
                });

            modelBuilder.Entity("Shoes_EF_2024.Entidades.Sizes", b =>
                {
                    b.Navigation("shoesize");
                });

            modelBuilder.Entity("Shoes_EF_2024.Entidades.Sports", b =>
                {
                    b.Navigation("shoes");
                });
#pragma warning restore 612, 618
        }
    }
}
