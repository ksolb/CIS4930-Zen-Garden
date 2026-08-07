using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Camera))]
public class CameraClearController : MonoBehaviour
{
    private Camera brushCam;

    void OnEnable()
    {
        brushCam = GetComponent<Camera>();
        // Subscribe to the URP rendering loop
        RenderPipelineManager.beginCameraRendering += OnBeginCameraRendering;
    }

    void OnDisable()
    {
        // Clean up the subscription
        RenderPipelineManager.beginCameraRendering -= OnBeginCameraRendering;
    }

    void OnBeginCameraRendering(ScriptableRenderContext context, Camera camera)
    {
        // Only apply this to the BrushCamera
        if (camera == brushCam)
        {
            // Force the camera to stop clearing entirely
            camera.clearFlags = CameraClearFlags.Nothing;

            // Disable shadows on this specific camera as they can trigger depth clears
            var camData = camera.GetUniversalAdditionalCameraData();
            if (camData != null)
            {
                camData.renderShadows = false;
            }
        }
    }
}