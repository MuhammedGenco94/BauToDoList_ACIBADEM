using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BauToDoList.Models;
using System.IO;

namespace BauToDoList.Controllers
{
    public class MediaController : Controller
    {
        public static bool Kayit = true;
        private appDbContext db = new appDbContext();

        // GET: Media
        public async Task<ActionResult> Index()
        {
            return View(await db.Medias.ToListAsync());
        }

        // GET: Media/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Media media = await db.Medias.FindAsync(id);
            if (media == null)
            {
                return HttpNotFound();
            }
            return View(media);
        }

        // GET: Media/Create
        public ActionResult Create()
        {
            var media = new Media();
            return View(media);
        }

        // POST: Media/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Description,Extension,FilePath,FileSize,Year,Month,ContentType,CreatedBy,CreateDate,UpdatedBy,UpdateDate")] Media media)
        {
            if (ModelState.IsValid && Kayit==true)
            {
                media.CreateDate = DateTime.Now;
                media.CreatedBy = User.Identity.Name;
                media.UpdateDate = DateTime.Now;
                media.UpdatedBy = User.Identity.Name;

                //  Upload İşlemi:
                if (!String.IsNullOrEmpty(media.FilePath))
                {
                    FileInfo fileInfo = new FileInfo(Server.MapPath("~" + media.FilePath));
                    //media.FileSize = (float)fileInfo.Length / (float)1024;
                    media.FileSize = (float)TempData["fileSize"];
                    media.Extension = fileInfo.Extension;
                    media.ContentType = fileInfo.Extension;

                    //var requestFiles = Request.Files[0];
                    //HttpPostedFileBase file = Request.Files[requestFiles.ToString()];
                    //if (!System.IO.File.Exists(fileInfo.FullName))
                    //{
                    //    file.SaveAs(fileInfo.FullName);
                    //}
                    //else
                    //{
                    //    throw new Exception("Bu dosya mevcut.");
                    //}
                }
                db.Medias.Add(media);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            media.FilePath = "/uploads" + (string)TempData["categoryFolder"] + (string)TempData["fileName"];
            return View(media);
        }

        public float fileSize;
        
        public ActionResult SaveUploadedFile()
        {
            bool isSavedSuccessfully = true;
            string fName = "";
            string categoryFolder = "";

            try
            {
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];
                    TempData["fileeeName"] = fileName;
                    if (file != null && file.ContentLength > 0)
                    {
                        var uploadLocation = Server.MapPath("~/uploads");
                        //TempData["uploadLocation"] = uploadLocation;
                        categoryFolder = "/" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "/";
                        TempData["categoryFolder"] = categoryFolder;
                        fileSize = ((float)file.ContentLength) / ((float)1024);
                        TempData["fileSize"] = fileSize;

                        fName = file.FileName;
                        TempData["fileName"] = fName;
                        var extention = Path.GetExtension(fName).ToLower();
                        var contentType = file.ContentType;

                        if (!Directory.Exists(uploadLocation + categoryFolder))
                        {
                            Directory.CreateDirectory(uploadLocation + categoryFolder);
                        }

                        if (!System.IO.File.Exists(uploadLocation + categoryFolder + fName))
                        {
                            Kayit = true;
                            file.SaveAs(uploadLocation + categoryFolder + fName);
                        }
                        else
                        {
                            Kayit = false;
                            throw new Exception("Bu dosya mevcut.");
                        }
                    }
                }
            }
            catch (Exception)
            {
                Kayit = false;
                isSavedSuccessfully = false;
            }

            if (isSavedSuccessfully == true)
            {
                return Json(new { Message = "/uploads" + categoryFolder + fName, success = true });
            }
            else
            {
                return Json(new { Message = "Hata oldu dosya kaydedilemedi. " + categoryFolder + fName, success = false });
            }
        }


        // GET: Media/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Media media = await db.Medias.FindAsync(id);
            if (media == null)
            {
                return HttpNotFound();
            }
            return View(media);
        }

        // POST: Media/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Description,Extension,FilePath,FileSize,Year,Month,ContentType,CreatedBy,CreateDate,UpdatedBy,UpdateDate")] Media media)
        {
            if (ModelState.IsValid)
            {
                media.UpdateDate = DateTime.Now;
                media.UpdatedBy = User.Identity.Name;

                //  Upload İşlemi:
                if (!String.IsNullOrEmpty(media.FilePath))
                {
                    FileInfo fileInfo = new FileInfo(Server.MapPath("~" + media.FilePath));
                    media.FileSize = ((float)fileInfo.Length) / ((float)1024);
                    media.Extension = fileInfo.Extension;
                    media.ContentType = fileInfo.Extension;
                }

                db.Entry(media).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(media);
        }

        //**************//
        //**************//
        //**************//

        // GET: Media/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Media media = await db.Medias.FindAsync(id);
            if (media == null)
            {
                return HttpNotFound();
            }
            return View(media);
        }

        // POST: Media/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Media media = await db.Medias.FindAsync(id);
            db.Medias.Remove(media);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
