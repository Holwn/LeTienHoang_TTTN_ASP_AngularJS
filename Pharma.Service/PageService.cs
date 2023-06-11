using Pharma.Data.Infrastructure;
using Pharma.Data.Repositories;
using Pharma.Model.Models;
using System.Collections.Generic;

namespace Pharma.Service
{
    public interface IPageService
    {
        Page Add(Page page);

        void Update(Page page);

        Page Delete(int id);

        IEnumerable<Page> GetAll();

        IEnumerable<Page> GetAll(string keyword);

        Page GetById(int id);
        Page GetByAlias(string alias);
        void Save();
    }

    public class PageService : IPageService
    {
        private IPageRepository _pageRepository;
        private IUnitOfWork _unitOfWork;

        public PageService(IPageRepository pageRepository, IUnitOfWork unitOfWork)
        {
            this._pageRepository = pageRepository;
            this._unitOfWork = unitOfWork;
        }

        public Page Add(Page page)
        {
            return _pageRepository.Add(page);
        }

        public Page Delete(int id)
        {
            return _pageRepository.Delete(id);
        }

        public IEnumerable<Page> GetAll()
        {
            return _pageRepository.GetAll();
        }

        public IEnumerable<Page> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _pageRepository.GetMulti(x => x.Name.Contains(keyword));
            else
                return _pageRepository.GetAll();
        }

        public Page GetByAlias(string alias)
        {
            return _pageRepository.GetSingleByCondition(x => x.Alias == alias && x.Status);
        }

        public Page GetById(int id)
        {
            return _pageRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Page page)
        {
            _pageRepository.Update(page);
        }
    }
}