using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConsole 
{
    private static GameConsole instance = null;
    // Start is called before the first frame update
    public static GameConsole Instance {
        get {
            if(instance == null)
                instance = new GameConsole();
            return instance;
        }
    }

    public static void WriteLine(string line) {
        Instance.Data.AddEntry(line);
    }

    public ConsoleModel Data = new ConsoleModel();
}
