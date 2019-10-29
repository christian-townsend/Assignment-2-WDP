using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AspNetUserRolesEntityFramework.Data;
using AspNetUserRolesEntityFramework.Models;
using Microsoft.AspNetCore.Authorization;

namespace AspNetUserRolesEntityFramework.Controllers
{
    public class MachineLearningCompaniesFeedbacksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MachineLearningCompaniesFeedbacksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MachineLearningCompaniesFeedbacks
        public async Task<IActionResult> Index()
        {
            var allDiscussions = from result in _context.MachineLearningCompaniesFeedback
                                 orderby result.PostDate descending
                                 select result;
            return View(await allDiscussions.ToListAsync());
        }

        // GET: MachineLearningCompaniesFeedbacks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var machineLearningCompaniesFeedback = await _context.MachineLearningCompaniesFeedback
                .FirstOrDefaultAsync(m => m.Id == id);
            if (machineLearningCompaniesFeedback == null)
            {
                return NotFound();
            }

            return View(machineLearningCompaniesFeedback);
        }

        // GET: MachineLearningCompaniesFeedbacks/Create
        [Authorize(Roles = "Manager, RegisteredUser")]
        public IActionResult Create()
        {
            MachineLearningCompaniesFeedback ml = new MachineLearningCompaniesFeedback();
            ml.PostDate = DateTime.Now;
            ml.UserName = User.Identity.Name;

            return View(ml);
        }

        // POST: MachineLearningCompaniesFeedbacks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager, RegisteredUser")]
        public async Task<IActionResult> Create([Bind("Id,PostDate,UserName,TopicTitle,Company,StarRating,MessageContent")] MachineLearningCompaniesFeedback machineLearningCompaniesFeedback)
        {
            if (ModelState.IsValid)
            {
                _context.Add(machineLearningCompaniesFeedback);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(machineLearningCompaniesFeedback);
        }

        // GET: MachineLearningCompaniesFeedbacks/Edit/5
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            

            var machineLearningCompaniesFeedback = await _context.MachineLearningCompaniesFeedback.FindAsync(id);
            machineLearningCompaniesFeedback.PostDate = DateTime.Now;
            if (machineLearningCompaniesFeedback == null)
            {
                return NotFound();
            }
            return View(machineLearningCompaniesFeedback);
        }

        // POST: MachineLearningCompaniesFeedbacks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PostDate,UserName,TopicTitle,Company,StarRating,MessageContent,Like,Dislike")] MachineLearningCompaniesFeedback machineLearningCompaniesFeedback)
        {
            if (id != machineLearningCompaniesFeedback.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(machineLearningCompaniesFeedback);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MachineLearningCompaniesFeedbackExists(machineLearningCompaniesFeedback.Id))
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
            return View(machineLearningCompaniesFeedback);
        }

        // GET: MachineLearningCompaniesFeedbacks/Delete/5
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var machineLearningCompaniesFeedback = await _context.MachineLearningCompaniesFeedback
                .FirstOrDefaultAsync(m => m.Id == id);
            if (machineLearningCompaniesFeedback == null)
            {
                return NotFound();
            }

            return View(machineLearningCompaniesFeedback);
        }

        // POST: MachineLearningCompaniesFeedbacks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var machineLearningCompaniesFeedback = await _context.MachineLearningCompaniesFeedback.FindAsync(id);
            _context.MachineLearningCompaniesFeedback.Remove(machineLearningCompaniesFeedback);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MachineLearningCompaniesFeedbackExists(int id)
        {
            return _context.MachineLearningCompaniesFeedback.Any(e => e.Id == id);
        }

        /////////////////////////////////////////////////////////////////////////////
        /// Increase Like on discussion forum //

        public async Task<IActionResult> IncreaseLike(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var machineLearningForum = await _context.MachineLearningCompaniesFeedback.FindAsync(id);

            if (machineLearningForum == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    if (User.Identity.IsAuthenticated && machineLearningForum.canIncreaseLike)
                    {
                        machineLearningForum.Like++;
                        machineLearningForum.canIncreaseLike = false;
                        _context.Update(machineLearningForum);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MachineLearningCompaniesFeedbackExists(machineLearningForum.Id))
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
            return RedirectToAction(nameof(Index));
        }


        /////////////////////////////////////////////////////////////////////////////
        /// Increase dislike on discussion forum //

        public async Task<IActionResult> IncreaseDislike(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var machineLearningForum = await _context.MachineLearningCompaniesFeedback.FindAsync(id);
            if (machineLearningForum == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    if (User.Identity.IsAuthenticated && machineLearningForum.canIncreaseDislike)
                    {
                        machineLearningForum.Dislike++;
                        machineLearningForum.canIncreaseDislike = false;
                        _context.Update(machineLearningForum);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MachineLearningCompaniesFeedbackExists(machineLearningForum.Id))
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
            return RedirectToAction(nameof(Index));
        }

    }
}
