using System.Collections.Generic;
using AutoMapper;
using LILI_CRM.DAL.CRM;

using LILI_CRM.Web.Models;
using LILI_CRM.Web.Areas.PCV.ViewModel;
//using LILI_CRM.Web.Areas.BMS.ViewModel;


namespace LILI_CRM.Web.Utility
{
    public static class BMSMappingExtensions
    {


        //User And Menu
        //public static UserMenuModel ToModel(this WebUserManager entity)
        //{
        //    return Mapper.Map<WebUserManager, UserMenuModel>(entity);
        //}
        //public static WebUserManager ToEntity(this UserMenuModel model)
        //{
        //    return Mapper.Map<UserMenuModel, WebUserManager>(model);
        //}

        public static UserMenuDetail ToModel(this BMS_tblUserWiseMenuAssign entity)
        {
            return Mapper.Map<BMS_tblUserWiseMenuAssign, UserMenuDetail>(entity);
        }
        public static BMS_tblUserWiseMenuAssign ToEntity(this UserMenuDetail detailModel)
        {
            return Mapper.Map<UserMenuDetail, BMS_tblUserWiseMenuAssign>(detailModel);
        }


        //User Manager
        public static ChangePassViewModel ToModel(this WebUserManager entity)
        {
            return Mapper.Map<WebUserManager, ChangePassViewModel>(entity);
        }
        public static WebUserManager ToEntity(this ChangePassViewModel entity)
        {
            return Mapper.Map<ChangePassViewModel, WebUserManager>(entity);
        }


        //Voucher Master
        public static VoucherViewModel ToModel(this WebVoucherMaster entity)
        {
            return Mapper.Map<WebVoucherMaster, VoucherViewModel>(entity);
        }
        public static WebVoucherMaster ToEntity(this VoucherViewModel entity)
        {
            return Mapper.Map<VoucherViewModel, WebVoucherMaster>(entity);
        }

        //Voucher Detail
        public static VoucherDetailEntityModel ToModel(this WebVoucherDetails entity)
        {
            return Mapper.Map<WebVoucherDetails, VoucherDetailEntityModel>(entity);
        }
        public static WebVoucherDetails ToEntity(this VoucherDetailEntityModel entity)
        {
            return Mapper.Map<VoucherDetailEntityModel, WebVoucherDetails>(entity);
        }

        //  Article 
        public static ProductModel ToModel(this tblProduct entity)
        {
            return Mapper.Map<tblProduct, ProductModel>(entity);
        }
        public static tblProduct ToEntity(this ProductModel entity)
        {
            return Mapper.Map<ProductModel, tblProduct>(entity);
        }

        //Divion 
        public static DivisionModel ToModel(this tblDivision entity)
        {
            return Mapper.Map<tblDivision, DivisionModel>(entity);
        }

        public static tblDivision ToEntity(this DivisionModel model)
        {
            return Mapper.Map<DivisionModel, tblDivision>(model);
        }

        //SBU 
        public static SBUModel ToModel(this tblSBU entity)
        {
            return Mapper.Map<tblSBU, SBUModel>(entity);
        }

        public static tblSBU ToEntity(this SBUModel model)
        {
            return Mapper.Map<SBUModel, tblSBU>(model);
        }

        //  Supplier 
        public static SupplierInfoModel ToModel(this tblSupplier entity)
        {
            return Mapper.Map<tblSupplier, SupplierInfoModel>(entity);
        }
        public static tblSupplier ToEntity(this SupplierInfoModel entity)
        {
            return Mapper.Map<SupplierInfoModel, tblSupplier>(entity);
        }

        //Customer Information
        public static CustomerInfoModel ToModel(this tblCustomer entity)
        {
            return Mapper.Map<tblCustomer, CustomerInfoModel>(entity);
        }

        public static tblCustomer ToEntity(this CustomerInfoModel model)
        {
            return Mapper.Map<CustomerInfoModel, tblCustomer>(model);
        }

        //Sample Request
        public static SampleRequestModel ToModel(this tblSampleRequest entity)
        {
            return Mapper.Map<tblSampleRequest, SampleRequestModel>(entity);
        }

        public static tblSampleRequest ToEntity(this SampleRequestModel model)
        {
            return Mapper.Map<SampleRequestModel, tblSampleRequest>(model);
        }
        
        public static SampleRequestDetailModel ToModel(this tblSampleRequestDetails entity)
        {
            return Mapper.Map<tblSampleRequestDetails, SampleRequestDetailModel>(entity);
        }

