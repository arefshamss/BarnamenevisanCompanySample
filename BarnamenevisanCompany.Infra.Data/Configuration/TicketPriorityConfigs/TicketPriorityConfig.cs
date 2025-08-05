using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarnamenevisanCompany.Infra.Data.Configuration.TicketPriorityConfigs;

public class TicketPriorityConfig : IEntityTypeConfiguration<Domain.Models.Ticket.TicketPriority>
{
    public void Configure(EntityTypeBuilder<Domain.Models.Ticket.TicketPriority> builder)
    {
        #region Key

        builder.HasKey(x => x.Id);

        #endregion

        #region Properties

        builder.Property(x => x.Title)
            .HasMaxLength(60)
            .IsRequired();

        #endregion
    }
}