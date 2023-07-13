using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Console.Exceptions.Files;

[Serializable]
public class MyException : Exception
{
    public string MyInfo { get; set; }

    public MyException() : base("Standard messasge.") { }

    public MyException(string message) : base(message) { }

    public MyException(string message, Exception innerException) : base(message, innerException) { }

    protected MyException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}
