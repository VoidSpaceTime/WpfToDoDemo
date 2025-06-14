﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Shared
{
    public class ApiResponse
    {

        public string Message { get; set; }

        public bool Status { get; set; }

        public object Data { get; set; }
    }

    public class ApiResponse<T>
    {
        public string Message { get; set; }

        public bool Status { get; set; }

        public T Data { get; set; }
    }
}
