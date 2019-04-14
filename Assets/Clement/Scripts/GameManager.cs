using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


static public class GameManager
{
    public static UnityEvent E_Death = new UnityEvent();

    public static bool m_JumpSpeedPower = false;
    public static Vector3 m_RespawnPoint = new Vector3(-8, 2, 0);

}
