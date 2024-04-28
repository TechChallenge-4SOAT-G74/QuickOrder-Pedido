using Microsoft.AspNetCore.Http;

namespace QuickOrderPedido.Application.Dtos.Base
{
    public class ErrorResultDetail
    {
        public string Code { get; }
        public string Message { get; }
        public int? StatusCode { get; set; }
        public List<ErrorResultDetail> Details { get; set; }

        public ErrorResultDetail(string code, string message = null)
        {
            Code = code;
            Message = message;
            Details = new List<ErrorResultDetail>();
        }

        public static readonly ErrorResultDetail INTERNAL_ERROR = new ErrorResultDetail("internal_error") { StatusCode = StatusCodes.Status500InternalServerError };
        public static readonly ErrorResultDetail INVALID_INPUT = new ErrorResultDetail("invalid_input") { StatusCode = StatusCodes.Status400BadRequest };
        public static readonly ErrorResultDetail BAD_REQUEST = new ErrorResultDetail("bad_request") { StatusCode = StatusCodes.Status400BadRequest };
        public static readonly ErrorResultDetail UNAUTHORIZED = new ErrorResultDetail("unauthorized") { StatusCode = StatusCodes.Status401Unauthorized };
        public static readonly ErrorResultDetail FORBIDDEN = new ErrorResultDetail("forbidden") { StatusCode = StatusCodes.Status403Forbidden };
        public static readonly ErrorResultDetail NOT_FOUND = new ErrorResultDetail("not_found") { StatusCode = StatusCodes.Status404NotFound };
        public static readonly ErrorResultDetail BAD_GATEWAY = new ErrorResultDetail("bad_gateway") { StatusCode = StatusCodes.Status502BadGateway };
        public static readonly ErrorResultDetail REQUEST_TIMEOUT = new ErrorResultDetail("request_timeout") { StatusCode = StatusCodes.Status408RequestTimeout };
        public static readonly ErrorResultDetail SERVICE_UNAVAILABLE = new ErrorResultDetail("service_unavailable") { StatusCode = StatusCodes.Status503ServiceUnavailable };
        public static readonly ErrorResultDetail PREDICATE_VALIDATOR = new ErrorResultDetail("PredicateValidator") { StatusCode = StatusCodes.Status506VariantAlsoNegotiates };

        public ErrorResultDetail Format(params object[] valor)
        {
            return new ErrorResultDetail(Code, string.Format(Message, valor));
        }
    }
}
