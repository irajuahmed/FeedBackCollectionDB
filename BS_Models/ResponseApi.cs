using System;
using System.Collections.Generic;
using System.Text;

namespace BS_Models
{
    public class ResponseApi<T>
    {
        public string Status; //(OK/FAILED)
        public string Message; //Api return message will appear here
        public T Result; //Api return Result will appear here
    }
}
