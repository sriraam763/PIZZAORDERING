using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PIZZAORDERING.Data;
using PIZZAORDERING.Model;
using PIZZAORDERING.Models;

namespace PIZZAORDERING.Controllers;

public class BrowserController : Controller
{
    private readonly AppDbContext _Db;
    public BrowserController( AppDbContext App)
    {
        _Db = App;
    }
    
    [HttpGet("Allproducts")]
    public async Task<IActionResult> GetAll()
    {
        var list = await _Db.Products.ToListAsync();
        var result = new List<ProductResponseDto>();
        foreach (var n in list)
        {
            var cato = await _Db.ProductCatogries.FirstOrDefaultAsync(u => u.Id == n.CatogoryId);
            var bran = await _Db.ProductBrands.FirstOrDefaultAsync(u => u.Id == n.BrandId);
            var now = new ProductResponseDto
            {
                ProductId = n.Id,
                Name = n.Name,
                Description = n.Description,
                Price = n.Price,
                PackagingInfo = n.PakaginInfo,
                ImageUrl = n.ImageUrl,
                IsAvailable = n.isAvailabel,
                StockQuantity = 99,
                CategoryName = cato.Name,
                BrandName = bran.Name,
                Veg = n.Veg
            };
            result.Add(now);
        }

        return Ok(result);
    }
    
    [HttpGet("getbyid")]
    public async Task<IActionResult> getbyid([FromBody]Guid id)
    {
        var pro = await _Db.Products.FirstOrDefaultAsync(u => u.Id == id);
        var brand = await _Db.ProductBrands.FirstOrDefaultAsync(u => u.Id == pro.BrandId);
        var cato = await _Db.ProductCatogries.FirstOrDefaultAsync(u => u.Id == pro.CatogoryId);
        var result = new ProductResponseDto
        {
            ProductId = pro.Id,
            Name = pro.Name,
            Description = pro.Name,
            Price = pro.Price,
            PackagingInfo = pro.PakaginInfo,
            ImageUrl = pro.ImageUrl,
            IsAvailable = true,
            StockQuantity = 99,
            CategoryName = cato.Name,
            BrandName = brand.Name,
            Veg = pro.Veg
        };
        return Ok(result);
    }

    [HttpGet("searchbyname")]
    public async Task<IActionResult> getbysearch([FromBody]string? name,[FromBody]string? catogories,string? brand,[FromBody]bool? veg)
    {
        var produc =  _Db.Products.AsQueryable();
        if (!string.IsNullOrEmpty(name))
        {
            produc=produc.Where(u => u.Name.Contains(name));
        }

        if (!string.IsNullOrEmpty(catogories))
        { 
            var cato =await _Db.ProductCatogries.FirstOrDefaultAsync(u => u.Name.Equals(catogories));
            produc = produc.Where(u => u.CatogoryId==cato.Id);
        }

        if (string.IsNullOrEmpty(brand))
        {
            var bran = await _Db.ProductBrands.FirstOrDefaultAsync(u => u.Name.Equals(brand));
            produc = produc.Where(u => u.BrandId == bran.Id);
        }

        if (veg!=null)
        {
            produc = produc.Where(u => u.Veg == veg);
        }

        var pro = await produc.ToListAsync();
        var result = new List<ProductResponseDto>();
        foreach (var n in pro)
        {
            var cato = await _Db.ProductCatogries.FirstOrDefaultAsync(u => u.Id == n.CatogoryId);
            var bran = await _Db.ProductBrands.FirstOrDefaultAsync(u => u.Id == n.BrandId);
            var now = new ProductResponseDto
            {
                ProductId = n.Id,
                Name = n.Name,
                Description = n.Description,
                Price = n.Price,
                PackagingInfo = n.PakaginInfo,
                ImageUrl = n.ImageUrl,
                IsAvailable = n.isAvailabel,
                StockQuantity = 99,
                CategoryName = cato.Name,
                BrandName = bran.Name,
                Veg = n.Veg
            };
            result.Add(now);
        }

        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("changedetails")]
    public async Task<IActionResult> changedetails(UpdateProductDto dto,Guid Id)
    {
        var product = await _Db.Products.FirstOrDefaultAsync(u => u.Id == Id);
        
        if (product is null) return NotFound();

        if (dto.Name        != null) product.Name         = dto.Name;
        if (dto.Description != null) product.Description  = dto.Description;
        if (dto.Price       != null) product.Price        = dto.Price.Value;
        if (dto.PackagingInfo != null) product.PakaginInfo = dto.PackagingInfo;
        if (dto.ImageUrl    != null) product.ImageUrl     = dto.ImageUrl;
        if (dto.IsAvailable != null) product.isAvailabel  = dto.IsAvailable;
        product.UpdatedAt = DateTime.UtcNow;

        await _Db.SaveChangesAsync();
        return Ok(new { message = "updated success" });
    }
    
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreateProductDto dto)
    {
        var categoryExists = await _Db.ProductCatogries.AnyAsync(c => c.Id == dto.CategoryId);
        if (!categoryExists) return BadRequest("Category not found.");

        var product = new Products
        {
            CatogoryId    = dto.CategoryId,
            BrandId       = dto.BrandId,
            Name          = dto.Name,
            Description   = dto.Description,
            Price         = dto.Price,
            PakaginInfo = dto.PackagingInfo,
            ImageUrl      = dto.ImageUrl,
            isAvailabel   = true,
            CreatedAt     = DateTime.UtcNow
        };

        _Db.Products.Add(product);
        await _Db.SaveChangesAsync();

        _Db.Inventories.Add(new Inventory
        {
            ProductId     = product.Id,
            StockQuantity = dto.InitialStock,
            RecordLEvel  = 10,
            LastUPDated   = DateTime.UtcNow
        });

        await _Db.SaveChangesAsync();

        return Ok(new {message="created the product"});
    }

    // [HttpDelete("deleteproduct")]
    // [Authorize(Roles = "Admin")]
    // public async Task<IActionResult> deleteprod([FromBody]Guid Id)
    // {
    //     
    // }
}