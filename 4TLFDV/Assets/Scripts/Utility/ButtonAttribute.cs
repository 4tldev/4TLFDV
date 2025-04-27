using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Method)]
public class ButtonAttribute : PropertyAttribute
{
    public string ButtonLabel { get; }

    public ButtonAttribute(string label = null)
    {
        ButtonLabel = label;
    }
}
