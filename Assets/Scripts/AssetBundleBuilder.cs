using System.IO;
using UnityEditor;
using UnityEngine;

public class AssetBundleBuilder
{
    [MenuItem("AssetBundle/Build")]
    public static void BuildAssetBundle()
    {
        var outputPath = Path.Combine(Application.streamingAssetsPath, "AssetBundles");
        var outPathDi = new DirectoryInfo(outputPath);

        if (outPathDi.Exists)
        {
            outPathDi.Delete(true);
        }

        outPathDi.Create();

        BuildPipeline.BuildAssetBundles(outputPath, BuildAssetBundleOptions.UncompressedAssetBundle, BuildTarget.StandaloneWindows);
    }
}
