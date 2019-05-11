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
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;

namespace BauToDoList.Controllers
{
    public class ToDoItemsController : Controller
    {
        private appDbContext db = new appDbContext();

        // GET: ToDoItems
        public async Task<ActionResult> Index()
        {
            var toDoItems = db.toDoItems.Include(t => t.Category).Include(t => t.Customer).Include(t => t.Department).Include(t => t.Manager).Include(t => t.Organizator).Include(t => t.Side);
            return View(await toDoItems.ToListAsync());
        }

        // GET: ToDoItems/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDoItem toDoItem = await db.toDoItems.FindAsync(id);
            if (toDoItem == null)
            {
                return HttpNotFound();
            }
            return View(toDoItem);
        }

        // GET: ToDoItems/Create
        public ActionResult Create()
        {

            var toDoItem = new ToDoItem();
            toDoItem.MeetingDate = DateTime.Now;
            toDoItem.MeetingHour = DateTime.Now;
            toDoItem.FinishDate = DateTime.Now;
            toDoItem.FinishHour = DateTime.Now;
            toDoItem.PlannedDate = DateTime.Now;
            toDoItem.PlannedHour = DateTime.Now;
            toDoItem.ReviseDate = DateTime.Now;
            toDoItem.ReviseHour = DateTime.Now;
            toDoItem.ScheduledOrganizationDate = DateTime.Now;
            toDoItem.ScheduledOrganizationHour = DateTime.Now;

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name");
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name");
            ViewBag.ManagerId = new SelectList(db.Contacts, "Id", "FirstName");
            ViewBag.OrganizatorId = new SelectList(db.Contacts, "Id", "FirstName");
            ViewBag.SideId = new SelectList(db.Sides, "Id", "Name");

            return View(toDoItem);
        }

        // POST: ToDoItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Title,Description,Status,CategoryId,Attachment,DepartmentId,SideId,CustomerId,ManagerId,OrganizatorId,MeetingDate,PlannedDate,FinishDate,ReviseDate,ConversationSubject,SupporterCompany,SupporterDoctor,ConversationAttendeeCount,ScheduledOrganizationDate,MailingSubjects,PosterSubjects,PosterCount,Elearning,TypesOfScans,AsoCountInScans,TypesOfOrganizations,AsoCountInOrganization,TypesOfVaccinationOrganization,AsoCountInVaccinationOrganization,AmountOfCompensantionForPoster,CorporateProductivityReport,CreatedBy,CreateDate,UpdatedBy,UpdateDate,MeetingHour,FinishHour,PlannedHour,ReviseHour,ScheduledOrganizationHour")] ToDoItem toDoItem)
        {
            if (ModelState.IsValid)
            {
                toDoItem.CreateDate = DateTime.Now;
                toDoItem.CreatedBy = User.Identity.Name;
                toDoItem.UpdateDate = DateTime.Now;
                toDoItem.UpdatedBy = User.Identity.Name;

                db.toDoItems.Add(toDoItem);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", toDoItem.CategoryId);
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", toDoItem.CustomerId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name", toDoItem.DepartmentId);
            ViewBag.ManagerId = new SelectList(db.Contacts, "Id", "FirstName", toDoItem.ManagerId);
            ViewBag.OrganizatorId = new SelectList(db.Contacts, "Id", "FirstName", toDoItem.OrganizatorId);
            ViewBag.SideId = new SelectList(db.Sides, "Id", "Name", toDoItem.SideId);
            return View(toDoItem);
        }

