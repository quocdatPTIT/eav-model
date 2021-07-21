using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using DynamicField.Authorization.Roles;
using DynamicField.Authorization.Users;
using DynamicField.EAV;
using DynamicField.MultiTenancy;
using DynamicField.TicketModel;

namespace DynamicField.EntityFrameworkCore
{
    public class DynamicFieldDbContext : AbpZeroDbContext<Tenant, Role, User, DynamicFieldDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public DynamicFieldDbContext(DbContextOptions<DynamicFieldDbContext> options)
            : base(options)
        {
            
        }
        
        public DbSet<Ticket> Tickets { get; set; }
        
        public DbSet<EavAttribute> EavAttributes { get; set; }
        
        public DbSet<EavEntityType> EavEntityTypes { get; set; }
        
        public DbSet<TicketDateTime> TicketDateTimeValues { get; set; }
        
        public DbSet<TicketDecimal> TicketDecimalValues { get; set; }
        
        public DbSet<TicketInt> TicketIntValues { get; set; }
        
        public DbSet<TicketText> TicketTextValues { get; set; }
        
        public DbSet<TicketVarchar> TicketVarcharValues { get; set; }
        
        public DbSet<EavAttributeOption> EavAttributeOptions { get; set; }
        
        public DbSet<EavAttributeOptionValue> EavAttributeOptionValues { get; set; }
    }
}
