using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarTest.Models.RadzenRentacar
{
  [Table("Categories", Schema = "dbo")]
  public partial class Category
  {
    [Key]
    public int Id
    {
      get;
      set;
    }


    public ICollection<Car> Cars { get; set; }
    public string Name
    {
      get;
      set;
    }
    public decimal DailyRate
    {
      get;
      set;
    }
  }
}
