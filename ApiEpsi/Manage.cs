namespace ApiEpsi
{
    public class Manage : IManage
    {
        private Dictionary<int, Reservation> items;

        public Manage()
        {
            items = new Dictionary<int, Reservation>();
            new List<Reservation> {
                new Reservation {Id=1, Nom = "Alessandra Quentin", DateDebut = "Date de début", DateFin ="Date de fin" },
                new Reservation {Id=2, Nom = "Barchi Mehdi", DateDebut = "Date de début", DateFin ="Date de fin" },
                new Reservation {Id=3, Nom = "Baran Lola", DateDebut = "Date de début", DateFin ="Date de fin" },
                new Reservation {Id=4, Nom = "Arnaud Rémi", DateDebut = "Date de début", DateFin = "Date de fin"}
                }.ForEach(r => AddReservation(r));
        }

        public Reservation this[int id] => items.ContainsKey(id) ? items[id] : null;

        public IEnumerable<Reservation> Reservations => items.Values;

        public Reservation AddReservation(Reservation reservation)
        {
            if (reservation.Id == 0)
            {
                int key = items.Count;
                while (items.ContainsKey(key)) { key++; };
                reservation.Id = key;
            }
            items[reservation.Id] = reservation;
            return reservation;
        }

        public void DeleteReservation(int id) => items.Remove(id);

        public Reservation UpdateReservation(Reservation reservation) => AddReservation(reservation);
    }
}
