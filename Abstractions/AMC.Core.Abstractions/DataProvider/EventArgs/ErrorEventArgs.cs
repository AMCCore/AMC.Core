using System;
using System.Collections.Generic;
using System.Text;

namespace AMC.Core.Abstractions.DataProvider.EventArgs
{
    public class ErrorEventArgs : System.EventArgs
    {
        public bool SupressError
        {
            get { return _supressError; }
            set { _supressError = value; }
        }
        private bool _supressError = false;

        public Exception Exception
        {
            get { return _exception; }
        }
        private readonly Exception _exception;

        public string ErrorMessage
        {
            get { return _errorMessage ?? (_exception != null ? _exception.Message : string.Empty); }
        }
        private readonly string _errorMessage;

        public ErrorEventArgs(Exception ex)
            : this(null, ex)
        {
        }

        public ErrorEventArgs(string message)
            : this(message, null)
        {
        }

        public ErrorEventArgs(string message, Exception ex)
        {
            _errorMessage = message;
            _exception = ex;
        }


    }
}
