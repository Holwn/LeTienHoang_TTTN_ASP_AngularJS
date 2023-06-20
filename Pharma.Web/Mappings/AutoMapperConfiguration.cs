using AutoMapper;
using Pharma.Model.Models;
using Pharma.Web.Models;

namespace Pharma.Web.Mappings
{
    public class AutoMapperConfiguration
    {
        public static Mapper InitializeAutomapper()
        {
            var config = new MapperConfiguration(cfg =>
              {
                  cfg.CreateMap<ApplicationUser, ApplicationUserViewModel>();
                  cfg.CreateMap<ApplicationRole, ApplicationRoleViewModel>();
                  cfg.CreateMap<ApplicationGroup, ApplicationGroupViewModel>();
                  cfg.CreateMap<ContactDetail, ContactDetailViewModel>();
                  cfg.CreateMap<DeliveryNoteItem, DeliveryNoteItemViewModel>();
                  cfg.CreateMap<DeliveryNote, DeliveryNoteViewModel>();
                  cfg.CreateMap<Footer, FooterViewModel>();
                  cfg.CreateMap<NoteBook, NoteBookViewModel>();
                  cfg.CreateMap<Page, PageViewModel>();
                  cfg.CreateMap<PostCategory, PostCategoryViewModel>();
                  cfg.CreateMap<PostTag, PostTagViewModel>();
                  cfg.CreateMap<Post, PostViewModel>();
                  cfg.CreateMap<ProductCategory, ProductCategoryViewModel>();
                  cfg.CreateMap<ProductMapping, ProductMappingViewModel>();
                  cfg.CreateMap<ProductTag, ProductTagViewModel>();
                  cfg.CreateMap<Product, ProductViewModel>();
                  cfg.CreateMap<ReceiptNoteItem, ReceiptNoteItemViewModel>();
                  cfg.CreateMap<ReceiptNote, ReceiptNoteViewModel>();
                  cfg.CreateMap<Slide, SlideViewModel>();
                  cfg.CreateMap<StoreRole, StoreRoleViewModel>();
                  cfg.CreateMap<Store, StoreViewModel>();
                  cfg.CreateMap<Subject, SubjectViewModel>();
                  cfg.CreateMap<Tag, TagViewModel>();
                  cfg.CreateMap<Unit, UnitViewModel>();
              });
            var mapper = new Mapper(config);
            return mapper;
        }
    }
}