using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Microsoft.Extensions.Configuration
{
    public static class MarcoExtensionsConfiguration
    {
        public static T TryGet<T>(this IConfiguration configuration)
        {
            var typeName = typeof(T).Name;

            if (configuration.GetChildren().Any(item => item.Key == typeName))
                configuration = configuration.GetSection(typeName);

            T model = configuration.Get<T>();

            if (model == null)
                throw new InvalidOperationException($"Item de configuração não encontrado para o tipo {typeof(T).FullName}.");

            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(model, serviceProvider: null, items: null);

            if (!Validator.TryValidateObject(model, validationContext, validationResults, true))
            {
                string message = $"A validação de configuração para o tipo '{model.GetType().FullName}' falhou: " +
                string.Join(", ", validationResults.Select(r => $"\"{r.ErrorMessage}\""));

                throw new InvalidOperationException(message);
            }

            return model;
        }
    }
}

