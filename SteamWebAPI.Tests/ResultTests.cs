using SteamWebAPI.Results;

namespace SteamWebAPI.Tests;

public class ResultTests
{
    [Fact]
    public void Success_IsSuccessTrue_IsFailureFalse()
    {
        var result = Result.Success();

        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
    }

    [Fact]
    public void Failure_IsFailureTrue_CarriesError()
    {
        var error = Error.Validation("bad input");
        var result = Result.Failure(error);

        Assert.True(result.IsFailure);
        Assert.False(result.IsSuccess);
        Assert.Same(error, result.Error);
    }

    [Fact]
    public void Success_AccessingError_Throws()
    {
        var result = Result.Success();

        Assert.Throws<InvalidOperationException>(() => result.Error);
    }

    [Fact]
    public void GenericSuccess_ValueIsAccessible()
    {
        var result = Result<int>.Success(42);

        Assert.True(result.IsSuccess);
        Assert.Equal(42, result.Value);
    }

    [Fact]
    public void GenericFailure_AccessingValue_Throws()
    {
        var result = Result<int>.Failure(Error.Network("offline"));

        Assert.True(result.IsFailure);
        Assert.Throws<InvalidOperationException>(() => result.Value);
    }

    [Fact]
    public void GenericFailure_AccessingError_ReturnsError()
    {
        var error = Error.RateLimit("slow down", 429);
        var result = Result<string>.Failure(error);

        Assert.Same(error, result.Error);
    }

    [Fact]
    public void GetValueOrDefault_ReturnsValue_OnSuccess()
    {
        var result = Result<int>.Success(7);

        Assert.Equal(7, result.GetValueOrDefault(-1));
    }

    [Fact]
    public void GetValueOrDefault_ReturnsFallback_OnFailure()
    {
        var result = Result<int>.Failure(Error.Http(500, "boom"));

        Assert.Equal(-1, result.GetValueOrDefault(-1));
    }

    [Fact]
    public void ImplicitConversion_FromValue_ProducesSuccess()
    {
        Result<string> result = "hello";

        Assert.True(result.IsSuccess);
        Assert.Equal("hello", result.Value);
    }

    [Theory]
    [InlineData(ErrorType.Http)]
    [InlineData(ErrorType.SteamApi)]
    [InlineData(ErrorType.Authentication)]
    [InlineData(ErrorType.Validation)]
    [InlineData(ErrorType.RateLimit)]
    [InlineData(ErrorType.Network)]
    [InlineData(ErrorType.Deserialization)]
    public void Error_FactoryMethods_SetExpectedType(ErrorType expectedType)
    {
        Error error = expectedType switch
        {
            ErrorType.Http => Error.Http(500, "server error"),
            ErrorType.SteamApi => Error.SteamApi("item not found"),
            ErrorType.Authentication => Error.Authentication("invalid key"),
            ErrorType.Validation => Error.Validation("missing param"),
            ErrorType.RateLimit => Error.RateLimit("too many requests"),
            ErrorType.Network => Error.Network("timed out"),
            ErrorType.Deserialization => Error.Deserialization("bad json"),
            _ => throw new ArgumentOutOfRangeException(nameof(expectedType)),
        };

        Assert.Equal(expectedType, error.Type);
    }

    [Fact]
    public void Error_ToString_IncludesStatusCodeWhenPresent()
    {
        var error = Error.Validation("bad input", 400);

        Assert.Contains("400", error.ToString());
        Assert.Contains("bad input", error.ToString());
    }

    [Fact]
    public void Error_ToString_OmitsStatusCodeWhenAbsent()
    {
        var error = Error.Network("offline");

        Assert.DoesNotContain("(", error.ToString());
    }

    [Fact]
    public void Error_Network_CanCarryUnderlyingException()
    {
        var inner = new InvalidOperationException("socket closed");
        var error = Error.Network("network failure", inner);

        Assert.Same(inner, error.Exception);
    }
}
