using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebAPI_Levinci.Dtos
{
    public class AddUserDto
    {
        public string? strID { get; set; }

        public string? strUserName { get; set; }

        public string? strName { get; set; }

        public string? strRole { get; set; }

        public string? strEmail { get; set;}

        public string? strPassword { get; set; }
    }
}
