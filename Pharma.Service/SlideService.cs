using Pharma.Data.Infrastructure;
using Pharma.Data.Repositories;
using Pharma.Model.Models;
using System.Collections.Generic;

namespace Pharma.Service
{
    public interface ISlideService
    {
        Slide Add(Slide slide);

        void Update(Slide slide);

        Slide Delete(int id);

        IEnumerable<Slide> GetAll();

        IEnumerable<Slide> GetAll(string keyword);

        Slide GetById(int id);

        void Save();
    }

    public class SlideService : ISlideService
    {
        private ISlideRepository _slideRepository;
        private IUnitOfWork _unitOfWork;

        public SlideService(ISlideRepository slideRepository, IUnitOfWork unitOfWork)
        {
            this._slideRepository = slideRepository;
            this._unitOfWork = unitOfWork;
        }

        public Slide Add(Slide slide)
        {
            return _slideRepository.Add(slide);
        }

        public Slide Delete(int id)
        {
            return _slideRepository.Delete(id);
        }

        public IEnumerable<Slide> GetAll()
        {
            return _slideRepository.GetAll();
        }

        public IEnumerable<Slide> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _slideRepository.GetMulti(x => x.Name.Contains(keyword));
            else
                return _slideRepository.GetAll();
        }

        public Slide GetById(int id)
        {
            return _slideRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Slide slide)
        {
            _slideRepository.Update(slide);
        }
    }
}