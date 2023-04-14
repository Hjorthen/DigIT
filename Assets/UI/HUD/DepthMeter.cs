using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DepthMeter : MonoBehaviour
{
    [SerializeField]
    private Text _Label;
    private string _LabelFormat;
    public Transform TrackedTransform;
    private World _World;
    void Start()
    {
        _LabelFormat = _Label.text;
        _World = ServiceRegistry.GetService<World>();
    }

    void Update()
    {
        var currentDepth = _World.WorldToGridPosition(TrackedTransform.position).y; 
        // Invert the number such that the number gets negative the deeper we go
        currentDepth = -currentDepth;
        _Label.text = string.Format(_LabelFormat, currentDepth);
    }
}
