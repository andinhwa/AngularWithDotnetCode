
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApp.Core.Models;

namespace WebApp.Core.DBContexts.EntityTypeConfigurations
{
    internal class CustomerTypeConfiguration : BaseEntityTypeConfiguration<Customer>
    {
        public CustomerTypeConfiguration(EntityTypeBuilder<Customer> builder, BaseEntityTypeConfigurationOption option) : base(builder, option)
        {

        }
    }
}