using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SolickManagerV3_4.DTO;

public partial class SolickManagerContext : DbContext
{
    public SolickManagerContext()
    {
    }

    public SolickManagerContext(DbContextOptions<SolickManagerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Application> Applications { get; set; }

    public virtual DbSet<Applicationassembly> Applicationassemblies { get; set; }

    public virtual DbSet<Applicationproduct> Applicationproducts { get; set; }

    public virtual DbSet<Applicationservice> Applicationservices { get; set; }

    public virtual DbSet<Assembly> Assemblies { get; set; }

    public virtual DbSet<Assemblyproduct> Assemblyproducts { get; set; }

    public virtual DbSet<Bankaccount> Bankaccounts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Categorycharacteristic> Categorycharacteristics { get; set; }

    public virtual DbSet<Characteristic> Characteristics { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Clientsdevice> Clientsdevices { get; set; }

    public virtual DbSet<Howpay> Howpays { get; set; }

    public virtual DbSet<Operation> Operations { get; set; }

    public virtual DbSet<Plan> Plans { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Productcharacteristic> Productcharacteristics { get; set; }

    public virtual DbSet<Productpricechange> Productpricechanges { get; set; }

    public virtual DbSet<Provider> Providers { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<Shipment> Shipments { get; set; }

    public virtual DbSet<Worker> Workers { get; set; }

    public virtual DbSet<Workingshift> Workingshifts { get; set; }

    // строка подключения на сдаче курсача: "Host=localhost;Port=5432;Database=SolickManager;Username=postgres;Password=student"
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=SolickManager;Username=postgres;Password=vova2005");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Application>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("application_pk");

            entity.ToTable("application");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Deleted).HasColumnName("deleted");
            entity.Property(e => e.Idclient).HasColumnName("idclient");
            entity.Property(e => e.Iddevice).HasColumnName("iddevice");
            entity.Property(e => e.Idmanager).HasColumnName("idmanager");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.Problem).HasColumnName("problem");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.IdclientNavigation).WithMany(p => p.Applications)
                .HasForeignKey(d => d.Idclient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("application_fk");

            entity.HasOne(d => d.IddeviceNavigation).WithMany(p => p.Applications)
                .HasForeignKey(d => d.Iddevice)
                .HasConstraintName("application_fk2");

            entity.HasOne(d => d.IdmanagerNavigation).WithMany(p => p.Applications)
                .HasForeignKey(d => d.Idmanager)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("application_fk_1");
        });

        modelBuilder.Entity<Applicationassembly>(entity =>
        {
            entity.HasKey(e => new { e.Idapplication, e.Idassemby }).HasName("applicationassembly_pk");

            entity.ToTable("applicationassembly");

            entity.Property(e => e.Idapplication).HasColumnName("idapplication");
            entity.Property(e => e.Idassemby).HasColumnName("idassemby");
            entity.Property(e => e.Deleted).HasColumnName("deleted");

            entity.HasOne(d => d.IdapplicationNavigation).WithMany(p => p.Applicationassemblies)
                .HasForeignKey(d => d.Idapplication)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("newtable_fk");

            entity.HasOne(d => d.IdassembyNavigation).WithMany(p => p.Applicationassemblies)
                .HasForeignKey(d => d.Idassemby)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("newtable_fk_1");
        });

        modelBuilder.Entity<Applicationproduct>(entity =>
        {
            entity.HasKey(e => new { e.Idapplication, e.Idproduct }).HasName("applicationproduct_pk");

            entity.ToTable("applicationproduct");

            entity.Property(e => e.Idapplication).HasColumnName("idapplication");
            entity.Property(e => e.Idproduct).HasColumnName("idproduct");
            entity.Property(e => e.Deleted).HasColumnName("deleted");

            entity.HasOne(d => d.IdapplicationNavigation).WithMany(p => p.Applicationproducts)
                .HasForeignKey(d => d.Idapplication)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("applicationproduct_fk");

            entity.HasOne(d => d.IdproductNavigation).WithMany(p => p.Applicationproducts)
                .HasForeignKey(d => d.Idproduct)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("applicationproduct_fk_1");
        });

