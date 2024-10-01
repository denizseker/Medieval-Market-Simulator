using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SO_Item))]
public class SO_ItemEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        SO_Item item = (SO_Item)target;
        if (GUILayout.Button("Update SO"))
        {
            UpdateItemName(item);
        }
    }

    private void UpdateItemName(SO_Item item)
    {
        string newName = $"SO_{item._itemName}";
        string assetPath = AssetDatabase.GetAssetPath(item);
        if (!string.IsNullOrEmpty(assetPath))
        {
            AssetDatabase.RenameAsset(assetPath, newName);
            AssetDatabase.SaveAssets();
        }
    }
}