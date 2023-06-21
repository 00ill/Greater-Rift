using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommandHandle
{
    public void ProcessCommand(Command command);
}
