using UnityEditor;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    private AssetBundle _assetBundle = null;

    void Start()
    {
        var assetBundlePAth = Path.Combine(Application.streamingAssetsPath, "AssetBundles", "player");
        
        _assetBundle = AssetBundle.LoadFromFile(assetBundlePAth);
        if (_assetBundle == null)
        {
            Debug.Log("AssetBundle is null");
            return;
        }

        var prefab = _assetBundle.LoadAsset<GameObject>("Assets/BundleResources/pantheon.png");

    }
}
