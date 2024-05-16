using AutoMapper;
using Castle.Core.Resource;
using HireSmartApp.Core.Data.Repository.IRepository;
using HireSmartApp.Core.Models.Domain;
using HireSmartApp.Core.Models.Domain.Authorization;
using HireSmartApp.Core.Models.DTO.UserDto;
using HireSmartApp.Core.Security;
using HireSmartApp.Core.Sevices;
using HireSmartApp.Core.Extensions.Constants;
using HireSmartApp.Core.Sevices.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HireSmartApp.UnitTest.Service
{
    public class ApplicationServiceTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IUserRepository> _userRepositorymock;
        private readonly Mock<IRoleRepository> _roleRepositorymock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IPasswordService> _passwordServiceMock;
        private readonly ApplicationService _applicationService;

        public ApplicationServiceTest()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _passwordServiceMock = new Mock<IPasswordService>();
            _applicationService = new ApplicationService(_mapperMock.Object, _passwordServiceMock.Object, _unitOfWorkMock.Object);
        }
        [Fact]
        public async Task CreateUserApplication_ShouldReturnFailure_WhenRoleTypeIsInvalid()
        {
            // Arrange
            var request = new AddUserDto { Email = "test@example.com", RoleType = RoleType.Candidate };

            // Act
            var result = await _applicationService.CreateUserApplication(request);

            // Assert
            Assert.False(result.Value.IsSuccessful);
            Assert.Equal(UserConstants.ErrorMessages.InvalidUserRole, result.Value.Message);
        }

        [Fact]
        public async Task CreateUserApplication_ShouldReturnFailure_WhenUserAlreadyExists()
        {
            // Arrange
            var request = new AddUserDto { Email = "test@example.com", RoleType = RoleType.Employer };
            var existingUser = new User { Email = "test@example.com" };

            _unitOfWorkMock.Setup(u => u.UserRepository).Returns(_userRepositorymock.Object);
            _userRepositorymock.Setup(repo => repo.GetFirstOrDefault(
                It.IsAny<Expression<Func<User, bool>>>(),
                It.IsAny<string>())).ReturnsAsync(existingUser);

            // Act
            var result = await _applicationService.CreateUserApplication(request);

            // Assert
            Assert.False(result.Value.IsSuccessful);
            Assert.Equal(UserConstants.ErrorMessages.UserExists, result.Value.Message);
        }

        [Fact]
        public async Task CreateUserApplication_ShouldCreateUser_WhenDataIsValid()
        {
            // Arrange
            var request = new AddUserDto { Email = "test@example.com", RoleType = RoleType.Employer, Password = "Password123" };
            var newUser = new User { Email = "test@example.com" };
            var role = new Role { RoleId = 1, Name = RoleType.Employer.ToString() };

            _unitOfWorkMock.Setup(u => u.UserRepository).Returns(_userRepositorymock.Object);
            _userRepositorymock.Setup(repo => repo.GetFirstOrDefault(
                It.IsAny<Expression<Func<User, bool>>>(),
                It.IsAny<string>())).ReturnsAsync(null as User);
            
            _unitOfWorkMock.Setup(u => u.RoleRepository).Returns(_roleRepositorymock.Object);
            _roleRepositorymock.Setup(repo => repo.GetFirstOrDefault(
                It.IsAny<Expression<Func<Role, bool>>>(),
                It.IsAny<string>())).ReturnsAsync(role);

            _mapperMock.Setup(m => m.Map<User>(request)).Returns(newUser);
            _passwordServiceMock.Setup(p => p.CreateSalt()).Returns("salt");
            _passwordServiceMock.Setup(p => p.CreateHash(request.Password, "salt")).Returns("hashedPassword");

            // Act
            var result = await _applicationService.CreateUserApplication(request);

            // Assert
            Assert.True(result.Value.IsSuccessful);
            Assert.Equal(UserConstants.Messages.UserSavedSuccessful, result.Value.Message);
            _unitOfWorkMock.Verify(u => u.UserRepository.Add(It.IsAny<User>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task CreateUserApplication_ShouldReturnFailure_WhenExceptionIsThrown()
        {
            // Arrange
            var request = new AddUserDto { Email = "test@example.com", RoleType = RoleType.Employer, Password = "Password123" };
            var newUser = new User { Email = "test@example.com" };
            var role = new Role { RoleId = 1, Name = RoleType.Employer.ToString() };

            _unitOfWorkMock.Setup(u => u.UserRepository).Returns(_userRepositorymock.Object);
            _userRepositorymock.Setup(repo => repo.GetFirstOrDefault(
                It.IsAny<Expression<Func<User, bool>>>(),
                It.IsAny<string>())).ReturnsAsync(null as User);

            _unitOfWorkMock.Setup(u => u.RoleRepository).Returns(_roleRepositorymock.Object);
            _roleRepositorymock.Setup(repo => repo.GetFirstOrDefault(
                It.IsAny<Expression<Func<Role, bool>>>(),
                It.IsAny<string>())).ReturnsAsync(role);

            _mapperMock.Setup(m => m.Map<User>(request)).Returns(newUser);
            _passwordServiceMock.Setup(p => p.CreateSalt()).Returns("salt");
            _passwordServiceMock.Setup(p => p.CreateHash(request.Password, "salt")).Returns("hashedPassword");
            _unitOfWorkMock.Setup(u => u.UserRepository.Add(It.IsAny<User>()))
                .ThrowsAsync(new Exception("Test Exception"));

            // Act
            var result = await _applicationService.CreateUserApplication(request);

            // Assert
            Assert.False(result.Value.IsSuccessful);
            Assert.Contains("An error has occured", result.Value.Message);
        }
    }
}
