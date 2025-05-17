using UnityEngine;
using UnityEditor;

public class GuardCreator : MonoBehaviour
{
    public static void CreateGuards(Vector3 position, Vector3 rotation)
    {
        Guards asset = ScriptableObject.CreateInstance<Guards>();


        asset.stances = new System.Collections.Generic.List<SwordStance>();
        asset.direction = AtkDirection.Up;
        SwordStance stance1 = new SwordStance();
        stance1.position = position;
        stance1.rotation = rotation;
        asset.stances.Add(stance1);

        // To use these classes you must be in "Editor" folder
        AssetDatabase.CreateAsset(asset, "Assets/NewGuardsAsset.asset");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }
}

