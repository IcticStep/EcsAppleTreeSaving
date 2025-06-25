using System.Collections.Generic;
using System.Reflection;
using Code.Runtime.Common.Extensions;
using UnityEditor;
using UnityEngine;

namespace Code.Editor.Extensions
{
  public static class TextureExtensions
  {
    public static string[] TextureGUID(string selectedPath, string nameFilter)
    {
      if (selectedPath.IsNullOrEmpty())
      {
        Debug.LogError("No folder selected!");
        return new string[0];
      }

      return AssetDatabase.FindAssets($"{nameFilter} t:texture2D", new[] {selectedPath});
    }
    
    public static Vector2 GetTextureSize(this TextureImporter importer)
    {
      object[] args = { 0, 0 };
      MethodInfo methodInfo = typeof(TextureImporter).GetMethod("GetWidthAndHeight", BindingFlags.NonPublic | BindingFlags.Instance);
      methodInfo.Invoke(importer, args);

      var width = (int)args[0];
      var height = (int)args[1];

      return new Vector2(width, height);
    }

    public static TextureImporter Save(this TextureImporter importer)
    {
      EditorUtility.SetDirty(importer);
      importer.SaveAndReimport();
      return importer;
    }

    public static TextureImporter WithUISpriteSettings(this TextureImporter importer, SpriteImportMode importMode = SpriteImportMode.Multiple)
    {
      importer.isReadable = true;
      importer.textureType = TextureImporterType.Sprite;
      importer.spriteImportMode = importMode;
      importer.mipmapEnabled = false;
      importer.filterMode = FilterMode.Bilinear;
      importer.textureCompression = TextureImporterCompression.Uncompressed;
      importer.wrapMode = TextureWrapMode.Clamp;
      
      Vector2 textureSize = importer.GetTextureSize();

      importer.maxTextureSize = Mathf.Max(textureSize.x.NextPowerOfTwo(), textureSize.y.NextPowerOfTwo());

      return importer;
    }

    public static List<TextureImporter> TextureImportersInSelection()
    {
      List<TextureImporter> textureImporters = new();

      foreach (string guid in Selection.assetGUIDs)
      {
        string path = AssetDatabase.GUIDToAssetPath(guid);

        if (AssetDatabase.IsValidFolder(path))
        {
          string[] allGuids = AssetDatabase.FindAssets("", new[] { path });
          foreach (string subGuid in allGuids)
          {
            string subPath = AssetDatabase.GUIDToAssetPath(subGuid);
            TextureImporter importer = AssetImporter.GetAtPath(subPath) as TextureImporter;
            if (importer != null)
            {
              textureImporters.Add(importer);
            }
          }
        }
        else
        {
          TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter;
          if (importer != null)
          {
            textureImporters.Add(importer);
          }
        }
      }

      return textureImporters;
    }

    public static void RestoreTexturesUnreadabilityInSelection()
    {
      IEnumerable<TextureImporter> importers = TextureImportersInSelection();

      foreach (TextureImporter importer in importers)
      {
        importer.isReadable = false;
        importer.Save();
        
        EditorUtility.DisplayProgressBar($"Restoring Texture Unreadability in selection", importer.name, 1);
      }
      
      EditorUtility.ClearProgressBar();
    }
  }
}