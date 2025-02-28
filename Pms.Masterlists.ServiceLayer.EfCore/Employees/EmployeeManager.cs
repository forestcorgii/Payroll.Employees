﻿using Microsoft.EntityFrameworkCore;
using Pms.Masterlists.Domain;
using Pms.Masterlists.Domain.Entities.Employees;
using Pms.Masterlists.Domain.Enums;
using Pms.Masterlists.Domain.Exceptions;
using Pms.Masterlists.Persistence;
using Pms.Masterlists.ServiceLayer.EfCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Masterlists.ServiceLayer.EfCore
{
    public class EmployeeManager
    {
        private IDbContextFactory<EmployeeDbContext> _factory;

        public EmployeeManager(IDbContextFactory<EmployeeDbContext> factory) =>
            _factory = factory;


        public void Save(Employee employee)
        {
            if (employee is null)
                employee = new() { EEId = employee.EEId };

            employee.ValidateAll();

            EmployeeDbContext Context = _factory.CreateDbContext();
            ValidateDuplicate(employee, Context);
            ValidatePayrollCode(employee, Context);


            AddOrUpdate(employee);
        }
        public void Save(IActive info)
        {
            EmployeeDbContext Context = _factory.CreateDbContext();
            Employee employee = Context.Employees.Where(ee => ee.EEId == info.EEId).FirstOrDefault();
            if (employee is null)
                employee = new() { EEId = info.EEId };

            employee.EEId = info.EEId;
            employee.Active = info.Active;

            AddOrUpdate(employee);
        }

        /// <summary>
        /// Update Only. Should not insert when employee doesn't exists.
        /// </summary>
        /// <param name="info"></param>
        public void Save(IMasterFileInformation info)
        {
            EmployeeDbContext Context = _factory.CreateDbContext();
            Employee employee = Context.Employees.Where(ee => ee.EEId == info.EEId).FirstOrDefault();
            if (employee is null)
                return;

            employee.JobCode = info.JobCode;

            AddOrUpdate(employee);
        }


        public void Save(IGovernmentInformation info)
        {
            EmployeeDbContext Context = _factory.CreateDbContext();
            Employee employee = Context.Employees.Where(ee => ee.EEId == info.EEId).FirstOrDefault();
            if (employee is null)
                employee = new() { EEId = info.EEId };

            employee.Pagibig = info.Pagibig;
            employee.PhilHealth = info.PhilHealth;
            employee.SSS = info.SSS;
            employee.TIN = info.TIN;

            employee.ValidateGovernmentInformation();

            AddOrUpdate(employee);
        }

        public void Save(IHRMSInformation info)
        {
            EmployeeDbContext Context = _factory.CreateDbContext();
            Employee employee = Context.Employees.Where(ee => ee.EEId == info.EEId).FirstOrDefault();
            if (employee is null)
                employee = new() { EEId = info.EEId };

            employee.EEId = info.EEId;
            employee.FirstName = info.FirstName;
            employee.LastName = info.LastName;
            employee.MiddleName = info.MiddleName;
            employee.JobCode = info.JobCode;
            employee.Location = info.Location;

            employee.ValidateGovernmentInformation();
            employee.ValidatePersonalInformation();

            AddOrUpdate(employee);
        }


        public void Save(IEEDataInformation info)
        {
            EmployeeDbContext Context = _factory.CreateDbContext();
            Employee employee = Context.Employees.Where(ee => ee.EEId == info.EEId).FirstOrDefault();
            if (employee is null)
                employee = new() { EEId = info.EEId };

            if (!string.IsNullOrEmpty(info.FirstName) && !string.IsNullOrEmpty(info.LastName))
            {
                employee.FirstName = info.FirstName;
                employee.LastName = info.LastName;
                employee.MiddleName = info.MiddleName;
                employee.NameExtension = info.NameExtension;
            }

            employee.BirthDate = info.BirthDate;

            employee.Pagibig = info.Pagibig.Replace("-", string.Empty);
            employee.PhilHealth = info.PhilHealth.Replace("-", string.Empty);
            employee.SSS = info.SSS.Replace("-", string.Empty);
            employee.TIN = info.TIN.Replace("-", string.Empty);

            employee.ValidateGovernmentInformation();
            employee.ValidatePersonalInformation();

            AddOrUpdate(employee);
        }

        public void Save(IBankInformation info)
        {
            EmployeeDbContext Context = _factory.CreateDbContext();

            Employee employee = Context.Employees.Where(ee => ee.EEId == info.EEId).FirstOrDefault();
            if (employee is null)
                employee = new() { EEId = info.EEId };

            if (!string.IsNullOrEmpty(info.FirstName) && !string.IsNullOrEmpty(info.LastName))
            {
                employee.FirstName = info.FirstName;
                employee.LastName = info.LastName;
                employee.MiddleName = info.MiddleName;
                employee.NameExtension = info.NameExtension;
            }

            employee.AccountNumber = info.AccountNumber;
            employee.CardNumber = info.CardNumber;
            employee.Bank = info.Bank;
            employee.PayrollCode = info.PayrollCode;

            employee.ValidateBankInformation();

            ValidateDuplicate(employee, Context);
            ValidatePayrollCode(employee, Context);




            AddOrUpdate(employee);
        }

        private void AddOrUpdate(Employee employee)
        {
            EmployeeDbContext Context = _factory.CreateDbContext();
            if (Context.Employees.Count(ee => ee.EEId == employee.EEId) > 0)
                Context.Update(employee);
            else
                Context.Add(employee);
            Context.SaveChanges();
        }






        public void ValidatePayrollCode(Employee employee, EmployeeDbContext Context)
        {
            if (employee.JobRemarks != "BACKOUT")
            {
                if (Context.PayrollCodes.Where(pc => pc.PayrollCodeId == employee.PayrollCode).FirstOrDefault() is PayrollCode payrollCode)
                {
                    employee.CompanyId = payrollCode.CompanyId;
                    employee.Site = payrollCode.Site;
                }
                else
                    throw new InvalidFieldValueException("Payroll Code", employee.PayrollCode, employee.EEId, "Unknown Payroll code.");
            }

        }

        public void ValidateDuplicate(Employee employee, EmployeeDbContext Context)
        {
            Employee hasDuplicateAccountNumber = null;
            Employee hasDuplicateCardNumber = null;
            if (employee.Bank == BankChoices.LBP)
            {
                hasDuplicateAccountNumber = Context.Employees.Where(ee => ee.EEId != employee.EEId && ee.AccountNumber == employee.AccountNumber).FirstOrDefault();
                hasDuplicateCardNumber = Context.Employees.Where(ee => ee.EEId != employee.EEId && ee.CardNumber == employee.CardNumber).FirstOrDefault();
            }
            else if (employee.Bank != BankChoices.CHK)
                hasDuplicateAccountNumber = Context.Employees.Where(ee => ee.EEId != employee.EEId && ee.AccountNumber == employee.AccountNumber).FirstOrDefault();


            if (hasDuplicateAccountNumber is not null && !string.IsNullOrEmpty(hasDuplicateAccountNumber.AccountNumber))
                throw new DuplicateBankInformationException(employee.EEId, hasDuplicateAccountNumber.EEId, "Account Number", hasDuplicateAccountNumber.AccountNumber);

            if (hasDuplicateCardNumber is not null && !string.IsNullOrEmpty(hasDuplicateAccountNumber.CardNumber))
                throw new DuplicateBankInformationException(employee.EEId, hasDuplicateCardNumber.EEId, "Card Number", hasDuplicateCardNumber.CardNumber);

        }


    }
}
