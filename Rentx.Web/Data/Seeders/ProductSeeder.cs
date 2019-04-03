using Microsoft.EntityFrameworkCore;
using Rentx.Web.Common;
using Rentx.Web.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace Rentx.Web.Data.Seeders
{
    public static class ProductSeeder
    {
        public static async Task SeedDatabase(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (!await context.Products.AnyAsync())
            {
                var categories = await context.Categories.ToListAsync();

                await context.Products.AddAsync(new Product
                {
                    Title = "Фотоапарат Canon IXUS 185 Black",
                    Description = @"Кратки характеристики:
20 мегапикселов 1/2.3'' сензор (6.2mm x 4.5mm)
процесор: DIGIC 4+
файлов формат: JPEG
HD видеоклип (1280 х 720)
светлочувствителност: ISO 100-1600
скорост на затвора: 15сек. - 1/2000сек.
серия снимки: 0.8 кад./сек.
дисплей: 2.7"" 230 000 точки LCD
тегло: 126гр.",
                AvailableQuantity = 15,
                CategoryId = categories.FirstOrDefault(c => c.Name == CategoryConstants.CameraName).Id,
                Image = "canon1.jpg",
                Price = 229,
                });

                await context.Products.AddAsync(new Product
                {
                    Title = "Фотоапарат Canon PowerShot SX430 IS Черен",
                    Description = @"Кратки характеристики:
20.5 мегапиксела,1/2.3-inch sensors (6.17 x 4.55 mm)
обектив: 24 – 1080мм (35mm екв.) f/3.5-6.8
процесор: DIGIC 4+
45 x оптичен зуум
файлов формат: JPEG
HD видеоклип (1280 x 720)
светлочувствителност: ISO 100-1600
скорост на затвора: 15сек. - 1/4000сек.
серия снимки: 0.5 кад./сек.
дисплей: 3.0"" LCD дисплей
свързаност: Wi - Fi
NFC
тегло: 323гр.",
                    AvailableQuantity = 20,
                    CategoryId = categories.FirstOrDefault(c => c.Name == CategoryConstants.CameraName).Id,
                    Image = "canon2.jpg",
                    Price = 246,
                });

                await context.Products.AddAsync(new Product
                {
                    Title = "Фотоапарат Canon EOS 4000D тяло + Обектив Canon EF-s 18-55mm f/3.5-5.6 III",
                    Description = @"Кратки характеристики:
18 мегапикселов APS-C сензор (22.3mm x 14.9mm)
процесор: DIGIC 4+
файлов формат: JPEG, RAW
фокусни точки: 9
Full HD видеоклип (1920 х 1080)
светлочувствителност: ISO 100-6400 (разширен до 12800)
скорост на затвора: 30сек. - 1/4000сек.
серия снимки: 3 кад./сек.
дисплей: 2.7"", 230, 000 точки, LCD
свързаност: Wi - Fi
тегло: 436гр.",
                    AvailableQuantity = 20,
                    CategoryId = categories.FirstOrDefault(c => c.Name == CategoryConstants.CameraName).Id,
                    Image = "canon3.jpg",
                    Price = 579,
                });

                await context.Products.AddAsync(new Product
                {
                    Title = "Обектив Canon EF 50mm f/1.8 STM",
                    Description = @"Кратки характеристики:
 тип: Нормален светлосилен автофокусен обектив, подходящ за видео заснемане
съвместим с: Canon EF/EF-s байонет
фокусно разстояние: 50mm
светлосила: f/1.8
брой ламели: 7 заоблени
минимално предметно разстояние: 35см.
резба за филтри: 49мм.
размери: 69.2x39.2мм.
тегло: 160гр.",
                    AvailableQuantity = 5,
                    CategoryId = categories.FirstOrDefault(c => c.Name == CategoryConstants.LensName).Id,
                    Image = "canon-obektiv1.jpg",
                    Price = 579,
                });

                await context.Products.AddAsync(new Product
                {
                    Title = "Монопод Nest NT-123A",
                    Description = @"Кратки характеристики:
товароносимост 10 кг
максимална височина 1225 мм
минимална височина 350 мм
тегло 0.290 кг",
                    AvailableQuantity = 60,
                    CategoryId = categories.FirstOrDefault(c => c.Name == CategoryConstants.TripodName).Id,
                    Image = "stativ1.jpg",
                    Price = 35,
                });

                context.SaveChanges();
            }

        }
    }
}