        public static tblSampleRequestDetails ToEntity(this SampleRequestDetailModel model)
        {
            return Mapper.Map<SampleRequestDetailModel, tblSampleRequestDetails>(model);
        }

        public static SampleRequestDocumentModel ToModel(this tblSampleRequestDocument entity)
        {
            return Mapper.Map<tblSampleRequestDocument, SampleRequestDocumentModel>(entity);
        }

        public static tblSampleRequestDocument ToEntity(this SampleRequestDocumentModel model)
        {
            return Mapper.Map<SampleRequestDocumentModel, tblSampleRequestDocument>(model);
        }


        //Sample Submission
        public static SampleSubmissionModel ToModel(this tblSampleSubmission entity)
        {
            return Mapper.Map<tblSampleSubmission, SampleSubmissionModel>(entity);
        }

        public static tblSampleSubmission ToEntity(this SampleSubmissionModel model)
        {
            return Mapper.Map<SampleSubmissionModel, tblSampleSubmission>(model);
        }

        public static SampleSubmissionDetailModel ToModel(this tblSampleSubmissionDet entity)
        {
            return Mapper.Map<tblSampleSubmissionDet, SampleSubmissionDetailModel>(entity);
        }

        public static tblSampleSubmissionDet ToEntity(this SampleSubmissionDetailModel model)
        {
            return Mapper.Map<SampleSubmissionDetailModel, tblSampleSubmissionDet>(model);
        }
        // Save Submission Attached Document 
        public static SampleSubmissionDocumentModel ToModel(this tblSampleSubmissionDocument entity)
        {
            return Mapper.Map<tblSampleSubmissionDocument, SampleSubmissionDocumentModel>(entity);
        }

        public static tblSampleSubmissionDocument ToEntity(this SampleSubmissionDocumentModel model)
        {
            return Mapper.Map<SampleSubmissionDocumentModel, tblSampleSubmissionDocument>(model);
        }


        //Quote
        public static QuoteModel ToModel(this tblQuote entity)
        {
            return Mapper.Map<tblQuote, QuoteModel>(entity);
        }

        public static tblQuote ToEntity(this QuoteModel model)
        {
            return Mapper.Map<QuoteModel, tblQuote>(model);
        }
        //  Quote Detail
        public static QuoteDetailModel ToModel(this tblQuoteDetails entity)
        {
            return Mapper.Map<tblQuoteDetails, QuoteDetailModel>(entity);
        }

        public static tblQuoteDetails ToEntity(this QuoteDetailModel model)
        {
            return Mapper.Map<QuoteDetailModel, tblQuoteDetails>(model);
        }

        //  Quote Attach Doc
        public static QuoteDocumentModel ToModel(this tblQuoteDocument entity)
        {
            return Mapper.Map<tblQuoteDocument, QuoteDocumentModel>(entity);
        }

        public static tblQuoteDocument ToEntity(this QuoteDocumentModel model)
        {
            return Mapper.Map<QuoteDocumentModel, tblQuoteDocument>(model);
        }

        //Invoice
        public static InvoiceModel ToModel(this tblInvoice entity)
        {
            return Mapper.Map<tblInvoice, InvoiceModel>(entity);
        }

        public static tblInvoice ToEntity(this InvoiceModel model)
        {
            return Mapper.Map<InvoiceModel, tblInvoice>(model);
        }

        public static InvoiceDetailModel ToModel(this tblInvoiceDetails entity)
        {
            return Mapper.Map<tblInvoiceDetails, InvoiceDetailModel>(entity);
        }

        public static tblInvoiceDetails ToEntity(this InvoiceDetailModel model)
        {
            return Mapper.Map<InvoiceDetailModel, tblInvoiceDetails>(model);
        }


        //Call Report
        public static CallReportProjectInfoModel ToModel(this tblCallReportProject entity)
        {
            return Mapper.Map<tblCallReportProject, CallReportProjectInfoModel>(entity);
        }
        public static tblCallReportProject ToEntity(this CallReportProjectInfoModel model)
        {
            return Mapper.Map<CallReportProjectInfoModel, tblCallReportProject>(model);
        }

