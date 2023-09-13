﻿namespace MISA.WebFresher072023.Demo.ErrorHandler
{
    public class ErrorResponse
    {
        public string ErrorMessage { get; set; }
        public string UserMessage { get; set; }
        public ErrorResponse(string errorMessage, string userMessage) 
        { 
            this.ErrorMessage = errorMessage;   
            this.UserMessage = userMessage;  
        }
    }
}
