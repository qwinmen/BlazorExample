using BlazingTrails.Api.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazingTrails.Api.Persistence
{
	/// <summary>
	///     Database context
	/// </summary>
	public class BlazingTrailsContext: DbContext
	{
		public BlazingTrailsContext(DbContextOptions options)
			: base(options)
		{
		}

		public DbSet<Trail> Trails => Set<Trail>();
		public DbSet<RouteInstruction> RouteInstructions => Set<RouteInstruction>();

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfiguration(new TrailConfig());
			modelBuilder.ApplyConfiguration(new RouteInstructionConfig());
		}
	}

	#region Entity db configuration:

	public class TrailConfig: IEntityTypeConfiguration<Trail>
	{
		public void Configure(EntityTypeBuilder<Trail> builder)
		{
			builder.Property(e => e.Name).IsRequired();
			builder.Property(e => e.Description).IsRequired();
			builder.Property(e => e.Location).IsRequired();
			builder.Property(e => e.TimeInMinutes).IsRequired();
			builder.Property(e => e.Length).IsRequired();
			builder.ToTable(t => t.HasComment("Описание маршрута."));
		}
	}

	public class RouteInstructionConfig: IEntityTypeConfiguration<RouteInstruction>
	{
		public void Configure(EntityTypeBuilder<RouteInstruction> builder)
		{
			builder.Property(e => e.TrailId).IsRequired();
			builder.Property(e => e.Description).IsRequired();
			builder.Property(e => e.Stage).IsRequired();
			builder.ToTable(t => t.HasComment("Путевые точки маршрута."));
		}
	}

	#endregion
}
