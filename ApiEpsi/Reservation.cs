namespace ApiEpsi
{
    public class Reservation
    {
        public int Id { get; set; }
        public string? Nom { get; set; }
        public string DateDebut { get; set; }
        public string DateFin { get; set; }

        /*public Reservation(int id, string nom, string datedebut, string datefin)
        {
            Id = id;
            Nom = nom;
            DateDebut = datedebut;
            DateFin = datefin;
        }*/
    }
}
