using CodenameRome.Contracts.Employees;
using CodenameRome.Models;

namespace CodenameRome.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetByClientId(string clientId);
        Task<Employee?> GetById(string id);
        Task<Employee?> GetByUsername(string username);
        Task Create(Employee employee);
        Task Update(string id, EmployeeDto employee);
        Task ChangePassword(string id, string newPassword);
        Task Remove(string id);
    }
}
