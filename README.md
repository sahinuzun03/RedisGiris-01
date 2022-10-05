###  REDİS NEDİR :
##### REDİS - Remote Dictionary Server , ilişkisel olmayan anahtar/değer veri tabanlarını ve önbellekleri uygulamak için yaygın olarak kullanılan açık kaynaklı bir bellek içi veri deposudur.

##### Önemli bir noktadır çünkü, Redis teknik olarak bir veri yapısı deposudur VERİ TABANI DEĞİLDİR!!!

##### Redis , verileri HDD veya SSD yerine RAM üzerinde tutarlar bu sayede disklere erişim ihtiyacını ortadan kaldırarak I/O bağlantılarını önler ve daha CPU kullanan basit algortimalar ile verilere erişir.

#### Redis verileri <key,value> çifti olarak tutmaktadır, burada herbir anahtara denk gelen değerler farklı veri yapılarında tutulabilmektedir.


## REDİS ÖZELLİKLERİ 

### Yüksek Düzeyde Veri Yapısı :
#### Redis, değerler için beş olası veri seçeneği sunar. Bunlar karmalar, listeler, kümeler, diziler ve sıralanmış kümelerdir. 

### Üstün Performans
#### Bellek içi doğası, karmaşıklığın minimumda kalmasını sağlama taahhüdü ve olay tabanlı bir programlama modeli olması nedeniyle, uygulama okuma ve yazma için olağanüstü bir performansa sahiptir.

### Son Derece Hafiftir ve Bağımlılık Yoktur
#### Redis, ANSI C dilinde yazılmıştır ve hiçbir dış bağımlılıkla sınırlı değildir. Program, tüm POSIX ortamlarında mükemmel şekilde çalışır. Windows platformu Redis için resmi olarak desteklenmemektedir, ancak Microsoft tarafından aynı şekilde deneysel bir yapı sağlanmıştır.

### Yüksek Kullanılabilirlik
#### Redis, yüksek düzeyde veri kullanılabilirliği sağlamak için yerleşik desteğe sahiptir. Şu anda Redis Sentinel olarak adlandırılan ve kullanılabilir durumda olan, ancak halen devam eden bir proje olarak kabul edilen yüksek kullanılabilirlikli bir çözüm vardır.

## REDİSTE SAKLANAN VERİLERİN SÜREKLİ DEĞİŞEN VERİLER OLMAMASI GEREKMEKTEDİR. BUNUN NEDENİ VERİLER DEĞİŞTİĞİ ZAMAN BİZE HATAL VERİNİN GELMESİNE NEDEN OLABİLİR.
