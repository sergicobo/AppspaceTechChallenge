namespace AppspaceTechChallenge.Domain.Entities
{
    public class GenreData
    {
        private readonly int _id;
        private readonly string _name;

        public GenreData(int id, string name)
        {
            _id = id;
            _name = name;
        }

        public int GetId()
        {
            return _id;
        }

        public string GetName()
        {
            return _name;
        }
    }
}
