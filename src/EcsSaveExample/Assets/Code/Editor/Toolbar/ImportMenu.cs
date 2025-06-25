using System.Collections.Generic;
using Code.Editor.Extensions;
using UnityEditor;

namespace Code.Editor.Toolbar
{
  public static class ImportMenu 
  {
    [MenuItem("Import/2d in selection %#u")]
    public static void SetSettingsForUISpritesInSelection()
    {
      List<TextureImporter> importers = TextureExtensions.TextureImportersInSelection();
      for (var i = 0; i < importers.Count; i++)
      {
        TextureImporter importer = importers[i];
        
        importer
          .WithUISpriteSettings(SpriteImportMode.Single)
          .Save();

        EditorUtility.DisplayProgressBar("Importing some fancy art...", importer.assetPath.Replace("Assets/", ""), (float) i / importers.Count);
      }

      TextureExtensions.RestoreTexturesUnreadabilityInSelection();
    
      EditorUtility.ClearProgressBar();
    }
  }
}