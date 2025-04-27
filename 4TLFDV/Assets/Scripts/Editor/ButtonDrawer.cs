using UnityEngine;
using UnityEditor;
using System.Reflection;

[CustomEditor(typeof(MonoBehaviour), true)]
public class ButtonDrawer : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var methods = target.GetType()
                            .GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

        foreach (var method in methods)
        {
            var buttonAttribute = method.GetCustomAttribute<ButtonAttribute>();
            if (buttonAttribute != null)
            {
                string buttonText = string.IsNullOrEmpty(buttonAttribute.ButtonLabel) ? method.Name : buttonAttribute.ButtonLabel;
                if (GUILayout.Button(buttonText))
                {
                    method.Invoke(target, null);
                }
            }
        }
    }
}
