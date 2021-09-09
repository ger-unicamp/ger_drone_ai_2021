using UnityEngine;
using UnityEngine.Perception.Randomization.Randomizers;
using Newtonsoft.Json.Linq;

[AddComponentMenu("Drone/RandomizerTags/CameraTag")]
[RequireComponent(typeof(Camera))]
public class CameraTag : RandomizerTag
{
    public float minFOV;
    public float maxFOV;

    #pragma warning disable 108
    Camera camera;

    void Start()
    {
        camera = this.GetComponent<Camera>();
    }
    public void SetCamera(float fovParameter)
    {
        float fov = (fovParameter*(maxFOV-minFOV)) + minFOV;
        camera.fieldOfView = fov;
    }    
}