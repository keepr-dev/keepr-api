﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Keeper.API.Models
{
    public class ErrorModel
    {
        public string Status => "Error";
        public string Message { get; set; }
        public string StackTrace { get; set; }

        public ErrorModel(Exception ex)
        {
            Message = ex.Message;
            StackTrace = ex.StackTrace;
        }

        public ErrorModel(string error)
        {
            Message = error;
        }
    }
}
