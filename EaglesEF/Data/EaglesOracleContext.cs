using System;
using System.Collections.Generic;
using Eagles.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace Eagles.EF.Data;

public partial class EaglesOracleContext : DbContext
{
    public EaglesOracleContext(DbContextOptions<EaglesOracleContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDefaultSchema("UD_RICKYK")
            .UseCollation("USING_NLS_COMP");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
