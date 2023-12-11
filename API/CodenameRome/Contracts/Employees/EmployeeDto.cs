namespace CodenameRome.Contracts.Employees
{
    public class EmployeeDto
    {
        private readonly string[] ACCEPTABLEACCESSLEVEL = {"10", "20", "30"};
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public decimal? Salary { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string AccessLevel { get; set; } = "30";

        public void ValidateEmployeeCreation()
        {
            if (this.Name == null)
                throw new Exception("Name must be inserted.");

            if (this.AccessLevel == null)
                throw new Exception("Access level can't be null.");

            if (this.Username != null && this.Password == null)
                throw new Exception("Password can't be null.");

            if (this.Password != null && this.Username == null)
                throw new Exception("Username can't be null.");

            this.ValidateEmployee();
        }

        public void ValidateEmployee()
        {
            if (this.Name != null)
                this.ValidateName();

            if (this.AccessLevel != null)
                this.ValidateAccessLevel();

            if (this.Address != null)
                this.ValidateAddress();

            if (this.PhoneNumber != null)
                this.ValidatePhoneNumber();

            if (this.Salary != null)
                this.ValidateSalary();

            if (this.Username != null)
                this.ValidateUsername();

            if (this.Password != null)
                this.ValidatePassword();
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

        public void ValidateUsername()
        {
            if (this.Username!.Length >= 20)
                throw new Exception("Username is too long.");
        }

        public void ValidateAccessLevel()
        {
            if (!ACCEPTABLEACCESSLEVEL.Contains(this.AccessLevel))
                throw new Exception("Invalid access level.");
        }

        public void ValidatePassword()
        {
            if (this.Password!.Length < 6 || this.Password.Length > 16)
                throw new Exception("Password is too short or too long.");
        }
    }
}
