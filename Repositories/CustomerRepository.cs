using csharp_api.DatabaseModels;
using csharp_api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace csharp_api.Repositories;

public class CustomerRepository : ICustomer
{
    // Objeto que facilita el acceso a la base de datos
    private readonly DatabaseContext _context;
    // Se inyecta el objeto DatabaseContext
    public CustomerRepository(DatabaseContext context)
        => _context = context;

    // Obtiene todos los clientes de la base de datos
    public async Task<Customer[]> GetAll()
    {
        try
        {
            // Se obtienen todos los clientes de la base de datos
            Customer[] customers = await _context.Customers.ToArrayAsync();
            // Se retornan los clientes
            return customers;
        }
        catch (Exception ex)
        {
            // Se lanza una excepción con el mensaje de error
            throw new Exception(ex.Message);
        }
    }

    public async Task<Customer> GetById(int id)
    {
        try
        {
            Customer customer = await _context.Customers.FirstAsync
            (
                x =>
                x.CustomerId == id
            );

            return customer;
        }
        catch (Exception ex)
        {
            // Se lanza una excepción con el mensaje de error
            throw new Exception(ex.Message);
        }
    }

    public async Task<string> Create(Customer customer)
    {
        try
        {
            // Se agrega el cliente a la base de datos.
            await _context.Customers.AddAsync(customer);
            // Se guardan los cambios en la base de datos.
            await _context.SaveChangesAsync();
            // Se retorna true para indicar que el cliente se agregó correctamente.
            return "Cliente agregado con éxito.";
        }
        catch (Exception ex)
        {
            // Se lanza una excepción con el mensaje de error
            return $"Error al agregar el cliente: {ex.InnerException?.Message ?? ex.Message}";
        }
    }

    public async Task<string> Update(Customer customer)
    {
        try
        {
            // Validar que el cliente se encuentre registrado en la base de datos.
            if (_context.Customers.Any
            (
                x =>
                x.CustomerId == customer.CustomerId
            ))
            {
                // Se actualiza el cliente en la base de datos.
                _context.Customers.Update(customer);
                // Se guardan los cambios en la base de datos.
                await _context.SaveChangesAsync();
                return "Cliente actualizado con éxito.";
            }

            return "El cliente no se encuentra registrado.";
        }
        catch (Exception ex)
        {
            // Se lanza una excepción con el mensaje de error
            return $"Error al actualizar el cliente: {ex.InnerException?.Message ?? ex.Message}";
        }
    }

    public async Task<string> Delete(int id)
    {
        try
        {
            // Validar que el cliente se encuentre registrado en la base de datos.
            if (_context.Customers.Any
            (
                x =>
                x.CustomerId == id
            ))
            {
                // Se elimina el cliente de la base de datos.
                _context.Customers.Remove(new()
                {
                    // Para eliminar un registro usando este método
                    // debes asegurarte de que estás proporcionando todos
                    // los campos que componen la llave primaria.
                    CustomerId = id
                });
                // Se guardan los cambios en la base de datos.
                await _context.SaveChangesAsync();
                return "Cliente eliminado con éxito.";
            }

            return "El cliente no se encuentra registrado.";
        }
        catch (Exception ex)
        {
            return $"Error al eliminar el cliente: {ex.InnerException?.Message ?? ex.Message}";
        }
    }
}
