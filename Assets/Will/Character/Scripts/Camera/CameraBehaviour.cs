using System.Collections;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    #region F/P
    public static CameraBehaviour Instance = null;
    [Header("Camera settings :")]
    [SerializeField]
    new Camera camera = null;
    [SerializeField]
    Transform targetToFocus;
    [SerializeField, Range(0, 20)]
    float xOffset, yOffset = 0;
    [SerializeField, Range(0, 1)]
    float screenShakeForce = .5f;
    [SerializeField, Range(0, 1)]
    float screenShakeLength = .5f;
    [SerializeField]
    float speed = 5;
    [SerializeField]
    private Vector3 cameraGeneralViewPoint;
    [SerializeField]
    private float cameraGeneralViewSize;
    private float cameraOriginalSize;
    private Vector3 cameraOriginalPosition;
    private float cameraViewshiftSpeed;
    private float cameraViewshiftChrono = 0;
    private bool isCameraToggled;

    #endregion

    #region Meths
    void FollowPlayer()
    {
        transform.position = new Vector3(targetToFocus.position.x + xOffset, targetToFocus.position.y + yOffset, -5);
    }
    void GetPlayer(PlayerController _player)
    {
        Rigidbody2D _playerRigidBody2D;
        targetToFocus = _player.transform;
        _playerRigidBody2D = _player.gameObject.GetComponent<Rigidbody2D>();
    }
    public void ScreenShake()
    {
        StartCoroutine(ScreenShakeCoroutine());
    }
    public void ToggleGeneralView(float _time)
    {
        isCameraToggled = true;
        cameraViewshiftChrono = 0;
        cameraViewshiftSpeed = _time;
        cameraOriginalPosition = transform.position;
    }
    public void UntoggleGeneralView()
    {
        isCameraToggled = false;
        camera.orthographicSize = cameraOriginalSize;
    }
    private IEnumerator ScreenShakeCoroutine()
    {
        float _timer = 0;
        while (_timer < screenShakeLength)
        {
            transform.position += Random.insideUnitSphere * screenShakeForce;

            yield return null;
            _timer += Time.deltaTime;
        }
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
        if (isCameraToggled)
        {
            if (cameraViewshiftChrono <= cameraViewshiftSpeed)
            {

                transform.position = Vector3.Lerp(cameraOriginalPosition, cameraGeneralViewPoint, cameraViewshiftChrono);
                camera.orthographicSize = Mathf.Lerp(cameraOriginalSize, cameraGeneralViewSize, cameraViewshiftChrono);

                cameraViewshiftChrono += Time.fixedDeltaTime;
            }
        }

        else
        {
            FollowPlayer();
        }
    }

    #endregion
}