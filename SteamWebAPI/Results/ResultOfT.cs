namespace SteamWebAPI.Results;

/// <summary>
/// Represents the outcome of an operation that produces a value of type <typeparamref name="T"/> and can fail in an expected way.
/// </summary>
/// <typeparam name="T">The type of the value produced on success.</typeparam>
public sealed class Result<T> : Result
{
    private readonly T _value = default!;

    /// <summary>The value produced by the operation. Throws if accessed on a failed result.</summary>
    public T Value =>
        IsSuccess ? _value : throw new InvalidOperationException("Cannot access Value of a failed result. Check IsSuccess first.");

    private Result(T value) : base(true, null) => _value = value;

    private Result(Error error) : base(false, error)
    {
    }

    /// <summary>Creates a successful <see cref="Result{T}"/> carrying <paramref name="value"/>.</summary>
    public static Result<T> Success(T value) => new(value);

    /// <summary>Creates a failed <see cref="Result{T}"/> carrying the given <paramref name="error"/>.</summary>
    public static new Result<T> Failure(Error error) => new(error ?? throw new ArgumentNullException(nameof(error)));

    /// <summary>Returns the value on success, or <paramref name="fallback"/> on failure.</summary>
    public T GetValueOrDefault(T fallback) => IsSuccess ? _value : fallback;

    /// <summary>Implicitly converts a value into a successful <see cref="Result{T}"/>.</summary>
    public static implicit operator Result<T>(T value) => Success(value);
}
