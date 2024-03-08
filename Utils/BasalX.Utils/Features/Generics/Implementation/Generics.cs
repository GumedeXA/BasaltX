using System.Text.Json;
using BasaltX.Utils.Features.Generics.Interfaces;

namespace BasaltX.Utils.Features.Generics.Implementation
{
    /// <summary>
    /// This Generics class will handle reusable functions
    /// </summary>
    internal class Generics : IGenerics
    {
        /// <summary>
        /// The json serializer options.
        /// </summary>
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        /// <summary>
        /// Initializes a new instance of the <see cref="Generics"/> class.
        /// </summary>
        public Generics()
        {
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            };
        }

        #region Public Methods(Abstraction)

        public string Serialize(dynamic param) => JsonSerializer.Serialize(param, _jsonSerializerOptions);

        /// <summary>
        ///  Deserializes the JSON to the specified .NET type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="param">Target object</param>
        /// <returns></returns>
        public T Deserialize<T>(dynamic param) => JsonSerializer.Deserialize<T>(param, _jsonSerializerOptions);


        public dynamic HandleGenericResponse(string message) => Deserialize<dynamic>(CreateResponse(message));

        private string CreateResponse(string message) => Serialize(new { message });

        #endregion Public Methods(Abstraction)
    }
}
