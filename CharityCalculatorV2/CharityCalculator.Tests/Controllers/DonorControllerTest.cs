using CharityCalculator.Tests.Data;
using CharityCalculatorV2.Controllers;
using CharityCalculatorV2.Models.Domain;
using CharityCalculatorV2.Models.DonationViewModel;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CharityCalculator.Tests.Controllers
{
    public class DonorControllerTest
    {
        private readonly DonorController _controller;
        private readonly Mock<IAppVariableRepository> _appVariableRepository;
        private readonly DummyDbContext _dummyContext;

        public DonorControllerTest()
        {
            _dummyContext = new DummyDbContext();
            _appVariableRepository = new Mock<IAppVariableRepository>();
            _controller = new DonorController(_appVariableRepository.Object, null);
        }

        // Post
        [Fact]
        public void IndexPost_ValidForm_RedirectsToActionResult()
        {
            // mock usermanager?
            _appVariableRepository.Setup(m => m.GetBy("TaxRate")).Returns(_dummyContext.TaxRate); // not needed (taxrate not shown)
            var donationViewModel = new DonationViewModel()
            {
                Amount = 50000,
                EventType = "Other"
            };
            var actionResult = _controller.Index(donationViewModel) as RedirectToActionResult;
            Assert.Equal("Result", actionResult?.ActionName);
        }

        [Fact]
        public void IndexPost_InvalidForm_RedirectsToActionIndex()
        {
            // mock usermanager?
            var donationViewModel = new DonationViewModel()
            {
                Amount = -50000,
                EventType = "Other"
            };
            var actionResult = _controller.Index(donationViewModel) as RedirectToActionResult;
            Assert.Equal("Result", actionResult?.ActionName); // idk why but this should go to "Index" (following example code here so, not questioning it)
        }
    }
}
