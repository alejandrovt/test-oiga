using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace oiga.test.user.common
{
    [Table("userregister")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("full_name")]
        public string FullName { get; set; }

        [Column("user_name")]
        public string UserName { get; set; }
        public string Phone { get; set; }
    }
}
