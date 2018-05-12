using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcBookStore.Models;
using PagedList;
using PagedList.Mvc;

namespace MvcBookStore.Controllers
{
    public class BookStoreController : Controller
    {
        dbQLBansachDataContext data = new dbQLBansachDataContext();
        // GET: BookStore
        private List<SACH> LaySachMoi(int count)
        {
            return data.SACHes.OrderByDescending(a => a.Ngaycapnhat).Take(count).ToList();
        }
        //public ActionResult Index()
        //{
        //    var sachmoi = LaySachMoi(5);
        //    return View(sachmoi);
        //}
        public ActionResult Chude()
        {
            var chude = from cd in data.CHUDEs select cd;
            return PartialView(chude);
        }
        public ActionResult Nhaxuatban()
        {
            var nhaxuatban = from nxb in data.NHAXUATBANs select nxb;
            return PartialView(nhaxuatban);
        }
        public ActionResult Index(string searchString, int? page)
        {
            int pageSize = 6;
            int pageNum = (page ?? 1);

            //if (!page.HasValue)
            //{
            //    page = 1; // set initial page value
            //}
            ViewBag.Keyword = searchString;
            var book = data.SACHes.ToList();
            if (!String.IsNullOrEmpty(searchString))
            {
                book = book.Where(s => s.Tensach.Contains(searchString)).ToList();
                book.OrderByDescending(v => v.Masach);
                var finalList = book.ToPagedList(pageNum, pageSize);
                return View(finalList);
            }
            else
            {
                var sachmoi = LaySachMoi(15);
                return View(sachmoi.ToPagedList(pageNum, pageSize));
            }
        }
        public ActionResult SPTheochude(int id)
        {
            var sach = from s in data.SACHes where s.MaCD == id select s;
            return View(sach);
        }
        public ActionResult SPTheoNXB(int id)
        {
            var sach = from s in data.SACHes where s.MaNXB == id select s;
            return View(sach);
        }
        public ActionResult Details(int id)
        {
            var sach = from s in data.SACHes
                       where s.Masach == id
                       select s;
            return View(sach.Single());
        }
    }
}