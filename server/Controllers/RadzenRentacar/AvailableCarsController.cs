using System;
using System.Net;
using System.Data;
using System.Linq;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;



namespace CarTest.Controllers.RadzenRentacar
{
  using Models;
  using Data;
  using Models.RadzenRentacar;

  public partial class AvailableCarsController : ODataController
  {
    private Data.RadzenRentacarContext context;

    public AvailableCarsController(Data.RadzenRentacarContext context)
    {
      this.context = context;
    }

    [HttpGet]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [Route("odata/RadzenRentacar/AvailableCarsFunc(EndDate={EndDate},StartDate={StartDate})")]
    public IActionResult AvailableCarsFunc([FromODataUri] string EndDate, [FromODataUri] string StartDate)
    {
        try
        {
            this.OnAvailableCarsDefaultParams(ref EndDate, ref StartDate);
            var items = this.context.AvailableCars.FromSqlRaw("EXEC [dbo].[availableCars] @EndDate={0}, @StartDate={1}", string.IsNullOrEmpty(EndDate) ? DBNull.Value : (object)DateTime.Parse(EndDate, null, System.Globalization.DateTimeStyles.RoundtripKind), string.IsNullOrEmpty(StartDate) ? DBNull.Value : (object)DateTime.Parse(StartDate, null, System.Globalization.DateTimeStyles.RoundtripKind)).AsNoTracking().ToList().AsQueryable();

            this.OnAvailableCarsInvoke(ref items);

            return Ok(items);
        }
        catch(Exception ex)
        {
            return new ObjectResult(new { message = ex.Message})
            {
                StatusCode = 400
            };
        }
    }

    partial void OnAvailableCarsDefaultParams(ref string EndDate, ref string StartDate);

    partial void OnAvailableCarsInvoke(ref IQueryable<Models.RadzenRentacar.AvailableCar> items);
  }
}
