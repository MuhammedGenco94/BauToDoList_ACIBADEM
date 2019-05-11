using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BauToDoList.Models
{
    public class ToDoItem : BaseEntity
    {
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        [StringLength(100)]
        [DisplayName("Başlık")]
        public string Title { get; set; }


        [StringLength(200)]
        [DisplayName("Açıklama")]
        public string Description { get; set; }


        [DisplayName("Durum")]
        public Status Status { get; set; }


        [DisplayName("Kategori Adı")]
        public int? CategoryId { get; set; }    //  ? İşaret, Id boş gelmesini engeller..
        [ForeignKey("CategoryId")]
        [DisplayName("Kategori")]
        public virtual Category Category { get; set; }


        [StringLength(200)]
        [DisplayName("Dosya Eki")]
        public string Attachment { get; set; }


        [DisplayName("Departman Adı")]
        public int? DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        [DisplayName("Departman")]
        public virtual Department Department { get; set; }


        [DisplayName("Taraf Adı")]
        public int? SideId { get; set; }
        [ForeignKey("SideId")]
        [DisplayName("Taraf")]
        public virtual Side Side { get; set; }


        [DisplayName("Müşteri Adı")]
        public int? CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        [DisplayName("Müşteri")]
        public virtual Customer Customer { get; set; }


        [DisplayName("Yönetici Adı")]
        public int? ManagerId { get; set; }
        [ForeignKey("ManagerId")]
        [DisplayName("Yönetici")]
        public virtual Contact Manager { get; set; }


        [DisplayName("Organizatör Adı")]
        public int? OrganizatorId { get; set; }
        [ForeignKey("OrganizatorId")]
        [DisplayName("Organizatör")]
        public virtual Contact Organizator { get; set; }


        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        [DisplayName("Toplantı Tarihi")]
        public DateTime MeetingDate { get; set; }

        [DataType(DataType.Time)]
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        [DisplayName("Toplantı Saati")]
        public DateTime MeetingHour { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        [DisplayName("Planlanan Tarihi")]
        public DateTime PlannedDate { get; set; }

        [DataType(DataType.Time)]
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        [DisplayName("Planlanan Saati")]
        public DateTime PlannedHour { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        [DisplayName("Bitiş Tarihi")]
        public DateTime FinishDate { get; set; }

        [DataType(DataType.Time)]
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        [DisplayName("Bitiş Saati")]
        public DateTime FinishHour { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        [DisplayName("Revize Tarihi")]
        public DateTime ReviseDate { get; set; }

        [DataType(DataType.Time)]
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        [DisplayName("Revize Saati")]
        public DateTime ReviseHour { get; set; }
        
        [StringLength(200)]
        [DisplayName("Görüşme Konusu")]
        public string ConversationSubject { get; set; }

        [StringLength(100)]
        [DisplayName("Sponsor Firma")]
        public string SupporterCompany { get; set; }


        [StringLength(100)]
        [DisplayName("Destekleyen Doktor")]
        public string SupporterDoctor { get; set; }

        [Required(ErrorMessage = "Bu alan zorunludur.")]
        [DisplayName("Görüşme Katılımcı Sayısı")]
        public int ConversationAttendeeCount { get; set; }


        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        [DisplayName("Planlanan Organizasyon Tarihi")]
        public DateTime ScheduledOrganizationDate { get; set; }

        [DataType(DataType.Time)]
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        [DisplayName("Planlanan Organizasyon Saati")]
        public DateTime ScheduledOrganizationHour { get; set; }

        [StringLength(300)]
        [DisplayName("Mail Konuları")]
        public string MailingSubjects { get; set; }


        [StringLength(300)]
        [DisplayName("Poster Konuları")]
        public string PosterSubjects { get; set; }


        [Required(ErrorMessage = "Bu alan zorunludur.")]
        [DisplayName("Poster Sayısı")]
        public int PosterCount { get; set; }

        [StringLength(100)]
        [DisplayName("Uzaktan Eğitim")]
        public string Elearning { get; set; }

        [StringLength(100)]
        [DisplayName("Yapılan Tarama Türleri")]
        public string TypesOfScans { get; set; }


        [StringLength(100)]
        [DisplayName("Yapılan Taramalardaki ASO Sayısı")]
        public string AsoCountInScans { get; set; }


        [StringLength(100)]
        [DisplayName("Organizasyon Türleri")]
        public string TypesOfOrganizations { get; set; }


        [StringLength(100)]
        [DisplayName("Organizasyonlardaki ASO Sayısı")]
        public string AsoCountInOrganization { get; set; }


        [StringLength(100)]
        [DisplayName("Aşı Organizasyonu Türleri")]
        public string TypesOfVaccinationOrganization { get; set; }


        [StringLength(100)]
        [DisplayName("Aşı Organizasyonundaki ASO sayısı")]
        public string AsoCountInVaccinationOrganization { get; set; }


        [StringLength(100)]
        [DisplayName("Afiş İçin Bütçe Miktarı")]
        public string AmountOfCompensantionForPoster { get; set; }

        
        [DisplayName("Kurumsal Verimlilik Raporu")]
        public string CorporateProductivityReport { get; set; }
    }
}