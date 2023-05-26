using Pharma.Data.Infrastructure;
using Pharma.Data.Repositories;
using Pharma.Model.Models;
using System.Collections.Generic;

namespace Pharma.Service
{
    public interface IReceiptNoteItemService
    {
        ReceiptNoteItem Add(ReceiptNoteItem receiptNoteItem);

        void Update(ReceiptNoteItem receiptNoteItem);

        ReceiptNoteItem Delete(int id);

        IEnumerable<ReceiptNoteItem> GetAll();

        ReceiptNoteItem GetById(int id);

        void Save();
    }

    public class ReceiptNoteItemService : IReceiptNoteItemService
    {
        private IReceiptNoteItemRepository _receiptNoteItemRepository;
        private IUnitOfWork _unitOfWork;

        public ReceiptNoteItemService(IReceiptNoteItemRepository receiptNoteItemRepository, IUnitOfWork unitOfWork)
        {
            this._receiptNoteItemRepository = receiptNoteItemRepository;
            this._unitOfWork = unitOfWork;
        }

        public ReceiptNoteItem Add(ReceiptNoteItem receiptNoteItem)
        {
            return _receiptNoteItemRepository.Add(receiptNoteItem);
        }

        public ReceiptNoteItem Delete(int id)
        {
            return _receiptNoteItemRepository.Delete(id);
        }

        public IEnumerable<ReceiptNoteItem> GetAll()
        {
            return _receiptNoteItemRepository.GetAll();
        }

        public ReceiptNoteItem GetById(int id)
        {
            return _receiptNoteItemRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(ReceiptNoteItem receiptNoteItem)
        {
            _receiptNoteItemRepository.Update(receiptNoteItem);
        }
    }
}