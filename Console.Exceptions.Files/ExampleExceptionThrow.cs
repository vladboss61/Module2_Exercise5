namespace Console.Exceptions.Files;

using System;

public class ExampleExceptionThrow
{
    public void Logic()
    {
        var objectException = new DivideByZeroException("Exception with LogicInvalid function.");
        //throw objectException;
        Console.WriteLine(".....");
        LogicInvalid();
    }

    public void LogicInvalid()
    {
        Console.WriteLine("LogicInvalid function");
        throw new MyException() { MyInfo = "123123" };
    }
}
