using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace oiga.test.user.common
{
    [Table("usersearch")]
    public class UserSearch
    {
        [Key]
        public int Id { get; set; }

        [Column("full_name")]
        public string FullName { get; set; }

        [Column("user_name")]
        public string UserName { get; set; }
    }
}
