using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Common.TestData
{
    public class Persion
    {
        public Name Name => new Name();

        public PhoneNumber PhoneNumber => new PhoneNumber();

        public Company Company => new Company();

        public Address Address => new Address();

        public Education Education => new Education();

        public Internet Internet => new Internet();

        public Lorem Lorem => new Lorem();

        public GeoLocation GeoLocation => new GeoLocation();

        public string Card => CreditCard.CreditCardNumber("VISA");

        public static List<Persion> Create(int count = 100)
        {
            List<Persion> result = new List<Persion>();

            for (int i = 0; i < count; i++)
            {
                result.Add(new Persion());
            }

            return result;
        }


        public override string ToString()
        {
            return this.Name.FullName;
        }
    }
}
