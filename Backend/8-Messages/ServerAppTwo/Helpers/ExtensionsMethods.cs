using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerAppTwo.Helpers
{
    public static class ExtensionsMethods
    {
        public static int CalculateAge(this DateTime dateOfBirth){
            int age = 0;
            age= DateTime.Now.Year-dateOfBirth.Year;
            if(DateTime.Now.DayOfYear<dateOfBirth.DayOfYear)
                age-=1;
                return age;
            
        }
    }
}