using System;
using System.Data.Entity.Migrations;


namespace Transprt.Data {
    sealed class TransprtConfiguration : DbMigrationsConfiguration<TransprtEntities> {
        public TransprtConfiguration() {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed= true;
        }
    }   
}
