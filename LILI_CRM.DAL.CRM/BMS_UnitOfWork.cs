using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LILI_CRM.DAL.CRM.CustomEntities;
using LILI_CRM.DAL.CRM;
using LILI_BMS.DAL.PCV.CustomEntities;



namespace LILI_CRM.DAL.CRM
{
    public class BMS_UnitOfWork
    {
        #region Fields

        BMS_ExecuteFunctions _functionRepository;
        
        //Security
        BMS_GenericRepository<MenuList> _MenuList;
        BMS_GenericRepository<WebUserManager> _UserInfoRepository;
        BMS_GenericRepository<BMS_tblUserWiseMenuAssign> _BMS_tblUserWiseMenuAssignRepository;
        BMS_GenericRepository<UserMenuSearch> _UserMenuSearch;
        BMS_GenericRepository<BMS_tblMenuList> _AllMenuRepository;
        BMS_GenericRepository<WebVoucherMaster> _WebVoucherMasterRepository;
        BMS_GenericRepository<WebVoucherDetails> _WebVoucherDetailsRepository;

        BMS_GenericRepository<viewWebACMaster> _viewWebACMasterRepository;

        // Organization
        BMS_GenericRepository<tblOrganization> _OrganizationRepository;

        // Supplier
        BMS_GenericRepository<tblSupplier> _SupplierRepository;

        //Customer
        BMS_GenericRepository<tblCustomer> _CustomerRepository;

        //Article
        BMS_GenericRepository<tblProduct> _ProductRepository;
        
        //Transporter
        BMS_GenericRepository<tblTransporter> _TransporterRepository;

        //Division
        BMS_GenericRepository<tblDivision> _DivisionRepository;

        //Currency
        BMS_GenericRepository<tblCurrency> _CurrencyRepository;

        //Price Terms
        BMS_GenericRepository<tblPriceTerm> _PriceTermRepository;

        //Commission Term/Type
        BMS_GenericRepository<tblCommCalculationType> _CommCalculationTypeRepository;

        //Country
        BMS_GenericRepository<tblCountry> _CountryRepository;

        //Port
        BMS_GenericRepository<tblPort> _PortRepository;

        //SBU
        BMS_GenericRepository<tblSBU> _SBURepository;
        
        //Sample Request
        BMS_GenericRepository<tblSampleRequest> _SampleRequestRepository;
        BMS_GenericRepository<tblSampleRequestDetails> _SampleRequestDetailsRepository;

        //Sample Submission
        BMS_GenericRepository<tblSampleSubmission> _SampleSubmissionRepository;
        BMS_GenericRepository<tblSampleSubmissionDet> _SampleSubmissionDetRepository;
        //Sample Submission Document
        BMS_GenericRepository<tblSampleSubmissionDocument> _SampleSubmissionDocumentRepository;

        //Sample Quote
        BMS_GenericRepository<tblQuote> _QuoteRepository;
        BMS_GenericRepository<tblQuoteDetails> _QuoteDetailsRepository;
        //Quote Document
        BMS_GenericRepository<tblQuoteDocument> _QuoteDocumentRepository;

        //Invoice & Invoice Details
        BMS_GenericRepository<tblInvoice> _InvoiceRepository;
        BMS_GenericRepository<tblInvoiceDetails> _InvoiceDetailsRepository;

       //Get Invoice Related Custom Data
        BMS_GenericRepository<TransactionRefCustomEntity> _GetNewTransactionId;

        //Sample Document 
        BMS_GenericRepository<tblSampleDocumentRequired> _SampleDocumentRequiredRepository;

        //Sample Request Document
        BMS_GenericRepository<tblSampleRequestDocument> _SampleRequestDocumentRepository;

        //DebitNote & DebitNote Details
        BMS_GenericRepository<tblDebitNote> _DebitNoteRepository;
        BMS_GenericRepository<tblDebitNoteDetail> _DebitNoteDetailRepository;


