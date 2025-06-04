﻿using System;
using System.Collections.Generic;
using CurrencyCloud.Entity;

namespace CurrencyCloud.Tests.Mock.Data
{
    static class Beneficiaries
    {
        public static readonly Beneficiary Beneficiary1 = new Beneficiary(
            "John Doe",
            "DE",
            "EUR",
            "Employee Funds"
            )
        {
                BeneficiaryAddress = new List<string>(new string[] { "23 Acacia Road" }),
                BeneficiaryCountry = "GB",
                BicSwift = "COBADEFF",
                Iban = "DE75512108001245126199",
                DefaultBeneficiary = true,
                BankAddress = new List<string>(new string[] { "KAISERSTRASSE 16" }),
                BankName = "COMMERZBANK AG",
                BankAccountType = "checking",
                BeneficiaryEntityType = "company",
                BeneficiaryCompanyName = "Some Company LLC",
                BeneficiaryFirstName = "John",
                BeneficiaryLastName = "Doe",
                BeneficiaryCity = "London",
                BeneficiaryPostcode = "W11 2BQ",
                BeneficiaryStateOrProvince = "TX",
                BeneficiaryDateOfBirth = new DateTime(1990, 7, 20),
                BeneficiaryIdentificationType = "none",
                BusinessNature = "law",
                CompanyWebsite = "test.com"
        };

        public static readonly Beneficiary Beneficiary2 = new Beneficiary(

            "Martin McFly",
            "US",
            "USD",
            "Employee Funds"
            )
        {
            BeneficiaryAddress = new List<string>(new string[] { "9303 Roslyndale Ave." }),
            BeneficiaryCountry = "US",
            AccountNumber = "13071472",
            RoutingCodeType1 = "sort_code",
            RoutingCodeValue1 = "200606",
            RoutingCodeType2 = "aba",
            RoutingCodeValue2 = "780",
            BicSwift = "DABADKKK",
            Iban = "US89370400440532013000",
            DefaultBeneficiary = true,
            BankAddress = new List<string>(new string[] { "1 Courthouse Square" }),
            BankName = "Emmet Bank",
            BankAccountType = "checking",
            BeneficiaryEntityType = "company",
            BeneficiaryCompanyName = "Back to the Future",
            BeneficiaryFirstName = "Martin",
            BeneficiaryLastName = "McFly",
            BeneficiaryCity = "Hill Valley",
            BeneficiaryPostcode = "91331",
            BeneficiaryStateOrProvince = "CA",
            BeneficiaryDateOfBirth = new DateTime(1968, 6, 9),
            BeneficiaryIdentificationType = "none"
        };
    }
}
