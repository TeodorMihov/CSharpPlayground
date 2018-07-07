namespace GoodPractices.ServiceLayer
{
    using System;

    public class ServiceResult<T> : ServiceResultBase
    {
        public static readonly ServiceResult<T> EmptySucess = new ServiceResult<T>();

        public static readonly new ServiceResult<T> NotFound = new ServiceResult<T> { ErrorMessage = "Not found" };

        public T Data { get; private set; }

        public static new ServiceResult<T> Success(T data) => new ServiceResult<T> { Data = data };

        public static new ServiceResult<T> Error(string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(errorMessage))
            {
                throw new ArgumentException("Error service result cannot have null or white space error message.");
            }

            return new ServiceResult<T> { ErrorMessage = errorMessage };
        }
    }
}
