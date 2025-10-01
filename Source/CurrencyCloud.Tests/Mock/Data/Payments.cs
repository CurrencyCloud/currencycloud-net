using System;
using System.Collections.Generic;
using CurrencyCloud.Entity;

namespace CurrencyCloud.Tests.Mock.Data
{
    static class Payments
    {
        public static readonly Entity.Payment Payment1 = new Entity.Payment(
            "EUR",
            "To be filled later",
            10000,
            "Salary for March",
            "INVOICE 9876"
            )
        {
            PaymentType = "regular"
        };
        public static readonly Entity.Payer Payer1 = new Entity.Payer
        {
            LegalEntityType = "individual",
            CompanyName = "Some Company LLC",
            FirstName = "John",
            LastName = "Doe",
            City = "London",
            Address = "27 Colmore Row",
            Postcode = "W11 2BQ",
            StateOrProvince = "TX",
            Country = "GB",
            DateOfBirth = new DateTime(1980, 10, 10),
            IdentificationType = "none"
        };

        public static readonly Entity.Payment Payment2 = new Entity.Payment(
            "EUR",
            "To be filled later",
            0.23m,
            "Prepayment of salary for April",
            "INVOICE 122"
            )
        {
            PaymentType = "regular"
        };

        public static readonly Entity.Payment ScaPayment = new Entity.Payment(
            "GBP",
            "c3dafe79-9394-4f43-a1a3-b7a518ab1cba",
            1000,
            "SCA Payment",
            "SCA Payment"
            )
        {
            PaymentType = "regular"
        };

        public static readonly Entity.Payer Payer2 = new Entity.Payer
        {
            LegalEntityType = "individual",
            CompanyName = "Jens enskild firma",
            FirstName = "Jennifer",
            LastName = "Waylon",
            City = "Stockholm",
            Address = "22 Garvargatan",
            Postcode = "12332",
            StateOrProvince = "Stockholm",
            Country = "SE",
            DateOfBirth = new DateTime(1981, 12, 10),
            IdentificationType = "none"
        };

        public static readonly Entity.PaymentSubmissionInfo SubmissionInfo1 = new Entity.PaymentSubmissionInfo
        {
            Status = "pending",
            Message = "{1:F01TCCLGB20AXXX0090000004}{2:I103BARCGB22XXXXN}{4: :20:20180101-ZSYWVY :23B:CRED :32A:160617GBP3000,0 :33B:GBP3000,0 :50K:/150618-00026 PCOMAPNY address New-York Province 555222 GB :53B:/20060513071472 :57C://SC200605 :59:/200605000 First Name Last Name e03036bf6c325dd12c58 London GB :70:test reference Test reason Payment group: 0160617-ZSYWVY :71A:SHA -}",
            SubmissionRef = "MXGGYAGJULIIQKDV",
            Format = "MT103"
        };

