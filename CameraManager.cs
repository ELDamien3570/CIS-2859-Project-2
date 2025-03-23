using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("Follow Target")]
    private Transform cameraTarget;
    
    private void Start()
    {
        cameraTarget = FindFirstObjectByType<PlayerController>().transform;
    }
    private void Update()
    {
        if (cameraTarget.GetComponent<PlayerController>().death == true)
            this.transform.position = this.transform.position;
        else
            this.transform.position = cameraTarget.position;
    }
}
