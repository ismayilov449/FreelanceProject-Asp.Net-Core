using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreelanceProject.Services
{
    static public class FakeRepo
    {

        static private List<string> Categories = new List<string>()
        {
             "Credit","Marketing","IT","Reseption"
        };

        static private List<string> Cities = new List<string>()
        {
            "Baku","Sumgayit","Ganja"
        };

        static private List<string> Education = new List<string>()
        {
            "Ali","Natamam Ali","Orta"
        };

        static private List<string> Practise = new List<string>()
        {
            "Baku","Sumgayit","Ganja"
        };


        static public List<string> GetCategories()
        {
            return Categories;
        }

        static public List<string> GetCities()
        {
            return Cities;
        }

        static public List<string> GetEducation()
        {
            return Education;
        }

        static public List<string> GetPractise()
        {
            return Practise;
        }

    }
}
