using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class Notifies
    {

        public Notifies() 
        {
            Notifications = new List<Notifies>();
        }

        [NotMapped]
        public string PropertyName { get; set; }

        [NotMapped]
        public string Message { get; set; }

        [NotMapped]
        public List<Notifies> Notifications { get; set; }
         [NotMapped]
        public string SuccessMessage { get; set; }


        public bool ValidateStringProperty(string value, string propertyName)
        {

            if (string.IsNullOrWhiteSpace(value) || string.IsNullOrWhiteSpace(propertyName))
            {
                Notifications.Add(new Notifies
                {
                    Message = $"{propertyName} is a required field."
                });

                return false;
            }

            return true;
        }

        public bool ValidateCompareDateProperty(DateTime manufactureDate, DateTime expiryDate, string propertyName)
        {
            int result = DateTime.Compare(manufactureDate, expiryDate);

            if (result > 0)
            {
                Notifications.Add(new Notifies
                {
                    Message = "The manufacture date cannot be later than the expiry date."
                });
                return false;
            }
            return true;
        }
    }
}