        public static readonly Entity.PaymentSubmissionInfo SubmissionInfo2 = new Entity.PaymentSubmissionInfo
        {
            Status = "pending",
            Message = "<?xml version=\\\"1.0\\\" encoding=\\\"UTF-8\\\" standalone=\\\"yes\\\"?> <Envelope xmlns:ns2=\\\"urn:iso:std:iso:20022:tech:xsd:pacs.008.001.08\\\" xmlns:ns3=\\\"urn:iso:std:iso:20022:tech:xsd:head.001.001.02\\\"> <ns3:AppHdr> <ns3:Fr> <ns3:FIId> <ns3:FinInstnId> <ns3:BICFI>TCCLGB22</ns3:BICFI> </ns3:FinInstnId> </ns3:FIId> </ns3:Fr> <ns3:To> <ns3:FIId> <ns3:FinInstnId> <ns3:BICFI>BARCGB22</ns3:BICFI> </ns3:FinInstnId> </ns3:FIId> </ns3:To> <ns3:BizMsgIdr>250327-TNJKSX349</ns3:BizMsgIdr> <ns3:MsgDefIdr>pacs.008.001.08</ns3:MsgDefIdr> <ns3:BizSvc>swift.cbprplus.02</ns3:BizSvc> <ns3:CreDt>2025-03-27T11:41:53.171+00:00</ns3:CreDt> </ns3:AppHdr> <ns2:Document> <ns2:FIToFICstmrCdtTrf> <ns2:GrpHdr> <ns2:MsgId>250327-TNJKSX349</ns2:MsgId> <ns2:CreDtTm>2025-03-27T11:41:53.172+00:00</ns2:CreDtTm> <ns2:NbOfTxs>1</ns2:NbOfTxs> <ns2:SttlmInf> <ns2:SttlmMtd>INDA</ns2:SttlmMtd> <ns2:SttlmAcct> <ns2:Id> <ns2:IBAN>AE460090000000123456789</ns2:IBAN> </ns2:Id> </ns2:SttlmAcct> </ns2:SttlmInf> </ns2:GrpHdr> <ns2:CdtTrfTxInf> <ns2:PmtId> <ns2:InstrId>250327-TNJKSX349</ns2:InstrId> <ns2:EndToEndId>250327-TNJKSX349</ns2:EndToEndId> <ns2:UETR>9d62ea45-4503-47a4-8206-5757b219d899</ns2:UETR> </ns2:PmtId> <ns2:IntrBkSttlmAmt Ccy=\\\"AED\\\">1.01</ns2:IntrBkSttlmAmt> <ns2:IntrBkSttlmDt>2025-03-28</ns2:IntrBkSttlmDt> <ns2:InstdAmt Ccy=\\\"AED\\\">1.01</ns2:InstdAmt> <ns2:ChrgBr>SHAR</ns2:ChrgBr> <ns2:PrvsInstgAgt1> <ns2:FinInstnId> <ns2:BICFI>TCCLGB22</ns2:BICFI> </ns2:FinInstnId> </ns2:PrvsInstgAgt1> <ns2:InstgAgt> <ns2:FinInstnId> <ns2:BICFI>TCCLGB22</ns2:BICFI> </ns2:FinInstnId> </ns2:InstgAgt> <ns2:InstdAgt> <ns2:FinInstnId> <ns2:BICFI>BARCGB22</ns2:BICFI> </ns2:FinInstnId> </ns2:InstdAgt> <ns2:IntrmyAgt1> <ns2:FinInstnId> <ns2:BICFI>BARCGB22</ns2:BICFI> </ns2:FinInstnId> </ns2:IntrmyAgt1> <ns2:IntrmyAgt1Acct> <ns2:Id> <ns2:Othr> <ns2:Id>20060569048211</ns2:Id> </ns2:Othr> </ns2:Id> </ns2:IntrmyAgt1Acct> <ns2:Dbtr> <ns2:Nm>Timmy Mortimer</ns2:Nm> <ns2:PstlAdr> <ns2:AdrLine>164 Bishopsgate,London,london,EC2A </ns2:AdrLine> <ns2:AdrLine>4LX,GB</ns2:AdrLine> </ns2:PstlAdr> </ns2:Dbtr> <ns2:DbtrAcct> <ns2:Id> <ns2:Othr> <ns2:Id>4ff29c49e0bd49ba86e0b80dcdd91211</ns2:Id> </ns2:Othr> </ns2:Id> </ns2:DbtrAcct> <ns2:DbtrAgt> <ns2:FinInstnId> <ns2:Nm>Account-KPUGUFRTTNJH</ns2:Nm> <ns2:PstlAdr> <ns2:AdrLine>Street,London,EC2M 4LX,GB</ns2:AdrLine> </ns2:PstlAdr> </ns2:FinInstnId> </ns2:DbtrAgt> <ns2:CdtrAgt> <ns2:FinInstnId> <ns2:BICFI>EBILAEADJAZ</ns2:BICFI> </ns2:FinInstnId> </ns2:CdtrAgt> <ns2:Cdtr> <ns2:Nm>AED20TO20AE</ns2:Nm> <ns2:PstlAdr> <ns2:AdrLine>ETOE20Address,ETOE20City,ETOE20stat</ns2:AdrLine> <ns2:AdrLine>e,E1206FQ,AE</ns2:AdrLine> </ns2:PstlAdr> </ns2:Cdtr> <ns2:CdtrAcct> <ns2:Id> <ns2:IBAN>AE460090000000123456789</ns2:IBAN> </ns2:Id> </ns2:CdtrAcct> <ns2:InstrForCdtrAgt> <ns2:InstrInf>/BENEFRES/AE//BON</ns2:InstrInf> </ns2:InstrForCdtrAgt> <ns2:RgltryRptg> <ns2:Dtls> <ns2:Tp>BENEFRES</ns2:Tp> <ns2:Ctry>AE</ns2:Ctry> <ns2:Cd>BON</ns2:Cd> </ns2:Dtls> </ns2:RgltryRptg> <ns2:RmtInf> <ns2:Ustrd>PAYMENT ref 1430710590781228003 PAYMENT reason 6049854388559588538</ns2:Ustrd> </ns2:RmtInf> </ns2:CdtTrfTxInf> </ns2:FIToFICstmrCdtTrf> </ns2:Document> </Envelope>\"}",
            SubmissionRef = "MXGGYAGJULIIQKDV",
            Format = "PACS008"
        };

