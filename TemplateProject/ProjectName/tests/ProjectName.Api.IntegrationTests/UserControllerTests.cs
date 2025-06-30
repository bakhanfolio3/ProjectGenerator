//using ProjectName.Api.IntegrationTests.Common;
//using System;
//using System.Net;
//using FluentAssertions;
//using ProjectName.Application.Common.Responses;
//using ProjectName.Application.Features.Users;
//using ProjectName.Application.Features.Users.CreateUser;
//using ProjectName.Application.Features.Users.GetAllUsers;
//using ProjectName.Application.Features.Users.UpdateUser;
//using ProjectName.Domain.Entities.Common;
//using ProjectName.Domain.Entities.Enums;
//using System.Net.Http.Json;

//namespace ProjectName.Api.IntegrationTests;

//public class UserControllerTests : BaseTest
//{
    
//    public UserControllerTests(CustomWebApplicationFactory apiFactory) : base(apiFactory)
//    {
//    }
//    #region GET

//    [Fact]
//    public async Task Get_AllUsers_ReturnsOk()
//    {
//        // Act
//        var response = await GetAsync<PaginatedResult<GetUserResponse>>("/api/User");

//        // Assert
//        response.Should().NotBeNull();
//        response!.Result.Should().OnlyHaveUniqueItems();
//        response.Result.Should().HaveCount(3);
//        response.CurrentPage.Should().Be(1);
//        response.TotalItems.Should().Be(3);
//        response.TotalPages.Should().Be(1);
//    }

//    [Fact]
//    public async Task Get_AllUsersWithPaginationFilter_ReturnsOk()
//    {
//        // Act
//        var response = await GetAsync<PaginatedResult<GetUserResponse>>("/api/User", 
//            new GetAllUsersRequest(null, null, null, null, 1, 1));

//        // Assert
//        response.Should().NotBeNull();
//        response!.Result.Should().OnlyHaveUniqueItems();
//        response.Result.Should().HaveCount(1);
//        response.CurrentPage.Should().Be(1);
//        response.TotalItems.Should().Be(3);
//        response.TotalPages.Should().Be(3);
//    }

//    [Fact]
//    public async Task Get_AllUsersWithNegativePageSize_ReturnsOk()
//    {
//        // Act
//        var response = await GetAsync<PaginatedResult<GetUserResponse>>("/api/User", new GetAllUsersRequest(
//            null, null, null, null, 1, -1));

//        // Assert
//        response.Should().NotBeNull();
//        response!.Result.Should().OnlyHaveUniqueItems();
//        response.Result.Should().HaveCount(3);
//        response.CurrentPage.Should().Be(1);
//        response.TotalItems.Should().Be(3);
//        response.TotalPages.Should().Be(1);
//    }

//    [Fact]
//    public async Task Get_AllUsersWithNegativeCurrentPage_ReturnsOk()
//    {
//        // Act
//        var response = await GetAsync<PaginatedResult<GetUserResponse>>("/api/User", 
//            new GetAllUsersRequest(null, null, null, null, -1, 15));

//        // Assert
//        response.Should().NotBeNull();
//        response!.Result.Should().OnlyHaveUniqueItems();
//        response.Result.Should().HaveCount(3);
//        response.CurrentPage.Should().Be(1);
//        response.TotalItems.Should().Be(3);
//        response.TotalPages.Should().Be(1);
//    }

//    [Fact]
//    public async Task Get_ExistingUsersWithFilter_ReturnsOk()
//    {
//        // Act
//        var response = await GetAsync<PaginatedResult<GetUserResponse>>("/api/User", new GetAllUsersRequest("Corban", null, null, null, 1, 10)
//        {
//            FirstName = "Corban"
//        });        

//        // Assert
//        response.Should().NotBeNull();
//        response!.Result.Should().OnlyHaveUniqueItems();
//        response.Result.Should().HaveCount(1);
//        response.CurrentPage.Should().Be(1);
//        response.TotalItems.Should().Be(1);
//        response.TotalPages.Should().Be(1);
//    }


//    [Fact]
//    public async Task Get_NonExistingUsersWithFilter_ReturnsOk()
//    {

//        // Act
//        var response = await GetAsync<PaginatedResult<GetUserResponse>>("/api/User", new GetAllUsersRequest()
//        {
//            FirstName = "asdsadsadsadsadasdsasadsa"
//        });

//        // Assert
//        response.Should().NotBeNull();
//        response!.Result.Should().BeEmpty();
//        response.CurrentPage.Should().Be(1);
//        response.TotalItems.Should().Be(0);
//        response.TotalPages.Should().Be(0);
//    }

//    [Fact]
//    public async Task GetById_ExistingUser_ReturnsOk()
//    {
//        // Act
//        var response = await GetAsync<GetUserResponse>("/api/Hero/824a7a65-b769-4b70-bccb-91f880b6ddf1");

