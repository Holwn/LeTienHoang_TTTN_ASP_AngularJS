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
    public interface IStoreService
    {
        Store Add(Store Store);

        void Update(Store Store);

        Store Delete(int id);

        IEnumerable<Store> GetAll();

        IEnumerable<Store> GetAll(string keyword);

        Store GetById(int id);

        void Save();
    }

    public class StoreService : IStoreService
    {
        private IStoreRepository _storeRepository;
        private IUnitOfWork _unitOfWork;

        public StoreService(IStoreRepository storeRepository, IUnitOfWork unitOfWork)
        {
            this._storeRepository = storeRepository;
            this._unitOfWork = unitOfWork;
        }

        public Store Add(Store Store)
        {
            return _storeRepository.Add(Store);
        }

        public Store Delete(int id)
        {
            return _storeRepository.Delete(id);
        }

        public IEnumerable<Store> GetAll()
        {
            return _storeRepository.GetAll();
        }

        public IEnumerable<Store> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _storeRepository.GetMulti(x => x.Name.Contains(keyword) || x.Description.Contains(keyword));
            else
                return _storeRepository.GetAll();
        }

        public Store GetById(int id)
        {
            return _storeRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Store Store)
        {
            _storeRepository.Update(Store);
        }
    }
}
