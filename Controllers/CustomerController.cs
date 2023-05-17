using csharp_api.DatabaseModels;
using csharp_api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace csharp_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    // Se instancia la interfaz
    private readonly ICustomer _interface;
    public CustomerController(ICustomer @interface)
        => _interface = @interface;

    // Obtiene todos los clientes de la base de datos
    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _interface.GetAll());

    [HttpGet("getById/{id}")]
    public async Task<IActionResult> GetById(int id)
        => Ok(await _interface.GetById(id));

    [HttpPost]
    public async Task<IActionResult> Create(Customer customer)
        => Ok(await _interface.Create(customer));

    [HttpPut]
    public async Task<IActionResult> Update(Customer customer)
        => Ok(await _interface.Update(customer));

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
        => Ok(await _interface.Delete(id));
}
