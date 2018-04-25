using Canducci.SqlKata.Dapper;
using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    [TableFrom("peoples")]
    public class People
    {
        [ColumnName("id")]
        [PrimaryKey(IsIncrement = true)]
        public int Id { get; set; }

        [ColumnName("name")]
        [Required(ErrorMessage = "Digite o nome")]
        public string Name { get; set; }

        [ColumnName("createdat")]
        [Required(ErrorMessage = "Digite a data")]
        [DataType(DataType.Date, ErrorMessage = "Data inválida")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreatedAt { get; set; }

        [ColumnName("active")]
        public bool Active { get; set; }
    }
}
