using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiEpsi.Controllers
{
    [Route("api/[controller]")]
    public class ReservationController : Controller
    {
        private IManage repository;
        public ReservationController(IManage repo) => repository = repo;

        [HttpGet]
        public IEnumerable<Reservation> Get() => repository.Reservations;

        [HttpGet("{id}")]
        public ActionResult<Reservation> Get(int id)
        {
            if (id == 0)
                return BadRequest("Value must be passed in the request body.");

            Reservation r = repository[id];

            if (r is null)
                return NotFound();

            return Ok(r);
        }

        [HttpPost]
        public Reservation Post([FromBody] Reservation res) =>
        repository.AddReservation(new Reservation
        {
            Nom = res.Nom,
            DateDebut = res.DateDebut,
            DateFin = res.DateFin
        });

        [HttpPut]
        public Reservation Put([FromBody] Reservation res) => repository.UpdateReservation(res);

        [HttpDelete("{id}")]
        public void Delete(int id) => repository.DeleteReservation(id);
    }
}
