using UnityEngine;

public class VRMenuFollow : MonoBehaviour
{
    [SerializeField] private Transform playerHead;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private float distance = 1.0f;
    [SerializeField] private float heightOffset = -0.1f;

    private bool menuOpen = false;

    public void ToggleMenu()
    {
        menuOpen = !menuOpen;
        menuPanel.SetActive(menuOpen);

        if (menuOpen)
        {
            PositionMenu();
        }
    }

    private void PositionMenu()
    {
        // Flatten head forward
        Vector3 flatForward = playerHead.forward;
        flatForward.y = 0;

        // Guard against zero-length vector (looking straight up/down)
        if (flatForward.sqrMagnitude < 0.001f)
        {
            flatForward = Vector3.ProjectOnPlane(playerHead.up, Vector3.up);
            if (flatForward.sqrMagnitude < 0.001f)
                flatForward = Vector3.forward; // last-resort fallback
        }
        flatForward.Normalize();

        Vector3 targetPos = playerHead.position + flatForward * distance;
        targetPos.y = playerHead.position.y + heightOffset;

        transform.position = targetPos;

        Vector3 lookDir = flatForward; // menu should face back toward the player
        transform.rotation = Quaternion.LookRotation(lookDir, Vector3.up);
    }
}