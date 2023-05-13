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
    public interface IStoreRoleService
    {
        StoreRole Add(StoreRole storeRole);

        void Update(StoreRole storeRole);

        StoreRole Delete(int id);

        IEnumerable<StoreRole> GetAll();

        IEnumerable<StoreRole> GetAll(string keyword);

        StoreRole GetById(int id);

        void Save();
    }

    public class StoreRoleService : IStoreRoleService
    {
        private IStoreRoleRepository _storeRoleRepository;
        private IUnitOfWork _unitOfWork;

        public StoreRoleService(IStoreRoleRepository storeRoleRepository, IUnitOfWork unitOfWork)
        {
            this._storeRoleRepository = storeRoleRepository;
            this._unitOfWork = unitOfWork;
        }

        public StoreRole Add(StoreRole storeRole)
        {
            return _storeRoleRepository.Add(storeRole);
        }

        public StoreRole Delete(int id)
        {
            return _storeRoleRepository.Delete(id);
        }

        public IEnumerable<StoreRole> GetAll()
        {
            return _storeRoleRepository.GetAll();
        }

        public IEnumerable<StoreRole> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _storeRoleRepository.GetMulti(x => x.Name.Contains(keyword) || x.Description.Contains(keyword));
            else
                return _storeRoleRepository.GetAll();
        }

        public StoreRole GetById(int id)
        {
            return _storeRoleRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(StoreRole storeRole)
        {
            _storeRoleRepository.Update(storeRole);
        }
    }
}
