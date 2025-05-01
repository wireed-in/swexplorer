using System.Collections.ObjectModel;

namespace swexplorer.application.Models;

/// <summary>
/// Represents the standard structure of an API response, 
/// encapsulating the outcome of an API request (success or failure), 
/// any relevant messages, and the data returned from the request.
/// </summary>
/// <typeparam name="T">The type of the data returned in the response.</typeparam>
public class ApiResponse<T>
{
    /// <summary>
    /// Indicates whether the API request was successful or not.
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// A message providing more details about the request's outcome.
    /// This can be an error message, success message, or other relevant information.
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// The actual data returned by the API. This could be the result of the operation or null.
    /// </summary>
    public T? Data { get; set; }

    /// <summary>
    /// List of errors returned by the API. This could be empty if no errors.
    /// </summary>
    public IEnumerable<string> Errors { get; set; } = [];

    /// <summary>
    /// Creates a successful response with the provided data and optional message.
    /// </summary>
    /// <param name="data">The data to return in the response.</param>
    /// <param name="message">An optional success message (default is "Request succeeded.").</param>
    /// <returns>An ApiResponse indicating success.</returns>
    public static ApiResponse<T> SuccessResponse(T data, string message = "Request succeeded.")
    {
        return new ApiResponse<T>
        {
            Success = true,
            Message = message,
            Data = data
        };
    }

    /// <summary>
    /// Creates an error response with the provided error message.
    /// </summary>
    /// <param name="message">List of errors to include in response.</param>
    /// <param name="message">The error message to return in the response.</param>
    /// <returns>An ApiResponse indicating failure.</returns>
    public static ApiResponse<T> ErrorResponse(IEnumerable<string> errors, string message = "Request failed.")
    {
        return new ApiResponse<T>
        {
            Success = false,
            Message = message,
            Errors = errors,
        };
    }
}