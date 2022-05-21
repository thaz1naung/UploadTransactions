using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace MyCoreMVCDemo.Entities
{
    public class DataRequestDTO: IValidatableObject
    {
        [RegularExpression(@"(((19|20)\d\d))-(0[1-9]|1[0-2])-((0|1)[0-9]|2[0-9]|3[0-1])$", ErrorMessage = "Date format must be yyyy-MM-dd.")]
        public string StrStartDate { get; set; }

        [RegularExpression(@"(((19|20)\d\d))-(0[1-9]|1[0-2])-((0|1)[0-9]|2[0-9]|3[0-1])$", ErrorMessage = "Date format must be yyyy-MM-dd.")]
        public string StrEndDate { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!IsValidDate(StrStartDate))
            {
                yield return new ValidationResult("Invalid Start Date Format");
            }

            if (!IsValidDate(StrEndDate))
            {
                yield return new ValidationResult("Invalid End Date Format");
            }

            if (!string.IsNullOrEmpty(StrStartDate) && !string.IsNullOrEmpty(StrEndDate))
            {
                if (IsValidDate(StrStartDate) && IsValidDate(StrEndDate))
                {
                    if (!IsStartEarlierThanEnd())
                    {
                        yield return new ValidationResult("Start Date must be earlier than End Date");
                    }
                }
            }
        }
        private bool IsStartEarlierThanEnd()
        {
            DateTime StartDate = DateTime.ParseExact(StrStartDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            DateTime EndDate = DateTime.ParseExact(StrEndDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            if (StartDate > EndDate)
            {
                return false;
            }
            return true;
        }


        private bool IsValidDate(string date)
        {

            if (!string.IsNullOrEmpty(date))
            {
                try
                {
                    DateTime reqDate = DateTime.ParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                }
                catch
                {
                    return false;

                }

            }
            return true;
        }
    }
}
