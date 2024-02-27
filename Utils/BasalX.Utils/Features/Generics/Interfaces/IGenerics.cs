namespace BasaltX.Utils.Features.Generics.Interfaces;

public interface IGenerics
{
    string Serialize(dynamic param);
    T Deserialize<T>(dynamic param);
    dynamic HandleGenericResponse(string message);
}
