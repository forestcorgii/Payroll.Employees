﻿using Pms.Masterlists.Domain.Entities.Employees;
using Pms.Masterlists.Domain.Enums;
using Pms.Masterlists.Domain.Exceptions;
using Xunit;

namespace Pms.Masterlists.Tests.Domain
{
    public class EmployeeTests
    {
        public class An_eeId_is_valid
        {
             
            [Fact]
            public void if_it_does_not_accept_characted_lenght_less_than_three()
            {
                Assert.Throws<InvalidFieldValueException>(() =>
                {
                    Employee employee = new();
                    employee.EEId = "DY";
                    employee.ValidatePersonalInformation();
                });
            }
            [Fact]
            public void if_it_does_not_accept_characted_lenght_greater_than_four()
            {
                Assert.Throws<InvalidFieldValueException>(() =>
                {
                    Employee employee = new();
                    employee.EEId = "DYYYJ";
                    employee.ValidatePersonalInformation();
                });
            }
        }

        public class A_firstname_is_valid
        {
            [Fact]
            public void if_it_does_not_accept_lower_case()
            {
                Assert.Throws<InvalidFieldValueException>(() =>
                {
                    Employee employee = new();
                    employee.FirstName = "Sean Ivan";
                    employee.ValidatePersonalInformation();
                });
            }

            [Fact]
            public void if_it_does_not_accept_length_less_than_two()
            {
                Assert.Throws<InvalidFieldValueException>(() =>
                {
                    Employee employee = new();
                    employee.FirstName = "I";
                    employee.ValidatePersonalInformation();
                });
            }

            [Fact]
            public void if_it_does_not_accept_length_greater_than_45()
            {
                Assert.Throws<InvalidFieldValueException>(() =>
                {
                    Employee employee = new();
                    employee.FirstName = "SEAN SEAN SEAN SEAN SEAN SEAN SEAN SEAN SEAN 1";
                    employee.ValidatePersonalInformation();
                });
            }

            [Fact]
            public void if_it_does_not_accept_numbers()
            {
                Assert.Throws<InvalidFieldValueException>(() =>
                {
                    Employee employee = new();
                    employee.FirstName = "IVAN 123";
                    employee.ValidatePersonalInformation();
                });
            }
        }

        public class A_lastname_is_valid
        {
            [Fact]
            public void if_it_does_not_accept_lower_case()
            {
                Assert.Throws<InvalidFieldValueException>(() =>
                {
                    Employee employee = new();
                    employee.LastName = "Fernandez";
                    employee.ValidatePersonalInformation();
                });
            }

            [Fact]
            public void if_it_does_not_accept_length_less_than_two()
            {
                Assert.Throws<InvalidFieldValueException>(() =>
                {
                    Employee employee = new();
                    employee.LastName = "F";
                    employee.ValidatePersonalInformation();
                });
            }

            [Fact]
            public void if_it_does_not_accept_length_greater_than_45()
            {
                Assert.Throws<InvalidFieldValueException>(() =>
                {
                    Employee employee = new();
                    employee.LastName = "SEAN SEAN SEAN SEAN SEAN SEAN FERNANDEZ FERNANDEZ";
                    employee.ValidatePersonalInformation();
                });
            }

            [Fact]
            public void if_it_does_not_accept_numbers()
            {
                Assert.Throws<InvalidFieldValueException>(() =>
                {
                    Employee employee = new();
                    employee.LastName = "IVAN 123";
                    employee.ValidatePersonalInformation();
                });
            }
        }

        public class A_middle_is_valid
        {
            [Fact]
            public void if_it_does_not_accept_lower_case()
            {
                Assert.Throws<InvalidFieldValueException>(() =>
                {
                    Employee employee = new();
                    employee.MiddleName = "sean IVAN";
                    employee.ValidatePersonalInformation();
                });
            }

            [Fact]
            public void if_it_does_not_accept_length_greater_than_45()
            {
                Assert.Throws<InvalidFieldValueException>(() =>
                {
                    Employee employee = new();
                    employee.MiddleName = "MARTINEZ MARTINEZ MARTINEZ MARTINEZ MARTINEZ F";
                    employee.ValidatePersonalInformation();
                });
            }

            [Fact]
            public void if_it_does_not_accept_numbers()
            {
                Assert.Throws<InvalidFieldValueException>(() =>
                {
                    Employee employee = new();
                    employee.MiddleName = "MARTINEZ 123";
                    employee.ValidatePersonalInformation();
                });
            }
        }




