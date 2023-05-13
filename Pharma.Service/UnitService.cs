using Pharma.Data.Infrastructure;
using Pharma.Data.Repositories;
using Pharma.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharma.Service
{
    public interface IUnitService
    {
        Unit Add(Unit unit);

        void Update(Unit unit);

        Unit Delete(int id);

        IEnumerable<Unit> GetAll();

        IEnumerable<Unit> GetAll(string keyword);

        Unit GetById(int id);

        void Save();
    }

    public class UnitService : IUnitService
    {
        private IUnitRepository _unitRepository;
        private IUnitOfWork _unitOfWork;

        public UnitService(IUnitRepository unitRepository, IUnitOfWork unitOfWork)
        {
            this._unitRepository = unitRepository;
            this._unitOfWork = unitOfWork;
        }

        public Unit Add(Unit unit)
        {
            return _unitRepository.Add(unit);
        }

        public Unit Delete(int id)
        {
            return _unitRepository.Delete(id);
        }

        public IEnumerable<Unit> GetAll()
        {
            return _unitRepository.GetAll();
        }

        public IEnumerable<Unit> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _unitRepository.GetMulti(x => x.Name.Contains(keyword));
            else
                return _unitRepository.GetAll();
        }

        public Unit GetById(int id)
        {
            return _unitRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Unit unit)
        {
            _unitRepository.Update(unit);
        }
    }
}
