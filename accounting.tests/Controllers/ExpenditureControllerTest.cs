using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Accounting.WS.Controllers;
using Accounting.WS.Models.DB;
using Accounting.WS.Models;
using Accounting.WS.Repositories;
using Accounting.WS.Test.Models.DB;
using Moq;
using Xunit;

namespace Accounting.WS.Test.Controllers
{
    public class ExpenditureControllerTest : Controller
    {
        private ExpenditureController _expendController;
        private Mock<IExpenditureRepository> _expendRepositoryMock = new Mock<IExpenditureRepository>();
        private Mock<IReFundRepository> _refundRepositoryMock = new Mock<IReFundRepository>();

        public ExpenditureControllerTest()
        {
            _expendController = new ExpenditureController(_refundRepositoryMock.Object, _expendRepositoryMock.Object);
        }

        [Fact]
        public void Get_NotFound()
        {
            // Arrange
            _expendRepositoryMock.Setup(m => m.Get(1)).Returns(Task.FromResult<Expenditure>(null));

            // Act
            var result = _expendController.Get(1);

            // Assert
            Assert.NotNull(result);

            var objectResult = result as NotFoundResult;
            Assert.NotNull(objectResult);
        }
    }
}
