using csharp_api.DatabaseModels;

namespace csharp_api.Interfaces;

public interface ICustomer
{
    // Obtiene todos los clientes de la base de datos
    Task<Customer[]> GetAll();
    // Obtiene un cliente de la base de datos por su id (llave primaria)
    Task<Customer> GetById(int id);
    // Inserta un nuevo cliente en la base de datos
    Task<string> Create(Customer customer);
    // Actualiza un cliente en la base de datos
    Task<string> Update(Customer customer);
    // Elimina un cliente de la base de datos
    Task<string> Delete(int id);
}
