﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolApp.Server.Data;
using SchoolApp.Shared.Models;

namespace SchoolApp.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubjectGradingItemController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public SubjectGradingItemController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/SubjectGradingItem
    [HttpGet]
    public ActionResult<IEnumerable<SubjectGradingItem>> GetAll()
    {
        return Ok(_context.SubjectGradingItems.ToList());
    }

    // GET: api/SubjectGradingItem/by-subject/{subjectId}
    [HttpGet("by-subject/{subjectId}")]
    public ActionResult<IEnumerable<SubjectGradingItem>> GetBySubject(Guid subjectId)
    {
        var items = _context.SubjectGradingItems
            .Where(x => x.SubjectId == subjectId)
            .ToList();

        return Ok(items);
    }

    // POST: api/SubjectGradingItem (For creating new items)
    [HttpPost]
    public ActionResult<SubjectGradingItem> Create(SubjectGradingItem newItem) 
    {
        if (newItem == null)
            return BadRequest();
        newItem.Id = Guid.NewGuid();
        newItem.CreatedAt = DateTime.Now;

        _context.SubjectGradingItems.Add(newItem);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetById), new { id = newItem.Id }, newItem);
    }

    // NEW: GET by Id (Good practice for CreatedAtAction and direct lookup)
    [HttpGet("{id}")]
    public ActionResult<SubjectGradingItem> GetById(Guid id)
    {
        var item = _context.SubjectGradingItems.FirstOrDefault(x => x.Id == id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }

    // NEW: PUT: api/SubjectGradingItem/{id} (For updating existing items)
    [HttpPut("{id}")]
    public IActionResult Update(Guid id, SubjectGradingItem updatedItem)
    {
        if (updatedItem == null || id != updatedItem.Id)
        {
            return BadRequest("Mismatched ID or invalid data.");
        }

        var existingItem = _context.SubjectGradingItems.FirstOrDefault(x => x.Id == id);
        if (existingItem == null)
        {
            return NotFound($"Item with ID {id} not found.");
        }

        // Update properties of the existing item
        existingItem.ItemCount = updatedItem.ItemCount;
        existingItem.GradingPeriod = updatedItem.GradingPeriod; 
        existingItem.SubcomponentId = updatedItem.SubcomponentId; 
        existingItem.SubjectId = updatedItem.SubjectId;         
        existingItem.CreatedAt = DateTime.Now; 

        _context.SaveChanges();

        return NoContent(); // 204 No Content is standard for successful PUT updates
    }


    // DELETE: api/SubjectGradingItem/{id}
    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        var item = _context.SubjectGradingItems.FirstOrDefault(x => x.Id == id);
        if (item == null)
            return NotFound();

        _context.SubjectGradingItems.Remove(item);
        _context.SaveChanges();

        return NoContent();
    }
}