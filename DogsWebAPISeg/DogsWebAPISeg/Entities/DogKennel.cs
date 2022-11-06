namespace DogsWebAPISeg.Entities
{
    public class DogKennel
    {
        public int DogId { get; set; }
        public int KennelId { get; set; }
        public int Order { get; set; }
        public Dog Dog { get; set; }    
        public Kennel Kennel { get; set; }  
    }
}
