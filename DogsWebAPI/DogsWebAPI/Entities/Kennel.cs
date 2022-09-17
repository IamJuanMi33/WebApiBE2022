namespace DogsWebAPI.Entities
{
    public class Kennel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Breed { get; set; }

        public int DogId { get; set; }

        public Dog Dog { get; set; }
    }
}
