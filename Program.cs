using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2._1
{
    class Müşteri
    {
        public string müşteriadı;
        public int ürünsayısı;
        public Müşteri(string müşteriAdı, int ürünSayısı)
        {
            this.müşteriadı = müşteriAdı;
            this.ürünsayısı = ürünSayısı;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Black;

            string[] müşteriAdı = { "Ali", "Merve", "Veli", "Gülay", "Okan", "Zekiye", "Kemal", "Banu", "İlker", "Songül", "Nuri", "Deniz" };// Veriler ilgili
            int[] ürünSayısı = { 8, 11, 16, 5, 15, 14, 19, 3, 18, 17, 13, 15 };                                                              // dizilerde tutulur
            ArrayList arrayList = new ArrayList();
            int müşteriSayısı = 0, gListSayısı = 0;
            Müşteri müşteri;
            Random r = new Random();
            List<Müşteri> genericList;
            while(müşteriSayısı < müşteriAdı.Length)
            {
                genericList = new List<Müşteri>(); // Müşteri sınıfı nesnelerini tutmak üzere bir List oluşturulur.
                int genericListLength = r.Next(1,6); // 1-5 aralığında rastgele sayılar üretilip Generic Listenin eleman sayısı olarak atanır.
                for (int i = 0; i < genericListLength; i++)
                {
                    müşteri = new Müşteri(müşteriAdı[müşteriSayısı], ürünSayısı[müşteriSayısı]); // Dizilerdeki veriler ile Müşteri nesneleri türetilir.
                    genericList.Add(müşteri);
                    müşteriSayısı++;
                    if (müşteriSayısı == müşteriAdı.Length ) break; // Dizilerin sonuna gelindiyse döngü sonlandırılır.
                }
                arrayList.Add(genericList); // Müşteri nesnelerinden oluşan Generic List ArrayList'e eklenir.
                gListSayısı++;

                Console.WriteLine("Dinamik dizideki "+(gListSayısı)+". listenin elemanları:");
                Console.WriteLine("----------");
                for (int j = 0; j < genericList.Count; j++)
                {
                    Console.WriteLine(genericList[j].müşteriadı + ", " + genericList[j].ürünsayısı);
                }
                Console.WriteLine("----------");
            }
            double ort = (double)müşteriSayısı/gListSayısı; // Ortalama eleman sayısı, atama işlemleri sırasında işleyen sayaçlar aracılığıyla belirlenir.
            Console.WriteLine("Dinamik dizide bulunan eleman(List) sayısı: "+gListSayısı);
            Console.WriteLine("Listelerin ortalama eleman sayısı: "+ort);

            Yığıt yığıt = new Yığıt(müşteriSayısı);
            foreach(List<Müşteri> l in arrayList) // Oluşturulan bileşik veri yapısındaki her eleman yığıta eklenir.
            {
                foreach(Müşteri m in l)
                {
                    yığıt.Push(m);  
                }
            }
            Console.WriteLine("\nYığıt yazdırılıyor...\n");
            Console.WriteLine("**********");
            while (!yığıt.IsEmpty())
            {
                Müşteri m = yığıt.Pop();
                Console.WriteLine(m.müşteriadı+", "+m.ürünsayısı);
            }
            Console.WriteLine("**********");

            Kuyruk kuyruk = new Kuyruk(müşteriSayısı);
            foreach(List<Müşteri> l in arrayList)
            {
                foreach(Müşteri m in l)
                {
                    kuyruk.Insert(m);
                }
            }
            Console.WriteLine("\nKuyruk yazdırılıyor...\n");
            while (!kuyruk.IsEmpty())
            {
                Müşteri m = kuyruk.Remove();
                Console.Write("|"+m.müşteriadı + ", " + m.ürünsayısı+"|");
            }

            ÖncelikliKuyruk öncelikliKuyruk = new ÖncelikliKuyruk();
            foreach (List<Müşteri> l in arrayList)
            {
                foreach (Müşteri m in l)
                {
                    öncelikliKuyruk.Ekle(m);
                }
            }
            Console.WriteLine("\n\nÖncelikli Kuyruk(azalan sırada) yazdırılıyor...\n");
            while (!öncelikliKuyruk.BosMu())
            {
                Müşteri m = öncelikliKuyruk.Sil();
                Console.Write("|" + m.müşteriadı + ", " + m.ürünsayısı + "|");
            }

            // ORTALAMA İŞLEM SÜRESİ HESABI KUYRUK
            foreach (List<Müşteri> l in arrayList)
            {
                foreach (Müşteri m in l)    // Yazdırırken dizi boşaldığı için elemanlar tekrar eklendi.
                {
                    kuyruk.Insert(m);
                }
            }
            int toplamSüre1 = 0;
            for (int i = 0; i < kuyruk.Size(); i++)
            {
                int süre1 = 0;
                for (int j = 0; j <= i; j++)
                {
                    süre1 += kuyruk.PeekAt(j).ürünsayısı;
                }
                toplamSüre1 += süre1;
            }
            double ortSüreKuyruk = (double)toplamSüre1 / müşteriSayısı;
            Console.WriteLine("\n\nKuyruk sınıfı için,\n Toplam işlem süresi: "+toplamSüre1+"\n Ortalama işlem süresi: "+ortSüreKuyruk);

            // ORTALAMA İŞLEM SÜRESİ HESABI ÖNCELİKLİ KUYRUK ARTAN
            ÖncelikliKuyrukArtan öncelikliKuyrukArtan = new ÖncelikliKuyrukArtan();
            foreach (List<Müşteri> l in arrayList)
            {
                foreach (Müşteri m in l)
                {
                    öncelikliKuyrukArtan.Ekle(m);
                }
            }
            List<Müşteri> sıralıList = new List<Müşteri>();
            while (!öncelikliKuyrukArtan.BosMu())
            {
                Müşteri m = öncelikliKuyrukArtan.Sil();
                sıralıList.Add(m);
            }
            int toplamSüre2 = 0;
            for (int i = 0; i < sıralıList.Count; i++)
            {
                int süre2 = 0;
                for (int j = 0; j <= i; j++)
                {
                    süre2 += sıralıList.ElementAt(j).ürünsayısı;
                }
                toplamSüre2 += süre2;
            }
            double ortSüreÖncelikliKuyruk = (double)toplamSüre2 / müşteriSayısı;
            Console.WriteLine("\nÖncelikliKuyrukArtan sınıfı için,\n Toplam işlem süresi: " + toplamSüre2 + "\n Ortalama işlem süresi: " + ortSüreÖncelikliKuyruk);

            Console.ReadKey();
        }
        
    }
    class Yığıt
    {
        private int maxSize;
        private Müşteri[] yığıtDizisi;
        private int top;

        public Yığıt(int max)
        {
            maxSize = max;
            yığıtDizisi = new Müşteri[maxSize]; // Girilen parametre kadar elemandan oluşan dizi oluşturulur
            top = -1; // Herhangi bir eleman olmadığı için en üst indeks değeri -1 olarak belirlenir
        }
        public void Push(Müşteri m)
        {
            yığıtDizisi[++top] = m; // Top indeksi bir artırılır ve yeni gelen eleman dizide o indekse yerleşir.
        }
        public Müşteri Pop()
        {
            return yığıtDizisi[top--]; // En üstteki eleman döndürülür ve top indeksi 1 azaltılarak eleman diziden çıkartılmış olur.
        }
        public Müşteri Peek()
        {
            return yığıtDizisi[top];
        }
        public Boolean IsEmpty()
        {
            return (top == -1);
        }
        public Boolean IsFull()
        {
            return (top == maxSize - 1);
        }
    }
    class Kuyruk
    {
        private int maxSize;
        private Müşteri[] kuyrukDizisi;
        private int front;
        private int rear;
        private int nItems;

        public Kuyruk(int max)
        {
            maxSize = max;
            kuyrukDizisi = new Müşteri[maxSize];// Belirli sayıda eleman içeren kuyruk dizisi oluşturulur.
            front = 0;
            rear = -1;
            nItems = 0;
        }
        public void Insert(Müşteri m)
        {
            if (rear == maxSize - 1) rear = -1;// Eğer kuyruğun sonuna gelinmişse en başa dönmek için rear -1 değerini alır.
            kuyrukDizisi[++rear] = m; // Yeni gelen eleman kuyruğun arkasına eklenceği için rear değeri 1 artırılarak eleman o indekse yerleştirilir.
            nItems++;
        }
        public Müşteri Remove()
        {
            Müşteri temp = kuyrukDizisi[front++]; // FIFO yapısı olduğu için eleman önden silinir.
            if (front == maxSize) front = 0;
            nItems--;
            return temp;
        }
        public Müşteri PeekFront()
        {
            return kuyrukDizisi[front];
        }
        public Müşteri PeekAt(int x)
        {
            return kuyrukDizisi[x];   // Ortalama işlem süresi hesabında kullanılmak üzere girilen indeksteki elemanı döndürür.
        }
        public Boolean IsEmpty()
        {
            return (nItems == 0);
        }
        public Boolean IsFull()
        {
            return (nItems == maxSize);
        }
        public int Size()
        {
            return nItems;
        }
    }
    class ÖncelikliKuyruk
    {
        private List<Müşteri> önceliklist;
        private int elemanSayısı;
        public ÖncelikliKuyruk()
        {
            önceliklist = new List<Müşteri>(); // Elemanları tutacak List oluşturulur.
        }
        public void Ekle(Müşteri m)
        {
            önceliklist.Add(m);
            elemanSayısı++;
        }
        public Müşteri Sil() // Geldiği sırayla eklenmiş elemanlar azalan sırada silinir.
        {
            Müşteri max = önceliklist.ElementAt(0);
            int maxIndex = 0;                                   // En büyük değerli eleman ilk eleman olarak varsayılır.
            for (int i = 1; i < önceliklist.Count; i++)         // Dizi tek tek dolaşılarak varsayılan eleman ile karşılaştırma yapılır.
            {
                if (max.ürünsayısı < önceliklist.ElementAt(i).ürünsayısı) // Eğer arkadaki bir eleman daha büyük değere sahipse 
                {                                                         // yeni çıkarılacak eleman ve indeks değeri arkadaki olarak değiştirilir.
                    max = önceliklist.ElementAt(i);
                    maxIndex = i;
                }
            }
            önceliklist.RemoveAt(maxIndex); // Bulunan max değerli eleman listeden silinir.
            elemanSayısı--;
            return max; // Eleman döndürülür.
        }
        public Boolean BosMu()
        {
            return (önceliklist.Count == 0);
        }
        public int Boyut()
        {
            return elemanSayısı;
        }
    }
    class ÖncelikliKuyrukArtan
    {
        private List<Müşteri> önceliklist2;
        private int elemanSay;
        public ÖncelikliKuyrukArtan()
        {
            önceliklist2 = new List<Müşteri>();
        }
        public void Ekle(Müşteri m)
        {
            önceliklist2.Add(m);
            elemanSay++;
        }
        public Müşteri Sil()
        {
            Müşteri min = önceliklist2.ElementAt(0);
            int minIndex = 0;
            for (int i = 1; i < önceliklist2.Count; i++)  // ÖncelikliKuyruk sınıfında yapılan işlemler, en küçük elemanı silecek şekilde güncellenir.
            {
                if (min.ürünsayısı > önceliklist2.ElementAt(i).ürünsayısı)
                {
                    min = önceliklist2.ElementAt(i);
                    minIndex = i;
                }
            }
            önceliklist2.RemoveAt(minIndex);
            elemanSay--;
            return min;
        }
        public Boolean BosMu()
        {
            return (önceliklist2.Count == 0);
        }
        public int Boyut()
        {
            return elemanSay;
        }
    }
}