        modelBuilder.Entity<Applicationservice>(entity =>
        {
            entity.HasKey(e => new { e.Idapplication, e.Idservice, e.Idworker }).HasName("applicationservice_pk");

            entity.ToTable("applicationservice");

            entity.Property(e => e.Idapplication).HasColumnName("idapplication");
            entity.Property(e => e.Idservice).HasColumnName("idservice");
            entity.Property(e => e.Idworker).HasColumnName("idworker");
            entity.Property(e => e.Deleted).HasColumnName("deleted");
            entity.Property(e => e.Notes).HasColumnName("notes");

            entity.HasOne(d => d.IdapplicationNavigation).WithMany(p => p.Applicationservices)
                .HasForeignKey(d => d.Idapplication)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("applicationservice_fk");

            entity.HasOne(d => d.IdserviceNavigation).WithMany(p => p.Applicationservices)
                .HasForeignKey(d => d.Idservice)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("applicationservice_fk_1");

            entity.HasOne(d => d.IdworkerNavigation).WithMany(p => p.Applicationservices)
                .HasForeignKey(d => d.Idworker)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("applicationservice_fk_2");
        });

        modelBuilder.Entity<Assembly>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("assembly_pk");

            entity.ToTable("assembly");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cost).HasColumnType("money");
            entity.Property(e => e.Deleted).HasColumnName("deleted");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Idmasterassembler).HasColumnName("idmasterassembler");
            entity.Property(e => e.Idmasterconfiguration).HasColumnName("idmasterconfiguration");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.Title)
                .HasColumnType("character varying")
                .HasColumnName("title");

            entity.HasOne(d => d.IdmasterassemblerNavigation).WithMany(p => p.AssemblyIdmasterassemblerNavigations)
                .HasForeignKey(d => d.Idmasterassembler)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("assembly_fk_1");

            entity.HasOne(d => d.IdmasterconfigurationNavigation).WithMany(p => p.AssemblyIdmasterconfigurationNavigations)
                .HasForeignKey(d => d.Idmasterconfiguration)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("assembly_fk");
        });

        modelBuilder.Entity<Assemblyproduct>(entity =>
        {
            entity.HasKey(e => new { e.Idassembly, e.Idproduct }).HasName("assemblyproduct_pk");

            entity.ToTable("assemblyproduct");

            entity.Property(e => e.Idassembly).HasColumnName("idassembly");
            entity.Property(e => e.Idproduct).HasColumnName("idproduct");
            entity.Property(e => e.Count)
                .HasDefaultValueSql("1")
                .HasColumnName("count");
            entity.Property(e => e.Deleted).HasColumnName("deleted");

            entity.HasOne(d => d.IdassemblyNavigation).WithMany(p => p.Assemblyproducts)
                .HasForeignKey(d => d.Idassembly)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("assemblyproduct_fk");

            entity.HasOne(d => d.IdproductNavigation).WithMany(p => p.Assemblyproducts)
                .HasForeignKey(d => d.Idproduct)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("assemblyproduct_fk_1");
        });

        modelBuilder.Entity<Bankaccount>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("bankaccount_pk");

            entity.ToTable("bankaccount", tb => tb.HasComment("Таблица содержит счета (внутренние счета) компании (организации)"));

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Balance)
                .HasColumnType("money")
                .HasColumnName("balance");
            entity.Property(e => e.Deleted)
                .HasDefaultValueSql("false")
                .HasColumnName("deleted");
            entity.Property(e => e.Title)
                .HasColumnType("character varying")
                .HasColumnName("title");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("category_pk");

            entity.ToTable("category");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Deleted).HasColumnName("deleted");
            entity.Property(e => e.Title)
                .HasColumnType("character varying")
                .HasColumnName("title");
        });

        modelBuilder.Entity<Categorycharacteristic>(entity =>
        {
            entity.HasKey(e => new { e.Idcategory, e.Idcharacteristic }).HasName("categorycharacteristic_pk");

            entity.ToTable("categorycharacteristic");

            entity.Property(e => e.Idcategory).HasColumnName("idcategory");
            entity.Property(e => e.Idcharacteristic).HasColumnName("idcharacteristic");
            entity.Property(e => e.Deleted).HasColumnName("deleted");

            entity.HasOne(d => d.IdcategoryNavigation).WithMany(p => p.Categorycharacteristics)
                .HasForeignKey(d => d.Idcategory)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("categorycharacteristic_fk");

            entity.HasOne(d => d.IdcharacteristicNavigation).WithMany(p => p.Categorycharacteristics)
                .HasForeignKey(d => d.Idcharacteristic)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("categorycharacteristic_fk_1");
        });

        modelBuilder.Entity<Characteristic>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("characteristic_pk");

            entity.ToTable("characteristic");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Deleted).HasColumnName("deleted");
            entity.Property(e => e.Title)
                .HasColumnType("character varying")
                .HasColumnName("title");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("client_pk");

            entity.ToTable("client");

            entity.HasIndex(e => e.Passport, "client_un").IsUnique();

            entity.HasIndex(e => e.Email, "client_un2").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Birthday).HasColumnName("birthday");
            entity.Property(e => e.Deleted).HasColumnName("deleted");
            entity.Property(e => e.Email)
                .HasColumnType("character varying")
                .HasColumnName("email");
            entity.Property(e => e.Firstname)
                .HasColumnType("character varying")
                .HasColumnName("firstname");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.Passport)
                .HasColumnType("character varying")
                .HasColumnName("passport");
            entity.Property(e => e.Patronymic)
                .HasColumnType("character varying")
                .HasColumnName("patronymic");
            entity.Property(e => e.Phone)
                .HasColumnType("character varying")
                .HasColumnName("phone");
            entity.Property(e => e.Secondname)
                .HasColumnType("character varying")
                .HasColumnName("secondname");
        });

        modelBuilder.Entity<Clientsdevice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("clientsdevice_pk");

            entity.ToTable("clientsdevice");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cost).HasColumnType("money");
            entity.Property(e => e.Deleted).HasColumnName("deleted");
            entity.Property(e => e.Description)
                .HasColumnType("character varying")
                .HasColumnName("description");
            entity.Property(e => e.Idclient).HasColumnName("idclient");
            entity.Property(e => e.Model)
                .HasColumnType("character varying")
                .HasColumnName("model");
            entity.Property(e => e.Notes).HasColumnName("notes");

            entity.HasOne(d => d.IdclientNavigation).WithMany(p => p.Clientsdevices)
                .HasForeignKey(d => d.Idclient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("clientsdevice_fk");
        });

        modelBuilder.Entity<Howpay>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("howpay_pk");

            entity.ToTable("howpay");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Deleted).HasColumnName("deleted");
            entity.Property(e => e.Title)
                .HasColumnType("character varying")
                .HasColumnName("title");
        });

        modelBuilder.Entity<Operation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("operation_pk");

            entity.ToTable("operation");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasColumnType("money")
                .HasColumnName("amount");
            entity.Property(e => e.Credit).HasColumnName("credit");
            entity.Property(e => e.Dataclose).HasColumnName("dataclose");
            entity.Property(e => e.Dataopen).HasColumnName("dataopen");
            entity.Property(e => e.Debet).HasColumnName("debet");
            entity.Property(e => e.Deleted).HasColumnName("deleted");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.Object).HasColumnType("character varying");
            entity.Property(e => e.Status)
                .HasColumnType("character varying")
                .HasColumnName("status");

            entity.HasOne(d => d.CreditNavigation).WithMany(p => p.OperationCreditNavigations)
                .HasForeignKey(d => d.Credit)
                .HasConstraintName("operation_fk_1");

            entity.HasOne(d => d.DebetNavigation).WithMany(p => p.OperationDebetNavigations)
                .HasForeignKey(d => d.Debet)
                .HasConstraintName("operation_fk");
        });

        modelBuilder.Entity<Plan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("plan_pk");

            entity.ToTable("plan");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Costofone)
                .HasColumnType("money")
                .HasColumnName("costofone");
            entity.Property(e => e.Deleted).HasColumnName("deleted");
            entity.Property(e => e.Idhowpay).HasColumnName("idhowpay");

            entity.HasOne(d => d.IdhowpayNavigation).WithMany(p => p.Plans)
                .HasForeignKey(d => d.Idhowpay)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("plan_fk");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("post_pk");

            entity.ToTable("post");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Deleted).HasColumnName("deleted");
            entity.Property(e => e.Title)
                .HasColumnType("character varying")
                .HasColumnName("title");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("product_pk");

            entity.ToTable("product");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.Cost).HasColumnType("money");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Idcategory).HasColumnName("idcategory");
            entity.Property(e => e.Idshipment).HasColumnName("idshipment");
            entity.Property(e => e.Model)
                .HasColumnType("character varying")
                .HasColumnName("model");

            entity.HasOne(d => d.IdcategoryNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.Idcategory)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_fk");

            entity.HasOne(d => d.IdshipmentNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.Idshipment)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_fk_1");
        });

        modelBuilder.Entity<Productcharacteristic>(entity =>
        {
            entity.HasKey(e => new { e.Idproduct, e.Idcharacteristic }).HasName("productcharacteristic_pk");

            entity.ToTable("productcharacteristic");

            entity.Property(e => e.Idproduct).HasColumnName("idproduct");
            entity.Property(e => e.Idcharacteristic).HasColumnName("idcharacteristic");
            entity.Property(e => e.Deleted).HasColumnName("deleted");
            entity.Property(e => e.Meaning)
                .HasColumnType("character varying")
                .HasColumnName("meaning");

            entity.HasOne(d => d.IdcharacteristicNavigation).WithMany(p => p.Productcharacteristics)
                .HasForeignKey(d => d.Idcharacteristic)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_characteristic_fk");

            entity.HasOne(d => d.IdproductNavigation).WithMany(p => p.Productcharacteristics)
                .HasForeignKey(d => d.Idproduct)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_characteristic_fk_1");
        });

        modelBuilder.Entity<Productpricechange>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("productpricechange_pk");

            entity.ToTable("productpricechange");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Deleted).HasColumnName("deleted");
            entity.Property(e => e.Idproduct).HasColumnName("idproduct");
            entity.Property(e => e.Newcost)
                .HasColumnType("money")
                .HasColumnName("newcost");
            entity.Property(e => e.Ratio)
                .HasPrecision(10, 3)
                .HasColumnName("ratio");

            entity.HasOne(d => d.IdproductNavigation).WithMany(p => p.Productpricechanges)
                .HasForeignKey(d => d.Idproduct)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("productpricechange_fk");
        });

        modelBuilder.Entity<Provider>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("provider_pk");

            entity.ToTable("provider");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Deleted).HasColumnName("deleted");
            entity.Property(e => e.DirectorManager)
                .HasColumnType("character varying")
                .HasColumnName("director_manager");
            entity.Property(e => e.Email)
                .HasColumnType("character varying")
                .HasColumnName("email");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.Phone)
                .HasColumnType("character varying")
                .HasColumnName("phone");
            entity.Property(e => e.Requisites).HasColumnName("requisites");
            entity.Property(e => e.Title)
                .HasColumnType("character varying")
                .HasColumnName("title");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("service_pk");

            entity.ToTable("service");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cost).HasColumnType("money");
            entity.Property(e => e.Deleted).HasColumnName("deleted");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Title)
                .HasColumnType("character varying")
                .HasColumnName("title");
        });

        modelBuilder.Entity<Shipment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("shipment_pk");

            entity.ToTable("shipment");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasColumnType("money")
                .HasColumnName("amount");
            entity.Property(e => e.Deleted).HasColumnName("deleted");
            entity.Property(e => e.Idprovider).HasColumnName("idprovider");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.Numberproducts).HasColumnName("numberproducts");

            entity.HasOne(d => d.IdproviderNavigation).WithMany(p => p.Shipments)
                .HasForeignKey(d => d.Idprovider)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("shipment_fk");
        });

        modelBuilder.Entity<Worker>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("worker_pk");

            entity.ToTable("worker");

            entity.HasIndex(e => e.Passport, "worker_un").IsUnique();

            entity.HasIndex(e => e.Login, "worker_un1").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Birthday).HasColumnName("birthday");
            entity.Property(e => e.Deleted).HasColumnName("deleted");
            entity.Property(e => e.Email)
                .HasColumnType("character varying")
                .HasColumnName("email");
            entity.Property(e => e.Firstname)
                .HasColumnType("character varying")
                .HasColumnName("firstname");
            entity.Property(e => e.Graphic)
                .HasColumnType("character varying")
                .HasColumnName("graphic");
            entity.Property(e => e.Idplan).HasColumnName("idplan");
            entity.Property(e => e.Idpost).HasColumnName("idpost");
            entity.Property(e => e.Login)
                .HasColumnType("character varying")
                .HasColumnName("login");
            entity.Property(e => e.Passport)
                .HasColumnType("character varying")
                .HasColumnName("passport");
            entity.Property(e => e.Password)
                .HasColumnType("character varying")
                .HasColumnName("password");
            entity.Property(e => e.Patronymic)
                .HasColumnType("character varying")
                .HasColumnName("patronymic");
            entity.Property(e => e.Phone)
                .HasColumnType("character varying")
                .HasColumnName("phone");
            entity.Property(e => e.Requisites)
                .HasColumnType("character varying")
                .HasColumnName("requisites");
            entity.Property(e => e.Surname)
                .HasColumnType("character varying")
                .HasColumnName("surname");

            entity.HasOne(d => d.IdplanNavigation).WithMany(p => p.Workers)
                .HasForeignKey(d => d.Idplan)
                .HasConstraintName("worker_fk");

            entity.HasOne(d => d.IdpostNavigation).WithMany(p => p.Workers)
                .HasForeignKey(d => d.Idpost)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("worker_fk_1");
        });

        modelBuilder.Entity<Workingshift>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("workingshift_pk");

            entity.ToTable("workingshift");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Deleted).HasColumnName("deleted");
            entity.Property(e => e.Idworker).HasColumnName("idworker");
            entity.Property(e => e.Spendunits).HasColumnName("spendunits");

            entity.HasOne(d => d.IdworkerNavigation).WithMany(p => p.Workingshifts)
                .HasForeignKey(d => d.Idworker)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("workingshift_fk");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
