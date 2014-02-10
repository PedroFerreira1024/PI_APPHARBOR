using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.IO;
using System.ComponentModel.DataAnnotations; 

namespace IMobileDataModel
{
    
    public partial class Estate
    {
        [StringLength(60,MinimumLength=3),Required]
        public String nameShort { get; set; }

        public String descriptionLong { get; set; }
        public Photos photos { get; set; }
        [Required, DataType(DataType.Currency,ErrorMessage="O valor deste campo tem de ser inteiro positivo!")]
        public long pricePerWeek { get; set; }
        [Required]
        public Location location { get; set; }
        [Required]
        public Characteristics characteristics { get; set; }
        public float averageRate;
        public List<Testimonial> testimonials { get; set; }
        public Availability availability { get; set; }

        public Estate() {
            averageRate = 3;
            testimonials = new List<Testimonial>();
            photos = new Photos();
        }

        public void setImages(HttpPostedFileBase mainPhoto, List<HttpPostedFileBase> otherPhotoList, String pathBase)
        {

            Directory.CreateDirectory(pathBase);

            if (mainPhoto != null)
            {
                photos.mainPhoto =mainPhoto.FileName;
                mainPhoto.SaveAs(pathBase + photos.mainPhoto);
            }

            if (otherPhotoList != null)
            {
                foreach (var photo in otherPhotoList)
                {
                    if (photo != null)
                    {
                        photos.otherPhotos.Add(photo.FileName);
                    }
                }
            }
        }

    }

    public class Photos
    {
        public String mainPhoto { get; set; }
        public List<String> otherPhotos { get; set; }

        public Photos()
        {
            otherPhotos = new List<String>();
        }
    }

    public class Location
    {
        [StringLength(40,MinimumLength=3),Required]
        public String name { get; set; }
        public MapCoordinate coordinates { get; set; }
    }

    public class MapCoordinate
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
    }

    public class Characteristics
    {
        [Required, RegularExpression(@"^[0-9]*$", ErrorMessage = "O valor deste campo tem de ser inteiro positivo!")]
        public int capacity { get; set; }
        [Required, RegularExpression(@"^[0-9]*$", ErrorMessage = "O valor deste campo tem de ser inteiro positivo!")]
        public int dimension { get; set; }
        [Required, RegularExpression(@"^[0-9]*$", ErrorMessage = "O valor deste campo tem de ser inteiro positivo!")]
        public int divisions { get; set; }
        [Required, RegularExpression(@"^[0-9]*$", ErrorMessage = "O valor deste campo tem de ser inteiro positivo!")]
        public int rooms { get; set; }
    }

    public class Testimonial
    {
        public int stars { get; set; }
        public String description { get; set; }
        public DateTime day { get; set; }
        public String author { get; set; }

        public void setTestimonial(int stars,String description,String auth)
        {
            this.author=(author==null)?"Anonymous":author;
            this.description = description;
            this.stars=stars;
            this.day = DateTime.Now;
        }
    }

    public class Availability
    {
        public List<DateInterval> occupied;
    }

    public class DateInterval
    {
        public DateTime from;
        public DateTime to;
    }
}
