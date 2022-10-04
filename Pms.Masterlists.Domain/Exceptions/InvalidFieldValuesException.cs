﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Masterlists.Domain.Exceptions
{
    public class InvalidFieldValuesException : Exception
    {
        public string EEId { get; set; }
        public string Detail { get; set; }
        public InvalidFieldValuesException(string eeId, IEnumerable<InvalidFieldValueException> exceptions)
            : base($"{eeId} encountered multiple validation errors.")
        {
            EEId = eeId;

            Detail = $"{eeId} encountered the following validation errors:\n";
            foreach (var exception in exceptions)
                Detail += $"• {exception.Message}\n";
        }
    }
}