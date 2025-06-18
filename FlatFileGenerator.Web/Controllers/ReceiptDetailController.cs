using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FlatFileGenerator.Core.Models;
using FlatFileGenerator.DataAccess.Models;
using FlatFileGenerator.DataAccess.Repositories;

namespace FlatFileGenerator.Web.Controllers
{
    public class ReceiptDetailController(ReceiptDetailRepository repository) : Controller
    {
        // GET: ReceiptDetail
        public async Task<IActionResult> Index()
        {
            return View(await repository.GetList());
        }

        // GET: ReceiptDetail/Details/5
        [HttpGet("[action]")]

        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receiptDetail = repository.Get(id.Value);
            if (receiptDetail == null)
            {
                return NotFound();
            }

            return View(receiptDetail);
        }

        // GET: ReceiptDetail/Create
        [HttpGet("[action]")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: ReceiptDetail/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ReceivedDate,LaborAgreementNumber,Cvr,PersonFullName,PaymentReference,PaymentDate,ReceiptType,PolicyNumber,BatchNumber,TransactionNumber,TotalContributionRate,EmployerContributionRate,EmployerContribution,ContributionRateFromDate,NormalContribution,NormalContributionStartDate,EmploymentTerminationDate,DeviationStartDate,DeviationEndDate,DeviationCode,EmployeeSalaryStartDate,EmployeeSalary,TermsOfSalary,EmploymentRateStartDate,EmploymentRate,CustomerNumber,SubmissionDate,FromDate,ToDate,Cpr,Amount,Id,CreatedDate")] ReceiptDetail receiptDetail)
        {
            if (ModelState.IsValid)
            {
                receiptDetail.Id = Guid.NewGuid();
                repository.Add(receiptDetail);
                return RedirectToAction(nameof(Index));
            }
            return View(receiptDetail);
        }

        // GET: ReceiptDetail/Edit/5
        [HttpGet("[action]")]
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receiptDetail = repository.Get(id.Value);
            if (receiptDetail == null)
            {
                return NotFound();
            }
            return View(receiptDetail);
        }

        // POST: ReceiptDetail/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("ReceivedDate,LaborAgreementNumber,Cvr,PersonFullName,PaymentReference,PaymentDate,ReceiptType,PolicyNumber,BatchNumber,TransactionNumber,TotalContributionRate,EmployerContributionRate,EmployerContribution,ContributionRateFromDate,NormalContribution,NormalContributionStartDate,EmploymentTerminationDate,DeviationStartDate,DeviationEndDate,DeviationCode,EmployeeSalaryStartDate,EmployeeSalary,TermsOfSalary,EmploymentRateStartDate,EmploymentRate,CustomerNumber,SubmissionDate,FromDate,ToDate,Cpr,Amount,Id,CreatedDate")] ReceiptDetail receiptDetail)
        {
            if (id != receiptDetail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    repository.Add(receiptDetail);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReceiptDetailExists(receiptDetail.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(receiptDetail);
        }

        // GET: ReceiptDetail/Delete/5
        [HttpGet("[action]")]

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receiptDetail = repository.Get(id.Value);
            if (receiptDetail == null)
            {
                return NotFound();
            }

            return View(receiptDetail);
        }

        // POST: ReceiptDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var receiptDetail = repository.Get(id);
            if (receiptDetail != null)
            {
                repository.Delete(receiptDetail.Id);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ReceiptDetailExists(Guid id)
        {
            return repository.Get(id)!= null;
        }
    }
}