        //Call Report 
        BMS_GenericRepository<tblCallReportProject> _CallReportProjectRepository;
        BMS_GenericRepository<tblCurrentStage> _CurrentStageRepository;
        BMS_GenericRepository<tblCommunicationChannel> _CommunicationChannelRepository;
        BMS_GenericRepository<tblCallReportSalesCallInquiry> _CallReportSalesCallInquiryRepository;
        BMS_GenericRepository<tblCallReportSalesCallInquiryDetail> _CallReportSalesCallInquiryDetailRepository;

        //Visit Information
        BMS_GenericRepository<tblVisitInformation> _VisitInformationRepository;


        //Added By Alam

        //SUB 
        //BMS_GenericRepository<tblSBU> _SUBRepository;
        //Sales Person
        BMS_GenericRepository<tblSalesPerson> _SalesPersonRepository;
        //Sales Budget
        BMS_GenericRepository<tblSalesBudget> _SalesBudgetRepository;
        //Sales Budget detail
        BMS_GenericRepository<tblSalesBudgetDetail> _SalesBudgetDetailRepository;
        //Expense Budget
        BMS_GenericRepository<tblExpenseBudget> _ExpenseBudgetRepository;
        //Expense Budget Detail
        BMS_GenericRepository<tblExpenseBudgetDetail> _ExpenseBudgetDetailRepository;
        //Particular 
        BMS_GenericRepository<tblParticular> _ParticularRepository;
        //Country
        //BMS_GenericRepository<tblCountry> _CountryRepository;
        //Priceterm
        //BMS_GenericRepository<tblPriceTerm> _PriceTermRepository;
        //PriceRequest
        BMS_GenericRepository<tblPriceRequest> _PriceRequestRepository;
        //PriceRequestDeail
        BMS_GenericRepository<tblPriceRequestDetail> _PriceRequestDetailRepository;

        //userinfoforsales
        BMS_GenericRepository<BMS_tblUserInfo> _UserInfoNameRepository;


        //New Add By Alam
        BMS_GenericRepository<tblSupplierContact> _SupplierContactRepository;

        

        //End


        //Bank
        BMS_GenericRepository<tblBank> _BankRepository;
        #endregion

        #region Constructor

