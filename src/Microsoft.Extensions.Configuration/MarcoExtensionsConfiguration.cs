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
                throw new InvalidOperationException($"Configuration item not found for type {typeof(T).FullName}.");

            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(model, serviceProvider: null, items: null);

            if (!Validator.TryValidateObject(model, validationContext, validationResults, true))
            {
                string message = $"Configuration validation for type '{model.GetType().FullName}' failed: " +
                string.Join(", ", validationResults.Select(r => $"\"{r.ErrorMessage}\""));

                throw new InvalidOperationException(message);
            }

            return model;
        }
    }
}