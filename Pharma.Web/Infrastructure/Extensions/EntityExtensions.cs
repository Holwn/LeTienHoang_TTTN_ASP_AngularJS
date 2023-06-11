using Pharma.Model.Models;
using Pharma.Web.Mappings;
using Pharma.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pharma.Web.Infrastructure.Extensions
{
    public static class EntityExtensions
    {
        public static void UpdateContactDetail(this ContactDetail contactDetail, ContactDetailViewModel contactDetailVm)
        {
            contactDetail.ID = contactDetailVm.ID;
            contactDetail.Name = contactDetailVm.Name;
            contactDetail.Phone = contactDetailVm.Phone;
            contactDetail.Email = contactDetailVm.Email;
            contactDetail.Website = contactDetailVm.Website;
            contactDetail.Address = contactDetailVm.Address;
            contactDetail.Other = contactDetailVm.Other;
            contactDetail.Lat = contactDetailVm.Lat;
            contactDetail.Lng = contactDetailVm.Lng;
            contactDetail.Status = contactDetailVm.Status;
        }
        public static void UpdateDeliveryNoteItem(this DeliveryNoteItem deliveryNoteItem, DeliveryNoteItemViewModel deliveryNoteItemVm)
        {
            deliveryNoteItem.NoteID = deliveryNoteItemVm.NoteID;
            deliveryNoteItem.ProductID = deliveryNoteItemVm.ProductID;
            deliveryNoteItem.UnitID = deliveryNoteItemVm.UnitID;
            deliveryNoteItem.Price = deliveryNoteItemVm.Price;
            deliveryNoteItem.Quantity = deliveryNoteItemVm.Quantity;
            deliveryNoteItem.VAT = deliveryNoteItemVm.VAT;
            deliveryNoteItem.Discount = deliveryNoteItemVm.Discount;
            deliveryNoteItem.BatchNumber = deliveryNoteItemVm.BatchNumber;
            deliveryNoteItem.ExpiredDate = deliveryNoteItemVm.ExpiredDate;
        }
        public static void UpdateDeliveryNote(this DeliveryNote deliveryNote, DeliveryNoteViewModel deliveryNoteVm)
        {
            deliveryNote.ID = deliveryNoteVm.ID;
            deliveryNote.Code = deliveryNoteVm.Code;
            deliveryNote.Number = deliveryNoteVm.Number;
            deliveryNote.Date = deliveryNoteVm.Date;
            deliveryNote.CustomerID = deliveryNoteVm.CustomerID;
            deliveryNote.Description = deliveryNoteVm.Description;
            deliveryNote.VAT = deliveryNoteVm.VAT;
            deliveryNote.Amount = deliveryNoteVm.Amount;
            deliveryNote.PaymentAmount = deliveryNoteVm.PaymentAmount;
            deliveryNote.PaymentMethod = deliveryNoteVm.PaymentMethod;
            deliveryNote.StoreID = deliveryNoteVm.StoreID;
            deliveryNote.DeliveryNoteProductItems = deliveryNoteVm.DeliveryNoteProductItems;
            deliveryNote.CreatedDate = deliveryNoteVm.CreatedDate;
            deliveryNote.CreatedBy = deliveryNoteVm.CreatedBy;
            deliveryNote.UpdatedDate = deliveryNoteVm.UpdatedDate;
            deliveryNote.UpdatedBy = deliveryNoteVm.UpdatedBy;
            deliveryNote.Status = deliveryNoteVm.Status;
        }
        public static void UpdateFooter(this Footer footer, FooterViewModel footerVm)
        {
            footer.ID = footerVm.ID;
            footer.Name = footerVm.Name;
            footer.Type = footerVm.Type;
            footer.Link = footerVm.Link;
            footer.ParentID = footerVm.ParentID;
        }
        public static void UpdateNoteBook(this NoteBook noteBook, NoteBookViewModel noteBookVm)
        {
            noteBook.ID = noteBookVm.ID;
            noteBook.Name = noteBookVm.Name;
            noteBook.Contents = noteBookVm.Contents;
            noteBook.ModelTargetLink = noteBookVm.ModelTargetLink;
            noteBook.CreatedDate = noteBookVm.CreatedDate;
            noteBook.CreatedBy = noteBookVm.CreatedBy;
            noteBook.UpdatedDate = noteBookVm.UpdatedDate;
            noteBook.UpdatedBy = noteBookVm.UpdatedBy;
            noteBook.Status = noteBookVm.Status;
        }
        public static void UpdatePage(this Page page, PageViewModel pageVm)
        {
            page.ID = pageVm.ID;
            page.Name = pageVm.Name;
            page.Alias = pageVm.Alias;
            page.Contents = pageVm.Contents;
            page.MetaKeyword = pageVm.MetaKeyword;
            page.MetaDescription = pageVm.MetaDescription;
            page.CreatedDate = pageVm.CreatedDate;
            page.CreatedBy = pageVm.CreatedBy;
            page.UpdatedDate = pageVm.UpdatedDate;
            page.UpdatedBy = pageVm.UpdatedBy;
            page.Status = pageVm.Status;
        }
        public static void UpdatePostCategory(this PostCategory postCategory, PostCategoryViewModel postCategoryVm)
        {
            postCategory.ID = postCategoryVm.ID;
            postCategory.Name = postCategoryVm.Name;
            postCategory.Alias = postCategoryVm.Alias;
            postCategory.Description = postCategoryVm.Description;
            postCategory.ParentID = postCategoryVm.ParentID;
            postCategory.DisplayOrder = postCategoryVm.DisplayOrder;
            postCategory.Image = postCategoryVm.Image;
            postCategory.MetaKeyword = postCategoryVm.MetaKeyword;
            postCategory.MetaDescription = postCategoryVm.MetaDescription;
            postCategory.HomeFlag = postCategoryVm.HomeFlag;
            postCategory.CreatedDate = postCategoryVm.CreatedDate;
            postCategory.CreatedBy = postCategoryVm.CreatedBy;
            postCategory.UpdatedDate = postCategoryVm.UpdatedDate;
            postCategory.UpdatedBy = postCategoryVm.UpdatedBy;
            postCategory.Status = postCategoryVm.Status;
        }
        public static void UpdatePostTag(this PostTag postTag, PostTagViewModel postTagVm)
        {
            postTag.PostID = postTagVm.PostID;
            postTag.TagID = postTagVm.TagID;
        }
        public static void UpdatePost(this Post post, PostViewModel postVm)
        {
            post.ID = postVm.ID;
            post.Name = postVm.Name;
            post.Alias = postVm.Alias;
            post.CategoryID = postVm.CategoryID;
            post.Image = postVm.Image;
            post.Description = postVm.Description;
            post.Contents = postVm.Contents;
            post.MetaKeyword = postVm.MetaKeyword;
            post.MetaDescription = postVm.MetaDescription;
            post.HomeFlag = postVm.HomeFlag;
            post.HotFlag = postVm.HotFlag;
            post.ViewCount = postVm.ViewCount;
            post.Tags = postVm.Tags;
            post.CreatedDate = postVm.CreatedDate;
            post.CreatedBy = postVm.CreatedBy;
            post.UpdatedDate = postVm.UpdatedDate;
            post.UpdatedBy = postVm.UpdatedBy;
            post.Status = postVm.Status;
        }
        public static void UpdateProductCategory(this ProductCategory productCategory, ProductCategoryViewModel productCategoryVm)
        {
            productCategory.ID = productCategoryVm.ID;
            productCategory.Name = productCategoryVm.Name;
            productCategory.Alias = productCategoryVm.Alias;
            productCategory.Description = productCategoryVm.Description;
            productCategory.ParentID = productCategoryVm.ParentID;
            productCategory.DisplayOrder = productCategoryVm.DisplayOrder;
            productCategory.Image = productCategoryVm.Image;
            productCategory.MetaKeyword = productCategoryVm.MetaKeyword;
            productCategory.MetaDescription = productCategoryVm.MetaDescription;
            productCategory.HomeFlag = productCategoryVm.HomeFlag;
            productCategory.CreatedDate = productCategoryVm.CreatedDate;
            productCategory.CreatedBy = productCategoryVm.CreatedBy;
            productCategory.UpdatedDate = productCategoryVm.UpdatedDate;
            productCategory.UpdatedBy = productCategoryVm.UpdatedBy;
            productCategory.Status = productCategoryVm.Status;
        }
        public static void UpdateProductMapping(this ProductMapping productMapping, ProductMappingViewModel productMappingVm)
        {
            productMapping.ID = productMappingVm.ID;
            productMapping.ProductID = productMappingVm.ProductID;
            productMapping.RetailID = productMappingVm.RetailID;
            productMapping.ReceiptQuantity = productMappingVm.ReceiptQuantity;
            productMapping.ReceiptPrice = productMappingVm.ReceiptPrice;
            productMapping.DeliveryQuantity = productMappingVm.DeliveryQuantity;
            productMapping.DeliveryPrice = productMappingVm.DeliveryPrice;
            productMapping.BatchNumber = productMappingVm.BatchNumber;
            productMapping.ExpiredDate = productMappingVm.ExpiredDate;
        }
        public static void UpdateProductTag(this ProductTag productTag, ProductTagViewModel productTagVm)
        {
            productTag.ProductID = productTagVm.ProductID;
            productTag.TagID = productTagVm.TagID;
        }
        public static void UpdateProduct(this Product product, ProductViewModel productVm)
        {
            product.ID = productVm.ID;
            product.Name = productVm.Name;
            product.Alias = productVm.Alias;
            product.CategoryID = productVm.CategoryID;
            product.Image = productVm.Image;
            product.MoreImage = productVm.MoreImage;
            product.BatchPrice = productVm.BatchPrice;
            product.RetailPrice = productVm.RetailPrice;
            product.BatchUnitID = productVm.BatchUnitID;
            product.RetailUnitID = productVm.RetailUnitID;
            product.Contain = productVm.Contain;
            product.MiduleUnitID = productVm.MiduleUnitID;
            product.ContainMidule = productVm.ContainMidule;
            product.Quantity = productVm.Quantity;
            product.BatchNumber = productVm.BatchNumber;
            product.ExpiredDate = productVm.ExpiredDate;
            product.IsHaveExpiredDate = productVm.IsHaveExpiredDate;
            product.Barcode = productVm.Barcode;
            product.Description = productVm.Description;
            product.Contents = productVm.Contents;
            product.Manufacturer = productVm.Manufacturer;
            product.AddressManufacturer = productVm.AddressManufacturer;
            product.StoreID = productVm.StoreID;
            product.MetaKeyword = productVm.MetaKeyword;
            product.MetaDescription = productVm.MetaDescription;
            product.Prescription = productVm.Prescription;
            product.HomeFlag = productVm.HomeFlag;
            product.HotFlag = productVm.HotFlag;
            product.ViewCount = productVm.ViewCount;
            product.CreatedDate = productVm.CreatedDate;
            product.CreatedBy = productVm.CreatedBy;
            product.UpdatedDate = productVm.UpdatedDate;
            product.UpdatedBy = productVm.UpdatedBy;
            product.Status = productVm.Status;
            product.Tags = productVm.Tags;
        }
        public static void UpdateReceiptNoteItem(this ReceiptNoteItem receiptNoteItem, ReceiptNoteItemViewModel receiptNoteItemVm)
        {
            receiptNoteItem.NoteID = receiptNoteItemVm.NoteID;
            receiptNoteItem.ProductID = receiptNoteItemVm.ProductID;
            receiptNoteItem.UnitID = receiptNoteItemVm.UnitID;
            receiptNoteItem.Price = receiptNoteItemVm.Price;
            receiptNoteItem.Quantity = receiptNoteItemVm.Quantity;
            receiptNoteItem.VAT = receiptNoteItemVm.VAT;
            receiptNoteItem.Discount = receiptNoteItemVm.Discount;
            receiptNoteItem.BatchNumber = receiptNoteItemVm.BatchNumber;
            receiptNoteItem.ExpiredDate = receiptNoteItemVm.ExpiredDate;
        }
        public static void UpdateReceiptNote(this ReceiptNote receiptNote, ReceiptNoteViewModel receiptNoteVm)
        {
            receiptNote.ID = receiptNoteVm.ID;
            receiptNote.Code = receiptNoteVm.Code;
            receiptNote.Date = receiptNoteVm.Date;
            receiptNote.SupplierID = receiptNoteVm.SupplierID;
            receiptNote.Description = receiptNoteVm.Description;
            receiptNote.VAT = receiptNoteVm.VAT;
            receiptNote.Amount = receiptNoteVm.Amount;
            receiptNote.PaymentAmount = receiptNoteVm.PaymentAmount;
            receiptNote.PaymentMethod = receiptNoteVm.PaymentMethod;
            receiptNote.StoreID = receiptNoteVm.StoreID;
            receiptNote.ReceiptNoteProductItems = receiptNoteVm.ReceiptNoteProductItems;
            receiptNote.CreatedDate = receiptNoteVm.CreatedDate;
            receiptNote.CreatedBy = receiptNoteVm.CreatedBy;
            receiptNote.UpdatedDate = receiptNoteVm.UpdatedDate;
            receiptNote.UpdatedBy = receiptNoteVm.UpdatedBy;
            receiptNote.Status = receiptNoteVm.Status;
        }
        public static void UpdateSlide(this Slide slide,SlideViewModel slideVm)
        {
            slide.ID = slideVm.ID;
            slide.Name = slideVm.Name;
            slide.Description = slideVm.Description;
            slide.Image = slideVm.Image;
            slide.Url = slideVm.Url;
            slide.DisplayOrder = slideVm.DisplayOrder;
            slide.Status = slideVm.Status;
        }
        public static void UpdateStoreRole(this StoreRole storeRole, StoreRoleViewModel storeRoleVm)
        {
            storeRole.ID = storeRoleVm.ID;
            storeRole.Name = storeRoleVm.Name;
            storeRole.Description = storeRoleVm.Description;
        }
        public static void UpdateStore(this Store store, StoreViewModel storeVm)
        {
            store.ID = storeVm.ID;
            store.Name = storeVm.Name;
            store.Alias = storeVm.Alias;
            store.Phone = storeVm.Phone;
            store.Email = storeVm.Email;
            store.Address = storeVm.Address;
            store.Description = storeVm.Description;
            store.Barcode = storeVm.Barcode;
            store.RefStoreID = storeVm.RefStoreID;
            store.OwnerID = storeVm.OwnerID;
            store.RoleID = storeVm.RoleID;
            store.CreatedDate = storeVm.CreatedDate;
            store.CreatedBy = storeVm.CreatedBy;
            store.UpdatedDate = storeVm.UpdatedDate;
            store.UpdatedBy = storeVm.UpdatedBy;
            store.Status = storeVm.Status;
        }
        public static void UpdateSubject(this Subject subject, SubjectViewModel subjectVm)
        {
            subject.ID = subjectVm.ID;
            subject.Name = subjectVm.Name;
            subject.Alias = subjectVm.Alias;
            subject.Phone = subjectVm.Phone;
            subject.Email = subjectVm.Email;
            subject.Address = subjectVm.Address;
            subject.Description = subjectVm.Description;
            subject.Barcode = subjectVm.Barcode;
            subject.TypeSub = subjectVm.TypeSub;
            subject.StoreID = subjectVm.StoreID;
            subject.CreatedDate = subjectVm.CreatedDate;
            subject.CreatedBy = subjectVm.CreatedBy;
            subject.UpdatedDate = subjectVm.UpdatedDate;
            subject.UpdatedBy = subjectVm.UpdatedBy;
            subject.Status = subjectVm.Status;
        }
        public static void UpdateTag(this Tag tag, TagViewModel tagVm)
        {
            tag.ID = tagVm.ID;
            tag.Name = tagVm.Name;
            tag.Type = tagVm.Type;
        }
        public static void UpdateUnit(this Unit unit, UnitViewModel unitVm)
        {
            unit.ID = unitVm.ID;
            unit.Name = unitVm.Name;
            unit.StoreID = unitVm.StoreID;
            unit.Description = unitVm.Description;
            unit.Status = unitVm.Status;
        }
    }
}