        public BMS_UnitOfWork(
            
            BMS_ExecuteFunctions functionRepository,

            //Security
            BMS_GenericRepository<MenuList> MenuList,
            BMS_GenericRepository<WebUserManager> UserInfoRepository,
            BMS_GenericRepository<BMS_tblUserWiseMenuAssign> BMS_tblUserWiseMenuAssignRepository,
            BMS_GenericRepository<UserMenuSearch> UserMenuSearch,
            BMS_GenericRepository<BMS_tblMenuList> AllMenuRepository,
            BMS_GenericRepository<WebVoucherMaster> WebVoucherMasterRepository,
            BMS_GenericRepository<WebVoucherDetails> WebVoucherDetailsRepository,
            BMS_GenericRepository<viewWebACMaster> viewWebACMasterRepository,
            BMS_GenericRepository<tblOrganization> OrganizationRepository,
            BMS_GenericRepository<tblSupplier> SupplierRepository,
            BMS_GenericRepository<tblCustomer> CustomerRepository,
            BMS_GenericRepository<tblProduct> ProductRepository,
            BMS_GenericRepository<tblTransporter> TransporterRepository,
            BMS_GenericRepository<tblDivision> DivisionRepository,
            BMS_GenericRepository<tblCurrency> CurrencyRepository,
            BMS_GenericRepository<tblPriceTerm> PriceTermRepository,
            BMS_GenericRepository<tblCommCalculationType> CommCalculationTypeRepository,
            BMS_GenericRepository<tblCountry> CountryRepository,
            BMS_GenericRepository<tblPort> PortRepository,
            BMS_GenericRepository<tblSBU> SBURepository,
            BMS_GenericRepository<tblSampleRequest> SampleRequestRepository,
            BMS_GenericRepository<tblSampleRequestDetails> SampleRequestDetailsRepository,
            BMS_GenericRepository<tblSampleSubmission> SampleSubmissionRepository,
            BMS_GenericRepository<tblSampleSubmissionDet> SampleSubmissionDetRepository,
            BMS_GenericRepository<tblSampleSubmissionDocument> SampleSubmissionDocumentRepository,
            BMS_GenericRepository<tblQuote> QuoteRepository,
            BMS_GenericRepository<tblQuoteDetails> QuoteDetailsRepository,
            BMS_GenericRepository<tblQuoteDocument> QuoteDocumentRepository,

            BMS_GenericRepository<tblInvoice> InvoiceRepository, 
            BMS_GenericRepository<tblInvoiceDetails> InvoiceDetailsRepository,

            BMS_GenericRepository<TransactionRefCustomEntity> GetNewTransactionId,
            BMS_GenericRepository<tblSampleDocumentRequired> SampleDocumentRequiredRepository,
            BMS_GenericRepository<tblSampleRequestDocument> SampleRequestDocumentRepository,

            BMS_GenericRepository<tblDebitNote> DebitNoteRepository,
            BMS_GenericRepository<tblDebitNoteDetail> DebitNoteDetaisRepository,


            BMS_GenericRepository<tblCallReportProject> CallReportProjectRepository,
            BMS_GenericRepository<tblCurrentStage> CurrentStageRepository,
            BMS_GenericRepository<tblCommunicationChannel> CommunicationChannelRepository,
            BMS_GenericRepository<tblCallReportSalesCallInquiry> CallReportSalesCallInquiryRepository,
            BMS_GenericRepository<tblCallReportSalesCallInquiryDetail> CallReportSalesCallInquiryDetailRepository,
            BMS_GenericRepository<tblVisitInformation> VisitInformationRepository,


            //Added By Alam             
            //BMS_GenericRepository<tblSBU> SBURepository,
            BMS_GenericRepository<tblSalesPerson> SalesPersonRepository,
            BMS_GenericRepository<tblSalesBudget> SalesBudgetRepository,
            BMS_GenericRepository<tblSalesBudgetDetail> SalesBudgetDetailRepository,
            BMS_GenericRepository<tblExpenseBudget> ExpenseBudgetRepository,
            BMS_GenericRepository<tblExpenseBudgetDetail> ExpenseBudgetDetailRepository,
            BMS_GenericRepository<tblParticular> ParticularRepository,
            //BMS_GenericRepository<tblCountry> CountryRepository,
            //BMS_GenericRepository<tblPriceTerm> PriceTermRepository,
            BMS_GenericRepository<tblPriceRequest> PriceRequestRepository,
            BMS_GenericRepository<tblPriceRequestDetail> PriceRequestDetailRepository,
            BMS_GenericRepository<BMS_tblUserInfo> UserInfoNameRepository,

            //new add by Alam
            BMS_GenericRepository<tblSupplierContact> SupplierContactRepository,
            //End

            BMS_GenericRepository<tblBank> BankRepository
            
            )
        {
          
            this._functionRepository = functionRepository;

            //Security
            this._MenuList = MenuList;
            this._UserInfoRepository = UserInfoRepository;
            this._BMS_tblUserWiseMenuAssignRepository = BMS_tblUserWiseMenuAssignRepository;
            this._UserMenuSearch = UserMenuSearch;
            this._AllMenuRepository = AllMenuRepository;
            this._WebVoucherMasterRepository = WebVoucherMasterRepository;
            this._WebVoucherDetailsRepository = WebVoucherDetailsRepository;
            this._viewWebACMasterRepository = viewWebACMasterRepository;

            //Organization Info
            this._OrganizationRepository = OrganizationRepository;

            //Supplier Info
            this._SupplierRepository = SupplierRepository;

            //Customer Info
            this._CustomerRepository = CustomerRepository;

            //Article - Category - Sub Category
            this._ProductRepository = ProductRepository;

            //Transporter
            this._TransporterRepository = TransporterRepository;

            //Division Info
            this._DivisionRepository = DivisionRepository;

            //Currency
            this._CurrencyRepository=CurrencyRepository;

            //Price Terms
            this._PriceTermRepository = PriceTermRepository;

            //Commission Term/Type
            this._CommCalculationTypeRepository = CommCalculationTypeRepository;

            //Country
            this._CountryRepository=CountryRepository;

            //Port
            this._PortRepository=PortRepository;

            //SBU
            this._SBURepository = SBURepository;

            this._SampleRequestRepository = SampleRequestRepository;
            this._SampleRequestDetailsRepository= SampleRequestDetailsRepository;

            this._SampleSubmissionRepository= SampleSubmissionRepository;
            this._SampleSubmissionDetRepository= SampleSubmissionDetRepository;
            this._SampleSubmissionDocumentRepository = SampleSubmissionDocumentRepository;

            this._QuoteRepository = QuoteRepository;
            this._QuoteDetailsRepository = QuoteDetailsRepository;
            this._QuoteDocumentRepository = QuoteDocumentRepository;

            this._InvoiceRepository = InvoiceRepository; 
            this._InvoiceDetailsRepository = InvoiceDetailsRepository;

            //Get Invoice Related Custom Data
            this._GetNewTransactionId = GetNewTransactionId;
            this._SampleDocumentRequiredRepository = SampleDocumentRequiredRepository;
            this._SampleRequestDocumentRepository = SampleRequestDocumentRepository;

            this._DebitNoteRepository = DebitNoteRepository;
            this._DebitNoteDetailRepository = DebitNoteDetaisRepository;

            //Call Report
            this._CallReportProjectRepository = CallReportProjectRepository;
            this._CurrentStageRepository = CurrentStageRepository;
            this._CommunicationChannelRepository = CommunicationChannelRepository;
            this._CallReportSalesCallInquiryRepository = CallReportSalesCallInquiryRepository;
            this._CallReportSalesCallInquiryDetailRepository = CallReportSalesCallInquiryDetailRepository;

            //Visit Information
            this._VisitInformationRepository = VisitInformationRepository;



            //Added By Alam

            //this._SUBRepository = SBURepository;
            this._SalesPersonRepository = SalesPersonRepository;
            this._SalesBudgetRepository = SalesBudgetRepository;
            this._SalesBudgetDetailRepository = SalesBudgetDetailRepository;
            this._ExpenseBudgetRepository = ExpenseBudgetRepository;
            this._ExpenseBudgetDetailRepository = ExpenseBudgetDetailRepository;
            this._ParticularRepository = ParticularRepository;
            //this._CountryRepository = CountryRepository;
            //this._PriceTermRepository = PriceTermRepository;
            this._PriceRequestRepository = PriceRequestRepository;
            this._PriceRequestDetailRepository = PriceRequestDetailRepository;
            this._UserInfoNameRepository = UserInfoNameRepository;
            
            //New Added By alam 
            this._SupplierContactRepository = SupplierContactRepository;

            //end ALam

            //Bank
            this._BankRepository = BankRepository;
        }

