using System;
using System.Data.Entity.Migrations;


namespace Transprt.Data {
    sealed class TransprtConfiguration : DbMigrationsConfiguration<TransportEntity> {
        public TransprtConfiguration() {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed= true;
        }
    }   
}
