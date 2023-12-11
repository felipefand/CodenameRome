namespace CodenameRome.Dtos
{
    public class ProductDto
    {
        public string Name { get; set; } = String.Empty;
        public int Price { get; set; } = 0;
        public string Description { get; set; } = String.Empty;
        public string Category { get; set; } = String.Empty;

        public void ValidateProduct()
        {
            this.ValidateName();
            this.ValidatePrice();
            this.ValidateDescription();
            this.ValidateCategory();
        }

        public void ValidateName()
        {
            if (this.Name.Length > 50)
                throw new Exception("Name is too long.");
        }

        public void ValidatePrice()
        {
            if (this.Price > 99999)
                throw new Exception("Price is too high.");
        }

        public void ValidateDescription()
        {
            if (this.Description.Length >= 500)
                throw new Exception("Description is too long.");
        }

        public void ValidateCategory()
        {
            if (this.Category.Length >= 50)
                throw new Exception("Category is too long.");
        }
    }
}
