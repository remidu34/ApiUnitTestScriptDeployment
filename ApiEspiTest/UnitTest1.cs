using ApiEpsi;
using ApiEpsi.Controllers;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ApiEspiTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test_GetAllReservation()
        {
            var mockRepo = new Mock<IManage>(MockBehavior.Strict);
            mockRepo.Setup(repo => repo.Reservations).Returns(Multiple());
            var controller = new ReservationController(mockRepo.Object);

            var result = controller.Get();

            result.Should().HaveCount(4);

            mockRepo.Verify(x => x.Reservations);
            mockRepo.VerifyNoOtherCalls();
        }

        private static IEnumerable<Reservation> Multiple()
        {
            var r = new List<Reservation>();
            r.Add(new Reservation()
            {
                Id = 1,
                Nom = "Test One",
                DateDebut = "SL1",
                DateFin = "EL1"
            });
            r.Add(new Reservation()
            {
                Id = 2,
                Nom = "Test Two",
                DateDebut = "SL2",
                DateFin = "EL2"
            });
            r.Add(new Reservation()
            {
                Id = 3,
                Nom = "Test Three",
                DateDebut = "SL3",
                DateFin = "EL3"
            });
            r.Add(new Reservation()
            {
                Id = 4,
                Nom = "Test Four",
                DateDebut = "SL4",
                DateFin = "EL4"
            });
            return r;
        }
    }
}