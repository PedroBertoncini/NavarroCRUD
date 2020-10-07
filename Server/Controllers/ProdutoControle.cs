
using CrudMysqlVSCODE.Server;
using Microsoft.AspNetCore.Mvc;
using CrudMysqlVSCODE.Shared;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

[ApiController]
[Route("[controller]")]
public class ProdutoController : Controller
{
    private readonly crudMysqlVSCODE.Server.AppDbContext db;

    public ProdutoController(crudMysqlVSCODE.Server.AppDbContext db)
    {
        this.db = db;
    }

    [HttpGet]
    [Route("List")]
    public async Task<IActionResult> Get()
    {
        var produtos = await db.Produtos.ToListAsync();
        return Ok(produtos);
    }

    [HttpGet]
    [Route("GetById")]
    public async Task<IActionResult> Get([FromQuery] string id)
    {
        var produto = await db.Produtos.SingleOrDefaultAsync(x => x.Id == Convert.ToInt32(id));
        return Ok(produto);
    }

    [HttpPost]
    [Route("Create")]
    public async Task<ActionResult> Post([FromBody] Produto produto)
    {
        try
        {
            var newProduto = new Produto
            {
                Id = produto.Id,
                valor = produto.valor,
                descricao = produto.descricao,
            };

            db.Add(newProduto);
            await db.SaveChangesAsync();//INSERT INTO
            return Ok();
        }
        catch (Exception e)
        {
            return View(e);
        }
    }

    [HttpPut]
    [Route("Update")]
    public async Task<IActionResult> Put([FromBody] Produto produto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        db.Entry(produto).State = EntityState.Modified;
        try
        {
            await db.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw (ex);
        }
        return NoContent();
    }

    [HttpDelete]
    [Route("Delete/{id}")]
    public async Task<ActionResult<Produto>> Delete(int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var produto = await db.Produtos.FindAsync(id);
        if (produto == null)
        {
            return NotFound();
        }
        db.Produtos.Remove(produto);
        await db.SaveChangesAsync();
        return produto;
    }

    private bool ProdutoExists(int id)
    {
        return db.Produtos.Any(e => e.Id == id);
    }

}