namespace Lesson04lab.Models
{
    public class DataLocal
    {
        public static List<People> _peoples = new List<People>()
        {
            new People(){Id=0,Name="Devmaster",Email="devmaster.edu.vn@gmail.com",Phone="0978611889",Address="25 Vũ Ngọc Phan", Avatar = "/images/avatar/01.jpg",Birthday=Convert.ToDateTime("2012/09/22"),Bio="Viện Công Nghệ Devmaster",Gender=0},
            new People(){Id=2,Name="Devmaster",Email="devmaster.edu.vn@gmail.com",Phone="0978611889",Address="25 Vũ Ngọc Phan", Avatar = "/images/avatar/02.png",Birthday=Convert.ToDateTime("2012/09/22"),Bio="Viện Công Nghệ Devmaster",Gender=1},
            new People(){Id=3,Name="Devmaster",Email="devmaster.edu.vn@gmail.com",Phone="0978611889",Address="25 Vũ Ngọc Phan", Avatar = "/images/avatar/03.jpg",Birthday=Convert.ToDateTime("2012/09/22"),Bio="Viện Công Nghệ Devmaster",Gender=2},
            new People(){Id=4,Name="Devmaster",Email="devmaster.edu.vn@gmail.com",Phone="0978611889",Address="25 Vũ Ngọc Phan", Avatar = "/images/avatar/04.jpg",Birthday=Convert.ToDateTime("2012/09/22"),Bio="Viện Công Nghệ Devmaster",Gender=2},
            new People(){Id=5,Name="Devmaster",Email="devmaster.edu.vn@gmail.com",Phone="0978611889",Address="25 Vũ Ngọc Phan", Avatar = "/images/avatar/05.jpg",Birthday=Convert.ToDateTime("2012/09/22"),Bio="Viện Công Nghệ Devmaster",Gender=2},
        };
        /// <summary>
        /// GetPeoples: lấy danh sahcs dữ liệu peoples
        /// </summary>
        ///<returns></returns>
        public static List<People> GetPeoples()
        {
            return _peoples;
        }
        /// <summary>
        /// GetPeoplebyId: lấy đối tượng peoples theo id
        /// </summary>
        /// <param name="Id"></param>
        ///<returns></returns>
        public static People? GetPeopleById(int Id)
        {
            var people = _peoples.FirstOrDefault(x => x.Id == Id);
            return people;
        }
    }
}
