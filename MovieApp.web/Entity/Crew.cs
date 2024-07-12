namespace MovieApp.web.Entity
{
    public class Crew
    {
        public int CrewId { get; set; }
        public Movie Movie { get; set; }
        public int MovieId { get; set; }
        public Person Person { get; set; }
        public int PersonId { get; set; }
        public string Job { get; set; }

    }
}
