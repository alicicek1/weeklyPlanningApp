<h1>Weekly Planner</h1>

Projede iki adet executable uygulama bulunmaktadır. Bunlar; toDoApp.Api ve toDoApp.Command.

Uygulamalar veritabananı olarak toDoApp.Database kütüphanesi kullanmaktadır. İçerisinde basic işlemler ve tablo olarak
kullanılan List tipinde değişkenlere sahiptir.

<h3>toDoApp.Command</h3>
Bir console uygulamasıdır. Config dosyasından okuduğu client'lara istek atarak ilgili verileri veritabanına yazmaktan
sorumludur. Eğer yeni bir client yani provider dahil edilmek istenirse yalnızca config dosyasına eklenmesi yeterlidir.

<h3>toDoApp.Api</h3>
Web server endpointlerinin dönüş tipi sabit tutulmalıdır (DataResult). Tek bir endpointe sahiptir. Bu endpointin
sorumluluğu veritabanından okuduğu task'ları zorluk ve süresine göre ilgili developer'a atayarak haftalık planlama
yapmaktır. Bu endpointin response'unda TotalDurationString alanı yer almaktadır ve task'ların süre hesabına yaparak
mesela1 hafta 6 saat şeklinde dönüş sağlamaktadır. Tabi bunula birlikte developer'ların yapacakları task'larda
response'da yer almaktedır.
<br>
Web Api'da bulunan ResponseLoggingActionAttribute'ne sahip controller'ın response'ları loglanması sağlanmaktadır. Bu
attribute'da BaseController'da yer aldığı için bütün istekler loglanacaktır. Bunun yanında okenRequiredAttribute ile
header'dan token kontrolü yapılarak istenmeye isteklerin önüne geçilmesi sağlanışmıştır. Log, bug ya da genel tracing
için CorrelationIdMiddleware ile request oluşturulurken oluşan CorrelationId ile istek trace edilebilir.
