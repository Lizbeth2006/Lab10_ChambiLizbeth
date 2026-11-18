using Lab10_ChambiLizbeth.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lab10_ChambiLizbeth.Infrastructure.Context;

public partial class TicketeraDbContext
{
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
    {
        // Regla de estados permitidos para un ticket
        modelBuilder.Entity<Ticket>().ToTable(t =>
            t.HasCheckConstraint("chk_ticket_status",
                "status IN ('abierto','en_proceso','cerrado')"));

        // Roles iniciales del sistema
        modelBuilder.Entity<Role>().HasData(
            new Role { RoleId = Guid.Parse("11111111-1111-1111-1111-111111111111"), RoleName = "Admin" },
            new Role { RoleId = Guid.Parse("22222222-2222-2222-2222-222222222222"), RoleName = "Soporte" },
            new Role { RoleId = Guid.Parse("33333333-3333-3333-3333-333333333333"), RoleName = "Cliente" });
    }
}