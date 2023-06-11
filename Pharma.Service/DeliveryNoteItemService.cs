using Pharma.Data.Infrastructure;
using Pharma.Data.Repositories;
using Pharma.Model.Models;
using Pharma.Model.ServiceModel;
using Pharma.Service.ServiceModel;
using System.Collections.Generic;

namespace Pharma.Service
{
    public interface IDeliveryNoteItemService
    {
        DeliveryNoteItem Add(DeliveryNoteItem deliveryNoteItem);

        void Update(DeliveryNoteItem deliveryNoteItem);

        DeliveryNoteItem Delete(int id);

        IEnumerable<DeliveryNoteItem> GetAll();

        IEnumerable<DNIGroupByExpiredDateModel> GroupByExpiredDateModels();
        IEnumerable<DeliveryNoteItem> GetAllByNoteId(int noteId);
        DeliveryNoteItem GetById(int id);
        DeliveryNoteItem GetByNoteId(int noteId, int productId);

        void Save();
    }

    public class DeliveryNoteItemService : IDeliveryNoteItemService
    {
        private IDeliveryNoteItemRepository _deliveryNoteItemRepository;
        private IUnitOfWork _unitOfWork;

        public DeliveryNoteItemService(IDeliveryNoteItemRepository deliveryNoteItemRepository, IUnitOfWork unitOfWork)
        {
            this._deliveryNoteItemRepository = deliveryNoteItemRepository;
            this._unitOfWork = unitOfWork;
        }

        public DeliveryNoteItem Add(DeliveryNoteItem deliveryNoteItem)
        {
            return _deliveryNoteItemRepository.Add(deliveryNoteItem);
        }

        public DeliveryNoteItem Delete(int id)
        {
            return _deliveryNoteItemRepository.Delete(id);
        }

        public IEnumerable<DeliveryNoteItem> GetAll()
        {
            return _deliveryNoteItemRepository.GetAll();
        }

        public IEnumerable<DeliveryNoteItem> GetAllByNoteId(int noteId)
        {
            return _deliveryNoteItemRepository.GetAllByNoteId(noteId);
        }

        public DeliveryNoteItem GetById(int id)
        {
            return _deliveryNoteItemRepository.GetSingleById(id);
        }

        public DeliveryNoteItem GetByNoteId(int noteId, int productId)
        {
            return _deliveryNoteItemRepository.GetSingleByCondition(x => x.NoteID == noteId && x.ProductID == productId);
        }

        public IEnumerable<DNIGroupByExpiredDateModel> GroupByExpiredDateModels()
        {
            return _deliveryNoteItemRepository.GroupByExpiredDateModels();
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(DeliveryNoteItem deliveryNoteItem)
        {
            _deliveryNoteItemRepository.Update(deliveryNoteItem);
        }
    }
}