        public static CallReportSalesCallInquiryInfoModel ToModel(this tblCallReportSalesCallInquiry entity)
        {
            return Mapper.Map<tblCallReportSalesCallInquiry, CallReportSalesCallInquiryInfoModel>(entity);
        }
        public static tblCallReportSalesCallInquiry ToEntity(this CallReportSalesCallInquiryInfoModel model)
        {
            return Mapper.Map<CallReportSalesCallInquiryInfoModel, tblCallReportSalesCallInquiry>(model);
        }

        public static SalesCallInquiryDetailViewModel ToModel(this tblCallReportSalesCallInquiryDetail entity)
        {
            return Mapper.Map<tblCallReportSalesCallInquiryDetail, SalesCallInquiryDetailViewModel>(entity);
        }
        public static tblCallReportSalesCallInquiryDetail ToEntity(this SalesCallInquiryDetailViewModel model)
        {
            return Mapper.Map<SalesCallInquiryDetailViewModel, tblCallReportSalesCallInquiryDetail>(model);
        }

        public static VisitInfoModel ToModel(this tblVisitInformation entity)
        {
            return Mapper.Map<tblVisitInformation, VisitInfoModel>(entity);
        }
        public static tblVisitInformation ToEntity(this VisitInfoModel model)
        {
            return Mapper.Map<VisitInfoModel, tblVisitInformation>(model);
        }



        //Added By Alam

        //Sales Budget
        public static SalesBudgetModel ToModel(this tblSalesBudget entity)
        {
            return Mapper.Map<tblSalesBudget, SalesBudgetModel>(entity);
        }

        public static tblSalesBudget ToEntity(this SalesBudgetModel model)
        {
            return Mapper.Map<SalesBudgetModel, tblSalesBudget>(model);
        }


        //Sales Budget Detail
        public static SalesBudgetDetail ToModel(this tblSalesBudgetDetail entity)
        {
            return Mapper.Map<tblSalesBudgetDetail, SalesBudgetDetail>(entity);
        }

        public static tblSalesBudgetDetail ToEntity(this SalesBudgetDetail model)
        {
            return Mapper.Map<SalesBudgetDetail, tblSalesBudgetDetail>(model);
        }

        //Expense Budget

        public static ExpenseBudgetModel ToModel(this tblExpenseBudget entity)
        {
            return Mapper.Map<tblExpenseBudget, ExpenseBudgetModel>(entity);
        }

        public static tblExpenseBudget ToEntity(this ExpenseBudgetModel model)
        {
            return Mapper.Map<ExpenseBudgetModel, tblExpenseBudget>(model);
        }

        //Expense Budget Detail
        public static ExpenseBudgetDetail ToModel(this tblExpenseBudgetDetail entity)
        {
            return Mapper.Map<tblExpenseBudgetDetail, ExpenseBudgetDetail>(entity);
        }

        public static tblExpenseBudgetDetail ToEntity(this ExpenseBudgetDetail model)
        {
            return Mapper.Map<ExpenseBudgetDetail, tblExpenseBudgetDetail>(model);
        }

        //Price Request Master

        public static PriceRequestModel ToModel(this tblPriceRequest entity)
        {
            return Mapper.Map<tblPriceRequest, PriceRequestModel>(entity);
        }

        public static tblPriceRequest ToEntity(this PriceRequestModel model)
        {
            return Mapper.Map<PriceRequestModel, tblPriceRequest>(model);
        }

        //price Request Detail

        public static PriceRequestDetailViewModel ToModel(this tblPriceRequestDetail entity)
        {
            return Mapper.Map<tblPriceRequestDetail, PriceRequestDetailViewModel>(entity);
        }

        public static tblPriceRequestDetail ToEntity(this PriceRequestDetailViewModel model)
        {
            return Mapper.Map<PriceRequestDetailViewModel, tblPriceRequestDetail>(model);
        }

        //End By Alam

        //Debit Note
        public static DebitNoteModel ToModel(this tblDebitNote entity)
        {
            return Mapper.Map<tblDebitNote, DebitNoteModel>(entity);
        }

        public static tblDebitNote ToEntity(this DebitNoteModel model)
        {
            return Mapper.Map<DebitNoteModel, tblDebitNote>(model);
        }

        public static DebitNoteDetailModel ToModel(this tblDebitNoteDetail entity)
        {
            return Mapper.Map<tblDebitNoteDetail, DebitNoteDetailModel>(entity);
        }

        public static tblDebitNoteDetail ToEntity(this DebitNoteDetailModel model)
        {
            return Mapper.Map<DebitNoteDetailModel, tblDebitNoteDetail>(model);
        }    
    
    }
}