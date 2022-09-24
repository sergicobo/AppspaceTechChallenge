namespace AppspaceTechChallenge.Domain.Entities
{
    public class GenreData
    {
        private int Id { get; }
        private string Name { get; }

        public GenreData(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int GetId()
        {
            return Id;
        }

        public string GetName()
        {
            return Name;
        }
    }
}
