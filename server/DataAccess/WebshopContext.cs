using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

{
    // ECommerceContext er DbContext-klassen, der styrer forbindelsen til databasen og kortlægger dine modeller til databasetabeller.
    public partial class WebshopContext : DbContext
    {
        // Konstruktoren modtager DbContextOptions, som indeholder konfigurationsindstillinger for databasen (f.eks. forbindelsesstreng)
        public WebshopContext(DbContextOptions<WebshopContext> options)
            : base(options)
        {
        }

        // DbSet-repræsentation af Customers-tabellen i databasen. DbSet bruges til CRUD-operationer på kundedata.
        public virtual DbSet<Customer> Customers { get; set; }

        // DbSet for Orders-tabellen, der indeholder ordrer, som kunderne har oprettet.
        public virtual DbSet<Order> Orders { get; set; }

        // DbSet for OrderEntries-tabellen, der indeholder ordrelinjer (hvert produkt i en ordre).
        public virtual DbSet<OrderEntry> OrderEntries { get; set; }

        // DbSet for Paper-tabellen, som repræsenterer papirprodukterne, der sælges i webshoppen.
        public virtual DbSet<Paper> PaperProducts { get; set; }

        // DbSet for Properties-tabellen, der indeholder produkt-egenskaber (f.eks. vandtæt, robust papir).
        public virtual DbSet<Property> Properties { get; set; }

        // DbSet for PaperProperties-tabellen, som repræsenterer mange-til-mange forhold mellem papir og dets egenskaber.
        public virtual DbSet<PaperProperty> PaperProperties { get; set; }

        // Denne metode bruges til at konfigurere databaseentiteterne, deres nøgler, relationer, og constraints.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Konfiguration af Customer-tabellen
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("customers_pkey"); // Definerer primærnøglen for tabellen

                entity.ToTable("customers"); // Kortlægger entiteten til customers-tabellen

                entity.Property(e => e.Id).HasColumnName("id"); // Kolonnenavn i databasen for kundens Id
                entity.Property(e => e.Name).HasColumnName("name").IsRequired(); // Navn skal være udfyldt
                entity.Property(e => e.Address).HasColumnName("address"); // Kolonnenavn for adresse
                entity.Property(e => e.Phone).HasColumnName("phone"); // Kolonnenavn for telefonnummer
                entity.Property(e => e.Email).HasColumnName("email").IsRequired(); // Email skal være udfyldt

                // Opretter et unikt indeks for emailfeltet for at sikre, at ingen kunder har samme emailadresse
                entity.HasIndex(e => e.Email).IsUnique().HasDatabaseName("customers_email_key");
            });

            // Konfiguration af Order-tabellen
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("orders_pkey"); // Primærnøglen for Order-tabellen

                entity.ToTable("orders"); // Kortlægger entiteten til orders-tabellen

                entity.Property(e => e.Id).HasColumnName("id"); // Kolonnenavn for ordren Id
                entity.Property(e => e.OrderDate)
                      .HasDefaultValueSql("CURRENT_TIMESTAMP") // Standardværdien for OrderDate er den aktuelle tidsstempel
                      .HasColumnName("order_date"); // Kolonnenavn for ordre dato
                entity.Property(e => e.DeliveryDate).HasColumnName("delivery_date"); // Kolonnenavn for leveringsdato
                entity.Property(e => e.Status).HasColumnName("status").IsRequired().HasDefaultValue("pending"); // Status på ordren (f.eks. pending, shipped)
                entity.Property(e => e.TotalAmount).HasColumnName("total_amount"); // Det totale beløb for ordren
                entity.Property(e => e.CustomerId).HasColumnName("customer_id"); // Fremmednøgle til Customers-tabellen

                // Definerer relationen mellem Order og Customer. En kunde kan have mange ordrer.
                entity.HasOne(d => d.Customer)
                      .WithMany(p => p.Orders)
                      .HasForeignKey(d => d.CustomerId)
                      .OnDelete(DeleteBehavior.Cascade) // Hvis en kunde slettes, slettes alle deres ordrer også
                      .HasConstraintName("orders_customer_id_fkey"); // Navn på constraint
            });

            // Konfiguration af OrderEntry-tabellen
            modelBuilder.Entity<OrderEntry>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("order_entries_pkey"); // Primærnøglen for OrderEntries-tabellen

                entity.ToTable("order_entries"); // Kortlægger entiteten til order_entries-tabellen

                entity.Property(e => e.Id).HasColumnName("id"); // Kolonnenavn for ordrelinje Id
                entity.Property(e => e.Quantity).HasColumnName("quantity").IsRequired(); // Mængden af hvert produkt i en ordrelinje
                entity.Property(e => e.ProductId).HasColumnName("product_id"); // Fremmednøgle til Paper-tabellen (produktet)
                entity.Property(e => e.OrderId).HasColumnName("order_id"); // Fremmednøgle til Order-tabellen

                // Relation mellem OrderEntry og Order. En ordre kan have mange OrderEntries (forskellige produkter).
                entity.HasOne(d => d.Order)
                      .WithMany(p => p.OrderEntries)
                      .HasForeignKey(d => d.OrderId)
                      .HasConstraintName("order_entries_order_id_fkey"); // Navn på constraint

                // Relation mellem OrderEntry og Paper. En ordrelinje refererer til ét produkt (papir).
                entity.HasOne(d => d.PaperProduct)
                      .WithMany(p => p.OrderEntries)
                      .HasForeignKey(d => d.ProductId)
                      .HasConstraintName("order_entries_product_id_fkey"); // Navn på constraint
            });

            // Konfiguration af Paper-tabellen
            modelBuilder.Entity<Paper>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("paper_pkey"); // Primærnøgle for Paper-tabellen

                entity.ToTable("paper"); // Kortlægger entiteten til paper-tabellen

                entity.Property(e => e.Id).HasColumnName("id"); // Kolonnenavn for produktets Id
                entity.Property(e => e.Name).HasColumnName("name").IsRequired(); // Navn på produktet er påkrævet
                entity.Property(e => e.Discontinued).HasColumnName("discontinued").HasDefaultValue(false); // Marker om produktet er udgået
                entity.Property(e => e.Stock).HasColumnName("stock").HasDefaultValue(0); // Lagerbeholdning af produktet
                entity.Property(e => e.Price).HasColumnName("price").IsRequired(); // Prisen på produktet

                // Sikrer, at hvert produktnavn er unikt
                entity.HasIndex(e => e.Name).IsUnique().HasDatabaseName("unique_product_name");
            });

            // Konfiguration af Property-tabellen
            modelBuilder.Entity<Property>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("properties_pkey"); // Primærnøgle for Property-tabellen

                entity.ToTable("properties"); // Kortlægger entiteten til properties-tabellen

                entity.Property(e => e.Id).HasColumnName("id"); // Kolonnenavn for egenskabens Id
                entity.Property(e => e.PropertyName).HasColumnName("property_name").IsRequired(); // Egenskabens navn er påkrævet
            });

            // Konfiguration af PaperProperty-tabellen (for mange-til-mange relationen mellem Paper og Property)
            modelBuilder.Entity<PaperProperty>(entity =>
            {
                entity.HasKey(e => new { e.PaperId, e.PropertyId }).HasName("paper_properties_pkey"); // Kombineret primærnøgle

                entity.ToTable("paper_properties"); // Kortlægger entiteten til paper_properties-tabellen

                // Relation mellem PaperProperty og Paper. Et papir kan have flere egenskaber.
                entity.HasOne(d => d.Paper)
                      .WithMany(p => p.PaperProperties)
                      .HasForeignKey(d => d.PaperId)
                      .HasConstraintName("paper_properties_paper_id_fkey"); // Navn på constraint

                // Relation mellem PaperProperty og Property. En egenskab kan være tilknyttet mange papirprodukter.
                entity.HasOne(d => d.Property)
                      .WithMany(p => p.PaperProperties)
                      .HasForeignKey(d => d.PropertyId)
                      .HasConstraintName("paper_properties_property_id_fkey"); // Navn på constraint
            });

            // Mulighed for yderligere konfiguration via en partial method
            OnModelCreatingPartial(modelBuilder);
        }

        // Partial method, hvis yderligere konfigurationer er nødvendige
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