        public BMS_UnitOfWork()
        {
            // TODO: Complete member initialization
        }

        #endregion

        #region Properties

        public BMS_ExecuteFunctions FunctionRepository
        {
            get
            {
                return _functionRepository;
            }
        }
        
        //Security
        public BMS_GenericRepository<MenuList> MenuList
        {
            get { return _MenuList; }
        }
        public BMS_GenericRepository<WebUserManager> UserInfoRepository
        {
            get { return _UserInfoRepository; }
        }
        public BMS_GenericRepository<BMS_tblUserWiseMenuAssign> BMS_tblUserWiseMenuRepository
        {
            get { return _BMS_tblUserWiseMenuAssignRepository; }
        }
        public BMS_GenericRepository<UserMenuSearch> UserMenuSearch
        {
            get { return _UserMenuSearch; }
        }
        public BMS_GenericRepository<BMS_tblMenuList> AllMenuRepository
        {
            get { return _AllMenuRepository; }
        }
        public BMS_GenericRepository<WebVoucherMaster> WebVoucherMasterRepository
        {
            get { return _WebVoucherMasterRepository; }
        }
        public BMS_GenericRepository<WebVoucherDetails> WebVoucherDetailsRepository
        {
            get { return _WebVoucherDetailsRepository; }
        }

