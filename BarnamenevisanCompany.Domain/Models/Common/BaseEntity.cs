using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace BarnamenevisanCompany.Domain.Models.Common;


public abstract class BaseEntity<T>
{
    protected BaseEntity()
    {
        CreatedDate = DateTime.Now;
    }
    
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public T Id { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? DeletedDate { get; set; }

    public bool IsDeleted { get; set; }
}

public abstract class BaseEntity : BaseEntity<int>;