        public class A_tin_number_have_the_right_format
        {
            [Theory]
            [InlineData("123456789")]
            [InlineData("1234567890")]
            [InlineData("12345678900")]
            [InlineData("123456789000")]
            [InlineData("1234567890000")]
            [InlineData("123-456-789")]
            [InlineData("123-45-6789")]
            [InlineData("123-456-789-000")]
            public void if_it_matched_the_regular_expression(string tin)
            {
                Employee employee = new();
                employee.TIN = tin;
                employee.ValidateGovernmentInformation();
            }

            [Fact]
            public void if_it_does_not_have_letters()
            {
                Assert.Throws<InvalidFieldValueException>(() =>
                {
                    Employee employee = new();
                    employee.Pagibig = "123A56789";
                    employee.ValidateGovernmentInformation();
                });
            }
        }

        public class A_pagibig_number_have_the_right_format
        {
            [Theory]
            [InlineData("123456789012")]
            [InlineData("1234-5678-9012")]
            public void if_it_matched_the_regular_expression(string pagibig)
            {
                Employee employee = new();
                employee.Pagibig = pagibig;
                employee.ValidateGovernmentInformation();
            }

            [Fact]
            public void if_it_does_not_have_letters()
            {
                Assert.Throws<InvalidFieldValueException>(() =>
                {
                    Employee employee = new();
                    employee.Pagibig = "123A56789012";
                    employee.ValidateGovernmentInformation();
                });
            }
        }

        public class A_sss_number_have_the_right_format
        {
            [Theory]
            [InlineData("1234567890")]
            [InlineData("12-3456789-0")]
            public void if_it_matched_the_regular_expression(string sss)
            {
                Employee employee = new();
                employee.SSS = sss;
                employee.ValidateGovernmentInformation();
            }

            [Fact]
            public void if_it_does_not_have_letters()
            {
                Assert.Throws<InvalidFieldValueException>(() =>
                {
                    Employee employee = new();
                    employee.SSS = "123A56789O";
                    employee.ValidateGovernmentInformation();
                });
            }
        }

        public class A_philhealth_number_have_the_right_format
        {
            [Theory]
            [InlineData("123456789012")]
            [InlineData("12-345678901-2")]
            public void if_it_matched_the_regular_expression(string philhealth)
            {
                Employee employee = new();
                employee.PhilHealth = philhealth;
                employee.ValidateGovernmentInformation();
            }

            [Fact]
            public void if_it_does_not_have_letters()
            {
                Assert.Throws<InvalidFieldValueException>(() =>
                {
                    Employee employee = new();
                    employee.PhilHealth = "123A56789OI2";
                    employee.ValidateGovernmentInformation();
                });
            }
        }




        public class A_account_number_is_valid
        {
            [Theory]
            [InlineData(BankChoices.LBP)]
            [InlineData(BankChoices.CBC)]
            [InlineData(BankChoices.CBC1)]
            [InlineData(BankChoices.MTAC)]
            [InlineData(BankChoices.MPALO)]
            public void if_it_throws_exception_when_account_number_is_empty(BankChoices bank)
            {
                Assert.Throws<InvalidFieldValueException>(() =>
                {
                    Employee employee = new();
                    employee.Bank = bank;
                    employee.AccountNumber = string.Empty;
                    employee.ValidateBankInformation();
                });
            }
            [Fact]
            public void if_it_does_not_have_letters()
            {
                Assert.Throws<InvalidFieldValueException>(() =>
                {
                    Employee employee = new();
                    employee.AccountNumber = "123A56789OI2";
                    employee.ValidateBankInformation();
                });
            }




            [Fact]
            public void if_it_throws_exception_when_bank_is_LBP_and_length_is_not_19()
            {
                Assert.Throws<InvalidFieldValueException>(() =>
                {
                    Employee employee = new();
                    employee.Bank = BankChoices.LBP;
                    employee.AccountNumber = "2206060193770081881";
                    employee.AccountNumber = employee.AccountNumber.PadRight(20);
                    employee.ValidateBankInformation();
                });
            }

            [Fact]
            public void if_it_throws_exception_when_bank_is_LBP_and_it_is_does_not_contain_19372()
            {
                Assert.Throws<InvalidFieldValueException>(() =>
                {
                    Employee employee = new();
                    employee.Bank = BankChoices.LBP;
                    employee.AccountNumber = "2206060193770081881";
                    employee.ValidateBankInformation();
                });
            }