        public BMS_GenericRepository<viewWebACMaster> viewWebACMasterRepository
        {
            get { return _viewWebACMasterRepository; }
        }

        public BMS_GenericRepository<tblOrganization> OrganizationRepository
        {
            get { return _OrganizationRepository; }
        }

        public BMS_GenericRepository<tblSupplier> SupplierRepository
        {
            get { return _SupplierRepository; }
        }

        public BMS_GenericRepository<tblProduct> ProductRepository
        {
            get { return _ProductRepository; }
        }

        public BMS_GenericRepository<tblCustomer> CustomerRepository
        {
            get { return _CustomerRepository; }
        }

        public BMS_GenericRepository<tblTransporter> TransporterRepository
        {
            get { return _TransporterRepository; }
        }

        //Division

        public BMS_GenericRepository<tblDivision> DivisionRepository
        {
            get { return _DivisionRepository; }
        }
        
        //Currency
        public BMS_GenericRepository<tblCurrency> CurrencyRepository
        {
            get { return _CurrencyRepository; }
        }
        //Price Terms
        public BMS_GenericRepository<tblPriceTerm> PriceTermRepository
        {
            get {return _PriceTermRepository;}
        }

        //Commission Term/Type
        public BMS_GenericRepository<tblCommCalculationType> CommCalculationTypeRepository
        {
            get { return _CommCalculationTypeRepository; }
        }
        //Country
        public BMS_GenericRepository<tblCountry> CountryRepository
        {
            get { return _CountryRepository; }
        }

        //Port
        public BMS_GenericRepository<tblPort> PortRepository
        {
            get { return _PortRepository; }
        }

        //SBU
        public BMS_GenericRepository<tblSBU> SBURepository
        {
            get { return _SBURepository; }
        }

        public BMS_GenericRepository<tblSampleRequest> SampleRequestRepository
        {
            get { return _SampleRequestRepository; }
        }
        public BMS_GenericRepository<tblSampleRequestDetails> SampleRequestDetailsRepository
        {
            get { return _SampleRequestDetailsRepository; }
        }
        public BMS_GenericRepository<tblSampleSubmission> SampleSubmissionRepository
        {
            get { return _SampleSubmissionRepository; }
        }
        public BMS_GenericRepository<tblSampleSubmissionDet> SampleSubmissionDetRepository
        {
            get { return _SampleSubmissionDetRepository; }
        }
        public BMS_GenericRepository<tblSampleSubmissionDocument> SampleSubmissionDocumentRepository
        {
            get { return _SampleSubmissionDocumentRepository; }
        }

        public BMS_GenericRepository<tblQuote> QuoteRepository
        {
            get { return _QuoteRepository; }
        }
        public BMS_GenericRepository<tblQuoteDetails> QuoteDetailsRepository
        {
            get { return _QuoteDetailsRepository; }
        }
        public BMS_GenericRepository<tblQuoteDocument> QuoteDocumentRepository
        {
            get { return _QuoteDocumentRepository; }
        }

        public BMS_GenericRepository<tblInvoice> InvoiceRepository
        {
            get { return _InvoiceRepository; }
        }
        public BMS_GenericRepository<tblInvoiceDetails> InvoiceDetailsRepository
        {
            get { return _InvoiceDetailsRepository; }
        }

