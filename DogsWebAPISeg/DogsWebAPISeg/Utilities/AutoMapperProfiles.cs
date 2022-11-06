using AutoMapper;
using DogsWebAPISeg.DTOs;
using DogsWebAPISeg.Entities;

namespace DogsWebAPISeg.Utilities
{
    public class AutoMapperProfiles : Profile
    { 
        public AutoMapperProfiles()
        {
            CreateMap<DogDTO, Dog>();
            CreateMap<Dog, GetDogDTO>();
            CreateMap<Dog, DogDTOWithKennels>()
                .ForMember(dogDTO => dogDTO.Kennels, options => options.MapFrom(MapDogDTOKennels));
            CreateMap<KennelCreationDTO, Kennel>()
                .ForMember(kennel => kennel.DogKennel, options => options.MapFrom(MapDogKennel));
            CreateMap<Kennel, KennelDTO>();
            CreateMap<Kennel, KennelDTOWithDogs>()
                .ForMember(kennelDTO => kennelDTO.Dogs, options => options.MapFrom(MapKennelDTODogs));
        }

        private List<KennelDTO> MapDogDTOKennels(Dog dog, GetDogDTO getDogDTO)
        {
            var result = new List<KennelDTO>();

            if(dog.DogKennel == null) { return result; }

            foreach(var dogKennel in dog.DogKennel)
            {
                result.Add(new KennelDTO()
                {
                    Id = dogKennel.KennelId,
                    Name = dogKennel.Kennel.Name
                });
            }

            return result;
        }

        private List<GetDogDTO> MapKennelDTODogs(Kennel kennel, KennelDTO kennelDTO)
        {
            var result = new List<GetDogDTO>();

            if (kennel.DogKennel == null)
            { 
                return result; 
            }

            foreach (var dogKennel in kennel.DogKennel)
            {
                result.Add(new GetDogDTO()
                {
                    Id = dogKennel.DogId,
                    Name = dogKennel.Dog.Name
                });
            }

            return result;
        }

        private List<DogKennel> MapDogKennel(KennelCreationDTO kennelCreationDTO, Kennel kennel)
        {
            var result = new List<DogKennel>();

            if (kennelCreationDTO.DogsIds == null)
            {
                return result;
            }

            foreach (var dogId in kennelCreationDTO.DogsIds)
            {
                result.Add(new DogKennel()
                {
                    DogId = dogId
                });
            }

            return result;
        }

    }
}
