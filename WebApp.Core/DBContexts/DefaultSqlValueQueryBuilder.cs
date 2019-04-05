namespace WebApp.Core.DBContexts.EntityTypeConfigurations {
    public class DefaultSqlValueQueryBuilder {
        public virtual string NewSequentialIdSql => string.Empty; //"NewSequentialId()";

        public virtual string GetUtcDate => "date('now')";//"GetUtcDate()";

        public virtual bool SkipSchema => true; //false;
    }
}