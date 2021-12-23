using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="PrefabsData", menuName="Scriptable Object/Prefabs Data", order=int.MaxValue)]
public class PrefabsData : ScriptableObject
{
    public List<GameObject> prefabs;
}
