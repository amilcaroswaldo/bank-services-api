﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace creditcard.Domain.Base
{
    public class GenericResponse
    {
        public int Code { get; set; }
        public string? Message { get; set; }
    }
}
