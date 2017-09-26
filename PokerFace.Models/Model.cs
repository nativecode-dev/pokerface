namespace PokerFace.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public abstract class Model : IValidatableObject
    {
        IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (Validator.TryValidateObject(this, validationContext, results))
            {
                return Enumerable.Empty<ValidationResult>();
            }

            return results;
        }
    }
}