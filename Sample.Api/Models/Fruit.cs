using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sample.Api.Models;

[Table("fruits")]
public class Fruit
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")] public string Name { get; set; }
    [Column("description")] public string Description { get; set; }
    [Column("class")] public string Class { get; set; }
}
