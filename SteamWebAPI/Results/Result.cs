namespace SteamWebAPI.Results;

/// <summary>
/// Represents the outcome of an operation that can fail in an expected way, without using exceptions for control flow.
/// </summary>
public class Result
{
    /// <summary>Whether the operation succeeded.</summary>
    public bool IsSuccess { get; }

    /// <summary>Whether the operation failed.</summary>
    public bool IsFailure => !IsSuccess;

    private readonly Error? _error;

    /// <summary>The error describing why the operation failed. Throws if accessed on a successful result.</summary>
    public Error Error =>
        _error ?? throw new InvalidOperationException("Cannot access Error of a successful result. Check IsFailure first.");

    /// <summary>Initializes a new <see cref="Result"/>.</summary>
    protected Result(bool isSuccess, Error? error)
    {
        if (isSuccess && error is not null)
            throw new ArgumentException("A successful result cannot carry an error.", nameof(error));
        if (!isSuccess && error is null)
            throw new ArgumentException("A failed result must carry an error.", nameof(error));

        IsSuccess = isSuccess;
        _error = error;
    }

    /// <summary>Creates a successful <see cref="Result"/>.</summary>
    public static Result Success() => new(true, null);

    /// <summary>Creates a failed <see cref="Result"/> carrying the given <paramref name="error"/>.</summary>
    public static Result Failure(Error error) => new(false, error ?? throw new ArgumentNullException(nameof(error)));

    /// <summary>Creates a successful <see cref="Result{T}"/> carrying <paramref name="value"/>.</summary>
    public static Result<T> Success<T>(T value) => Result<T>.Success(value);

    /// <summary>Creates a failed <see cref="Result{T}"/> carrying the given <paramref name="error"/>.</summary>
    public static Result<T> Failure<T>(Error error) => Result<T>.Failure(error);
}
