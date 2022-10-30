using System.Linq;
using TestingBasics.Functionalities;
using Xunit;

namespace TestingBasics.Test;

public class UserManagementTest
{
    [Fact]
    public void Add_CreateUser()
    {
        // Arrange
        var userManagement = new UserManagement();

        // Act
        userManagement.Add(new(
                "Mohamad", "Lawand"
        ));

        // Assert
        var savedUser = Assert.Single(userManagement.AllUsers);
        Assert.NotNull(savedUser);
        Assert.Equal("Mohamad", savedUser.FirstName);
        Assert.Equal("Lawand", savedUser.LastName);
        Assert.NotEmpty(savedUser.Phone);
        Assert.False(savedUser.VerifiedEmail);
    }

    [Fact]
    public void Verify_VerifyEmailAddress()
    {
        // Arrange
        var userManagement = new UserManagement();

        // Act
        userManagement.Add(new(
                "Mohamad", "Lawand"
        ));

        var firstUser = userManagement.AllUsers.ToList().First();
        userManagement.VerifyEmail(firstUser.Id);

        // Assert
        var savedUser = Assert.Single(userManagement.AllUsers);
        Assert.True(savedUser.VerifiedEmail);
    }

    [Fact]
    public void Update_UpdateMobileNumber()
    {
        // Arrange
        var userManagement = new UserManagement();

        // Act
        userManagement.Add(new(
                "Mohamad", "Lawand"
        ));

        var firstUser = userManagement.AllUsers.ToList().First();
        firstUser.Phone = "+4409090909090";
        userManagement.UpdatePhone(firstUser);

        // Assert
        var savedUser = Assert.Single(userManagement.AllUsers);
        Assert.Equal("+4409090909090",savedUser.Phone);
    }
}
