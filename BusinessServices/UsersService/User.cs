using System;
namespace BusinessServices.UsersService
{
    public class User
    {
		public static int LastId { get; private set; } = 0;

		public User(string name, char gender, DateTime birthDate)
        {
			this.Id = LastId;
			LastId++;
			this.Name = name;
			this.Gender = gender;
			this.BirthDate = birthDate;

        }
		public int Id { get; }
		public string Name { get; set; }
		public char Gender { get; set; }
		public DateTime BirthDate { get; set; }

		public override string ToString()
		{
			return string.Format("[{0}] - {1}", Id,Name);
		}
    }
}
