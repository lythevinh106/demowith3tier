using DTOShared.Validation;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace MydemoFirst.Services.StorageServices
{
    public class BlobRequest
    {
        [Display(Name = "File")]
        [Required(ErrorMessage = "Please select a {0}.")]

        [FileExtension(new string[] { ".jpg", ".jpeg", ".png" })]

        //MaxSize 5MB

        [MaxFileSize(100)]

        public IFormFile? file { get; set; }


    }
    public class BlobDelete
    {
        [Display(Name = "Tên File")]
        [Required(ErrorMessage = " {0} Là Bắt Buộc .")]
        public string BlobFileName { get; set; }


    }


    public class BlobDownload
    {
        [Display(Name = "Tên File")]
        [Required(ErrorMessage = " {0} Là Bắt Buộc .")]
        public string BlobFileName { get; set; }


    }




    public class BlobObject
    {

        public string? ContentType { get; set; }
        public Stream? Content { get; set; }


    }
}
