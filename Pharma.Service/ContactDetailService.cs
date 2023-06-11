using Pharma.Data.Infrastructure;
using Pharma.Data.Repositories;
using Pharma.Model.Models;
using System.Collections.Generic;

namespace Pharma.Service
{
    public interface IContactDetailService
    {
        ContactDetail Add(ContactDetail contactDetail);

        void Update(ContactDetail contactDetail);

        ContactDetail Delete(int id);

        IEnumerable<ContactDetail> GetAll();

        IEnumerable<ContactDetail> GetAll(string keyword);

        ContactDetail GetById(int id);

        ContactDetail GetDefaultContact();

        void Save();
    }

    public class ContactDetailService : IContactDetailService
    {
        private IContactDetailRepository _contactDetailRepository;
        private IUnitOfWork _unitOfWork;

        public ContactDetailService(IContactDetailRepository contactDetailRepository, IUnitOfWork unitOfWork)
        {
            this._contactDetailRepository = contactDetailRepository;
            this._unitOfWork = unitOfWork;
        }

        public ContactDetail Add(ContactDetail contactDetail)
        {
            return _contactDetailRepository.Add(contactDetail);
        }

        public ContactDetail Delete(int id)
        {
            return _contactDetailRepository.Delete(id);
        }

        public IEnumerable<ContactDetail> GetAll()
        {
            return _contactDetailRepository.GetAll();
        }

        public IEnumerable<ContactDetail> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _contactDetailRepository.GetMulti(x => x.Name.Contains(keyword));
            else
                return _contactDetailRepository.GetAll();
        }

        public ContactDetail GetById(int id)
        {
            return _contactDetailRepository.GetSingleById(id);
        }

        public ContactDetail GetDefaultContact()
        {
            return _contactDetailRepository.GetSingleByCondition(x => x.Status);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(ContactDetail contactDetail)
        {
            _contactDetailRepository.Update(contactDetail);
        }
    }
}