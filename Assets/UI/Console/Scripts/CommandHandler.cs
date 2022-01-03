using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommandHandler
{
    bool HandleCommand(string command);
}
