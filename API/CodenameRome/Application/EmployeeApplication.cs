using CodenameRome.Application.Interfaces;
using CodenameRome.Contracts.Employees;
using CodenameRome.Models;
using CodenameRome.Services.Interfaces;

namespace CodenameRome.Application
{
    public class EmployeeApplication : IEmployeeApplication
    {
        private readonly IEmployeeService _employeeService;
        private readonly string MANAGERACCESSLEVEL = "20";
        private readonly string NOTFOUND = "Employee not found.";
        private readonly string UNAUTHORIZED = "Not authorized.";
        private readonly string WRONGPASSWORD = "The password you entered is incorrect.";
        private readonly string USERALREADYEXISTS = "The username already exists in the system.";
        private readonly string MUSTHAVEUSERNAME = "The employee must have a username.";
        private readonly string MUSTHAVEPASSWORD = "The employee must have a password.";

        public EmployeeApplication(IEmployeeService employeeService) =>
            _employeeService = employeeService;

        public void ObfuscatePasswords(List<Employee> employeeList)
        {
            foreach (var employee in employeeList)
            {
                employee.ObfuscatePassword();
            }
        }
        public async Task ChangePass(string id, string newPassword)
        {
            var employeeDto = new EmployeeDto();
            employeeDto.Password = newPassword;

            employeeDto.ValidatePassword();
            await _employeeService.ChangePassword(id, employeeDto.Password);
            return;
        }

        public async Task<Employee> Update(string id, EmployeeDto employeeDto)
        {
            employeeDto.ValidateEmployee();
            await _employeeService.Update(id, employeeDto);
            var employee = await _employeeService.GetById(id);
            employee!.ObfuscatePassword();

            return employee;
        }

        public async Task<List<Employee>> GetEmployeesByClientId(string id)
        {
            var employeeList = await _employeeService.GetByClientId(id);

            ObfuscatePasswords(employeeList);

            return employeeList;
        }

        public async Task<Employee?> GetEmployeeById(string id, string clientId)
        {
            var employee = await _employeeService.GetById(id);
            
            if (employee == null || employee.ClientId != clientId)
                throw new Exception(NOTFOUND);

            employee.ObfuscatePassword();
            return employee;
        }

        public async Task<Employee> CreateEmployee(EmployeeDto employee, string clientId, string employeeRole)
        {
            employee.ValidateEmployeeCreation();

            if (employee.Username != null)
            {
                var isUsernameInUse = await _employeeService.GetByUsername(employee.Username!);
                if (isUsernameInUse != null)
                    throw new Exception(USERALREADYEXISTS);
            }

            var newEmployee = new Employee
            {
                ClientId = clientId,
                Name = employee.Name!,
                Address = employee.Address,
                PhoneNumber = employee.PhoneNumber,
                Salary = employee.Salary,
                Username = employee.Username,
                Password = employee.Password,
                AccessLevel = employeeRole == MANAGERACCESSLEVEL? "30" : employee.AccessLevel
            };

            await _employeeService.Create(newEmployee);
            newEmployee.ObfuscatePassword();
            return newEmployee;
        }

        public async Task ChangePassword(string clientId, string employeeId, string employeeRole, string id, string newPassword)
        {
            var employee = await _employeeService.GetById(id);

            if (employee == null || employee.ClientId != clientId)
                throw new ArgumentNullException(NOTFOUND);

            if (employee.Username == null)
                throw new Exception(MUSTHAVEUSERNAME);

            if (employee.Id == employeeId)
            {
                await ChangePass(id, newPassword);
                return;
            }

            if (employeeRole.CompareTo(employee.AccessLevel) >= 0)
                throw new InvalidOperationException(UNAUTHORIZED);


            await ChangePass(id, newPassword);
            return;
        }

        public async Task ChangeOwnPassword(string employeeId, ChangePassRequest changePasswordDto)
        {
            var employee = await _employeeService.GetById(employeeId);

            if (employee == null)
                throw new ArgumentNullException(NOTFOUND);

            if (!employee.VerifyPassword(changePasswordDto.OldPassword))
                throw new InvalidOperationException(WRONGPASSWORD);

            await ChangePass(employeeId, changePasswordDto.NewPassword);
            return;
        }

        public async Task<Employee> UpdateEmployee(string clientId, string employeeId, string employeeRole, string id, EmployeeDto employeeDto)
        {
            var employee = await _employeeService.GetById(id);

            if (employee == null || employee.ClientId != clientId)
                throw new ArgumentNullException(NOTFOUND);

            if (employee.Username == null && employee.Password == null)
            {
                if (employeeDto.Username != null && employeeDto.Password == null)
                    throw new Exception(MUSTHAVEPASSWORD);

                if (employeeDto.Password != null && employeeDto.Username == null)
                    throw new Exception(MUSTHAVEUSERNAME);
            }

            if (employee.Id == employeeId)
            {
                employee = await Update(id, employeeDto);
                return employee!;
            }

            if (employeeRole.CompareTo(employee.AccessLevel) >= 0)
                throw new InvalidOperationException(UNAUTHORIZED);

            employee = await Update(id, employeeDto);
            return employee!;
        }

        public async Task<Employee> DeleteEmployee(string clientId, string employeeRole, string id)
        {
            var employee = await _employeeService.GetById(id);

            if (employee == null || employee.ClientId != clientId)
                throw new ArgumentNullException(NOTFOUND);

            if (employeeRole.CompareTo(employee.AccessLevel) >= 0)
                throw new InvalidOperationException(UNAUTHORIZED);

            await _employeeService.Remove(id);
            return employee;
        }
    }
}
