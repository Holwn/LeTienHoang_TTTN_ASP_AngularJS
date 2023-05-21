using Pharma.Data.Infrastructure;
using Pharma.Data.Repositories;
using Pharma.Model.Models;
using System.Collections.Generic;

namespace Pharma.Service
{
    public interface ISubjectService
    {
        Subject Add(Subject subject);

        void Update(Subject subject);

        Subject Delete(int id);

        IEnumerable<Subject> GetAll();

        IEnumerable<Subject> GetAll(string keyword);

        Subject GetById(int id);

        void Save();
    }

    public class SubjectService : ISubjectService
    {
        private ISubjectRepository _subjectRepository;
        private IUnitOfWork _unitOfWork;

        public SubjectService(ISubjectRepository subjectRepository, IUnitOfWork unitOfWork)
        {
            this._subjectRepository = subjectRepository;
            this._unitOfWork = unitOfWork;
        }

        public Subject Add(Subject subject)
        {
            return _subjectRepository.Add(subject);
        }

        public Subject Delete(int id)
        {
            return _subjectRepository.Delete(id);
        }

        public IEnumerable<Subject> GetAll()
        {
            return _subjectRepository.GetAll();
        }

        public IEnumerable<Subject> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _subjectRepository.GetMulti(x => x.Name.Contains(keyword));
            else
                return _subjectRepository.GetAll();
        }

        public Subject GetById(int id)
        {
            return _subjectRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Subject subject)
        {
            _subjectRepository.Update(subject);
        }
    }
}