using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform focus;

    [SerializeField]
    private bool useOffset = false;
    [SerializeField]
    private Vector3 offset;

    private void OnValidate() {
        if (focus == null) {
            Debug.LogError($"Script {this.GetType().Name}, attached to {name}, is missing {nameof(focus)} reference", this);
        }
    }

    private void Awake()
    {
        // Calculates the camera's offset from the focus, unless assigned in editor
        if (!useOffset) {
            offset = transform.position - focus.position;
        }
    }

    private void Update()
    {
        // Maintains the same offset from the player
        transform.position = focus.position + offset;
    }
}
