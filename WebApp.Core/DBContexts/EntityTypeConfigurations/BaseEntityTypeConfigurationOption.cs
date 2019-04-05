namespace  WebApp.Core.DBContexts.EntityTypeConfigurations
{
    internal class BaseEntityTypeConfigurationOption
    {
        public DefaultSqlValueQueryBuilder DefaultSqlValue { get; set; }

        public WebAppContext Context { get; set; }
    }
}
