using CodenameRome.Contracts.Employees;
using CodenameRome.Models;

namespace CodenameRome.Application.Interfaces
{
    public interface IEmployeeApplication
    {
        void ObfuscatePasswords(List<Employee> employeeList);
        Task ChangePass(string id, string newPassword);
        Task<Employee> Update(string id, EmployeeDto employeeDto);
        Task<List<Employee>> GetEmployeesByClientId(string id);
        Task<Employee?> GetEmployeeById(string id, string clientId);
        Task<Employee> CreateEmployee(EmployeeDto employee, string clientId, string employeeRole);
        Task ChangePassword(string clientId, string employeeId, string employeeRole, string id, string newPassword);
        Task ChangeOwnPassword(string employeeId, ChangePassRequest changePasswordDto);
        Task<Employee> UpdateEmployee(string clientId, string employeeId, string employeeRole, string id, EmployeeDto employeeDto);
        Task<Employee> DeleteEmployee(string clientId, string employeeRole, string id);
    }
}
