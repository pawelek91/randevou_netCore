using System;

namespace BusinessServices.InitialValues.Startup
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Test only");
            using (var dbc = new EFRandevouDAL.RandevouDbContext())
            {
                UsersInitialValues.AddUsers(dbc);
                DictionaryDetailsInitialValues.AddDictionaryValues(dbc);
            }
        }
    }
}
