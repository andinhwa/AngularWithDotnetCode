using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections;
using System.Linq;
using WebApp.Core.Enums;
using WebApp.Core.Models;

namespace  WebApp.Core.DBContexts.EntityTypeConfigurations
{
    internal abstract class BaseEntityTypeConfiguration<TEntity> : IBaseEntityTypeConfiguration where TEntity : BaseEntity
    {
        protected string UniqueIndexFilterSql = $"{nameof(BaseEntity.RecordStatus)} = {(short)RecordStatus.Active}";

        public BaseEntityTypeConfiguration(EntityTypeBuilder<TEntity> builder, BaseEntityTypeConfigurationOption option)
        {
            ConfigureBaseEntity(builder, option);
        }

        private void ConfigureBaseEntity(EntityTypeBuilder<TEntity> Builder, BaseEntityTypeConfigurationOption option)
        {
            var dbSetProperties = typeof(WebAppContext).GetProperties().Where(p => p.PropertyType.Name.StartsWith(nameof(DbSet<TEntity>)));
            if (!dbSetProperties.Any(p => p.PropertyType == typeof(DbSet<TEntity>)))
            {
                throw new Exception($"Missing {typeof(TEntity).FullName} with DbSet in Context");
            }

            var entityProperties = typeof(TEntity).GetProperties().Where(p => p.PropertyType is ICollection || typeof(BaseEntity).IsAssignableFrom(p.PropertyType));
            var missingDbSets = entityProperties.Select(p => p.PropertyType).Except(dbSetProperties.Select(p => p.PropertyType.GenericTypeArguments[0]));
            if (missingDbSets.Any())
            {
                throw new Exception($"{string.Join(",", missingDbSets.Select(p => p.Name))} used as a navigation but Missing DbSet in Context");
            }

            Builder
               .HasKey(e => e.Id);

            if (!string.IsNullOrEmpty(option.DefaultSqlValue.NewSequentialIdSql))
            {
                Builder
                .Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql(option.DefaultSqlValue.NewSequentialIdSql);
            }

            Builder
               .Property(e => e.CreatedDate)
               .ValueGeneratedOnAdd()
               .HasDefaultValueSql(option.DefaultSqlValue.GetUtcDate);

            Builder
                .Property(e => e.RecordStatus)
                .HasDefaultValue(RecordStatus.Active);

            Builder
                .HasQueryFilter(e => e.RecordStatus == RecordStatus.Active);
            
            Builder
                .Property(e => e.RowVersion)
                .IsRowVersion();
        }
    }
}
