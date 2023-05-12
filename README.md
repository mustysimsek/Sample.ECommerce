# Proje
Stok kontrolü ile birlikte ürünlerin sepete eklenmesi

# Açıklama
Manuel DB Kurulumu olmadan çalışması için ürünleri ürünleri bellekte sakladım. Proje açılırken direk ürünler sistemde kaydediliyor ( GET /api/product endpointi ile de ürünleri listeyebilirsiniz.).
Sepet NoSql(Redis) üzerinde tutulmaktadır. Sepet'te Id bilgisi Guid tipinde tutulmaktadır. POST isteğinde Guid değer verseniz de yeni bir Guid oluşturacaktır. Response 'ta oluşacak sepet Id sine tekrardan post atacak olursanız
o sepeti güncelleyecektir(Aynı ürün ü eklediyseniz Miktar bilgisini girilen miktar kadar arttırır ve fiyatı günceller. Aynı ürün ile birlikte farklı bir ürün eklediyseniz yeni ürünü bir item olarak yeni bir item olarak ekleyip fiyatı güncelleyecektir).
Sepet Id 'si için Token alacağım servis olmadığından dolayı manuel giriş yapıyorum (Üyelik girişi gibi bir yapı ile bir şekilde tutulabilir).

# Proje Çalıştırılmadan Önce
Proje .Net 5 ve Docker'a ihtiyaç duymaktadır.

1. Projenin klasörüne bir komut işlemci ile gidildikten sonra "docker-compose up -d" kodu çalıştırılmalıdır
2. "Sample.ECommerce.Api" StartUp Project olarak seçilmelidir. Sonrasında proje açılacaktır.

# Kullanılan Teknolojiler
- .Net 6
- Entity Framework Core 6
- Repository Pattern
- Mapster
- Fluent Validation
- Custom Exception Middleware
- Docker
- Redis
