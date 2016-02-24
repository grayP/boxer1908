namespace DataRepository.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class BW_Model : DbContext
    {
        public BW_Model()
            : base("name=boxerdb")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new MyDBContextInitialiser());
            base.OnModelCreating(modelBuilder);

        }

        public class MyDBContextInitialiser : DropCreateDatabaseIfModelChanges<BW_Model>
        {
            protected override void Seed(BW_Model context)
            {
                base.Seed(context);
            }

        }
    }


}

