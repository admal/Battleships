using Battleships.App.Services.Models;

namespace Battleships.Tests.Helpers;

public static class AssertHelpers
{
    public static void AssertResponseIsSuccessful<T>(ResponseModel<T> response)
    {
        Assert.That(response, Is.Not.Null);
        Assert.That(response.IsSuccess, Is.True);
        Assert.That(response.Result, Is.Not.Null);
    }

    public static void AssertResponseIsNotSuccessful<T>(ResponseModel<T> response, string errorMsgPart)
    {
        Assert.That(response, Is.Not.Null);
        Assert.That(response.IsSuccess, Is.False);
        Assert.That(response.Result, Is.Null);
        Assert.That(response.ErrorMessage, Is.Not.Empty);
        
        // we don't want to fail the test if the error message is not exactly the same
        Assert.That(response.ErrorMessage, Contains.Substring(errorMsgPart));
    }
}
