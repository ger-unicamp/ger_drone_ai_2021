using UnityEngine;
using UnityEngine.Perception.Randomization.Randomizers;
using Newtonsoft.Json.Linq;

[AddComponentMenu("Drone/RandomizerTags/CameraTag")]
[RequireComponent(typeof(Camera))]
public class CameraTag : RandomizerTag
{
    [SerializeField]
    JObject json_file;
    public void SetCamera()
    {

    }    
}