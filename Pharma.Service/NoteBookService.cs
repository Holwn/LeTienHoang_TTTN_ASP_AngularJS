using Pharma.Data.Infrastructure;
using Pharma.Data.Repositories;
using Pharma.Model.Models;
using System.Collections.Generic;
namespace Pharma.Service
{
    public interface INoteBookService
    {
        NoteBook Add(NoteBook NoteBook);

        void Update(NoteBook NoteBook);

        NoteBook Delete(int id);

        IEnumerable<NoteBook> GetAll();

        IEnumerable<NoteBook> GetAll(string keyword);

        NoteBook GetById(int id);

        void Save();
    }

    public class NoteBookService : INoteBookService
    {
        private INoteBookRepository _noteBookRepository;
        private IUnitOfWork _unitOfWork;

        public NoteBookService(INoteBookRepository NoteBookRepository, IUnitOfWork unitOfWork)
        {
            this._noteBookRepository = NoteBookRepository;
            this._unitOfWork = unitOfWork;
        }

        public NoteBook Add(NoteBook NoteBook)
        {
            return _noteBookRepository.Add(NoteBook);
        }

        public NoteBook Delete(int id)
        {
            return _noteBookRepository.Delete(id);
        }

        public IEnumerable<NoteBook> GetAll()
        {
            return _noteBookRepository.GetAll();
        }

        public IEnumerable<NoteBook> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _noteBookRepository.GetMulti(x => x.Name.Contains(keyword));
            else
                return _noteBookRepository.GetAll();
        }

        public NoteBook GetById(int id)
        {
            return _noteBookRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(NoteBook NoteBook)
        {
            _noteBookRepository.Update(NoteBook);
        }
    }
}