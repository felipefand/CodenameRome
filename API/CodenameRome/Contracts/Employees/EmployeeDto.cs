using CodenameRome.Models;

namespace CodenameRome.Contracts.Employees
{
    public class EmployeeDto
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public decimal? Salary { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public EmployeeRole? Role { get; set; }

        public void ValidateEmployeeCreation()
        {
            if (this.Name == null)
                throw new Exception("Name must be inserted.");

            if (this.Role == null)
                throw new Exception("Role must be inserted.");

            if (this.Email != null && this.Password == null)
                throw new Exception("Password can't be null.");

            if (this.Password != null && this.Email == null)
                throw new Exception("Email can't be null.");

            this.ValidateEmployee();
        }

        public void ValidateEmployee()
        {
            if (this.Name != null)
                this.ValidateName();

            if (this.Address != null)
                this.ValidateAddress();

            if (this.PhoneNumber != null)
                this.ValidatePhoneNumber();

            if (this.Salary != null)
                this.ValidateSalary();

            if (this.Email != null)
                this.ValidateEmail();

            if (this.Password != null)
                this.ValidatePassword();

            if (this.Role != null)
                this.ValidateRole();
        }

        public void ValidateName()
        {
            if (this.Name!.Length >= 20)
                throw new Exception("Name is too long.");
        }

        public void ValidateAddress()
        {
            if (this.Address!.Length >= 20)
                throw new Exception("Address is too long.");
        }

        public void ValidatePhoneNumber()
        {
            if (this.PhoneNumber!.Length > 11)
                throw new Exception("Phone number is too long.");
        }

        public void ValidateSalary()
        {
            if (this.Salary >= 99999999)
                throw new Exception("Salary is too big");
        }

        public void ValidateEmail()
        {
            if (this.Email!.Length >= 20)
                throw new Exception("Email is too long.");
        }

        public void ValidateRole()
        {
            if (Enum.IsDefined(typeof(EmployeeRole), this.Role!))
                throw new Exception("Invalid role.");
        }

        public void ValidatePassword()
        {
            if (this.Password!.Length < 6 || this.Password.Length > 16)
                throw new Exception("Password is too short or too long.");
        }
    }
}