            [Theory]
            [InlineData(1)]
            [InlineData(2)]
            [InlineData(3)]
            [InlineData(4)]
            [InlineData(5)]
            [InlineData(6)]
            [InlineData(7)]
            [InlineData(8)]
            [InlineData(9)]
            [InlineData(11)]
            [InlineData(13)]
            [InlineData(14)]
            [InlineData(15)]
            [InlineData(16)]
            [InlineData(17)]
            [InlineData(20)]
            [InlineData(21)]
            public void if_it_throws_exception_when_bank_is_CBC_CBC1_and_length_is_not_10_12_18_19(int length)
            {
                Assert.Throws<InvalidFieldValueException>(() =>
                {
                    Employee employee = new();
                    employee.Bank = BankChoices.CBC;
                    employee.AccountNumber = employee.AccountNumber.PadRight(length);
                    employee.ValidateBankInformation();
                });
            }


            [Theory]
            [InlineData(10)]
            [InlineData(12)]
            [InlineData(18)]
            [InlineData(19)]
            public void if_it_does_not_throws_exception_when_bank_is_CBC_CBC1_and_length_is_10_12_18_19(int length)
            {
                    Employee employee = new();
                    employee.Bank = BankChoices.CBC;
                    employee.AccountNumber = employee.AccountNumber.PadRight(length);
                    employee.ValidateBankInformation();
            }






            [Fact]
            public void if_it_throws_exception_when_bank_is_MTAC_and_length_is_not_13()
            {
                Assert.Throws<InvalidFieldValueException>(() =>
                {
                    Employee employee = new();
                    employee.Bank = BankChoices.MTAC;
                    employee.AccountNumber = "5253525602126";
                    employee.AccountNumber = employee.AccountNumber.PadRight(14);
                    employee.ValidateBankInformation();
                });
            }

            [Fact]
            public void if_it_throws_exception_when_bank_is_MTAC_and_it_is_does_not_have_a_leading_525()
            {
                Assert.Throws<InvalidFieldValueException>(() =>
                {
                    Employee employee = new();
                    employee.Bank = BankChoices.MTAC;
                    employee.AccountNumber = "5223525602126";
                    employee.ValidateBankInformation();
                });
            }










            [Fact]
            public void if_it_throws_exception_when_bank_is_MPALO_and_length_is_not_13()
            {
                Assert.Throws<InvalidFieldValueException>(() =>
                {
                    Employee employee = new();
                    employee.Bank = BankChoices.MPALO;
                    employee.AccountNumber = "7563899703376";
                    employee.AccountNumber = employee.AccountNumber.PadRight(14);
                    employee.ValidateBankInformation();
                });
            }

            [Fact]
            public void if_it_throws_exception_when_bank_is_MPALO_and_it_is_does_not_have_a_leading_756()
            {
                Assert.Throws<InvalidFieldValueException>(() =>
                {
                    Employee employee = new();
                    employee.Bank = BankChoices.MPALO;
                    employee.AccountNumber = "7553899703376";
                    employee.ValidateBankInformation();
                });
            }






        }

        public class A_card_number_is_valid
        {
            [Theory]
            [InlineData(BankChoices.LBP)]
            [InlineData(BankChoices.CBC)]
            [InlineData(BankChoices.CBC1)]
            [InlineData(BankChoices.MTAC)]
            [InlineData(BankChoices.MPALO)]
            public void if_it_throws_exception_when_bank_is_empty(BankChoices bank)
            {
                Assert.Throws<InvalidFieldValueException>(() =>
                {
                    Employee employee = new();
                    employee.Bank = bank;
                    employee.CardNumber = string.Empty;
                    employee.ValidateBankInformation();
                });
            }

            [Fact]
            public void if_it_does_not_have_letters()
            {
                Assert.Throws<InvalidFieldValueException>(() =>
                {
                    Employee employee = new();
                    employee.Bank = BankChoices.LBP;
                    employee.CardNumber = "123A56789OI2";
                    employee.ValidateBankInformation();
                });
            }


            [Fact]
            public void if_it_throws_exception_when_bank_is_LBP_and_length_is_not_16()
            {
                Assert.Throws<InvalidFieldValueException>(() =>
                {
                    Employee employee = new();
                    employee.Bank = BankChoices.LBP;
                    employee.CardNumber = employee.CardNumber.PadRight(13);
                    employee.ValidateBankInformation();
                });
            }

        }
    }
}
