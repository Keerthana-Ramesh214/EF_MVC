using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EntityFramewor_MVC.Models;

namespace EntityFramewor_MVC.Controllers
{
    public class NewTransactionsController : Controller
    {
        private readonly Bank_DbContext _context;

        public NewTransactionsController(Bank_DbContext context)
        {
            _context = context;
        }

        // GET: NewTransactions
        public async Task<IActionResult> Index()
        {
            var bank_DbContext = _context.NewTransactions.Include(n => n.TransactionAccountNumNavigation);
            return View(await bank_DbContext.ToListAsync());
        }

        // GET: NewTransactions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newTransaction = await _context.NewTransactions
                .Include(n => n.TransactionAccountNumNavigation)
                .FirstOrDefaultAsync(m => m.TransactionId == id);
            if (newTransaction == null)
            {
                return NotFound();
            }

            return View(newTransaction);
        }

        // GET: NewTransactions/Create
        public IActionResult Create()
        {
            ViewData["TransactionAccountNum"] = new SelectList(_context.Accounts, "AccountNo", "AccountHolderName");
            return View();
        }

        // POST: NewTransactions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TransactionId,TransactionAccountNum,TransactionAmount,TransactionTime")] NewTransaction newTransaction)
        {
            var account = await _context.Accounts
                .FirstOrDefaultAsync(m => m.AccountNo == newTransaction.TransactionAccountNum);
            account.BalanceAmount = account.BalanceAmount - newTransaction.TransactionAmount;
            _context.Update(account);
            _context.SaveChanges();
            if (ModelState.IsValid)
                _context.Add(newTransaction);
            {
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TransactionAccountNum"] = new SelectList(_context.Accounts, "AccountNo", "AccountHolderName", newTransaction.TransactionAccountNum);
            return View(newTransaction);
        }

        // GET: NewTransactions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newTransaction = await _context.NewTransactions.FindAsync(id);
            if (newTransaction == null)
            {
                return NotFound();
            }
            ViewData["TransactionAccountNum"] = new SelectList(_context.Accounts, "AccountNo", "AccountHolderName", newTransaction.TransactionAccountNum);
            return View(newTransaction);
        }

        // POST: NewTransactions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TransactionId,TransactionAccountNum,TransactionAmount,TransactionTime")] NewTransaction newTransaction)
        {
            if (id != newTransaction.TransactionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(newTransaction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewTransactionExists(newTransaction.TransactionId))
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
            ViewData["TransactionAccountNum"] = new SelectList(_context.Accounts, "AccountNo", "AccountHolderName", newTransaction.TransactionAccountNum);
            return View(newTransaction);
        }

        // GET: NewTransactions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newTransaction = await _context.NewTransactions
                .Include(n => n.TransactionAccountNumNavigation)
                .FirstOrDefaultAsync(m => m.TransactionId == id);
            if (newTransaction == null)
            {
                return NotFound();
            }

            return View(newTransaction);
        }

        // POST: NewTransactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var newTransaction = await _context.NewTransactions.FindAsync(id);
            _context.NewTransactions.Remove(newTransaction);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewTransactionExists(int id)
        {
            return _context.NewTransactions.Any(e => e.TransactionId == id);
        }
    }
}
