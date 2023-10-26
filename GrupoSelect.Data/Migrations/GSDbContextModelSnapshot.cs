﻿// <auto-generated />
using System;
using GrupoSelect.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GrupoSelect.Data.Migrations
{
    [DbContext(typeof(GSDbContext))]
    partial class GSDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("GrupoSelect.Domain.Entity.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)")
                        .HasColumnName("Address");

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("CPF");

                    b.Property<string>("Cep")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Cep");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("City");

                    b.Property<string>("Complement")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)")
                        .HasColumnName("Complement");

                    b.Property<string>("Contact")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Contact");

                    b.Property<string>("DateBirth")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("DateBirth");

                    b.Property<DateTime>("DateCreate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("DateCreate");

                    b.Property<string>("DateExp")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("DateExp");

                    b.Property<DateTime?>("DateUpdate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("DateUpdate");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Email");

                    b.Property<string>("Income")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("Income");

                    b.Property<string>("MaritalStatus")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("MaritalStatus");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("Name");

                    b.Property<string>("Nationality")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Nationality");

                    b.Property<string>("NaturalFrom")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("NaturalFrom");

                    b.Property<string>("Neighborhood")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Neighborhood");

                    b.Property<string>("OrganExp")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Profession")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Profession");

                    b.Property<string>("RG")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("RG");

                    b.Property<string>("Sex")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("Sex");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("State");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Clients", (string)null);
                });

            modelBuilder.Entity("GrupoSelect.Domain.Entity.Contract", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    b.Property<byte[]>("ContractConsultancy")
                        .HasColumnType("longblob")
                        .HasColumnName("ContractConsultancy");

                    b.Property<string>("ContractConsultancyFileType")
                        .HasColumnType("longtext")
                        .HasColumnName("ContractConsultancyFileType");

                    b.Property<byte[]>("ContractFinancialAdmin")
                        .HasColumnType("longblob")
                        .HasColumnName("ContractFinancialAdmin");

                    b.Property<string>("ContractFinancialAdminFileType")
                        .HasColumnType("longtext")
                        .HasColumnName("ContractFinancialAdminFileType");

                    b.Property<string>("ContractNum")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("ContractNum");

                    b.Property<DateTime?>("DateAproved")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("DateAproved");

                    b.Property<DateTime>("DateCreate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("DateCreate");

                    b.Property<DateTime?>("DateStatus")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("DateStatus");

                    b.Property<int>("ProposalId")
                        .HasColumnType("int")
                        .HasColumnName("ProposalId");

                    b.Property<string>("ReprovedExplain")
                        .HasColumnType("longtext")
                        .HasColumnName("ReprovedExplain");

                    b.Property<string>("ReprovedReason")
                        .HasColumnType("longtext")
                        .HasColumnName("ReprovedReason");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("Status");

                    b.Property<int?>("UserIdAproved")
                        .HasColumnType("int")
                        .HasColumnName("UserIdAproved");

                    b.Property<byte[]>("VideoAgree")
                        .HasColumnType("longblob")
                        .HasColumnName("VideoAgree");

                    b.Property<string>("VideoAgreeFileType")
                        .HasColumnType("longtext")
                        .HasColumnName("VideoAgreeFileType");

                    b.HasKey("Id");

                    b.HasIndex("ProposalId");

                    b.ToTable("Contract", (string)null);
                });

            modelBuilder.Entity("GrupoSelect.Domain.Entity.ContractConfig", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    b.Property<string>("ContractNum")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("ContractNum");

                    b.Property<string>("DayWeekUpdate")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("DayWeekUpdate");

                    b.Property<bool>("HasWeekUpdate")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("HasWeekUpdate");

                    b.HasKey("Id");

                    b.ToTable("ContractConfig", (string)null);
                });

            modelBuilder.Entity("GrupoSelect.Domain.Entity.ContractHistoric", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)")
                        .HasColumnName("Id");

                    b.Property<string>("ContractNum")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("ContractNum");

                    b.Property<DateTime>("DateRegister")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("DateRegister");

                    b.Property<int>("ProposalId")
                        .HasColumnType("int")
                        .HasColumnName("ProposalId");

                    b.Property<string>("ReprovedExplain")
                        .HasColumnType("longtext")
                        .HasColumnName("ReprovedExplain");

                    b.Property<string>("ReprovedReason")
                        .HasColumnType("longtext")
                        .HasColumnName("ReprovedReason");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("Status");

                    b.Property<int>("UserIdRegister")
                        .HasColumnType("int")
                        .HasColumnName("UserIdRegister");

                    b.HasKey("Id");

                    b.ToTable("ContractHistoric", (string)null);
                });

            modelBuilder.Entity("GrupoSelect.Domain.Entity.Credit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    b.Property<string>("CreditValue")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("CreditValue");

                    b.Property<DateTime>("DateCreate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("DateCreate");

                    b.Property<DateTime?>("DateUpdate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("DateUpdate");

                    b.Property<int>("FinancialAdminId")
                        .HasColumnType("int")
                        .HasColumnName("FinancialAdminId");

                    b.Property<string>("MembershipValue")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("MembershipValue");

                    b.Property<int>("Months")
                        .HasColumnType("int")
                        .HasColumnName("Months");

                    b.Property<string>("PortionValue")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("PortionValue");

                    b.Property<int>("ProductTypeId")
                        .HasColumnType("int")
                        .HasColumnName("ProductTypeId");

                    b.Property<int>("TableTypeId")
                        .HasColumnType("int")
                        .HasColumnName("TableTypeId");

                    b.Property<string>("TotalValue")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("TotalValue");

                    b.HasKey("Id");

                    b.HasIndex("FinancialAdminId");

                    b.HasIndex("ProductTypeId");

                    b.HasIndex("TableTypeId");

                    b.ToTable("Credit", (string)null);
                });

            modelBuilder.Entity("GrupoSelect.Domain.Entity.FinancialAdmin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    b.Property<bool?>("Active")
                        .IsRequired()
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("Active");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.ToTable("FinancialAdmin", (string)null);
                });

            modelBuilder.Entity("GrupoSelect.Domain.Entity.LogError", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)")
                        .HasColumnName("Id");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("Date");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("Message");

                    b.Property<string>("Method")
                        .HasColumnType("longtext")
                        .HasColumnName("Method");

                    b.Property<string>("Object")
                        .HasColumnType("longtext")
                        .HasColumnName("Object");

                    b.Property<string>("Username")
                        .HasColumnType("longtext")
                        .HasColumnName("Username");

                    b.HasKey("Id");

                    b.ToTable("LogError", (string)null);
                });

            modelBuilder.Entity("GrupoSelect.Domain.Entity.LogSystem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)")
                        .HasColumnName("Id");

                    b.Property<string>("Action")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("Action");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("Date");

                    b.Property<string>("NewValues")
                        .HasColumnType("longtext")
                        .HasColumnName("NewValues");

                    b.Property<string>("Object")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("Object");

                    b.Property<string>("OriginalValues")
                        .HasColumnType("longtext")
                        .HasColumnName("OriginalValues");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("Username");

                    b.HasKey("Id");

                    b.ToTable("LogSystem", (string)null);
                });

            modelBuilder.Entity("GrupoSelect.Domain.Entity.ProductType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("ProductName");

                    b.HasKey("Id");

                    b.ToTable("ProductType", (string)null);
                });

            modelBuilder.Entity("GrupoSelect.Domain.Entity.Profile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    b.Property<bool>("Active")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("Active");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.ToTable("Profiles", (string)null);
                });

            modelBuilder.Entity("GrupoSelect.Domain.Entity.Proposal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    b.Property<bool>("Aproved")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("Aproved");

                    b.Property<int>("ClientId")
                        .HasColumnType("int")
                        .HasColumnName("ClientId");

                    b.Property<string>("CreditMembershipValue")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("CreditMembershipValue");

                    b.Property<string>("CreditPortionValue")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("CreditPortionValue");

                    b.Property<string>("CreditTotalValue")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("CreditTotalValue");

                    b.Property<string>("CreditValue")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("CreditValue");

                    b.Property<DateTime?>("DateChecked")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("DateChecked");

                    b.Property<DateTime>("DateCreate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("DateCreate");

                    b.Property<string>("FinancialAdminName")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("FinancialAdminName");

                    b.Property<string>("ProductTypeName")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("ProductTypeName");

                    b.Property<string>("Seller")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("Seller");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("Status");

                    b.Property<string>("TableTypeCommission")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("TableTypeCommission");

                    b.Property<string>("TableTypeFee")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("TableTypeFee");

                    b.Property<string>("TableTypeManager")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("TableTypeManager");

                    b.Property<string>("TableTypeRate")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("TableTypeRate");

                    b.Property<string>("TableTypeTax")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("TableTypeTax");

                    b.Property<int?>("UserChecked")
                        .HasColumnType("int")
                        .HasColumnName("UserChecked");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("UserId");

                    b.ToTable("Proposal", (string)null);
                });

            modelBuilder.Entity("GrupoSelect.Domain.Entity.TableType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    b.Property<string>("CommissionFee")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("CommissionFee");

                    b.Property<string>("ManagerFee")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("ManagerFee");

                    b.Property<string>("MembershipFee")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("MembershipFee");

                    b.Property<string>("RemainingRate")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("RemainingRate");

                    b.Property<string>("TableTax")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("TableTax");

                    b.HasKey("Id");

                    b.ToTable("TableType", (string)null);
                });

            modelBuilder.Entity("GrupoSelect.Domain.Entity.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    b.Property<bool>("Active")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("Active");

                    b.Property<bool>("BranchWithoutAdm")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("BranchWithoutAdm");

                    b.Property<string>("Cnpj")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Cnpj");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)")
                        .HasColumnName("Email");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Login");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)")
                        .HasColumnName("Name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Password");

                    b.Property<string>("Profile")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Profile");

                    b.Property<string>("Representation")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("Representation");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("GrupoSelect.Domain.Entity.Client", b =>
                {
                    b.HasOne("GrupoSelect.Domain.Entity.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("GrupoSelect.Domain.Entity.Contract", b =>
                {
                    b.HasOne("GrupoSelect.Domain.Entity.Proposal", "Proposal")
                        .WithMany()
                        .HasForeignKey("ProposalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Proposal");
                });

            modelBuilder.Entity("GrupoSelect.Domain.Entity.Credit", b =>
                {
                    b.HasOne("GrupoSelect.Domain.Entity.FinancialAdmin", "FinancialAdmin")
                        .WithMany()
                        .HasForeignKey("FinancialAdminId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GrupoSelect.Domain.Entity.ProductType", "ProductType")
                        .WithMany()
                        .HasForeignKey("ProductTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GrupoSelect.Domain.Entity.TableType", "TableType")
                        .WithMany()
                        .HasForeignKey("TableTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FinancialAdmin");

                    b.Navigation("ProductType");

                    b.Navigation("TableType");
                });

            modelBuilder.Entity("GrupoSelect.Domain.Entity.Proposal", b =>
                {
                    b.HasOne("GrupoSelect.Domain.Entity.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GrupoSelect.Domain.Entity.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
