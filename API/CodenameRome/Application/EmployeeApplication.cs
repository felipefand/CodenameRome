using CodenameRome.Application.Interfaces;
using CodenameRome.Contracts.Employees;
using CodenameRome.Models;
using CodenameRome.Services.Interfaces;

namespace CodenameRome.Application
{
    public class EmployeeApplication : IEmployeeApplication
    {
        private readonly IEmployeeService _employeeService;
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

        public EmployeeRole GetEmployeeRole(string creatorRoleString, EmployeeRole? desiredRole)
        {
            var creatorRole = StringToEmployeeRole(creatorRoleString);

            if (creatorRole == EmployeeRole.Owner)
                return desiredRole ?? EmployeeRole.Employee;

            return EmployeeRole.Employee;
        }

        public EmployeeRole StringToEmployeeRole(string role)
        {
            if (Enum.IsDefined(typeof(EmployeeRole), role))
                return (EmployeeRole)Enum.Parse(typeof(EmployeeRole), role);

            return EmployeeRole.Employee;
        }

        public void ValidateEmployeeChangeByStringAndRole(string editorRoleString, EmployeeRole editeeRole)
        {
            var editorRole = StringToEmployeeRole(editorRoleString);
            if (editorRole >= editeeRole)
                throw new InvalidOperationException(UNAUTHORIZED);
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

        public async Task<Employee> CreateEmployee(EmployeeDto employee, string clientId, string creatorRole)
        {
            employee.ValidateEmployeeCreation();

            if (employee.Email != null)
            {
                var isUsernameInUse = await _employeeService.GetByEmail(employee.Email!);
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
                Email = employee.Email,
                Password = employee.Password,
                Role = GetEmployeeRole(creatorRole, employee.Role)
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

            if (employee.Email == null)
                throw new Exception(MUSTHAVEUSERNAME);

            if (employee.Id == employeeId)
            {
                await ChangePass(id, newPassword);
                return;
            }

            ValidateEmployeeChangeByStringAndRole(employeeRole, employee.Role);

            await ChangePass(id, newPassword);
            return;
        }

        public async Task ChangeOwnPassword(string employeeId, ChangePassRequest changePasswordRequest)
        {
            var employee = await _employeeService.GetById(employeeId);

            if (employee == null)
                throw new ArgumentNullException(NOTFOUND);

            if (!employee.VerifyPassword(changePasswordRequest.OldPassword))
                throw new InvalidOperationException(WRONGPASSWORD);

            await ChangePass(employeeId, changePasswordRequest.NewPassword);
            return;
        }

        public async Task<Employee> UpdateEmployee(string clientId, string employeeId, string employeeRole, string id, EmployeeDto employeeDto)
        {
            var employee = await _employeeService.GetById(id);

            if (employee == null || employee.ClientId != clientId)
                throw new ArgumentNullException(NOTFOUND);

            if (employee.Email == null && employee.Password == null)
            {
                if (employeeDto.Email != null && employeeDto.Password == null)
                    throw new Exception(MUSTHAVEPASSWORD);

                if (employeeDto.Password != null && employeeDto.Email == null)
                    throw new Exception(MUSTHAVEUSERNAME);
            }

            if (employee.Id == employeeId)
            {
                employee = await Update(id, employeeDto);
                return employee!;
            }

            ValidateEmployeeChangeByStringAndRole(employeeRole, employee.Role);

            employee = await Update(id, employeeDto);
            return employee!;
        }

        public async Task<Employee> DeleteEmployee(string clientId, string employeeRole, string id)
        {
            var employee = await _employeeService.GetById(id);

            if (employee == null || employee.ClientId != clientId)
                throw new ArgumentNullException(NOTFOUND);

            ValidateEmployeeChangeByStringAndRole(employeeRole, employee.Role);

            await _employeeService.Remove(id);
            return employee;
        }
    }
}
