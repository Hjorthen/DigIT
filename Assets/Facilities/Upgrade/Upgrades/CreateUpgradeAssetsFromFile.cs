using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class GenerateUpdateAssetsFromFile : MonoBehaviour
{
    [MenuItem("Assets/Upgrades/GenerateFromCSV")]
    public static bool GenerateAssetsFromCSV() {
        var selectedObject = Selection.activeObject as TextAsset;
        var selectedAssetPath = AssetDatabase.GetAssetPath(selectedObject);
        Debug.Log(selectedAssetPath);
        if(!selectedObject.name.EndsWith(".csv")) {
            return false;
        }
        return true;
    }

    public static void Generate() {

       // string csvPath = selectedObject.name;
       // string csvFolder = Path.GetDirectoryName(csvPath);
       // 
       // var drill = new Drill() { BasePrice = 200, Cooldown = 0.1f, Name = "Test Drill"};
       // AssetDatabase.CreateAsset(drill, Path.Combine(csvFolder, "test"));

       // return true;
    }
}
