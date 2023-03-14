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

  [Route("odata/RadzenRentacar/Categories")]
  public partial class CategoriesController : ODataController
  {
    private Data.RadzenRentacarContext context;

    public CategoriesController(Data.RadzenRentacarContext context)
    {
      this.context = context;
    }
    // GET /odata/RadzenRentacar/Categories
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.RadzenRentacar.Category> GetCategories()
    {
      var items = this.context.Categories.AsQueryable<Models.RadzenRentacar.Category>();
      this.OnCategoriesRead(ref items);

      return items;
    }

    partial void OnCategoriesRead(ref IQueryable<Models.RadzenRentacar.Category> items);

    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet("/odata/RadzenRentacar/Categories(Id={Id})")]
    public SingleResult<Category> GetCategory(int key)
    {
        var items = this.context.Categories.Where(i=>i.Id == key);
        this.OnCategoriesGet(ref items);

        return SingleResult.Create(items);
    }

    partial void OnCategoriesGet(ref IQueryable<Models.RadzenRentacar.Category> items);

    partial void OnCategoryDeleted(Models.RadzenRentacar.Category item);

    [HttpDelete("/odata/RadzenRentacar/Categories(Id={Id})")]
    public IActionResult DeleteCategory(int key)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var itemToDelete = this.context.Categories
                .Where(i => i.Id == key)
                .Include(i => i.Cars)
                .FirstOrDefault();

            if (itemToDelete == null)
            {
                ModelState.AddModelError("", "Item no longer available");
                return BadRequest(ModelState);
            }

            this.OnCategoryDeleted(itemToDelete);
            this.context.Categories.Remove(itemToDelete);
            this.context.SaveChanges();

            return new NoContentResult();
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnCategoryUpdated(Models.RadzenRentacar.Category item);

    [HttpPut("/odata/RadzenRentacar/Categories(Id={Id})")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PutCategory(int key, [FromBody]Models.RadzenRentacar.Category newItem)
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

            this.OnCategoryUpdated(newItem);
            this.context.Categories.Update(newItem);
            this.context.SaveChanges();

            var itemToReturn = this.context.Categories.Where(i => i.Id == key);
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    [HttpPatch("/odata/RadzenRentacar/Categories(Id={Id})")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PatchCategory(int key, [FromBody]Delta<Models.RadzenRentacar.Category> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var itemToUpdate = this.context.Categories.Where(i => i.Id == key).FirstOrDefault();

            if (itemToUpdate == null)
            {
                ModelState.AddModelError("", "Item no longer available");
                return BadRequest(ModelState);
            }

            patch.Patch(itemToUpdate);

            this.OnCategoryUpdated(itemToUpdate);
            this.context.Categories.Update(itemToUpdate);
            this.context.SaveChanges();

            var itemToReturn = this.context.Categories.Where(i => i.Id == key);
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnCategoryCreated(Models.RadzenRentacar.Category item);

    [HttpPost]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult Post([FromBody] Models.RadzenRentacar.Category item)
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

            this.OnCategoryCreated(item);
            this.context.Categories.Add(item);
            this.context.SaveChanges();

            return Created($"odata/RadzenRentacar/Categories/{item.Id}", item);
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }
  }
}
