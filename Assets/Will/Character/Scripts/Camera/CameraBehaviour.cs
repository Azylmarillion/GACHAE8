using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    #region F/P
    public static CameraBehaviour Instance = null;
    [SerializeField]
    Transform playerToFocus;    
    [SerializeField, Range(0, 5)]
    float xOffset, yOffset = 0;
    #endregion

    #region Meths
    void FollowPlayer()
    {
        transform.position = new Vector3(playerToFocus.position.x + xOffset, playerToFocus.position.y + yOffset, -5);
    }
    void GetPlayer(PlayerController _player)
    {
        Rigidbody2D _playerRigidBody2D;
        playerToFocus = _player.transform;
        _playerRigidBody2D = _player.gameObject.GetComponent<Rigidbody2D>();
    }
    #endregion

    #region UniMeths
    void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("There's already an instance in the scene !!");
            Destroy(this);
            return;
        }
    }
    void OnDestroy()
    {
        Instance = null;
    }
    void Update()
    {
        if (!playerToFocus) return;

        FollowPlayer();
    }
    #endregion
}