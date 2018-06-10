using System;
using System.Collections.Generic;
using System.Text;

namespace Banking.Common.Notification
{
    public class Notification
    {
        private List<Error> errors = new List<Error>();

        public void addError(String message)
        {
            addError(message, null);
        }

        public void addError(String message, Exception e)
        {
            errors.Add(new Error(message, e));
        }

        //public String errorMessage()
        //{
        //   // return errors.Select().map(e => e.getMessage()).collect(Collectors.joining(", "));
        //}

        //public String errorMessage(String separator)
        //{
        //   // return errors.stream().map(e => e.getMessage()).collect(Collectors.joining(separator));
        //}

        //public Boolean hasErrors()
        //{
        //   // return !errors.isEmpty();
        //}

        public List<Error> getErrors()
        {
            return errors;
        }

        public void setErrors(List<Error> errors)
        {
            this.errors = errors;
        }
    }
}
