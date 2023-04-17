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
    public interface IErrorService
    {
        Error Create(Error error);
        void Save(); 
    }
    public class ErrorService : IErrorService
    {
        IErrorRepository _errorRepository;
        IUnitOfWork _unitOfWork;
        public ErrorService(IErrorRepository errorRepository,IUnitOfWork unitOfWork)
        {
            this._errorRepository = errorRepository;
            this._unitOfWork = unitOfWork;
        }
        public Error Create(Error error)
        {
            return _errorRepository.Add(error);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}
