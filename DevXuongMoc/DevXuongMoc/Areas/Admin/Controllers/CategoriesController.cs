﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DevXuongMoc.Models;
using X.PagedList;

namespace DevXuongMoc.Areas.Admin.Controllers
{
    //[Area("Admin")]
    public class CategoriesController : BaseController
    {
        private readonly XuongMocContext _context;

        public CategoriesController(XuongMocContext context)
        {
            _context = context;
        }

        // GET: Admin/Categories
        public async Task<IActionResult> Index(string name, int page = 1)
        {
            int limit = 3;

            // Đảm bảo `page` không nhỏ hơn 1
            page = Math.Max(page, 1);

            IPagedList<Category> category;

            // Nếu có tham số `name`, tìm kiếm theo `name`
            if (!string.IsNullOrEmpty(name))
            {
                category = await _context.Categories
                    .Where(c => c.Title.Contains(name))
                    .OrderBy(c => c.Id)
                    .ToPagedListAsync(page, limit);
            }
            else
            {
                // Nếu không có `name`, lấy toàn bộ danh sách
                category = await _context.Categories
                    .OrderBy(c => c.Id)
                    .ToPagedListAsync(page, limit);
            }

            ViewBag.keyword = name;
            return View(category);
        }

        // GET: Admin/Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Admin/Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Icon,MateTitle,MetaKeyword,MetaDescription,Slug,Orders,Parentid,CreatedDate,UpdatedDate,AdminCreated,AdminUpdated,Notes,Status,Isdelete")] Category category)
        {
            try
            {
                var files = HttpContext.Request.Form.Files;
                if (files.Count() > 0 && files[0].Length > 0)
                {
                    var file = files[0];
                    var FileName = file.FileName;
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\Category", FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(stream);
                        category.Icon = "/images/categories/" + FileName;
                    }
                }
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View(category);
            }
        }

        // GET: Admin/Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Admin/Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Icon,MateTitle,MetaKeyword,MetaDescription,Slug,Orders,Parentid,CreatedDate,UpdatedDate,AdminCreated,AdminUpdated,Notes,Status,Isdelete")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var files = HttpContext.Request.Form.Files;
                    if (files.Count() > 0 && files[0].Length > 0)
                    {
                        var file = files[0];
                        var FileName = file.FileName;
                        // upload ảnh vào thư mục wwwroot\\images\\Category 
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\anhcat", FileName);
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            file.CopyTo(stream);
                            category.Icon = "/images/anhcat/" + FileName; //gán tên ảnh cho thuộc tính Image
                        }
                    }
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
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
            return View(category);
        }

        // GET: Admin/Categories/Delete/5



        // POST: Admin/Categories/Delete/5
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var category = _context.Categories.FirstOrDefault(u => u.Id == id);
            if (category == null)
            {
                // Nếu không tìm thấy người dùng, redirect đến danh sách hoặc hiển thị lỗi
                return NotFound();
            }

            // Tiến hành xóa người dùng
            _context.Categories.Remove(category);
            _context.SaveChanges();

            // Redirect về trang danh sách người dùng hoặc trang khác sau khi xóa thành công
            return RedirectToAction(nameof(Index));  // Giả sử bạn sẽ chuyển hướng về trang danh sách người dùng
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
