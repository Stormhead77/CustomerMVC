using System.Data.SqlClient;
using CustomerDatalayer.Entities;
using CustomerDatalayer.Repositories;
using CustomerDatalayer.Tests.Entities;
using CustomerDatalayer.WebMvc.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CustomerDatalayer.WebMvc.Tests.Controllers;

public class CustomerControllerTest
{
    [Fact]
    public void ShouldCreateCustomerController()
    {
        var controller = new CustomerController(new CustomerRepository());

        controller.Should().NotBeNull();
    }

    [Fact]
    public void ShouldReturnListOfCustomers()
    {
        var customerControllerMock = new Mock<CustomerRepository>();
        customerControllerMock.Setup(repo => repo.GetAll()).Returns(new List<Customer>
        {
            CustomersRepositoryFixture.GetCustomer()
        });

        var controller = new CustomerController(customerControllerMock.Object);
        var customerModel = (controller.Index() as ViewResult)!.Model as List<Customer>;

        customerControllerMock.Verify(repo => repo.GetAll(), Times.AtLeastOnce);
        customerModel.Should().NotBeNull();
    }

    [Fact]
    public void ShouldDetailCustomer()
    {
        var customerControllerMock = new Mock<CustomerRepository>();
        customerControllerMock.Setup(repo => repo.Read(It.IsAny<int>()))
            .Returns(CustomersRepositoryFixture.GetCustomer());
        var customerController = new CustomerController(customerControllerMock.Object);
        
        var result = customerController.Details(123) as ViewResult;

        result!.Model.Should().BeEquivalentTo(CustomersRepositoryFixture.GetCustomer());
        customerControllerMock.Verify(repo => repo.Read(It.Is<int>(i => i == 123)), Times.AtLeastOnce);
    }

    [Fact]
    public void ShouldGetCreateCustomer()
    {
        var customerControllerMock = new Mock<CustomerRepository>();
        var customerController = new CustomerController(customerControllerMock.Object);
        var result = customerController.Create();

        result.Should().NotBeNull();
    }

    [Fact]
    public void ShouldPostCreateCustomer()
    {
        var customerControllerMock = new Mock<CustomerRepository>();
        customerControllerMock.Setup(repo => repo.Create(It.IsAny<Customer>()))
            .Returns(CustomersRepositoryFixture.GetCustomer());
        var customerController = new CustomerController(customerControllerMock.Object);

        var result = customerController.Create(CustomersRepositoryFixture.GetCustomer()) as RedirectToActionResult;

        result.Should().NotBeNull();
        customerControllerMock.Verify(repo => repo.Create(It.IsAny<Customer>()), Times.AtLeastOnce);
    }

    [Fact]
    public void ShouldPostCreateInvalidCustomer()
    {
        var customerControllerMock = new Mock<CustomerRepository>();
        customerControllerMock.Setup(repo => repo.Create(It.IsAny<Customer>())).Throws(new Exception());
        var customerController = new CustomerController(customerControllerMock.Object);

        var result = customerController.Create(new Customer()) as ViewResult;

        result.Should().NotBeNull();
        customerControllerMock.Verify(repo => repo.Create(It.IsAny<Customer>()), Times.AtLeastOnce);
    }

    [Fact]
    public void ShouldGetEditCustomer()
    {
        var customerControllerMock = new Mock<CustomerRepository>();
        customerControllerMock.Setup(repo => repo.Read(It.IsAny<int>()))
            .Returns(CustomersRepositoryFixture.GetCustomer());
        var customerController = new CustomerController(customerControllerMock.Object);

        var result = customerController.Edit(123) as ViewResult;

        result!.Model.Should().BeEquivalentTo(CustomersRepositoryFixture.GetCustomer());
        customerControllerMock.Verify(repo => repo.Read(It.Is<int>(i => i == 123)), Times.AtLeastOnce);
    }

    [Fact]
    public void ShouldPostEditCustomer()
    {
        var customerControllerMock = new Mock<CustomerRepository>();
        customerControllerMock.Setup(repo => repo.Update(It.IsAny<Customer>())).Returns(1);
        var customerController = new CustomerController(customerControllerMock.Object);

        var result = customerController.Edit(123, CustomersRepositoryFixture.GetCustomer()) as RedirectToActionResult;

        result.Should().NotBeNull();
        customerControllerMock.Verify(repo => repo.Update(It.IsAny<Customer>()), Times.AtLeastOnce);
    }

    [Fact]
    public void ShouldPostInvalidEditCustomer()
    {
        var customerControllerMock = new Mock<CustomerRepository>();
        customerControllerMock.Setup(repo => repo.Read(It.IsAny<int>()))
            .Returns(CustomersRepositoryFixture.GetCustomer());
        customerControllerMock.Setup(repo => repo.Update(It.IsAny<Customer>())).Throws(new Exception());
        var customerController = new CustomerController(customerControllerMock.Object);

        var result = customerController.Edit(123, new Customer()) as ViewResult;

        result!.Model.Should().BeEquivalentTo(CustomersRepositoryFixture.GetCustomer());
        customerControllerMock.Verify(repo => repo.Read(It.Is<int>(i => i == 123)), Times.AtLeastOnce);
        customerControllerMock.Verify(repo => repo.Update(It.IsAny<Customer>()), Times.AtLeastOnce);
    }

    [Fact]
    public void ShouldGetDeleteCustomer()
    {
        var customerControllerMock = new Mock<CustomerRepository>();
        customerControllerMock.Setup(repo => repo.Read(It.IsAny<int>()))
            .Returns(CustomersRepositoryFixture.GetCustomer());
        var customerController = new CustomerController(customerControllerMock.Object);

        var result = customerController.Delete(123) as ViewResult;

        result!.Model.Should().BeEquivalentTo(CustomersRepositoryFixture.GetCustomer());
        customerControllerMock.Verify(repo => repo.Read(It.Is<int>(i => i == 123)), Times.AtLeastOnce);
    }

    [Fact]
    public void ShouldPostDeleteCustomer()
    {
        var customerControllerMock = new Mock<CustomerRepository>();
        customerControllerMock.Setup(repo => repo.Delete(It.IsAny<int>())).Returns(1);
        var customerController = new CustomerController(customerControllerMock.Object);

        var result = customerController.Delete(123, CustomersRepositoryFixture.GetCustomer()) as RedirectToActionResult;

        result.Should().NotBeNull();
        customerControllerMock.Verify(repo => repo.Delete(It.Is<int>(i => i == 123)), Times.AtLeastOnce);
    }

    [Fact]
    public void ShouldPostInvalidDeleteCustomer()
    {
        var customerControllerMock = new Mock<CustomerRepository>();
        customerControllerMock.Setup(repo => repo.Delete(It.IsAny<int>())).Throws(new Exception());
        var customerController = new CustomerController(customerControllerMock.Object);

        var result = customerController.Delete(123, CustomersRepositoryFixture.GetCustomer()) as RedirectToActionResult;

        result.Should().NotBeNull();
        customerControllerMock.Verify(repo => repo.Delete(It.Is<int>(i => i == 123)), Times.AtLeastOnce);
    }
}