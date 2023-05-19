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
    public interface IFooterService
    {
        Footer Add(Footer footer);

        void Update(Footer footer);

        Footer Delete(int id);

        IEnumerable<Footer> GetAll();

        IEnumerable<Footer> GetAll(string keyword);

        Footer GetById(int id);

        void Save();
    }

    public class FooterService : IFooterService
    {
        private IFooterRepository _footerRepository;
        private IUnitOfWork _unitOfWork;

        public FooterService(IFooterRepository footerRepository, IUnitOfWork unitOfWork)
        {
            this._footerRepository = footerRepository;
            this._unitOfWork = unitOfWork;
        }

        public Footer Add(Footer footer)
        {
            return _footerRepository.Add(footer);
        }

        public Footer Delete(int id)
        {
            return _footerRepository.Delete(id);
        }

        public IEnumerable<Footer> GetAll()
        {
            return _footerRepository.GetAll();
        }

        public IEnumerable<Footer> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _footerRepository.GetMulti(x => x.Name.Contains(keyword));
            else
                return _footerRepository.GetAll();
        }

        public Footer GetById(int id)
        {
            return _footerRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Footer footer)
        {
            _footerRepository.Update(footer);
        }
    }
}
