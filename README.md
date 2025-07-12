# Bilgisayar Parçaları Satış Simülasyonu

Bu proje, C# Windows Forms uygulaması kullanılarak geliştirilmiş, temel bir bilgisayar parçaları satış sistemi simülasyonudur. Amacı, ürün listeleme, kullanıcı yönetimi, sanal sipariş oluşturma ve kredi kartı bilgilerini yönetme gibi temel e-ticaret akışlarını masaüstü ortamında göstermektir. Özellikle kullanıcı kayıt ve şifre işlemleri sırasında e-posta gönderimi özelliği ile dikkat çekmektedir. Bu proje, C# ve veritabanı entegrasyonu becerilerini sergileyen basit ve eğitici bir örnek niteliğindedir.

## İçindekiler

1.  [Proje Hakkında](#proje-hakkında)
2.  [Temel Özellikler](#temel-özellikler)
    * [Kullanıcı Yönetimi](#kullanıcı-yönetimi)
    * [Ürün Kataloğu](#ürün-kataloğu)
    * [Sipariş Sistemi](#sipariş-sistemi)
    * [Adres Yönetimi](#adres-yönetimi)
    * [Kredi Kartı Bilgileri Yönetimi](#kredi-kartı-bilgileri-yönetimi)
    * [Ürün Yorumları](#ürün-yorumları)
3.  [Kullanılan Teknolojiler](#kullanılan-teknolojiler)
4.  [Önemli Güvenlik Notu](#önemli-güvenlik-notu)

---

## Proje Hakkında

Bu simülasyon, bir e-ticaret platformunun masaüstü uygulamasında nasıl çalışabileceğini temel düzeyde göstermektedir. Kullanıcı kayıtlarından ürün siparişine kadar olan süreci adım adım izleyebilir, veritabanı etkileşimlerini gözlemleyebilirsiniz.

## Temel Özellikler

### Kullanıcı Yönetimi

* **Kayıt Sistemi**: Yeni kullanıcıların kolayca hesap oluşturmasına olanak tanır.
* **Giriş Sistemi**: Mevcut kullanıcıların güvenli bir şekilde sisteme erişimini sağlar.
* **Şifre Değiştirme/Sıfırlama**: Kullanıcıların şifrelerini güncelleme veya unutulmuş şifrelerini e-posta ile sıfırlama işlevselliği sunar.
* **E-posta Doğrulama/Bildirimleri**: Kayıt ve şifre sıfırlama gibi kritik işlemler sırasında kullanıcılara otomatik e-posta gönderimi yapar.

### Ürün Kataloğu

* **Ürün Listeleme**: Bilgisayar parçalarını (tür, marka, model, fotoğraf, link, fiyat, stok ve detaylı özellikler) listeleyebilir ve görüntüleyebilirsiniz.
* **Ürün Detayları**: Her ürünün ayrıntılı özelliklerini inceleme imkanı sunar.
* **Stok Yönetimi**: Ürünler için basit bir stok takibi sistemi içerir.

### Sipariş Sistemi

* **Sanal Sipariş Oluşturma**: Kullanıcıların ürünleri sepetlerine ekleyerek sanal siparişler oluşturmasına olanak tanır.
* **Sipariş Detayları**: Taksit bilgisi, sipariş tarihi ve ödenen miktar gibi detayların kaydını tutar.

### Adres Yönetimi

* Kullanıcılara ait birden fazla adres bilgisini kaydetme ve siparişler sırasında bu adresler arasından seçim yapabilme özelliği.

### Kredi Kartı Bilgileri Yönetimi

* Kullanıcıların kredi kartı bilgilerini **güvenli olmayan (simülasyon amaçlı)** bir ortamda saklama ve kullanma imkanı sunar.

### Ürün Yorumları

* Kullanıcıların satın aldıkları veya inceledikleri ürünler hakkında yorum yapabilmesi ve yıldız derecelendirmesi verebilmesi.

## Kullanılan Teknolojiler

* **C#**: Uygulamanın ana geliştirme dili.
* **Windows Forms**: Kullanıcı arayüzünün oluşturulması için.
* **Veritabanı (SQL Server/SQLite vb.)**: Kullanıcı, ürün, sipariş ve kredi kartı bilgilerinin depolanması için (detaylı veritabanı belirtilmemiş, genellikle C# Windows Forms projelerinde SQL Server veya SQLite kullanılır).
* **SMTP Protokolü**: E-posta gönderimi için (genellikle .NET'in SmtpClient sınıfı kullanılır).

## Önemli Güvenlik Notu

Bu proje bir **simülasyon** olup, özellikle kredi kartı bilgilerinin yönetimi konusunda **gerçek dünya güvenlik standartlarını karşılamamaktadır**. Gerçek bir e-ticaret uygulamasında hassas kullanıcı verileri (kredi kartı bilgileri, şifreler vb.) çok daha gelişmiş şifreleme, tokenizasyon ve güvenlik protokolleri kullanılarak işlenmelidir. Bu projede kullanılan yöntemler yalnızca eğitici ve gösterim amaçlıdır.
