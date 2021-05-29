using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace PISLab.Models
{
    public class Product
    {
        public Guid Id { get; set; } = Guid.Empty;
        public string Name { get; set; }
        public string Company { get; set; }
        public double Price_kg { get; set; }
        public double Weight { get; set; }
    public BaseModelValidationResult Validate()
       {
           var validationResult = new BaseModelValidationResult();

           if (string.IsNullOrWhiteSpace(Name)) validationResult.Append($"Name cannot be empty");
           if (string.IsNullOrWhiteSpace(Company)) validationResult.Append($"Surname cannot be empty");
           if (Price_kg<0) validationResult.Append($"Price_kg cannot be negative");
           if (Weight<0) validationResult.Append($"Weight cannot be negative");

           if (!string.IsNullOrEmpty(Name) && !char.IsUpper(Name.FirstOrDefault())) validationResult.Append($"Name {Name} should start from capital letter");
           if (!string.IsNullOrEmpty(Company) && !char.IsUpper(Company.FirstOrDefault())) validationResult.Append($"Company {Company} should start from capital letter");

           return validationResult;
       }

       public override string ToString()
       {
           return $"Product {Name} from {Company} at a cost of {Price_kg} rub per kg and weighing {Weight} kg";
       }
    }
}