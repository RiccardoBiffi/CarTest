using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarTest.Models.RadzenRentacar
{
  [Table("Cars", Schema = "dbo")]
  public partial class Car
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int? Id
    {
      get;
      set;
    }


    public ICollection<Order> Orders { get; set; }
    public int CategoryId
    {
      get;
      set;
    }

    public Category Category { get; set; }
    public string Make
    {
      get;
      set;
    }
    public string Model
    {
      get;
      set;
    }
    public string Picture
    {
      get;
      set;
    }
    public bool AirConditioning
    {
      get;
      set;
    }
    public int Passengers
    {
      get;
      set;
    }
    public int Doors
    {
      get;
      set;
    }
    public bool IsAutomatic
    {
      get;
      set;
    }
  }
}
