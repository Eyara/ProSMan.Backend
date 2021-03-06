﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProSMan.Backend.Core.Extensions;

namespace ProSMan.Backend.Model
{
	public class CategoryConfiguration : DbEntityConfiguration<Category>
	{
		public override void Configure(EntityTypeBuilder<Category> entity)
		{
			entity.ToTable("Categories");
			entity.HasKey(x => x.Id);

			entity.Property(x => x.Id)
				.IsRequired()
				.ValueGeneratedNever();

			entity.HasOne(x => x.Project)
				.WithMany(x => x.Categories)
				.HasForeignKey(x => x.ProjectId)
				.OnDelete(DeleteBehavior.Restrict);

			entity.HasMany(x => x.Tasks)
				.WithOne(y => y.Category);
		}
	}
}
