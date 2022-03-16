using UnityEngine;
using UnityEngine.Perception.Randomization.Randomizers;

/// <summary>
/// Tag para indicar que a região do objeto precisa ser randomizada
/// </summary>
[AddComponentMenu("Perception/RandomizerTags/RegionTag")]
public class RegionTag : RandomizerTag
{
    [Tooltip("Posição mínima para o objeto no eixo X.")] public float minX;
    [Tooltip("Posição máxima para o objeto no eixo X.")] public float maxX;
    [Tooltip("Posição mínima para o objeto no eixo Y.")] public float minY;
    [Tooltip("Posição máxima para o objeto no eixo Y.")] public float maxY;
    [Tooltip("Posição mínima para o objeto no eixo Z.")] public float minZ;
    [Tooltip("Posição máxima para o objeto no eixo Z.")] public float maxZ;

    /// <summary>
    /// Define a posição do objeto.
    /// </summary>
    /// <param name="xPosition">Variável aleatória entre 0 e 1 que indica a posição onde colocar o objeto no eixo x.</param>
    /// <param name="yPosition">Variável aleatória entre 0 e 1 que indica a posição onde colocar o objeto no eixo y.</param>
    /// <param name="zPosition">Variável aleatória entre 0 e 1 que indica a posição onde colocar o objeto no eixo z.</param>
    public void SetPosition(float xPosition, float yPosition, float zPosition)
    {
        xPosition = (xPosition * (maxX-minX)) + minX;
        yPosition = (yPosition * (maxY-minY)) + minY;
        zPosition = (zPosition * (maxZ-minZ)) + minZ;

        //Debug.Log(xPosition + " " + yPosition + " " + zPosition); 

        transform.localPosition = new Vector3(xPosition, yPosition, zPosition);     
    } 
}

