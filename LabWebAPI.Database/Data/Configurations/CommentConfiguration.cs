using LabWebAPI.Contracts.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWebAPI.Database.Data.Configurations
{

    internal class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder
            .HasKey(x => x.Id);
            builder
            .Property(x => x.Text)
            .IsRequired();
            builder
            .Property(x => x.PublicationDate)
            .HasColumnType("datetime")
            .IsRequired();
            builder
            .HasOne(x => x.UserWhoCreated)
            .WithMany(x => x.Comments)
            .HasForeignKey(x => x.UserWhoCreatedId);
            builder
         .HasOne(x => x.Product)
         .WithMany(x => x.Comments)
         .HasForeignKey(x => x.ProductId)
              .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
