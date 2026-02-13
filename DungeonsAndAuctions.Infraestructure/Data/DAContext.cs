using System;
using System.Collections.Generic;
using D_A.Infraestructure.Models;
using Microsoft.EntityFrameworkCore;

namespace D_A.Infraestructure.Data;

public partial class DAContext : DbContext
{
    public DAContext(DbContextOptions<DAContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AuctionBidHistory> AuctionBidHistory { get; set; }

    public virtual DbSet<AuctionHistory> AuctionHistory { get; set; }

    public virtual DbSet<AuctionWinner> AuctionWinner { get; set; }

    public virtual DbSet<Auctions> Auctions { get; set; }

    public virtual DbSet<AunctionState> AunctionState { get; set; }

    public virtual DbSet<Categories> Categories { get; set; }

    public virtual DbSet<Conditions> Conditions { get; set; }

    public virtual DbSet<Countries> Countries { get; set; }

    public virtual DbSet<Genders> Genders { get; set; }

    public virtual DbSet<Images> Images { get; set; }

    public virtual DbSet<Objects> Objects { get; set; }

    public virtual DbSet<Payment> Payment { get; set; }

    public virtual DbSet<Paymentstatus> Paymentstatus { get; set; }

    public virtual DbSet<ProceduresHistory> ProceduresHistory { get; set; }

    public virtual DbSet<Qualities> Qualities { get; set; }

    public virtual DbSet<Roles> Roles { get; set; }

    public virtual DbSet<Users> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuctionBidHistory>(entity =>
        {
            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("id");
            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.BidDate).HasColumnType("datetime");

            entity.HasOne(d => d.Auction).WithMany(p => p.AuctionBidHistory)
                .HasForeignKey(d => d.AuctionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AuctionBidHistory_Auctions");

            entity.HasOne(d => d.User).WithMany(p => p.AuctionBidHistory)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AuctionBidHistory_Users");
        });

        modelBuilder.Entity<AuctionHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AuctionH__3214EC077971FC48");

            entity.HasOne(d => d.Auction).WithMany(p => p.AuctionHistory)
                .HasForeignKey(d => d.AuctionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AuctionHistory_Auctions");

            entity.HasOne(d => d.User).WithMany(p => p.AuctionHistory)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AuctionHistory_Users");
        });

