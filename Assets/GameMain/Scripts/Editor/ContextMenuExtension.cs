using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ContextMenuExtension
{
    [MenuItem("CONTEXT/MonoBehaviour/Auto Set Variables")]
    static void AutoSetVariables(MenuCommand command)
    {
        var context = command.context as MonoBehaviour;
        var contextTransform = context.transform;

        var serializedObject = new SerializedObject(context);

        var property = serializedObject.GetIterator();
        property.Next(true);

        while (property.Next(false))
        {
            if (property.type == "PPtr<$GameObject>")
            {
                var child = FindChild(contextTransform, property.name);
                if (child) property.objectReferenceValue = child.gameObject;
            }
            else
            {
                var component = FindChildComponent(contextTransform, property.name, property.type);
                if (component) property.objectReferenceValue = component;
            }
        }

        serializedObject.ApplyModifiedProperties();
    }

    static MonoBehaviour FindChildComponent(Transform transform, string name, string type)
    {
        if (name.Length == 0) return null;

        Transform child = FindChild(transform, name);

        if (child)
        {
            var expectedType = type.Replace("PPtr<$", "").Replace(">", "");

            var components = child.GetComponents<MonoBehaviour>();
            foreach (var com in components)
            {
                if (com.GetType().Name == expectedType)
                    return com;
            }
        }

        return null;
    }

    static Transform FindChild(Transform trans, string name)
    {
        // TODO: 查找内层？
        if (string.IsNullOrEmpty(name))
            return null;

        Transform child;

        child = trans.Find(name);
        if (!child)
        {
            name = name.First().ToString().ToUpper() + name.Substring(1);
            child = trans.Find(name);
        }

        return child;
    }
}
