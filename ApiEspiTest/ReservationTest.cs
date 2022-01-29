using ApiEpsi;
using ApiEpsi.Controllers;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Xunit;

namespace ApiEspiTest
{
    public class ReservationTest
    {
        [Fact]
        public void Test_GetAllReservation()
        {
            var mockManage = new Mock<IManage>(MockBehavior.Strict);
            mockManage.Setup(repo => repo.Reservations).Returns(Multiple());
            var controller = new ReservationController(mockManage.Object);

            var result = controller.Get();

            result.Should().HaveCount(4);

            mockManage.Verify(x => x.Reservations);
            mockManage.VerifyNoOtherCalls();
        }

        [Fact]
        public void Test_GETAReservations_BadRequest()
        {
            int id = 0;
            var mockManage = new Mock<IManage>(MockBehavior.Strict);
            mockManage.Setup(repo => repo[It.IsAny<int>()]).Returns<int>((id) => Single(id));
            var controller = new ReservationController(mockManage.Object);

            var result = controller.Get(id);

            Action action = () => result.Should().Be(HttpStatusCode.BadRequest);

            mockManage.VerifyNoOtherCalls();
        }

        [Fact]
        public void Test_GETAReservations_Ok()
        {
            int id = 1;
            var mockManage = new Mock<IManage>(MockBehavior.Strict);
            mockManage.Setup(repo => repo[It.IsAny<int>()]).Returns<int>((id) => Single(id));
            var controller = new ReservationController(mockManage.Object);

            var result = controller.Get(id);

            Action action = () => result.Value.Should().Be(HttpStatusCode.OK);

            mockManage.Verify(x => x[id]);
            mockManage.VerifyNoOtherCalls();
        }

        [Fact]
        public void Test_GETAReservations_NotFound()
        {
            int id = 4;
            var mockManage = new Mock<IManage>(MockBehavior.Strict);
            mockManage.Setup(repo => repo[It.IsAny<int>()]).Returns<int>((id) => Single(id));
            var controller = new ReservationController(mockManage.Object);

            var result = controller.Get(id);

            Action action = () => result.Value.Should().Be(HttpStatusCode.NotFound);

            mockManage.Verify(x => x[id]);
            mockManage.VerifyNoOtherCalls();
        }

        [Fact]
        public void Test_PostAddReservation_Ok()
        {
            Reservation r = new Reservation()
            {
                Id = 5,
                Nom = "Test Five",
                DateDebut = "DD5",
                DateFin = "DF5"
            };
            var mockManage = new Mock<IManage>();
            mockManage.Setup(repo => repo.AddReservation(It.IsAny<Reservation>())).Returns(r);
            var controller = new ReservationController(mockManage.Object);

            var result = controller.Post(r);

            using (new AssertionScope())
            {              
                result.Id.Should().Be(5);
                result.Nom.Should().Be("Test Five");
                result.DateDebut.Should().Be("DD5");
                result.DateFin.Should().Be("DF5");
            }
   
            mockManage.Verify(mockManage => mockManage.AddReservation(It.IsAny<Reservation>()));
            mockManage.VerifyNoOtherCalls();
        }

        [Fact]
        public void Test_PUTUpdateReservation()
        {
            Reservation r = new Reservation()
            {
                Id = 3,
                Nom = "new name",
                DateDebut = "DDNew",
                DateFin = "DFNew"
            };
            var mockManage = new Mock<IManage>(MockBehavior.Strict);
            mockManage.Setup(repo => repo.UpdateReservation(It.IsAny<Reservation>())).Returns(r);
            var controller = new ReservationController(mockManage.Object);

            var result = controller.Put(r);

            using (new AssertionScope())
            {
                result.Id.Should().Be(3);
                result.Nom.Should().Be("new name");
                result.DateDebut.Should().Be("DDNew");
                result.DateFin.Should().Be("DFNew");
            }

            mockManage.Verify(x => x.UpdateReservation(It.IsAny<Reservation>()));
            mockManage.VerifyNoOtherCalls();
        }

        [Fact]
        public void Test_DeleteReservation()
        {
            // Arrange
            var mockManage = new Mock<IManage>(MockBehavior.Strict);
            mockManage.Setup(repo => repo.DeleteReservation(It.IsAny<int>()));
            var controller = new ReservationController(mockManage.Object);

            controller.Delete(3);

            mockManage.Verify(x => x.DeleteReservation(It.IsAny<int>()));
            mockManage.VerifyNoOtherCalls();
        }
        #region Methods static
        private static Reservation Single(int id)
        {
            IEnumerable<Reservation> reservations = Multiple();
            return reservations.Where(a => a.Id == id).FirstOrDefault();
        }

        private static IEnumerable<Reservation> Multiple()
        {
            var r = new List<Reservation>();
            r.Add(new Reservation()
            {
                Id = 1,
                Nom = "Test One",
                DateDebut = "DD1",
                DateFin = "DF1"
            });
            r.Add(new Reservation()
            {
                Id = 2,
                Nom = "Test Two",
                DateDebut = "DD2",
                DateFin = "DF2"
            });
            r.Add(new Reservation()
            {
                Id = 3,
                Nom = "Test Three",
                DateDebut = "DD3",
                DateFin = "DF3"
            });
            r.Add(new Reservation()
            {
                Id = 4,
                Nom = "Test Four",
                DateDebut = "DD4",
                DateFin = "DF4"
            });
            return r;
        }
        #endregion
    }
}