        modelBuilder.Entity<AuctionWinner>(entity =>
        {
            entity.HasKey(e => e.Idauctionwinner);

            entity.Property(e => e.Idauctionwinner)
                .ValueGeneratedNever()
                .HasColumnName("idauctionwinner");
            entity.Property(e => e.Actionid).HasColumnName("actionid");
            entity.Property(e => e.Bidwinningid)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("bidwinningid");
            entity.Property(e => e.Closeddate)
                .HasColumnType("datetime")
                .HasColumnName("closeddate");
            entity.Property(e => e.Finalprice)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("finalprice");
            entity.Property(e => e.Winnerid).HasColumnName("winnerid");

            entity.HasOne(d => d.Action).WithMany(p => p.AuctionWinner)
                .HasForeignKey(d => d.Actionid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AuctionWinner_Auctions");

            entity.HasOne(d => d.Bidwinning).WithMany(p => p.AuctionWinner)
                .HasForeignKey(d => d.Bidwinningid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AuctionWinner_AuctionBidHistory");
        });

        modelBuilder.Entity<Auctions>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Auctions__3214EC0753850EB7");

            entity.Property(e => e.BasePrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Idobject).HasColumnName("idobject");
            entity.Property(e => e.Idstate).HasColumnName("idstate");
            entity.Property(e => e.Idusercreator).HasColumnName("idusercreator");
            entity.Property(e => e.IncrementoMinimo).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.IdobjectNavigation).WithMany(p => p.Auctions)
                .HasForeignKey(d => d.Idobject)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Auctions_Objects");

            entity.HasOne(d => d.IdstateNavigation).WithMany(p => p.Auctions)
                .HasForeignKey(d => d.Idstate)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Auctions_AunctionState");

            entity.HasOne(d => d.IdusercreatorNavigation).WithMany(p => p.Auctions)
                .HasForeignKey(d => d.Idusercreator)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Auctions_Users");
        });

        modelBuilder.Entity<AunctionState>(entity =>
        {
            entity.HasKey(e => e.Idstate);

            entity.Property(e => e.Idstate)
                .ValueGeneratedNever()
                .HasColumnName("idstate");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Categories>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Categori__3214EC079B666AC8");

            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Conditions>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Conditio__3214EC0794F4A031");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Countries>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Countrie__3214EC07C0F92E6E");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Genders>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Genders__3214EC072303DDB7");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Images>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Images__3214EC0790AA63BE");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Objects>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Objects__3214EC077F2EF0BC");

            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.IdCondition).HasColumnName("idCondition");
            entity.Property(e => e.IdState).HasColumnName("idState");
            entity.Property(e => e.Idimage).HasColumnName("idimage");
            entity.Property(e => e.MarketPrice).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.IdConditionNavigation).WithMany(p => p.Objects)
                .HasForeignKey(d => d.IdCondition)
                .HasConstraintName("FK_Objects_Conditions");

            entity.HasOne(d => d.IdStateNavigation).WithMany(p => p.Objects)
                .HasForeignKey(d => d.IdState)
                .HasConstraintName("FK_Objects_Qualities1");

            entity.HasOne(d => d.IdimageNavigation).WithMany(p => p.Objects)
                .HasForeignKey(d => d.Idimage)
                .HasConstraintName("FK_Objects_Images");

            entity.HasOne(d => d.User).WithMany(p => p.Objects)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Objects_Users");

            entity.HasMany(d => d.Category).WithMany(p => p.Object)
                .UsingEntity<Dictionary<string, object>>(
                    "ObjectCategories",
                    r => r.HasOne<Categories>().WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_ObjectCategories_Categories"),
                    l => l.HasOne<Objects>().WithMany()
                        .HasForeignKey("ObjectId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_ObjectCategories_Objects"),
                    j =>
                    {
                        j.HasKey("ObjectId", "CategoryId");
                    });
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.Property(e => e.PaymentId)
                .ValueGeneratedNever()
                .HasColumnName("paymentID");
            entity.Property(e => e.Amount)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("amount");
            entity.Property(e => e.AuctionId).HasColumnName("auctionID");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("dateCreated");
            entity.Property(e => e.Dateconfirmed)
                .HasColumnType("datetime")
                .HasColumnName("dateconfirmed");
            entity.Property(e => e.PaymentStatusId).HasColumnName("paymentStatusID");
            entity.Property(e => e.WinnerUserId).HasColumnName("winnerUserID");

            entity.HasOne(d => d.Auction).WithMany(p => p.Payment)
                .HasForeignKey(d => d.AuctionId)
                .HasConstraintName("FK_Payment_Auctions");

            entity.HasOne(d => d.PaymentStatus).WithMany(p => p.Payment)
                .HasForeignKey(d => d.PaymentStatusId)
                .HasConstraintName("FK_Payment_paymentstatus");

            entity.HasOne(d => d.WinnerUser).WithMany(p => p.Payment)
                .HasForeignKey(d => d.WinnerUserId)
                .HasConstraintName("FK_Payment_AuctionWinner");
        });

        modelBuilder.Entity<Paymentstatus>(entity =>
        {
            entity.ToTable("paymentstatus");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
        });

        modelBuilder.Entity<ProceduresHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Procedur__3214EC077193CB03");

            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.User).WithMany(p => p.ProceduresHistory)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProceduresHistory_Users");
        });

        modelBuilder.Entity<Qualities>(entity =>
        {
            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Quality).HasMaxLength(50);
        });

        modelBuilder.Entity<Roles>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Roles__3214EC079C5F9996");

            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Users>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC0791DEB0DF");

            entity.Property(e => e.AboutMe).HasMaxLength(255);
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.PasswordHash).HasMaxLength(256);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.UserName).HasMaxLength(100);

            entity.HasOne(d => d.Country).WithMany(p => p.Users)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_Countries");

            entity.HasOne(d => d.Gender).WithMany(p => p.Users)
                .HasForeignKey(d => d.GenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_Genders");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_Roles");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