        // GET: ToDoItems/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDoItem toDoItem = await db.toDoItems.FindAsync(id);
            if (toDoItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", toDoItem.CategoryId);
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", toDoItem.CustomerId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name", toDoItem.DepartmentId);
            ViewBag.ManagerId = new SelectList(db.Contacts, "Id", "FirstName", toDoItem.ManagerId);
            ViewBag.OrganizatorId = new SelectList(db.Contacts, "Id", "FirstName", toDoItem.OrganizatorId);
            ViewBag.SideId = new SelectList(db.Sides, "Id", "Name", toDoItem.SideId);
            return View(toDoItem);
        }

        // POST: ToDoItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title,Description,Status,CategoryId,Attachment,DepartmentId,SideId,CustomerId,ManagerId,OrganizatorId,MeetingDate,PlannedDate,FinishDate,ReviseDate,ConversationSubject,SupporterCompany,SupporterDoctor,ConversationAttendeeCount,ScheduledOrganizationDate,MailingSubjects,PosterSubjects,PosterCount,Elearning,TypesOfScans,AsoCountInScans,TypesOfOrganizations,AsoCountInOrganization,TypesOfVaccinationOrganization,AsoCountInVaccinationOrganization,AmountOfCompensantionForPoster,CorporateProductivityReport,CreatedBy,CreateDate,UpdatedBy,UpdateDate,MeetingHour,FinishHour,PlannedHour,ReviseHour,ScheduledOrganizationHour")] ToDoItem toDoItem)
        {
            if (ModelState.IsValid)
            {
                toDoItem.UpdateDate = DateTime.Now;
                toDoItem.UpdatedBy = User.Identity.Name;

                db.Entry(toDoItem).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", toDoItem.CategoryId);
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", toDoItem.CustomerId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name", toDoItem.DepartmentId);
            ViewBag.ManagerId = new SelectList(db.Contacts, "Id", "FirstName", toDoItem.ManagerId);
            ViewBag.OrganizatorId = new SelectList(db.Contacts, "Id", "FirstName", toDoItem.OrganizatorId);
            ViewBag.SideId = new SelectList(db.Sides, "Id", "Name", toDoItem.SideId);
            return View(toDoItem);
        }

        // GET: ToDoItems/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDoItem toDoItem = await db.toDoItems.FindAsync(id);
            if (toDoItem == null)
            {
                return HttpNotFound();
            }
            return View(toDoItem);
        }

        // POST: ToDoItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ToDoItem toDoItem = await db.toDoItems.FindAsync(id);
            db.toDoItems.Remove(toDoItem);
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

        public void ExportToExcel()
        {
            var grid = new GridView();
            grid.DataSource = from data in db.toDoItems.ToList()
                              select new
                              {
                                  Baslik = data.Title,
                                  Aciklama = data.Description,
                                  Durum = data.Status,
                                  KategoriAdi = data.Category.Name,
                                  DosyaEki = data.Attachment,
                                  DepartmanAdi = data.Department.Name,
                                  TarafAdi = data.Side.Name,
                                  MusteriAdi = data.Customer.Name,
                                  YoneticiAdi = data.Manager.FirstName,
                                  OrganizatorAdi = data.Organizator.FirstName,
                                  ToplantıTarihi = data.MeetingDate.ToShortDateString(),
                                  ToplantıSaati = data.MeetingHour.ToShortTimeString(),
                                  PlanlananTarihi = data.PlannedDate.ToShortDateString(),
                                  PlanlananSaati = data.PlannedHour.ToShortTimeString(),
                                  BitişTarihi = data.FinishDate.ToShortDateString(),
                                  BitişSaati = data.FinishHour.ToShortTimeString(),
                                  RevizeTarihi = data.ReviseDate.ToShortDateString(),
                                  RevizeSaati = data.ReviseHour.ToShortTimeString(),
                                  GörüşmeKonusu = data.ConversationSubject,
                                  SponsorFirma = data.SupporterCompany,
                                  DestekleyenDoktor = data.SupporterDoctor,
                                  GörüşmeKatılımcıSayısı = data.ConversationAttendeeCount,
                                  PlanlananOrganizasyonTarihi = data.ScheduledOrganizationDate.ToShortDateString(),
                                  PlanlananOrganizasyonSaati = data.ScheduledOrganizationHour.ToShortTimeString(),
                                  MailKonuları = data.MailingSubjects,
                                  PosterKonuları = data.PosterSubjects,
                                  PosterSayısı = data.PosterCount,
                                  UzaktanEğitim = data.Elearning,
                                  YapılanTaramaTürleri = data.TypesOfScans,
                                  YapılanTaramalardakiASOSayısı = data.AsoCountInScans,
                                  OrganizasyonTürleri = data.TypesOfOrganizations,
                                  OrganizasyonlardakiASOSayısı = data.AsoCountInOrganization,
                                  AşıOrganizasyonuTürleri = data.TypesOfVaccinationOrganization,
                                  AşıOrganizasyonundakiASOsayısı = data.AsoCountInVaccinationOrganization,
                                  AfişİçinBütçeMiktarı = data.AmountOfCompensantionForPoster,
                                  KurumsalVerimlilikRaporu = data.CorporateProductivityReport,
                                  OlusturmaTarihi = data.CreateDate,
                                  OlusturanKullanici = data.CreatedBy,
                                  guncellemeTarihi = data.UpdateDate,
                                  GuncelleyenKullanici = data.UpdatedBy
                              };
            grid.DataBind();
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=Yapilacaklar.xls");
            Response.ContentType = "application/ms-excel";
            Response.ContentEncoding = System.Text.Encoding.Unicode;
            Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());

            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            grid.RenderControl(hw);
            Response.Write(sw.ToString());
            Response.End();
        }

        public void ExportToCsv()
        {
            StringWriter sw = new StringWriter();
            sw.WriteLine("Baslik,Aciklama,Durum,KategoriAdi,DosyaEki,DepartmanAdi,TarafAdi,MusteriAdi,YoneticiAdi,OrganizatorAdi,ToplantıTarihi,ToplantıSaati,PlanlananTarihi,PlanlananSaati,BitişTarihi,BitişSaati,RevizeTarihi,RevizeSaati,GörüşmeKonusu,SponsorFirma,DestekleyenDoktor,GörüşmeKatılımcıSayısı,PlanlananOrganizasyonTarihi,PlanlananOrganizasyonSaati,MailKonuları,PosterKonuları,PosterSayısı,UzaktanEğitim,YapılanTaramaTürleri,YapılanTaramalardakiASOSayısı,OrganizasyonTürleri,OrganizasyonlardakiASOSayısı,AşıOrganizasyonuTürleri,AşıOrganizasyonundakiASOsayısı,AfişİçinBütçeMiktarı,KurumsalVerimlilikRaporu,OlusturmaTarihi,OlusturanKullanici,guncellemeTarihi,GuncelleyenKullanici");
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment;filename=Yapilacaklar.csv");
            Response.ContentType = "text/csv";
            Response.ContentEncoding = System.Text.Encoding.Unicode;
            Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());

            var toDoItem = db.toDoItems.ToList();
            foreach (var item in toDoItem)
            {
                sw.WriteLine(string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24},{25},{26},{27},{28},{29},{30},{31},{32},{33},{34},{35},{36},{37},{38},{39}",
                    item.Title,
                    item.Description,
                    item.Status,
                    item.Category.Name,
                    item.Attachment,
                    item.Department.Name,
                    item.Side.Name,
                    item.Customer.Name,
                    item.Manager.FirstName,
                    item.Organizator.FirstName,
                    item.MeetingDate.ToShortDateString(),
                    item.MeetingHour.ToShortTimeString(),
                    item.PlannedDate.ToShortDateString(),
                    item.PlannedHour.ToShortTimeString(),
                    item.FinishDate.ToShortDateString(),
                    item.FinishHour.ToShortTimeString(),
                    item.ReviseDate.ToShortDateString(),
                    item.ReviseHour.ToShortTimeString(),
                    item.ConversationSubject,
                    item.SupporterCompany,
                    item.SupporterDoctor,
                    item.ConversationAttendeeCount,
                    item.ScheduledOrganizationDate.ToShortDateString(),
                    item.ScheduledOrganizationHour.ToShortTimeString(),
                    item.MailingSubjects,
                    item.PosterSubjects,
                    item.PosterCount,
                    item.Elearning,
                    item.TypesOfScans,
                    item.AsoCountInScans,
                    item.TypesOfOrganizations,
                    item.AsoCountInOrganization,
                    item.TypesOfVaccinationOrganization,
                    item.AsoCountInVaccinationOrganization,
                    item.AmountOfCompensantionForPoster,
                    item.CorporateProductivityReport,
                    item.CreateDate,
                    item.CreatedBy,
                    item.UpdateDate,
                    item.UpdatedBy
                    ));
            }
            Response.Write(sw.ToString());
            Response.End();
        }

    }
}
