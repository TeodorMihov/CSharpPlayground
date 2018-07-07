namespace GoodPractices.ServiceLayer
{
    using System;

    public class ServiceResultBase
    {
        public static readonly ServiceResultBase Success = new ServiceResultBase();

        public static readonly ServiceResultBase NotFound = new ServiceResultBase { ErrorMessage = "Not found" };

        public bool IsError => !string.IsNullOrWhiteSpace(this.ErrorMessage);

        public string ErrorMessage { get; protected set; }

        public static ServiceResultBase Error(string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(errorMessage))
            {
                throw new ArgumentException("Error service result cannot have null or white space error message.");
            }

            return new ServiceResultBase { ErrorMessage = errorMessage };
        }
    }
}
