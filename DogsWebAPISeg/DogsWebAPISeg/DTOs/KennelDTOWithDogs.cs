namespace DogsWebAPISeg.DTOs
{
    public class KennelDTOWithDogs : KennelDTO
    {
        public List<GetDogDTO> Dogs { get; set; }
    }
}
