using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using JetBrains.Annotations;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace Code.Tests.OlfEditorTests.Common
{
  [TestFixture]
  public class ScriptableObjectsTests
  {
    [Test]
    public void AllScriptableObjectsHasNoNulls()
    {
      foreach(string path in EditorExtensions.GetAllProjectScriptableObjectsPath())
        ScriptableObjectHasNoNulls(path);
    }

    private static void ScriptableObjectHasNoNulls(string path)
    {
      ScriptableObject scriptableObject = AssetDatabase.LoadAssetAtPath<ScriptableObject>(path);

      IEnumerable<string> fieldsWithNulls = scriptableObject
        .GetType()
        .GetFields()
        .Where(field => field.FieldType.IsClass)
        .Where(field =>
            FieldIsNull(field, scriptableObject)
            || FieldIsListWithNullElement(field, scriptableObject)
            || FieldIsDictionaryWithNullElement(field, scriptableObject))
        .Select(field => $"\n{field.Name}");
      
      fieldsWithNulls.Should().BeEmpty();
    }

    private static bool FieldIsNull(FieldInfo field, ScriptableObject scriptableObject) =>
      field.GetValue(scriptableObject) == null 
      && field.GetCustomAttributes().All(attribute => attribute is not CanBeNullAttribute);

    private static bool FieldIsListWithNullElement(FieldInfo field, ScriptableObject scriptableObject)
    {
      if(!field.FieldType.IsGenericType || !field.FieldType.GetGenericTypeDefinition().IsAssignableFrom(typeof(List<>)))
        return false;

      IList list = (IList)field.GetValue(scriptableObject);
      return list
        .Cast<object>()
        .Any(element => element == null);
    }

    private static bool FieldIsDictionaryWithNullElement(FieldInfo field, ScriptableObject scriptableObject)
    {
      if(!field.FieldType.IsGenericType || !field.FieldType.GetGenericTypeDefinition().IsAssignableFrom(typeof(Dictionary<,>)))
        return false;

      IDictionary dictionary = (IDictionary)field.GetValue(scriptableObject);
      return dictionary
        .Values
        .Cast<object>()
        .Union(dictionary.Keys.Cast<object>())
        .Any(element => element == null);
    }
  }
}