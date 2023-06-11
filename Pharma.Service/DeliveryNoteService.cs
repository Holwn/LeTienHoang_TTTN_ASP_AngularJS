using Pharma.Data.Infrastructure;
using Pharma.Data.Repositories;
using Pharma.Model.Models;
using System.Collections.Generic;

namespace Pharma.Service
{
    public interface IDeliveryNoteService
    {
        DeliveryNote Add(DeliveryNote deliveryNote);

        void Update(DeliveryNote deliveryNote);

        DeliveryNote Delete(int id);

        IEnumerable<DeliveryNote> GetAll();

        IEnumerable<DeliveryNote> GetAll(string keyword);

        DeliveryNote GetById(int id);

        void Save();
    }

    public class DeliveryNoteService : IDeliveryNoteService
    {
        private IDeliveryNoteRepository _deliveryNoteRepository;
        private IUnitOfWork _unitOfWork;

        public DeliveryNoteService(IDeliveryNoteRepository deliveryNoteRepository, IUnitOfWork unitOfWork)
        {
            this._deliveryNoteRepository = deliveryNoteRepository;
            this._unitOfWork = unitOfWork;
        }

        public DeliveryNote Add(DeliveryNote deliveryNote)
        {
            return _deliveryNoteRepository.Add(deliveryNote);
        }

        public DeliveryNote Delete(int id)
        {
            return _deliveryNoteRepository.Delete(id);
        }

        public IEnumerable<DeliveryNote> GetAll()
        {
            return _deliveryNoteRepository.GetAll();
        }

        public IEnumerable<DeliveryNote> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _deliveryNoteRepository.GetMulti(x => x.Code.Contains(keyword));
            else
                return _deliveryNoteRepository.GetAll();
        }

        public DeliveryNote GetById(int id)
        {
            return _deliveryNoteRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(DeliveryNote deliveryNote)
        {
            _deliveryNoteRepository.Update(deliveryNote);
        }
    }
}