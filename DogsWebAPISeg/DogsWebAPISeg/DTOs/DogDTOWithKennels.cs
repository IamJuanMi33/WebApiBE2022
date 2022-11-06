namespace DogsWebAPISeg.DTOs
{
    public class DogDTOWithKennels : GetDogDTO
    {
        public List<KennelDTO> Kennels { get; set; }
    }
}
