using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Prefabs", menuName="Scriptable Object/Prefabs Data", order=int.MaxValue)]
public class Prefabs : ScriptableObject
{
    public List<GameObject> prefabs;
}
