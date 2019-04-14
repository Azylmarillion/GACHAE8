using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


static public class GameManager
{
    // This game manager is the ugliest thing i've ever seen
    // But it's okay, it's a GameJam :)

    public static UnityEvent E_Death = new UnityEvent();

    public static bool m_JumpSpeedPower = false;
    public static bool m_PushPower = false;
    public static bool m_SightPower = false;
    public static bool m_ProjectPower = false;
    public static Vector3 m_RespawnPoint = new Vector3(-8, 2, 0);
    public static int m_PickUpsGot = 0;
    public static float m_SaturationValue = -100f;

}
