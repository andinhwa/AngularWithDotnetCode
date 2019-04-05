using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApp.Core.DBContexts.EntityTypeConfigurations;
using WebApp.Core.Models;
using WebApp.Core.Models.Identity;

namespace WebApp.Core {
    public class WebAppContext : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken> {

        private readonly BaseEntityTypeConfigurationOption _baseEntityTypeConfigurationOption;
        private DefaultSqlValueQueryBuilder _defaultSqlValueQueryBuilder;
        protected virtual DefaultSqlValueQueryBuilder DefaultSqlValueQueryBuilder
            => _defaultSqlValueQueryBuilder ?? (_defaultSqlValueQueryBuilder = new DefaultSqlValueQueryBuilder());

        public WebAppContext (DbContextOptions options) : base (options) {

              _baseEntityTypeConfigurationOption = new BaseEntityTypeConfigurationOption
            {
                DefaultSqlValue = DefaultSqlValueQueryBuilder
            };
         }

        public DbSet<Client> Clients { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            _baseEntityTypeConfigurationOption.Context = this;

            base.OnModelCreating(modelBuilder);
            ApplyEntityConfiguration();
            ApplyIdentityConfiguration();

            void ApplyEntityConfiguration()
            {
                var entityTypeBuilderType = typeof(EntityTypeBuilder);
                var configurationType = typeof(IBaseEntityTypeConfiguration);
                var baseConfigurationTypes = new[] { typeof(BaseEntityTypeConfiguration<BaseEntity>)};
                var assembly = configurationType.GetTypeInfo().Assembly;
                var sqlCommentBuilder = typeof(DefaultSqlValueQueryBuilder);

                var configurationImplementedTypes = assembly.DefinedTypes
                    .Where(t => configurationType.IsAssignableFrom(t) && configurationType != t && !baseConfigurationTypes.Any(bt => bt.Name == t.Name));

                foreach (var configurationImplementedType in configurationImplementedTypes)
                {
                    var entityType = configurationImplementedType.BaseType.GenericTypeArguments.FirstOrDefault();

                    var entityMethod = typeof(ModelBuilder)
                        .GetMethods()
                        .FirstOrDefault(m => m.Name == nameof(ModelBuilder.Entity) && m.IsGenericMethod && entityTypeBuilderType.IsAssignableFrom(m.ReturnType));

                    var entityBuilder = entityMethod
                        .MakeGenericMethod(entityType)
                        .Invoke(modelBuilder, new object[] { });

                    var configurationInstance = Activator.CreateInstance(configurationImplementedType, entityBuilder, _baseEntityTypeConfigurationOption);
                }
            }

            void ApplyIdentityConfiguration()
            {
                var identitySchema = "Identity";

                var userBuilder = modelBuilder.Entity<User>();
                var roleBuilder = modelBuilder.Entity<Role>();
                var userClaimBuilder = modelBuilder.Entity<UserClaim>();
                var userRoleBuilder = modelBuilder.Entity<UserRole>();
                var userLoginBuilder = modelBuilder.Entity<UserLogin>();
                var roleClaimBuilder = modelBuilder.Entity<RoleClaim>();
                var userTokenBuilder = modelBuilder.Entity<UserToken>();

                var clientBuilder = modelBuilder.Entity<Client>();
                var refreshTokenBuilder = modelBuilder.Entity<RefreshToken>();

                // userBuilder.Property(u => u.Id)
                //        .HasDefaultValueSql(_defaultSqlValueQueryBuilder.NewSequentialIdSql);

                userRoleBuilder
                    .HasOne(ur => ur.User)
                    .WithMany(u => u.UserRoles)
                    .HasForeignKey(ur => ur.UserId);

                userRoleBuilder
                    .HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId);

                // clientBuilder.Property(u => u.Id)
                //         .HasDefaultValueSql(_defaultSqlValueQueryBuilder.NewSequentialIdSql);

                // refreshTokenBuilder.Property(u => u.Id)
                //         .HasDefaultValueSql(_defaultSqlValueQueryBuilder.NewSequentialIdSql);

                refreshTokenBuilder.HasOne(at => at.Client)
                    .WithMany(u => u.RefreshTokens)
                    .HasForeignKey(at => at.ClientId);

                if (!DefaultSqlValueQueryBuilder.SkipSchema)
                {
                    userBuilder
                        .ToTable(nameof(Users), identitySchema);

                    roleBuilder
                        .ToTable(nameof(Roles), identitySchema);

                    userClaimBuilder
                        .ToTable(nameof(UserClaims), identitySchema);

                    userRoleBuilder
                        .ToTable(nameof(UserRoles), identitySchema);

                    userLoginBuilder
                        .ToTable(nameof(UserLogins), identitySchema);

                    roleClaimBuilder
                        .ToTable(nameof(RoleClaims), identitySchema);

                    userTokenBuilder
                        .ToTable(nameof(UserTokens), identitySchema);
                }
            }
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void OnBeforeSaving()
        {
            ChangeTracker.DetectChanges();

            PopulateUpdatedDate();
            //PopulateRecordStatus();

            void PopulateUpdatedDate()
            {
                foreach (var entry in ChangeTracker.Entries().Where(e => e.State == EntityState.Modified))
                {
                    var propertyName = nameof(BaseEntity.UpdatedDate);
                    if (entry.Properties.Any(p => p.Metadata.Name.Equals(propertyName, StringComparison.InvariantCultureIgnoreCase)))
                    {
                        SetPropertyEntryValue(entry, propertyName, DateTime.UtcNow);
                    }
                }
            }

            //void PopulateRecordStatus()
            //{
            //    foreach (var entry in ChangeTracker.Entries().Where(e => e.State == EntityState.Deleted))
            //    {
            //        var recordStatusPropertyName = nameof(BaseEntity.RecordStatus);
            //        if (entry.Properties.Any(p => p.Metadata.Name.Equals(recordStatusPropertyName, StringComparison.InvariantCultureIgnoreCase)))
            //        {
            //            SetPropertyEntryValue(entry, recordStatusPropertyName, RecordStatus.Delete, EntityState.Modified);
            //            SetPropertyEntryValue(entry, nameof(BaseEntity.DeletedDate), DateTime.UtcNow);
            //        }
            //    }
            //}

            void SetPropertyEntryValue(EntityEntry entry, string propertyName, object value, EntityState? entityState = null)
            {
                var property = entry.Property(propertyName);
                if (property != null)
                {
                    property.CurrentValue = value;

                    if (entityState.HasValue)
                    {
                        entry.State = entityState.Value;
                    }
                }
            }
        }
    }
}