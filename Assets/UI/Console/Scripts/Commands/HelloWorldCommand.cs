using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelloWorldCommand : ICommandHandler
{
    public bool HandleCommand(string command)
    {
        if(command.Equals("hello", System.StringComparison.CurrentCultureIgnoreCase)) {
            GameConsole.WriteLine("Hello world!");
        }
        return true;
    }
}
