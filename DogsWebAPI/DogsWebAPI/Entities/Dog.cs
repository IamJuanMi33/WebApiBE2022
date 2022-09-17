namespace DogsWebAPI.Entities
{
    public class Dog
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Kennel> kennels { get; set; }
    }
}
