using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarTest.Models.RadzenRentacar
{
  [Table("availableCars", Schema = "dbo")]
  public partial class AvailableCar
  {
    [Key]
    public int Id
    {
      get;
      set;
    }
    public int CategoryId
    {
      get;
      set;
    }
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
    public bool AirConditioning
    {
      get;
      set;
    }
    public int Doors
    {
      get;
      set;
    }
    public int Passengers
    {
      get;
      set;
    }
    public bool IsAutomatic
    {
      get;
      set;
    }
    public string Picture
    {
      get;
      set;
    }
    public string CategoryName
    {
      get;
      set;
    }
    public decimal? DailyRate
    {
      get;
      set;
    }
  }
}
