using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class SurveyResultConfiguration : IEntityTypeConfiguration<SurveyResult>
{
    public void Configure(EntityTypeBuilder<SurveyResult> builder)
    {
        builder.ToTable("SurveyResults").HasKey(sr => sr.Id);

        builder.Property(sr => sr.Id).HasColumnName("Id").IsRequired();
        builder.Property(sr => sr.SurveyId).HasColumnName("SurveyId");
        builder.Property(sr => sr.ParticipantId).HasColumnName("ParticipantId");
        builder.Property(sr => sr.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(sr => sr.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(sr => sr.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(sr => !sr.DeletedDate.HasValue);
    }
}