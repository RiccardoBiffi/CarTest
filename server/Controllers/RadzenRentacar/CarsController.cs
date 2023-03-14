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

  [Route("odata/RadzenRentacar/Cars")]
  public partial class CarsController : ODataController
  {
    private Data.RadzenRentacarContext context;

    public CarsController(Data.RadzenRentacarContext context)
    {
      this.context = context;
    }
    // GET /odata/RadzenRentacar/Cars
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.RadzenRentacar.Car> GetCars()
    {
      var items = this.context.Cars.AsQueryable<Models.RadzenRentacar.Car>();
      this.OnCarsRead(ref items);

      return items;
    }

    partial void OnCarsRead(ref IQueryable<Models.RadzenRentacar.Car> items);

    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet("/odata/RadzenRentacar/Cars(Id={Id})")]
    public SingleResult<Car> GetCar(int? key)
    {
        var items = this.context.Cars.Where(i=>i.Id == key);
        this.OnCarsGet(ref items);

        return SingleResult.Create(items);
    }

    partial void OnCarsGet(ref IQueryable<Models.RadzenRentacar.Car> items);

    partial void OnCarDeleted(Models.RadzenRentacar.Car item);

    [HttpDelete("/odata/RadzenRentacar/Cars(Id={Id})")]
    public IActionResult DeleteCar(int? key)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var itemToDelete = this.context.Cars
                .Where(i => i.Id == key)
                .Include(i => i.Orders)
                .FirstOrDefault();

            if (itemToDelete == null)
            {
                ModelState.AddModelError("", "Item no longer available");
                return BadRequest(ModelState);
            }

            this.OnCarDeleted(itemToDelete);
            this.context.Cars.Remove(itemToDelete);
            this.context.SaveChanges();

            return new NoContentResult();
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnCarUpdated(Models.RadzenRentacar.Car item);

    [HttpPut("/odata/RadzenRentacar/Cars(Id={Id})")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PutCar(int? key, [FromBody]Models.RadzenRentacar.Car newItem)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (newItem == null || (newItem.Id != key))
            {
                return BadRequest();
            }

            this.OnCarUpdated(newItem);
            this.context.Cars.Update(newItem);
            this.context.SaveChanges();

            var itemToReturn = this.context.Cars.Where(i => i.Id == key);
            Request.QueryString = Request.QueryString.Add("$expand", "Category");
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    [HttpPatch("/odata/RadzenRentacar/Cars(Id={Id})")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PatchCar(int? key, [FromBody]Delta<Models.RadzenRentacar.Car> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var itemToUpdate = this.context.Cars.Where(i => i.Id == key).FirstOrDefault();

            if (itemToUpdate == null)
            {
                ModelState.AddModelError("", "Item no longer available");
                return BadRequest(ModelState);
            }

            patch.Patch(itemToUpdate);

            this.OnCarUpdated(itemToUpdate);
            this.context.Cars.Update(itemToUpdate);
            this.context.SaveChanges();

            var itemToReturn = this.context.Cars.Where(i => i.Id == key);
            Request.QueryString = Request.QueryString.Add("$expand", "Category");
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnCarCreated(Models.RadzenRentacar.Car item);

    [HttpPost]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult Post([FromBody] Models.RadzenRentacar.Car item)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (item == null)
            {
                return BadRequest();
            }

            this.OnCarCreated(item);
            this.context.Cars.Add(item);
            this.context.SaveChanges();

            var key = item.Id;

            var itemToReturn = this.context.Cars.Where(i => i.Id == key);

            Request.QueryString = Request.QueryString.Add("$expand", "Category");

            return new ObjectResult(SingleResult.Create(itemToReturn))
            {
                StatusCode = 201
            };
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }
  }
}
