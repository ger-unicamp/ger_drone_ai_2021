using UnityEngine;
using UnityEngine.Perception.Randomization.Randomizers;
using Newtonsoft.Json.Linq;

/// <summary>
/// Tag que deve ser componente da câmera para randomizar seu campo de visão 
/// </summary>
[AddComponentMenu("Drone/RandomizerTags/CameraTag")]
[RequireComponent(typeof(Camera))]
public class CameraTag : RandomizerTag
{
    [Tooltip("FoV mínimo para a câmera")] public float minFOV;
    [Tooltip("FoV máximo para a câmera")] public float maxFOV;

    #pragma warning disable 108
    
    /// <summary>
    /// Função que instancia o objeto com a tag como câmera.
    /// </summary>
    Camera camera;

    void Start()
    {
        camera = this.GetComponent<Camera>();
    }
    /// <summary>
    /// Função que altera o FoV da câmera em função do parâmetro aleatorizado.
    /// </summary>
    /// <param name="fovParameter">Variável aleatória entre 0 e 1 que é responsável pela parte aleatorizada do novo foco da câmera.</param>
    public void SetCamera(float fovParameter)
    {
        float fov = (fovParameter*(maxFOV-minFOV)) + minFOV;
        camera.fieldOfView = fov;
    }    
}