//        // Assert
//        response.Should().NotBeNull();
//        response!.Id.Should().NotBe(0);
//        response.Name.Should().NotBeNull();
//        response.UserType.Should().NotBeNull();
//    }

//    [Fact]
//    public async Task GetById_ExistingUser_ReturnsNotFound()
//    {
//        // Act
//        var response = await GetAsync($"/api/User/{Guid.NewGuid()}");

//        // Assert
//        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
//    }

//    #endregion

//    #region POST

//    [Fact]
//    public async Task Post_ValidHero_ReturnsCreated()
//    {
//        // Act
//        var newHero = new CreateUserRequest()
//        {
//            FirstName = "Name user success",
//            UserType = UserType.SuperAdmin,
//            //Individuality = "all for one"
            
//        };
//        var response = await PostAsync("/api/User", newHero);

//        // Assert
//        response.StatusCode.Should().Be(HttpStatusCode.OK);
//        var json = await response.Content.ReadFromJsonAsync<GetUserResponse>();
//        json.Should().NotBeNull();
//        json!.Id.Should().NotBe(0);
//        json.Name.Should().NotBeNull();
//        json.UserType.Should().NotBeNull();
//    }

//    [Fact]
//    public async Task Post_NamelessHero_ReturnsBadRequest()
//    {
//        // Act
//        var newHero = new CreateUserRequest()
//        {
//            //Individuality = "Individuality User badrequest",
            
//        };
//        var response = await PostAsync("/api/User", newHero);

//        // Assert
//        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
//    }
    
//    [Fact]
//    public async Task Post_Negative_Age_Hero_ReturnsBadRequest()
//    {
//        // Act
//        var newHero = new CreateUserRequest()
//        {
//            //Individuality = "Individuality User badrequest",
//            FirstName = "Test User",
//            //Age = -1
            
//        };
//        var response = await PostAsync("/api/Hero", newHero);

//        // Assert
//        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
//    }  

//    [Fact]
//    public async Task Post_EmptyHero_ReturnsBadRequest()
//    {
//        // Act
//        var newHero = new CreateUserRequest();
//        var response = await PostAsync("/api/User", newHero);

//        // Assert
//        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
//    }

//    #endregion


//    #region PUT

//    [Fact]
//    public async Task Update_ValidHero_Should_Return_Ok()
//    {
//        // Arrange
        

//        // Act
//        var newHero = new UpdateUserRequest()
//        {
//            FirstName = "Name hero success",
//            UserType = UserType.Other,
//            //Individuality = "Invisibility"
            
//        };
//        var response = await PutAsync("/api/User/1", newHero);

//        // Assert
//        response.StatusCode.Should().Be(HttpStatusCode.OK);
//    }


//    [Fact]
//    public async Task Put_NamelessHero_ReturnsBadRequest()
//    {
//        // Act
//        var newHero = new UpdateUserRequest()
//        {
//            UserType = UserType.Admin
//        };
//        var response = await PutAsync("/api/Hero/2", newHero);

//        // Assert
//        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
//    }

//    [Fact]
//    public async Task Put_Individualityless_ReturnsBadRequest()
//    {
//        // Act
//        var newHero = new UpdateUserRequest()
//        {
//            FirstName = "Name User badrequest"
//        };
//        var response = await PutAsync("/api/Hero/2", newHero);

//        // Assert
//        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
//    }

//    [Fact]
//    public async Task Put_EmptyHero_ReturnsBadRequest()
//    {
//        // Act
//        var newHero = new UpdateUserRequest();
//        var response = await PutAsync("/api/User/3", newHero);

//        // Assert
//        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
//    }

//    [Fact]
//    public async Task Put_InvalidHeroId_ReturnsNotFound()
//    {
//        // Act
//        var newHero = new UpdateUserRequest()
//        {
//            FirstName = "Name hero not found",
//            UserType = UserType.Other,
//            //Individuality = "one for all"
//        };
//        var response = await PutAsync($"/api/User/{0}", newHero);

//        // Assert
//        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
//    }

//    #endregion

//    #region DELETE

//    [Fact]
//    public async Task Delete_ValidHero_Returns_NoContent()
//    {
//        // Arrange
//        var response = await DeleteAsync("/api/User/3");

//        // Assert
//        response.StatusCode.Should().Be(HttpStatusCode.OK);
//    }
    
//    [Fact]
//    public async Task DeleteHero_EmptyId_Should_Return_BadRequest()
//    {
//        // Arrange
//        var response = await DeleteAsync($"/api/User/{0}");

//        // Assert
//        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
//    }

//    [Fact]
//    public async Task Delete_InvalidHero_ReturnsNotFound()
//    {
//        // Arrange
//        var response = await DeleteAsync($"/api/User/{0}");

//        // Assert
//        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
//    }

//    #endregion


//}