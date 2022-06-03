using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace net6Api.Models
{
    public class author
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }

        public List<Book> Books { get; set; }
    }
}
