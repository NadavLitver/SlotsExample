using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class CreateAssetBundle : Editor
{
    [MenuItem("Assets/Build AssetBundles For Android")]
    static void BuildAllAssetBundlesAndroid()
    {
        string assetBundleDirectory = "Assets/AssetBundles";
        if (!Directory.Exists(assetBundleDirectory))
        {
            Directory.CreateDirectory(assetBundleDirectory);
        }
        BuildPipeline.BuildAssetBundles(assetBundleDirectory,
                                        BuildAssetBundleOptions.None,
                                        BuildTarget.Android);
    }
    [MenuItem("Assets/Build AssetBundles For PC")]//and editor
    static void BuildAllAssetBundlesWindows()
    {
        string assetBundleDirectory = "Assets/AssetBundles/Windows";
        if (!Directory.Exists(assetBundleDirectory))
        {
            Directory.CreateDirectory(assetBundleDirectory);
        }
        BuildPipeline.BuildAssetBundles(assetBundleDirectory,
                                        BuildAssetBundleOptions.None,
                                        BuildTarget.StandaloneWindows);
    }
}
