using FluentValidation.Results;
using Microsoft.AspNetCore.Http;

namespace QuickOrderPedido.Application.Dtos.Base
{
    //OWASP: https://cheatsheetseries.owasp.org/cheatsheets/AJAX_Security_Cheat_Sheet.html
    public class ServiceResult<T> : ServiceResult
    {
        public T Data { get; set; }
        public ServiceResult() : this(default) { }
        public ServiceResult(T result) => Data = result;

        public static implicit operator ServiceResult<T>(T value)
        {
            return new ServiceResult<T>(value);
        }

        public static implicit operator ServiceResult<T>(ErrorResultDetail value)
        {
            var result = new ServiceResult<T>();
            result.AddError(value);
            return result;
        }
    }

    public class ServiceResult
    {
        public int CodeId { get; set; } = StatusCodes.Status200OK;
        public string Message { get; set; }
        public bool IsSuccess { get { return !(Errors?.Count > 0); } }
        public List<ErrorResultDetail> Errors { get; set; } = new List<ErrorResultDetail>();

        /// <summary>
        /// Adiciona um erro ao retorno considerando a mensagem informada e Http Status Code como 400 (BadRequest).
        /// <para>Internamente adicionada a lista de erros, a instância de detalhamento informada.</para>
        /// <para>OBS: O Http Status Code do resultado (não do detalhe) é ajustado com o maior valor recebido. Ex: Se um erro 400 for informado e também um 500, ainda que o resultado possua o detalhamento de ambos, fica valendo 500 HttpStatusCode. Também é possível modificar manualmente.</para>
        /// </summary>
        /// <param name="message">Mensagem de erro</param>
        public void AddError(string message)
        {
            var errosDetail = new ErrorResultDetail(ErrorResultDetail.BAD_REQUEST.Code, message) { StatusCode = StatusCodes.Status400BadRequest };
            AddError(errosDetail);
        }

        /// <summary>
        /// Adiciona um erro ao retorno.
        /// <para>Internamente é criada uma instância de detalhamento para cada erro adicionado.</para>
        /// <para>OBS: O Http Status Code do resultado (não do detalhe) é ajustado com o maior valor recebido. Ex: Se um erro 400 for informado e também um 500, ainda que o resultado possua o detalhamento de ambos, fica valendo 500 HttpStatusCode. Também é possível modificar manualmente.</para>
        /// </summary>
        /// <param name="codeId">Http Status Code</param>
        /// <param name="code">Código interno de erro</param>
        /// <param name="message">Mensagem de erro (opcional)</param>
        public void AddError(int codeId, string code, string message = null)
        {
            AddError(codeId, new ErrorResultDetail(code, message));
        }

        /// <summary>
        /// Adiciona um erro ao retorno considerando Http Status Code como 400 (BadRequest).
        /// <para>Internamente adicionada a lista de erros, a instância de detalhamento informada.</para>
        /// <para>OBS: O Http Status Code do resultado (não do detalhe) é ajustado com o maior valor recebido. Ex: Se um erro 400 for informado e também um 500, ainda que o resultado possua o detalhamento de ambos, fica valendo 500 HttpStatusCode. Também é possível modificar manualmente.</para>
        /// </summary>
        /// <param name="errorResultDetail">Instância de detalhamento do erro</param>
        public void AddError(ErrorResultDetail errorResultDetail)
        {
            var codeId = errorResultDetail.StatusCode ?? StatusCodes.Status400BadRequest;
            AddError(codeId, errorResultDetail);
        }

        /// <summary>
        /// Adiciona um erro ao retorno.
        /// <para>Internamente adicionada a lista de erros, a instância de detalhamento informada.</para>
        /// <para>OBS: O Http Status Code do resultado (não do detalhe) é ajustado com o maior valor recebido. Ex: Se um erro 400 for informado e também um 500, ainda que o resultado possua o detalhamento de ambos, fica valendo 500 HttpStatusCode. Também é possível modificar manualmente.</para>
        /// </summary>
        /// <param name="codeId">Http Status Code</param>
        /// <param name="errorResultDetail">Instância de detalhamento do erro</param>
        public void AddError(int codeId, ErrorResultDetail errorResultDetail)
        {
            CodeId = CodeId > codeId
                ? CodeId
                : codeId;

            Errors.Add(errorResultDetail);
        }

        /// <summary>
        /// Adiciona uma lista de erros geradas pelo FluentValidator ao retorno considerando Http Status Code como 400 (BadRequest).
        /// <para>Internamente adicionada a lista de erros, a instância de detalhamento informada.</para>
        /// <para>OBS: O Http Status Code do resultado (não do detalhe) é ajustado com o maior valor recebido. Ex: Se um erro 400 for informado e também um 500, ainda que o resultado possua o detalhamento de ambos, fica valendo 500 HttpStatusCode. Também é possível modificar manualmente.</para>
        /// </summary>
        /// <param name="errors">Lista de erros de validação do FluentValidator</param>
        public void AddErrors(IEnumerable<ValidationFailure> errors)
        {
            AddErrors(StatusCodes.Status400BadRequest, errors);
        }

        /// <summary>
        /// Adiciona uma lista de erros geradas pelo FluentValidator ao retorno considerando Http Status Code como 400 (BadRequest).
        /// <para>Internamente adicionada a lista de erros, a instância de detalhamento informada.</para>
        /// <para>OBS: O Http Status Code do resultado (não do detalhe) é ajustado com o maior valor recebido. Ex: Se um erro 400 for informado e também um 500, ainda que o resultado possua o detalhamento de ambos, fica valendo 500 HttpStatusCode. Também é possível modificar manualmente.</para>
        /// </summary>
        /// <param name="codeId">Http Status Code</param>
        /// <param name="errors">Lista de erros de validação do FluentValidator</param>
        public void AddErrors(int codeId, IEnumerable<ValidationFailure> errors)
        {
            foreach (var error in errors)
                AddError(codeId, error.ErrorCode, error.ErrorMessage);
        }

        public bool ContainsError(string code)
        {
            var result = false;

            if (Errors != null)
                result = Errors.Any(p => p.Code == code || p.Details.Any(x => x.Code == code));

            return result;
        }

        public static implicit operator ServiceResult(string errorMsg)
        {
            var result = new ServiceResult();
            result.Message = errorMsg;
            result.AddError(errorMsg);
            return result;
        }

        public static implicit operator ServiceResult(Exception ex)
        {
            var result = new ServiceResult();
            result.Message = ex.Message;
            result.AddError(ex.ToString());
            return result;
        }
    }
}
