using FluentValidation;
using FluentValidation.Validators;

using System;
using System.Net.Sockets;

namespace Aragas.Extensions.Options.FluentValidation.FluentValidation
{
    public interface IIsUrlTcpEndpointAvailableValidator : IPropertyValidator { }

    public class IsUrlTcpEndpointAvailableValidator<T> : PropertyValidator<T, string>, IIsUrlTcpEndpointAvailableValidator
    {
        public override string Name => "IsUrlTcpEndpointAvailableValidator";

        public override bool IsValid(ValidationContext<T> context, string value)
        {
            try
            {
                var uri = new Uri(value);
                var host = uri.GetComponents(UriComponents.Host, UriFormat.Unescaped);
                var portStr = uri.GetComponents(UriComponents.Port, UriFormat.Unescaped);
                if (!ushort.TryParse(portStr, out var port))
                {
                    context.MessageFormatter.AppendArgument("RequestException", new Exception("The port in the url is invalid!"));
                    return false;
                }

                using var client = new TcpClient();
                client.Connect(host, port);
                return true;
            }
            catch (Exception e)
            {
                context.MessageFormatter.AppendArgument("RequestException", e);
                return false;
            }
        }

        protected override string GetDefaultMessageTemplate(string errorCode) => "{PropertyName} Url TCP endpoint is not available! Message:\n{RequestException}";
    }
}