using Microsoft.Azure.Mobile.Server;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace EShope.API.DataObjects
{
    [Table("Product")]
    public class Product : EntityData
    {
        private static readonly char delimiter = ';';
        //public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(500)]
        public string Description { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        [ConcurrencyCheck]
        public int StockQuantity { get; set; }
        [DataType(DataType.ImageUrl)]
        public string ThumnailURL { get; set; }

        string _picturesURls = string.Empty;
        [NotMapped]
        public string[] PicturesURls { get { return _picturesURls.Split(delimiter); } set { _picturesURls = string.Join($"{delimiter}", value); } }
        //public ICollection<OrderItem> OrderProducts { get; set; }
        public class ProductConfiguration : EntityTypeConfiguration<Product>
        {
            public ProductConfiguration() {
                Property(p => p._picturesURls)
                    .IsUnicode(false)
                    .HasColumnType("varchar")
                    .HasColumnName("PicturesURls")
                    .HasMaxLength(200)
                    .IsOptional()
                    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            }
        }
    }
}