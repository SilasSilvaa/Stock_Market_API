using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using api.Mappers;
using api.DTOs;
using api.DTOs.Stock;

namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController(AplicationDBContext context) : ControllerBase
    {
        private readonly AplicationDBContext _context = context;

        [HttpGet]
        public IActionResult GetAll()
        {
            var stocks= _context.Stock.ToList()
            .Select(s => s.ToStockDTO());
            ;
            return Ok(stocks);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            Stock stock = _context.Stock.Find(id);
            
            if(stock == null)
            {
                return NotFound();
            }

            return Ok(stock.ToStockDTO());
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateStockRequestDto stockDto)
        {
            var stockModel = stockDto.ToStockFromCreateDTO();
            _context.Stock.Add(stockModel);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new {id = stockModel.Id }, stockModel.ToStockDTO());
            
        }    
    }
}