        public BMS_GenericRepository<TransactionRefCustomEntity> GetNewTransactionId
        {
            get { return _GetNewTransactionId; }
        }
        public BMS_GenericRepository<tblSampleDocumentRequired> SampleDocumentRequiredRepository
        {
            get { return _SampleDocumentRequiredRepository; }
        }
        public BMS_GenericRepository<tblSampleRequestDocument> SampleRequestDocumentRepository
        {
            get { return _SampleRequestDocumentRepository; }
        }

        public BMS_GenericRepository<tblDebitNote> DebitNoteRepository
        {
            get { return _DebitNoteRepository; }
        }
        public BMS_GenericRepository<tblDebitNoteDetail> DebitNoteDetailRepository
        {
            get { return _DebitNoteDetailRepository; }
        }




        public BMS_GenericRepository<tblCallReportProject> CallReportProjectRepository
        {
            get { return _CallReportProjectRepository; }
        }

        public BMS_GenericRepository<tblCurrentStage> CurrentStageRepository
        {
            get { return _CurrentStageRepository; }
        }

        public BMS_GenericRepository<tblCommunicationChannel> CommunicationChannelRepository
        {
            get { return _CommunicationChannelRepository; }
        }

        public BMS_GenericRepository<tblCallReportSalesCallInquiry> CallReportSalesCallInquiryRepository
        {
            get { return _CallReportSalesCallInquiryRepository; }
        }

        public BMS_GenericRepository<tblCallReportSalesCallInquiryDetail> CallReportSalesCallInquiryDetailRepository
        {
            get { return _CallReportSalesCallInquiryDetailRepository; }
        }

        public BMS_GenericRepository<tblVisitInformation> VisitInformationRepository
        {
            get { return _VisitInformationRepository; }
        }




        //Added By Alam
        //public BMS_GenericRepository<tblSBU> SBURepository
        //{
        //    get { return _SUBRepository; }
        //}

        public BMS_GenericRepository<tblSalesPerson> SalesPersonRepository
        {
            get { return _SalesPersonRepository; }
        }

        public BMS_GenericRepository<tblSalesBudget> SalesBudgetRepository
        {
            get { return _SalesBudgetRepository; }
        }

        public BMS_GenericRepository<tblSalesBudgetDetail> SalesBudgetDetailRepository
        {
            get { return _SalesBudgetDetailRepository; }
        }

        public BMS_GenericRepository<tblExpenseBudget> ExpenseBudgetRepository
        {
            get { return _ExpenseBudgetRepository; }
        }

        public BMS_GenericRepository<tblExpenseBudgetDetail> ExpenseBudgetDetailRepository
        {
            get { return _ExpenseBudgetDetailRepository; }

        }

        public BMS_GenericRepository<tblParticular> ParticularRepository
        {
            get { return _ParticularRepository; }
        }

        //public BMS_GenericRepository<tblCountry> CountryRepository
        //{
        //    get { return _CountryRepository; }
        //}

        //public BMS_GenericRepository<tblPriceTerm> PriceTermRepository
        //{
        //    get { return _PriceTermRepository; }
        //}

        public BMS_GenericRepository<tblPriceRequest> PriceRequestRepository
        {
            get { return _PriceRequestRepository; }
        }
        public BMS_GenericRepository<tblPriceRequestDetail> PriceRequestDetailRepository
        {
            get { return _PriceRequestDetailRepository; }
        }

        public BMS_GenericRepository<BMS_tblUserInfo> UserInfoNameRepository
        {
            get { return _UserInfoNameRepository; }
        }

        public BMS_GenericRepository<tblSupplierContact> SupplierContactRepository
         {
             get { return _SupplierContactRepository; }
         }
        


        //end Alam

         //Bank
         public BMS_GenericRepository<tblBank> BankRepository
         {
             get { return _BankRepository; }
         }

        #endregion
    }
}