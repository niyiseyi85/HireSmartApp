using AutoMapper;
using Castle.Core.Resource;
using HireSmartApp.Core.Data.Repository.IRepository;
using HireSmartApp.Core.Models.Domain;
using HireSmartApp.Core.Models.DTO.ApplicationProgramDto;
using HireSmartApp.Core.Sevices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HireSmartApp.UnitTest.Service
{
    public class ProgramServiceTest
    {
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IProgramRepository> _programRepositoryMock;
        private readonly ProgramService _programService;

        public ProgramServiceTest()
        {
            _mapperMock = new Mock<IMapper>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _programRepositoryMock = new Mock<IProgramRepository>();
            _programService = new ProgramService(_unitOfWorkMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task CreateProgramAsync_ShouldReturnSuccessResponse_WhenProgramIsCreated()
        {
            // Arrange
            var request = new AddApplicationProgramDto { };
            var program = new ApplicationProgram { };

            _mapperMock.Setup(m => m.Map<ApplicationProgram>(request)).Returns(program);
            _unitOfWorkMock.Setup(u => u.ProgramRepository.Add(program)).Returns((Task<ApplicationProgram>)Task.CompletedTask);

            // Act
            var result = await _programService.CreateProgramAsync(request);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(Core.Extensions.Constants.UserConstants.Messages.UserSavedSuccessful, result.Value.Message);
            _unitOfWorkMock.Verify(u => u.ProgramRepository.Add(program), Times.Once);
        }

        [Fact]
        public async Task CreateProgramAsync_ShouldReturnFailureResponse_WhenExceptionIsThrown()
        {
            // Arrange
            var request = new AddApplicationProgramDto { };
            var program = new ApplicationProgram { };

            _mapperMock.Setup(m => m.Map<ApplicationProgram>(request)).Returns(program);
            _unitOfWorkMock.Setup(u => u.ProgramRepository.Add(program)).ThrowsAsync(new Exception("Test Exception"));

            // Act
            var result = await _programService.CreateProgramAsync(request);

            // Assert
            Assert.False(result.Value.IsSuccessful);
            Assert.Contains("An error has occurred", result.Value.Message);
        }
        [Fact]
        public async Task GetAllProgramsAsync_ShouldReturnPrograms_WhenProgramsExist()
        {
            // Arrange
            var programs = new List<ApplicationProgram>
        {
            new ApplicationProgram {  },
            new ApplicationProgram {  }
        };

            var mappedPrograms = new GetApplicationProgramDto { };

            _unitOfWorkMock.Setup(u => u.ProgramRepository).Returns(_programRepositoryMock.Object);
            _programRepositoryMock.Setup(x => x.GetAll(
            It.IsAny<Expression<Func<ApplicationProgram, bool>>>(),
            It.IsAny<Func<IQueryable<ApplicationProgram>, IOrderedQueryable<ApplicationProgram>>>(),
            It.IsAny<string>())).ReturnsAsync(programs);
            _mapperMock.Setup(m => m.Map<GetApplicationProgramDto>(programs)).Returns(mappedPrograms);

            // Act
            var result = await _programService.GetAllProgramsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Value.IsSuccessful);
            Assert.Equal(mappedPrograms, result.Value.Data);
            _mapperMock.Verify(m => m.Map<GetApplicationProgramDto>(programs), Times.Once);
        }

        [Fact]
        public async Task GetAllProgramsAsync_ShouldReturnEmptyPrograms_WhenNoProgramsExist()
        {
            // Arrange
            var programs = new List<ApplicationProgram>();
            var mappedPrograms = new GetApplicationProgramDto { /* Initialize with test data */ };

            _unitOfWorkMock.Setup(u => u.ProgramRepository).Returns(_programRepositoryMock.Object);
            _programRepositoryMock.Setup(x => x.GetAll(
            It.IsAny<Expression<Func<ApplicationProgram, bool>>>(),
            It.IsAny<Func<IQueryable<ApplicationProgram>, IOrderedQueryable<ApplicationProgram>>>(),
            It.IsAny<string>())).ReturnsAsync(programs);
            _mapperMock.Setup(m => m.Map<GetApplicationProgramDto>(programs)).Returns(mappedPrograms);

            // Act
            var result = await _programService.GetAllProgramsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Value.IsSuccessful);
            Assert.Equal(mappedPrograms, result.Value.Data);
            _mapperMock.Verify(m => m.Map<GetApplicationProgramDto>(programs), Times.AtLeastOnce);
        }
    }
}
