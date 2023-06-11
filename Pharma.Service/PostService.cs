using Pharma.Common;
using Pharma.Data.Infrastructure;
using Pharma.Data.Repositories;
using Pharma.Model.Models;
using System.Collections.Generic;

namespace Pharma.Service
{
    public interface IPostService
    {
        Post Add(Post post);

        void Update(Post post);

        Post Delete(int id);

        IEnumerable<Post> GetAll();

        IEnumerable<Post> GetAll(string keyword);

        IEnumerable<Post> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow);

        IEnumerable<Post> GetAllByCategoryPaging(int categoryId, int page, int pageSize, out int totalRow);

        IEnumerable<Post> GetAllPaging(int page, int pageSize, out int totalRow);

        Post GetById(int id);
        Post GetByAlias(string alias);

        void Save();
    }

    public class PostService : IPostService
    {
        private IPostRepository _postRepository;
        private ITagRepository _tagRepository;
        private IPostTagRepository _postTagRepository;
        private IUnitOfWork _unitOfWork;

        public PostService(IPostRepository postRepository, ITagRepository tagRepository, IPostTagRepository postTagRepository, IUnitOfWork unitOfWork)
        {
            this._postRepository = postRepository;
            this._tagRepository = tagRepository;
            this._postTagRepository = postTagRepository;
            this._unitOfWork = unitOfWork;
        }

        public Post Add(Post post)
        {
            var postAdd = _postRepository.Add(post);
            _unitOfWork.Commit();
            if (!string.IsNullOrEmpty(post.Tags))
            {
                string[] tags = post.Tags.Split(',');
                for (var i = 0; i < tags.Length; i++)
                {
                    var tagId = StringHelper.ToUnsignString(tags[i]);
                    if (_tagRepository.Count(x => x.ID == tagId) == 0)
                    {
                        Tag tag = new Tag();
                        tag.ID = tagId;
                        tag.Name = tags[i];
                        tag.Type = CommonConstants.PostTag;
                        _tagRepository.Add(tag);
                    }
                    PostTag postTag = new PostTag();
                    postTag.PostID = post.ID;
                    postTag.TagID = tagId;

                    _postTagRepository.Add(postTag);
                }
                _unitOfWork.Commit();
            }
            return postAdd;
        }

        public Post Delete(int id)
        {
            return _postRepository.Delete(id);
        }

        public IEnumerable<Post> GetAll()
        {
            return _postRepository.GetAll(new string[] { "PostCategory" });
        }

        public IEnumerable<Post> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _postRepository.GetMulti(x => x.Name.Contains(keyword) || x.Description.Contains(keyword));
            else
                return _postRepository.GetAll();
        }

        public IEnumerable<Post> GetAllByCategoryPaging(int categoryId, int page, int pageSize, out int totalRow)
        {
            return _postRepository.GetMultiPaging(x => x.Status && x.CategoryID == categoryId, out totalRow, page, pageSize, new string[] { "PostCategory" });
        }

        public IEnumerable<Post> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow)
        {
            //TODO: Select all post by tag
            return _postRepository.GetAllByTag(tag, page, pageSize, out totalRow);
        }

        public IEnumerable<Post> GetAllPaging(int page, int pageSize, out int totalRow)
        {
            return _postRepository.GetMultiPaging(x => x.Status, out totalRow, page, pageSize);
        }

        public Post GetByAlias(string alias)
        {
            return _postRepository.GetSingleByCondition(x => x.Alias == alias && x.Status);
        }

        public Post GetById(int id)
        {
            return _postRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Post post)
        {
            _postRepository.Update(post);
            if (!string.IsNullOrEmpty(post.Tags))
            {
                string[] tags = post.Tags.Split(',');
                for (var i = 0; i < tags.Length; i++)
                {
                    var tagId = StringHelper.ToUnsignString(tags[i]);
                    if (_tagRepository.Count(x => x.ID == tagId) == 0)
                    {
                        Tag tag = new Tag();
                        tag.ID = tagId;
                        tag.Name = tags[i];
                        tag.Type = CommonConstants.PostTag;
                        _tagRepository.Add(tag);
                    }
                    _postTagRepository.DeleteMulti(x => x.PostID == post.ID);
                    PostTag postTag = new PostTag();
                    postTag.PostID = post.ID;
                    postTag.TagID = tagId;

                    _postTagRepository.Add(postTag);
                }
            }
        }
    }
}