        public static readonly Entity.List.PaymentAuthorisationsList.Authorisation Authorisation1 =
            new Entity.List.PaymentAuthorisationsList.Authorisation
            {
                PaymentId = "8e3aeeb8-deeb-4665-96de-54b880a953ac",
                PaymentStatus = "authorised",
                Updated = false,
                Error = "",
                AuthStepsTaken = 1,
                AuthStepsRequired = 1,
                ShortReference = "180802-YKGDVV001"
            };

        public static readonly Entity.List.PaymentAuthorisationsList.Authorisation Authorisation2 =
            new Entity.List.PaymentAuthorisationsList.Authorisation
        {
            PaymentId = "f16cafe4-1f8f-472e-99d9-8c828918d4f8",
            PaymentStatus = "authorised",
            Updated = true,
            Error = "",
            AuthStepsTaken = 1,
            AuthStepsRequired = 0,
            ShortReference = "180802-BXXTBP001"
        };

        public static readonly Entity.List.PaymentAuthorisationsList.Authorisation Authorisation3 =
            new Entity.List.PaymentAuthorisationsList.Authorisation
        {
            PaymentId = "d025f90f-a23c-46f9-979a-35a9f98d9491",
            PaymentStatus = "authorised",
            Updated = false,
            Error = "",
            AuthStepsTaken = 1,
            AuthStepsRequired = 1,
            ShortReference = "180802-ZVTTLF001"
        };

        public static readonly Entity.PaymentConfirmation Confirmation1 = new Entity.PaymentConfirmation
        {
            Id = "e6b30f2d-0088-4d99-bb47-c6b136fcf447",
            PaymentId = "855fa573-1ace-4da2-a55b-912f10103055",
            AccountId = "72970a7c-7921-431c-b95f-3438724ba16f",
            ShortReference = "PC-2436231-LYODVS",
            Status = "completed",
            ConfirmationUrl = "https://ccycloud-reports.example.com/payment_confirmations/404b407a-d143-4497-b5b9-6eb856377e20",
            CreatedAt = new DateTime(2018, 01, 01, 12, 34, 56),
            UpdatedAt = new DateTime(2018, 01, 01, 12, 34, 56),
            ExpiresAt = new DateTime(2018, 01, 03)
        };

