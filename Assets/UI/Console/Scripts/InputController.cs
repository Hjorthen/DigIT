using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[Serializable]
public class InputController : MonoBehaviour
{
    private List<ICommandHandler> Commands;

    [SerializeField]
    private InputField input;
    void Start()
    {
        Commands = ServiceRegistry.GetServices<ICommandHandler>();

        input.onSubmit.AddListener(s => {OnSubmit(s);} );
    }

    void OnSubmit(string s) {
        Commands.ForEach( command => command.HandleCommand(s));
        input.text = "";
    }
}
