using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BarnamenevisanCompany.Domain.ViewModels.Admin.OrderStep;

public class AdminOrderStepViewModel
{
    public short Id { get; set; }
    public string Title { get; set; }

    public short PriorityId { get; set; }

    public DateTime CreateDate { get; set; }

    public bool IsDeleted { get; set; }
}