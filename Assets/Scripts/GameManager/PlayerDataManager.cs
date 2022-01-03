using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager : Singleton<PlayerDataManager>
{
    private CharaterData playerData;
    public CharaterData PlayerData { get { return playerData; } }

    private void Awake()
    {
        playerData = new CharaterData(GameObject.Find("Player").GetComponent<Player>());
    }
}

public class CharaterData
{
    private Player player;
    public Player Player { get { return player; } }
    private Rigidbody rigid;
    public Rigidbody Rigid { get { return rigid; } }
    private Transform transform;
    public Transform Transform { get { return transform; } }

    public CharaterData(Player player)
    {
        this.player = player;
        rigid = player.GetComponent<Rigidbody>();
        transform = player.GetComponent<Transform>();
    }
}