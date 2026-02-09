using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TMB_REST.Data;
using TMB_REST.Models;

namespace TMB_REST.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("_myAllowSpecificOrigins")]
    public class OrderModelsController : Controller
    {
        private readonly OrderContext _context;

        public OrderModelsController(OrderContext context)
        {
            _context = context;
        }

        // GET: api/OrderModels
        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _context.OrderModel.ToListAsync();
            return Ok(orders);
        }

        // GET: api/OrderModels/index
        [HttpGet("index")]
        public async Task<IActionResult> Index()
        {
            return Ok(await _context.OrderModel.ToListAsync());
        }

        // GET: api/OrderModels/details/5
        [HttpGet("details/{id:int}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var orderModel = await _context.OrderModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderModel == null) return NotFound();

            return Ok(orderModel);
        }

        // GET: api/OrderModels/create
        [HttpGet("create")]
        public IActionResult Create()
        {
            return Ok();
        }

        // POST: api/OrderModels/create
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] OrderModel dto)
        {
            if (dto == null) return BadRequest();

            var order = new OrderModel(0, dto.Cliente, dto.Produto, dto.Valor, dto.Status, dto.Data_Criacao);
            _context.OrderModel.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Details), new { id = order.Id }, order);
        }

        // GET: api/OrderModels/edit/5
        [HttpGet("edit/{id:int}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var orderModel = await _context.OrderModel.FindAsync(id);
            if (orderModel == null) return NotFound();
            return Ok(orderModel);
        }

        // POST: api/OrderModels/edit/5
        [HttpPost("edit/{id:int}")]
        public async Task<IActionResult> Edit(int id, [FromBody] OrderModel dto)
        {
            if (dto == null) return BadRequest();

            var orderModel = await _context.OrderModel.FindAsync(id);
            if (orderModel == null) return NotFound();

            // Atualiza propriedades
            orderModel.Cliente = dto.Cliente;
            orderModel.Produto = dto.Produto;
            orderModel.Valor = dto.Valor;
            orderModel.Status = dto.Status;
            orderModel.Data_Criacao = dto.Data_Criacao;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderModelExists(orderModel.Id)) return NotFound();
                throw;
            }

            return NoContent();
        }

        // GET: api/OrderModels/delete/5
        [HttpGet("delete/{id:int}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var orderModel = await _context.OrderModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderModel == null) return NotFound();

            return Ok(orderModel);
        }

        // POST: api/OrderModels/delete/5
        [HttpPost("delete/{id:int}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderModel = await _context.OrderModel.FindAsync(id);
            if (orderModel != null)
            {
                _context.OrderModel.Remove(orderModel);
                await _context.SaveChangesAsync();
            }

            return NoContent();
        }

        private bool OrderModelExists(int id)
        {
            return _context.OrderModel.Any(e => e.Id == id);
        }
    }
}
