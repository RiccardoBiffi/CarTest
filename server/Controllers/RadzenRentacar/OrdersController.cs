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

  [Route("odata/RadzenRentacar/Orders")]
  public partial class OrdersController : ODataController
  {
    private Data.RadzenRentacarContext context;

    public OrdersController(Data.RadzenRentacarContext context)
    {
      this.context = context;
    }
    // GET /odata/RadzenRentacar/Orders
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.RadzenRentacar.Order> GetOrders()
    {
      var items = this.context.Orders.AsQueryable<Models.RadzenRentacar.Order>();
      this.OnOrdersRead(ref items);

      return items;
    }

    partial void OnOrdersRead(ref IQueryable<Models.RadzenRentacar.Order> items);

    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet("/odata/RadzenRentacar/Orders(Id={Id})")]
    public SingleResult<Order> GetOrder(int? key)
    {
        var items = this.context.Orders.Where(i=>i.Id == key);
        this.OnOrdersGet(ref items);

        return SingleResult.Create(items);
    }

    partial void OnOrdersGet(ref IQueryable<Models.RadzenRentacar.Order> items);

    partial void OnOrderDeleted(Models.RadzenRentacar.Order item);

    [HttpDelete("/odata/RadzenRentacar/Orders(Id={Id})")]
    public IActionResult DeleteOrder(int? key)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var itemToDelete = this.context.Orders
                .Where(i => i.Id == key)
                .FirstOrDefault();

            if (itemToDelete == null)
            {
                ModelState.AddModelError("", "Item no longer available");
                return BadRequest(ModelState);
            }

            this.OnOrderDeleted(itemToDelete);
            this.context.Orders.Remove(itemToDelete);
            this.context.SaveChanges();

            return new NoContentResult();
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnOrderUpdated(Models.RadzenRentacar.Order item);

    [HttpPut("/odata/RadzenRentacar/Orders(Id={Id})")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PutOrder(int? key, [FromBody]Models.RadzenRentacar.Order newItem)
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

            this.OnOrderUpdated(newItem);
            this.context.Orders.Update(newItem);
            this.context.SaveChanges();

            var itemToReturn = this.context.Orders.Where(i => i.Id == key);
            Request.QueryString = Request.QueryString.Add("$expand", "Car");
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    [HttpPatch("/odata/RadzenRentacar/Orders(Id={Id})")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PatchOrder(int? key, [FromBody]Delta<Models.RadzenRentacar.Order> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var itemToUpdate = this.context.Orders.Where(i => i.Id == key).FirstOrDefault();

            if (itemToUpdate == null)
            {
                ModelState.AddModelError("", "Item no longer available");
                return BadRequest(ModelState);
            }

            patch.Patch(itemToUpdate);

            this.OnOrderUpdated(itemToUpdate);
            this.context.Orders.Update(itemToUpdate);
            this.context.SaveChanges();

            var itemToReturn = this.context.Orders.Where(i => i.Id == key);
            Request.QueryString = Request.QueryString.Add("$expand", "Car");
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnOrderCreated(Models.RadzenRentacar.Order item);

    [HttpPost]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult Post([FromBody] Models.RadzenRentacar.Order item)
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

            this.OnOrderCreated(item);
            this.context.Orders.Add(item);
            this.context.SaveChanges();

            var key = item.Id;

            var itemToReturn = this.context.Orders.Where(i => i.Id == key);

            Request.QueryString = Request.QueryString.Add("$expand", "Car");

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
