using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using LILI_BMS.DAL.PCV;
using LILI_CRM.Web.Models;
using LILI_CRM.DAL.CRM;
using LILI_CRM.Web.Areas.PCV.ViewModel;


namespace LILI_CRM.Web.Utility
{
    public class BMSMapper
    {
        public BMSMapper()
        {

            //User And Menu Assign
            Mapper.CreateMap<UserMenuModel, WebUserManager>();
            Mapper.CreateMap<WebUserManager, UserMenuModel>();
            Mapper.CreateMap<UserMenuDetail, BMS_tblUserWiseMenuAssign>();
            Mapper.CreateMap<BMS_tblUserWiseMenuAssign, UserMenuDetail>();


            //User Manager
            Mapper.CreateMap<ChangePassViewModel, WebUserManager>();
            Mapper.CreateMap<WebUserManager, ChangePassViewModel>();

            //Voucher Master
            Mapper.CreateMap<VoucherViewModel, WebVoucherMaster>();
            Mapper.CreateMap<WebVoucherMaster, VoucherViewModel>();

            //Voucher Detail
            Mapper.CreateMap<VoucherDetailEntityModel, WebVoucherDetails>();
            Mapper.CreateMap<WebVoucherDetails, VoucherDetailEntityModel>();

            //Supplier Information
            Mapper.CreateMap<SupplierInfoModel, tblSupplier>();
            Mapper.CreateMap<tblSupplier, SupplierInfoModel>();


            //Customer Information
            Mapper.CreateMap<CustomerInfoModel, tblCustomer>();
            Mapper.CreateMap<tblCustomer, CustomerInfoModel>();

            //Article Master
            Mapper.CreateMap<ProductModel, tblProduct>();
            Mapper.CreateMap<tblProduct, ProductModel>();

            //Division Information
            Mapper.CreateMap<DivisionModel, tblDivision>();
            Mapper.CreateMap<tblDivision, DivisionModel>();

            //SBU Information
            Mapper.CreateMap<SBUModel, tblSBU>();
            Mapper.CreateMap<tblSBU, SBUModel>();

            //Bank Information
            //Mapper.CreateMap<SBUModel, tblSBU>();
            //Mapper.CreateMap<tblSBU, SBUModel>();

            //Sample Request
            Mapper.CreateMap<SampleRequestModel, tblSampleRequest>();
            Mapper.CreateMap<tblSampleRequest, SampleRequestModel>();
            
            Mapper.CreateMap<SampleRequestDetailModel, tblSampleRequestDetails>();
            Mapper.CreateMap<tblSampleRequestDetails, SampleRequestDetailModel>();

            Mapper.CreateMap<SampleRequestDocumentModel, tblSampleRequestDocument>();
            Mapper.CreateMap<tblSampleRequestDocument, SampleRequestDocumentModel>();

            //Sample Submission
            Mapper.CreateMap<SampleSubmissionModel, tblSampleSubmission>();
            Mapper.CreateMap<tblSampleSubmission, SampleSubmissionModel>();

            Mapper.CreateMap<SampleSubmissionDetailModel, tblSampleSubmissionDet>();
            Mapper.CreateMap<tblSampleSubmissionDet, SampleSubmissionDetailModel>();

            Mapper.CreateMap<SampleSubmissionDocumentModel, tblSampleSubmissionDocument>();
            Mapper.CreateMap<tblSampleSubmissionDocument, SampleSubmissionDocumentModel>();


            //Quote
            Mapper.CreateMap<QuoteModel, tblQuote>();
            Mapper.CreateMap<tblQuote, QuoteModel>();

            Mapper.CreateMap<QuoteDetailModel, tblQuoteDetails>();
            Mapper.CreateMap<tblQuoteDetails, QuoteDetailModel>();

            Mapper.CreateMap<QuoteDocumentModel, tblQuoteDocument>();
            Mapper.CreateMap<tblQuoteDocument, QuoteDocumentModel>();

            
            //Invoice
            Mapper.CreateMap<InvoiceModel, tblInvoice>();
            Mapper.CreateMap<tblInvoice, InvoiceModel>();
            Mapper.CreateMap<InvoiceDetailModel, tblInvoiceDetails>();
            Mapper.CreateMap<tblInvoiceDetails, InvoiceDetailModel>();

            //Call Report
            Mapper.CreateMap<tblCallReportProject, CallReportProjectInfoModel>();
            Mapper.CreateMap<CallReportProjectInfoModel, tblCallReportProject>();

            Mapper.CreateMap<tblCallReportSalesCallInquiry, CallReportSalesCallInquiryInfoModel>();
            Mapper.CreateMap<CallReportSalesCallInquiryInfoModel, tblCallReportSalesCallInquiry>();

            Mapper.CreateMap<tblCallReportSalesCallInquiryDetail, SalesCallInquiryDetailViewModel>();
            Mapper.CreateMap<SalesCallInquiryDetailViewModel, tblCallReportSalesCallInquiryDetail>();

            //Visit Information
            Mapper.CreateMap<tblVisitInformation, VisitInfoModel>();
            Mapper.CreateMap<VisitInfoModel, tblVisitInformation>();



            //Added By Alam

            //SalesBudgetMaster Information
            Mapper.CreateMap<SalesBudgetModel, tblSalesBudget>();
            Mapper.CreateMap<tblSalesBudget, SalesBudgetModel>();

            //SalesBudgetDetail Information
            Mapper.CreateMap<SalesBudgetDetail, tblSalesBudgetDetail>();
            Mapper.CreateMap<tblSalesBudgetDetail, SalesBudgetDetail>();

            //ExpenseBudgetMaster Info
            Mapper.CreateMap<ExpenseBudgetModel, tblExpenseBudget>();
            Mapper.CreateMap<tblExpenseBudget, ExpenseBudgetModel>();

            //ExpenseBudgetDetail
            Mapper.CreateMap<ExpenseBudgetDetail, tblExpenseBudgetDetail>();
            Mapper.CreateMap<tblExpenseBudgetDetail, ExpenseBudgetDetail>();

            //Price Request Master
            Mapper.CreateMap<PriceRequestModel, tblPriceRequest>();
            Mapper.CreateMap<tblPriceRequest, PriceRequestModel>();

            //Price Request Detail
            Mapper.CreateMap<PriceRequestDetailViewModel, tblPriceRequestDetail>();
            Mapper.CreateMap<tblPriceRequestDetail, PriceRequestDetailViewModel>();

            //End Alam

            //Debit Note
            Mapper.CreateMap<DebitNoteModel, tblDebitNote>();
            Mapper.CreateMap<tblDebitNote, DebitNoteModel>();
            Mapper.CreateMap<DebitNoteDetailModel, tblDebitNoteDetail>();
            Mapper.CreateMap<tblDebitNoteDetail, DebitNoteDetailModel>();


        }
    }
}