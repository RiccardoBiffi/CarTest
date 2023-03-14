using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarTest.Models.RadzenRentacar
{
  [Table("Orders", Schema = "dbo")]
  public partial class Order
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int? Id
    {
      get;
      set;
    }
    public DateTime OrderDate
    {
      get;
      set;
    }
    public DateTime StartDate
    {
      get;
      set;
    }
    public DateTime EndDate
    {
      get;
      set;
    }
    public string Description
    {
      get;
      set;
    }
    public int CarId
    {
      get;
      set;
    }

    public Car Car { get; set; }
  }
}
