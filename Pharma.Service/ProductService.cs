using Pharma.Common;
using Pharma.Data.Infrastructure;
using Pharma.Data.Repositories;
using Pharma.Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace Pharma.Service
{
    public interface IProductService
    {
        Product Add(Product product);

        void Update(Product product);

        Product Delete(int id);

        IEnumerable<Product> GetAll();
        IEnumerable<Product> GetProductHot();

        IEnumerable<Product> GetAll(string keyword);

        IEnumerable<Product> GetListProductByCategoryIdPaging(int catgoryId, int page, int pageSize, out int totalRow);
        IEnumerable<Product> Search(string keyword, int page, int pageSize, out int totalRow);
        IEnumerable<string> GetListProductByName(string name);
        IEnumerable<Product> GetRatedProducts(int id,int top);
        Product GetById(int id);
        IEnumerable<Tag> GetListTagByProductId(int id);
        Tag GetTag(string tagId);
        void IncreaseView(int id);
        IEnumerable<Product> GetListProductByTag(string tagId, int page, int pageSize, out int totalRow);

        void Save();
    }

    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;
        private ITagRepository _tagRepository;
        private IProductTagRepository _productTagRepository;
        private IUnitOfWork _unitOfWork;

        public ProductService(IProductRepository productRepository, ITagRepository tagRepository, IProductTagRepository productTagRepository, IUnitOfWork unitOfWork)
        {
            this._productRepository = productRepository;
            this._tagRepository = tagRepository;
            this._productTagRepository = productTagRepository;
            this._unitOfWork = unitOfWork;
        }

        public Product Add(Product product)
        {
            var productAdd = _productRepository.Add(product);
            _unitOfWork.Commit();
            if (!string.IsNullOrEmpty(product.Tags))
            {
                string[] tags = product.Tags.Split(',');
                for(var i = 0 ; i < tags.Length ; i++)
                {
                    var tagId = StringHelper.ToUnsignString(tags[i]);
                    if(_tagRepository.Count(x=>x.ID==tagId)==0)
                    {
                        Tag tag = new Tag();
                        tag.ID = tagId;
                        tag.Name = tags[i];
                        tag.Type = CommonConstants.ProductTag;
                        _tagRepository.Add(tag);
                    }
                    ProductTag productTag = new ProductTag();
                    productTag.ProductID = productAdd.ID;
                    productTag.TagID = tagId;

                    _productTagRepository.Add(productTag);
                }
                _unitOfWork.Commit();
            }
            return productAdd;
        }

        public Product Delete(int id)
        {
            return _productRepository.Delete(id);
        }

        public IEnumerable<Product> GetAll()
        {
            return _productRepository.GetAll();
        }

        public IEnumerable<Product> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _productRepository.GetMulti(x => x.Name.Contains(keyword) || x.Description.Contains(keyword));
            else
                return _productRepository.GetAll();
        }

        public Product GetById(int id)
        {
            return _productRepository.GetSingleById(id);
        }

        public IEnumerable<Product> GetListProductByCategoryIdPaging(int catgoryId, int page, int pageSize, out int totalRow)
        {
            var query = _productRepository.GetMulti(x => x.Status && x.CategoryID == catgoryId);
            totalRow = query.Count();

            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public IEnumerable<string> GetListProductByName(string name)
        {
            return _productRepository.GetMulti(x => x.Status && x.Name.Contains(name)).Select(y=>y.Name);
        }

        public IEnumerable<Product> GetProductHot()
        {
            return _productRepository.GetProductHot();
        }

        public IEnumerable<Product> GetRatedProducts(int id, int top)
        {
            var product = _productRepository.GetSingleById(id);
            return _productRepository.GetMulti(x => x.Status && x.ID != id && x.CategoryID==product.CategoryID).OrderByDescending(x=>x.CreatedDate).Take(top);
        }
        public IEnumerable<Product> GetListProductByTag(string tagId, int page, int pageSize, out int totalRow)
        {
            var model = _productRepository.GetListProductByTag(tagId, page, pageSize, out totalRow);

            return model;
        }

        public IEnumerable<Tag> GetListTagByProductId(int id)
        {
            return _productTagRepository.GetMulti(x => x.ProductID == id, new string[] { "Tag" }).Select(y => y.Tag);
        }
        public void IncreaseView(int id)
        {
            var product = _productRepository.GetSingleById(id);
            if (product.ViewCount.HasValue)
                product.ViewCount += 1;
            else
                product.ViewCount = 1;
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<Product> Search(string keyword, int page, int pageSize, out int totalRow)
        {
            var query = _productRepository.GetMulti(x => x.Status && x.Name.Contains(keyword));
            totalRow = query.Count();

            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public void Update(Product product)
        {
            _productRepository.Update(product);
            if (!string.IsNullOrEmpty(product.Tags))
            {
                string[] tags = product.Tags.Split(',');
                for (var i = 0; i < tags.Length; i++)
                {
                    var tagId = StringHelper.ToUnsignString(tags[i]);
                    if (_tagRepository.Count(x => x.ID == tagId) == 0)
                    {
                        Tag tag = new Tag();
                        tag.ID = tagId;
                        tag.Name = tags[i];
                        tag.Type = CommonConstants.ProductTag;
                        _tagRepository.Add(tag);
                    }
                    _productTagRepository.DeleteMulti(x => x.ProductID == product.ID);
                    ProductTag productTag = new ProductTag();
                    productTag.ProductID = product.ID;
                    productTag.TagID = tagId;

                    _productTagRepository.Add(productTag);
                }
            }
        }

        public Tag GetTag(string tagId)
        {
            return _tagRepository.GetSingleByCondition(x=>x.ID==tagId);
        }
    }
}