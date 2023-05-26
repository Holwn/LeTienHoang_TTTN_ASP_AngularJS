using Pharma.Data.Infrastructure;
using Pharma.Data.Repositories;
using Pharma.Model.Models;
using System.Collections.Generic;

namespace Pharma.Service
{
    public interface IReceiptNoteService
    {
        ReceiptNote Add(ReceiptNote receiptNote);

        void Update(ReceiptNote receiptNote);

        ReceiptNote Delete(int id);

        IEnumerable<ReceiptNote> GetAll();

        IEnumerable<ReceiptNote> GetAll(string keyword);

        ReceiptNote GetById(int id);

        void Save();
    }

    public class ReceiptNoteService : IReceiptNoteService
    {
        private IReceiptNoteRepository _receiptNoteRepository;
        private IUnitOfWork _unitOfWork;

        public ReceiptNoteService(IReceiptNoteRepository receiptNoteRepository, IUnitOfWork unitOfWork)
        {
            this._receiptNoteRepository = receiptNoteRepository;
            this._unitOfWork = unitOfWork;
        }

        public ReceiptNote Add(ReceiptNote receiptNote)
        {
            return _receiptNoteRepository.Add(receiptNote);
        }

        public ReceiptNote Delete(int id)
        {
            return _receiptNoteRepository.Delete(id);
        }

        public IEnumerable<ReceiptNote> GetAll()
        {
            return _receiptNoteRepository.GetAll();
        }

        public IEnumerable<ReceiptNote> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _receiptNoteRepository.GetMulti(x => x.Code.Contains(keyword));
            else
                return _receiptNoteRepository.GetAll();
        }

        public ReceiptNote GetById(int id)
        {
            return _receiptNoteRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(ReceiptNote receiptNote)
        {
            _receiptNoteRepository.Update(receiptNote);
        }
    }
}