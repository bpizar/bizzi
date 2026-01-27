using JayGor.People.Entities.Responses;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JayGor.People.ErrorManager
{
    public static class ErrorsManager
    {
        public static GenericPair GetFormatedError(Exception ex)
        {
            var response = new GenericPair();
            var errorMessage = string.Empty;

            errorMessage += string.Format(" Location: {0}", ex.Source);
            errorMessage += string.Format(" Message: {0}", ex.Message);
            errorMessage += ex.InnerException !=null ? string.Format("InnerException: {0}", ex.InnerException) : string.Empty;

            response.Description = errorMessage;
            response.Id = "1000"; // Unknow error.
            return response;
        }

        public static GenericPair GetUnknowErrorWithDataBaseReference(string dabaBaseId)
        {
            var response = new GenericPair();
            response.Description = string.Format("Internal Server Error for see the exception see log for error number:  {0}", dabaBaseId);
            response.Id = "1000"; // Unknow error.
            return response;
        }

        public static GenericPair GetKnowError(string IdError, string extraMessage = "")
        {
            var response = new GenericPair();
            response.Id = IdError;
            var error = ErrorList.Errors.Where(c => c.Key == IdError).FirstOrDefault();
            response.Description = error.Key == null ? 
                                   string.Format("The error {0} is not in know errors list.", IdError) : 
                                   string.IsNullOrEmpty(extraMessage) ? error.Value: string.Format("{0}, ExtraMessage: {1}", error.Value, extraMessage);

            return response;
        }
    }

    public static class ErrorList
    {
        private static Dictionary<string, string> errors_ { get; set; } = null;

        public static Dictionary<string, string> Errors
        {
            get {
                    if (errors_ == null)
                    {
                        CreateListErrors();
                    }

                    return errors_;
                }
        }

        private static void CreateListErrors()
        {
            errors_ = new Dictionary<string, string>();
            errors_.Add("1000", "Unknow Error");
            errors_.Add("1001", "Bad Authentication");            
            // Add here all errors.
        }
    }

}
