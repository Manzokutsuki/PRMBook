using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using BookCore.Entities;

namespace BookCore.Data
{
    public partial class BookDbContext : DbContext
    {
        public BookDbContext()
        {
        }

        public BookDbContext(DbContextOptions<BookDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblBook> TblBooks { get; set; } = null!;
        public virtual DbSet<TblCart> TblCarts { get; set; } = null!;
        public virtual DbSet<TblCartItem> TblCartItems { get; set; } = null!;
        public virtual DbSet<TblCategory> TblCategories { get; set; } = null!;
        public virtual DbSet<TblOrder> TblOrders { get; set; } = null!;
        public virtual DbSet<TblOrderDetail> TblOrderDetails { get; set; } = null!;
        public virtual DbSet<TblReceiver> TblReceivers { get; set; } = null!;
        public virtual DbSet<TblReceiverDetail> TblReceiverDetails { get; set; } = null!;
        public virtual DbSet<TblUser> TblUsers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=prm-book.ck2aaha0wa9a.ap-southeast-1.rds.amazonaws.com;Database=Book;uid=admin;pwd=prm12345;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblBook>(entity =>
            {
                entity.ToTable("tblBook");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.CategoryId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("categoryID");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("date")
                    .HasColumnName("createDate")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Image)
                    .IsUnicode(false)
                    .HasColumnName("image");

                entity.Property(e => e.Language)
                    .HasMaxLength(50)
                    .HasColumnName("language");

                entity.Property(e => e.Name)
                    .HasMaxLength(25)
                    .HasColumnName("name");

                entity.Property(e => e.Page).HasColumnName("page");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.PublisherName)
                    .HasMaxLength(50)
                    .HasColumnName("publisherName");

                entity.Property(e => e.PublisherPhone)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("publisherPhone");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.ReleaseYear).HasColumnName("releaseYear");

                entity.Property(e => e.Size).HasColumnName("size");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Supplier)
                    .HasMaxLength(50)
                    .HasColumnName("supplier");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.TblBooks)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_tblBook_tblCategory");
            });

            modelBuilder.Entity<TblCart>(entity =>
            {
                entity.ToTable("tblCart");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("userID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblCarts)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_tblCart_tblUser");
            });

            modelBuilder.Entity<TblCartItem>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tblCartItem");

                entity.Property(e => e.BookId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("bookID");

                entity.Property(e => e.CartId).HasColumnName("cartID");

                entity.Property(e => e.Price)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("price");

                entity.Property(e => e.Quantity)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("quantity");

                entity.Property(e => e.StatusId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("statusID");

                entity.HasOne(d => d.Book)
                    .WithMany()
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK_tblCartItem_tblUser");

                entity.HasOne(d => d.Cart)
                    .WithMany()
                    .HasForeignKey(d => d.CartId)
                    .HasConstraintName("FK_tblCartItem_tblCart");
            });

            modelBuilder.Entity<TblCategory>(entity =>
            {
                entity.HasKey(e => e.CategoryId);

                entity.ToTable("tblCategory");

                entity.Property(e => e.CategoryId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("categoryID");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("categoryName");
            });

            modelBuilder.Entity<TblOrder>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK_tblOrders");

                entity.ToTable("tblOrder");

                entity.Property(e => e.OrderId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("orderID");

                entity.Property(e => e.OrderDate)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("orderDate");

                entity.Property(e => e.Quantity)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("quantity");

                entity.Property(e => e.ReceiverDetailId).HasColumnName("receiverDetailID");

                entity.Property(e => e.StatusId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("statusID");

                entity.Property(e => e.TotalMoney)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("totalMoney");

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("userID");

                entity.HasOne(d => d.ReceiverDetail)
                    .WithMany(p => p.TblOrders)
                    .HasForeignKey(d => d.ReceiverDetailId)
                    .HasConstraintName("FK_tblOrder_tblReceiverDetail");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblOrders)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_tblOrders_tblUsers");
            });

            modelBuilder.Entity<TblOrderDetail>(entity =>
            {
                entity.HasKey(e => e.OrderDetailId)
                    .HasName("PK__tblOrder__E4FEDE2AA4D6E259");

                entity.ToTable("tblOrderDetail");

                entity.Property(e => e.OrderDetailId).HasColumnName("orderDetailID");

                entity.Property(e => e.BookId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("bookID");

                entity.Property(e => e.OrderId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("orderID");

                entity.Property(e => e.Price)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("price");

                entity.Property(e => e.PublisherName)
                    .HasMaxLength(50)
                    .HasColumnName("publisherName");

                entity.Property(e => e.PublisherPhone)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("publisherPhone");

                entity.Property(e => e.Quantity)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("quantity");

                entity.Property(e => e.StatusId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("statusID");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.TblOrderDetails)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK_tblOrderDetail_tblBook");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.TblOrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_tblOrderDetail_tblOrders");
            });

            modelBuilder.Entity<TblReceiver>(entity =>
            {
                entity.ToTable("tblReceiver");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("userID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblReceivers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_tblReceiver_tblUser");
            });

            modelBuilder.Entity<TblReceiverDetail>(entity =>
            {
                entity.ToTable("tblReceiverDetail");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("address");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("phone");

                entity.Property(e => e.ReceiverId).HasColumnName("receiverID");

                entity.HasOne(d => d.Receiver)
                    .WithMany(p => p.TblReceiverDetails)
                    .HasForeignKey(d => d.ReceiverId)
                    .HasConstraintName("FK_tblReceiverDetail_tblReceiver");
            });

            modelBuilder.Entity<TblUser>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK_tblUsers");

                entity.ToTable("tblUser");

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("userID");

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("address");

                entity.Property(e => e.CreateDate)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("createDate");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("phone");

                entity.Property(e => e.Role)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("role");

                entity.Property(e => e.StatusId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("statusID");

                entity.Property(e => e.Uid)
                    .IsUnicode(false)
                    .HasColumnName("uid");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
