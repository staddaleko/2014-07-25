using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WcfService1;


namespace WcfService4
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Dziennik : IService2
    {
        public DataClasses1DataContext db = new DataClasses1DataContext();

        public IService1 studentSvc;
        public Dziennik() //kod skopiowany ze wczorajszej aplikacji
        {
            EndpointAddress address = new EndpointAddress(new Uri("Net.tcp://localhost:2202/Service1"));

            NetTcpBinding binding = new NetTcpBinding();

            ChannelFactory<IService1> factory = new ChannelFactory<IService1>(binding, address);
            studentSvc = factory.CreateChannel();
        }

        public Oceny GetOcena(int id_s, int id_p)
        {
            Oceny O = new Oceny();
            var query = (from o in db.Ocenies
                         where o.ID_Oceny == id_s && o.ID_Przedmiotu == id_p
                         select new { o.ID_Oceny, o.Wartosc }).ToList();
            O.ID_Oceny = query[0].ID_Oceny;
            O.ID_Przedmiotu = id_p;
            O.ID_Studenta = id_s;
            O.Wartosc = query[0].Wartosc;
            return O;
        }

        public List<WcfService4.Przedmioty.KrotkaOcen> GetOcena(int id_s)
        {
            List<WcfService4.Przedmioty.KrotkaOcen> Lista_o = new List<WcfService4.Przedmioty.KrotkaOcen>();
            Lista_o = (from o in db.Ocenies
                       join p in db.Przedmioties on o.ID_Przedmiotu equals p.ID_Przedmiotu
                       where o.ID_Studenta == id_s
                       select new WcfService4.Przedmioty.KrotkaOcen(p.Nazwa_przedmiotu, o.Wartosc)).ToList();
            return Lista_o;
        }

        public int SetOcena(int id_s, int id_p, int wartosc_o)
        {
            Oceny O = new Oceny();
            O.ID_Przedmiotu = id_p;
            O.ID_Studenta = id_s;
            O.Wartosc = wartosc_o;
            db.Ocenies.InsertOnSubmit(O);
            db.SubmitChanges();
            var query = (from o in db.Ocenies
                         where o.ID_Studenta == id_s && o.ID_Przedmiotu == id_p && o.Wartosc == wartosc_o
                         select o.ID_Oceny).ToList();
            return query[0];
        }

        public Przedmioty GetPrzedmiot(int id_p)
        {
            Przedmioty P = new Przedmioty();
            var query = (from p in db.Przedmioties
                         where p.ID_Przedmiotu == id_p
                         select new { p.ID_Przedmiotu, p.Nazwa_przedmiotu, p.Ocenies }).ToList();
            P.ID_Przedmiotu = id_p;
            P.Nazwa_przedmiotu = query[0].Nazwa_przedmiotu;
            return P;
        }

        public int SetPrzedmiot(string nazwa_p)
        {
            Przedmioty P = new Przedmioty();
            P.Nazwa_przedmiotu = nazwa_p;
            db.Przedmioties.InsertOnSubmit(P);
            db.SubmitChanges();
            var query = (from p in db.Przedmioties
                         where p.Nazwa_przedmiotu == nazwa_p
                         select p.ID_Przedmiotu).ToList();
            return query[0];
        }

        public int SetStudent (string imie, string nazwisko)
        {
            return studentSvc.SetStudent(imie, nazwisko);
        }
        
        public StudentKlasa GetStudent(int id)
        {
            return studentSvc.GetStudent(id);
        }
    }
}
