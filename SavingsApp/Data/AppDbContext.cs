namespace SavingsApp.Data
{
    using Microsoft.EntityFrameworkCore;
   
    using SavingsApp.Models.Entities;

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<SavingGoal> SavingGoals { get; set; }
        public DbSet<SavingTransaction> SavingTransactions { get; set; }
        public DbSet<EventSaving> EventSavings { get; set; }
        public DbSet<TravelSaving> TravelSavings { get; set; }
        public DbSet<GroupSaving> GroupSavings { get; set; }
        public DbSet<GroupMember> GroupMembers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User -> SavingGoals
            modelBuilder.Entity<SavingGoal>()
                .HasOne(x => x.User)
                .WithMany(x => x.SavingGoals)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // SavingGoal -> Transactions
            modelBuilder.Entity<SavingTransaction>()
                .HasOne(x => x.SavingGoal)
                .WithMany(x => x.SavingTransactions)
                .HasForeignKey(x => x.SavingGoalId);

            modelBuilder.Entity<SavingTransaction>()
                .HasOne(x => x.User)
                .WithMany(x => x.SavingTransactions)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // SavingGoal -> Event (1:1)
            modelBuilder.Entity<SavingGoal>()
                .HasOne(x => x.EventSaving)
                .WithOne(x => x.SavingGoal)
                .HasForeignKey<EventSaving>(x => x.SavingGoalId);

            // SavingGoal -> Travel (1:1)
            modelBuilder.Entity<SavingGoal>()
                .HasOne(x => x.TravelSaving)
                .WithOne(x => x.SavingGoal)
                .HasForeignKey<TravelSaving>(x => x.SavingGoalId);

            // SavingGoal -> Group (1:1)
            modelBuilder.Entity<SavingGoal>()
                .HasOne(x => x.GroupSaving)
                .WithOne(x => x.SavingGoal)
                .HasForeignKey<GroupSaving>(x => x.SavingGoalId);

            // Group -> Members
            modelBuilder.Entity<GroupMember>()
                .HasOne(x => x.GroupSaving)
                .WithMany(x => x.GroupMembers)
                .HasForeignKey(x => x.GroupSavingId);

            modelBuilder.Entity<GroupMember>()
                .HasOne(x => x.User)
                .WithMany(x => x.GroupMembers)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
