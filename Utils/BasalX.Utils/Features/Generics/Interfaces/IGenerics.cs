namespace BasaltX.Utils.Features.Generics.Interfaces;

/// <summary>
/// The generics interface.
/// </summary>
public interface IGenerics
{
    string Serialize(dynamic param);
    T Deserialize<T>(dynamic param);
    /// <summary>
    /// Handle generic response.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <returns>A dynamic</returns>
    dynamic HandleGenericResponse(string message);
}
