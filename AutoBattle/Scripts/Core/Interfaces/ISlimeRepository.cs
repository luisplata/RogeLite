using System.Collections.Generic;
using AutoBattle.Scripts.Core.Entities;

namespace AutoBattle.Scripts.Core.Interfaces
{
    public interface ISlimeRepository
    {
        Slime GetById(string id);
        IEnumerable<Slime> GetAll();
        void Save(Slime slime);
    }
}


namespace Game.Application.UseCases
{
    public class GetAllSlimesUseCase
    {
        private readonly ISlimeRepository _slimeRepository;

        public GetAllSlimesUseCase(ISlimeRepository slimeRepository)
        {
            _slimeRepository = slimeRepository;
        }

        public IEnumerable<Slime> Execute()
        {
            return _slimeRepository.GetAll();
        }
    }
}