        public static readonly PaymentTrackingInfo TrackingInfo1 = new Entity.PaymentTrackingInfo
        {
            Uetr = "46ed4827-7b6f-4491-a06f-b548d5a7512d",
            InitiationTime = DateTime.Parse("2019-07-09T13:20:30+00:00"),
            LastUpdateTime = DateTime.Parse("2019-07-10T15:39:08+00:00"),
            TransactionStatus = new Entity.PaymentTrackingInfo.TransactionStatusDef {
                Status = "processing",
                Reason = "transferred_and_tracked"
            },
            PaymentEvents = new List<PaymentTrackingInfo.PaymentEvent>() {
                    new PaymentTrackingInfo.PaymentEvent(){ TrackerEventType = "customer_credit_transfer_payment_cancellation_request",
                        Valid = true,
                        From = "BANABEBBXXX",
                        To = "BANAUS33XXX",
                        SenderAcknowledgementReceipt = DateTime.Parse("2019-07-10T15:39:08+00:00"),
                        LastUpdateTime =  DateTime.Parse("2019-07-10T15:39:08+00:00")
                    },
                    new PaymentTrackingInfo.PaymentEvent(){ TrackerEventType = "customer_credit_transfer_payment_cancellation_request",
                        Valid = true,
                        From = "BANABEBBXXX",
                        To = "BANAUS33XXX",
                        SenderAcknowledgementReceipt = DateTime.Parse("2019-07-10T14:22:41+00:00"),
                        LastUpdateTime =  DateTime.Parse("2019-07-10T14:22:41+00:00")
                    },
                    new PaymentTrackingInfo.PaymentEvent(){ TrackerEventType = "credit_transfer_payment_cancellation_request",
                        Valid = true,
                        From = "BANABEBBXXX",
                        To = "BANAUS33XXX",
                        Originator = "BANABEBBXXX",
                        SenderAcknowledgementReceipt = DateTime.Parse("2019-07-10T14:22:41+00:00"),
                        InterbankSettlementDate = DateTime.Parse("2019-07-09T00:00:00+00:00"),
                        InterbankSettlementAmount = new PaymentTrackingInfo.AmountDef() {
                            Currency = "USD",
                            Amount = 745437.57m
                        },
                        LastUpdateTime =  DateTime.Parse("2019-07-10T14:22:41+00:00")
                    },
                    new PaymentTrackingInfo.PaymentEvent(){ TrackerEventType = "customer_credit_transfer_payment_cancellation_request",
                        Valid = true,
                        From = "BANABEBBXXX",
                        To = "BANAUS33XXX",
                        SenderAcknowledgementReceipt = DateTime.Parse("2019-07-10T14:22:41+00:00"),
                        LastUpdateTime =  DateTime.Parse("2019-07-10T14:22:41+00:00")
                    },
                    new PaymentTrackingInfo.PaymentEvent(){ TrackerEventType = "customer_credit_transfer_payment_cancellation_request",
                        Valid = true,
                        From = "BANABEBBXXX",
                        To = "BANAUS33XXX",
                        SenderAcknowledgementReceipt = DateTime.Parse("2019-07-10T14:22:41+00:00"),
                        LastUpdateTime =  DateTime.Parse("2019-07-10T14:22:41+00:00")
                    },
                    new PaymentTrackingInfo.PaymentEvent(){ TrackerEventType = "credit_transfer_payment_cancellation_request",
                        Valid = true,
                        From = "BANABEBBXXX",
                        To = "BANAUS33XXX",
                        SenderAcknowledgementReceipt = DateTime.Parse("2019-07-10T14:17:39+00:00"),
                        LastUpdateTime =  DateTime.Parse("2019-07-10T14:22:41+00:00")
                    },
                    new PaymentTrackingInfo.PaymentEvent(){ TrackerEventType = "customer_credit_transfer_payment",
                        Valid = true, TransactionStatus = new Entity.PaymentTrackingInfo.TransactionStatusDef {
                            Status = "processing",
                            Reason = "transferred_and_tracked"
                        },
                        From = "BANABEBBXXX",
                        To = "BANAUS33XXX",
                        Originator = "BANABEBBXXX",
                        SerialParties = new Entity.PaymentTrackingInfo.SerialPartiesDef {
                           DebtorAgent = "GPMRCH30",
                            CreditorAgent = "GPMRQAJ0"
                        },
                        SenderAcknowledgementReceipt = DateTime.Parse("2019-07-09T13:20:30+00:00"),
                        InterbankSettlementDate = DateTime.Parse("2019-07-09T00:00:00+00:00"),
                        ChargeType = "shared",
                        InstructedAmount = new PaymentTrackingInfo.AmountDef() {
                            Currency = "USD",
                            Amount = 745437.57m
                        },
                        InterbankSettlementAmount = new PaymentTrackingInfo.AmountDef() {
                            Currency = "USD",
                            Amount = 745437.57m
                        },
                        LastUpdateTime =  DateTime.Parse("2019-07-09T13:20:50+00:00")
                    }
            }
        };
    }
}
