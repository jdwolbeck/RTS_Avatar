using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : HandlerBase
{
    public static GameHandler instance;
    public Material ColorPlayer = null;
    public Material ColorEnemy = null;

    private void Awake()
    {
        instance = this;
    }
}
