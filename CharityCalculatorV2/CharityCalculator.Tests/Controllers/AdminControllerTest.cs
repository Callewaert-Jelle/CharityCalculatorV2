using CharityCalculator.Tests.Data;
using CharityCalculatorV2.Controllers;
using CharityCalculatorV2.Models.Domain;
using CharityCalculatorV2.Models.TaxRateViewModel;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CharityCalculator.Tests.Controllers
{
    public class AdminControllerTest
    {
        private readonly AdminController _controller;
        private readonly Mock<IAppVariableRepository> _appVariableRepository;
        private readonly DummyDbContext _dummyContext;

        public AdminControllerTest()
        {
            _dummyContext = new DummyDbContext();
            _appVariableRepository = new Mock<IAppVariableRepository>();
            _controller = new AdminController(_appVariableRepository.Object);
        }

        [Fact]
        public void IndexGet_ReturnsViewWithViewbagData()
        {
            _appVariableRepository.Setup(m => m.GetBy("TaxRate")).Returns(_dummyContext.TaxRate);

            var actionResult = _controller.Index() as ViewResult;

            Assert.Equal(20.0, actionResult?.ViewData["CurrentTaxRate"]);
        }

        [Fact]
        public void IndexPost_ValidForm_SavesChanges()
        {
            // mock usermanager?
            _appVariableRepository.Setup(m => m.GetBy("TaxRate")).Returns(_dummyContext.TaxRate); // not needed (taxrate not shown)
            var taxrateViewModel = new TaxRateViewModel()
            {
                TaxRate = 25
            };
            _controller.Index(taxrateViewModel);

            AppVariable taxRate = _dummyContext.TaxRate;

            Assert.Equal("25", taxRate.Value);
            _appVariableRepository.Verify(m => m.SaveChanges(), Times.Once());


        }
    }
}
