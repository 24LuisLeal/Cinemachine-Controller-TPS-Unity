using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineFreeLook))]

public class CameraLook : MonoBehaviour
{
    private CinemachineFreeLook cinemachine;
    private InputMovement inputPlayer;
    private float lookSpeed = 1f;

    private void Awake()
    {
        inputPlayer = new InputMovement();
        cinemachine = GetComponent<CinemachineFreeLook>();
    }

    private void OnEnable()
    {
        inputPlayer.Enable();
    }
    private void OnDisable()
    {
        inputPlayer.Disable();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 delta = inputPlayer.PlayerMain.Look.ReadValue<Vector2>();
        cinemachine.m_XAxis.Value += delta.x * 200 *  lookSpeed * Time.deltaTime;
        cinemachine.m_YAxis.Value += delta.y * 200 * lookSpeed * Time.deltaTime;
    }
}
