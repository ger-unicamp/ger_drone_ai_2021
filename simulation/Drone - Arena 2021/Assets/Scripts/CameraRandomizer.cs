using System;
using UnityEngine;
using UnityEngine.Perception.Randomization.Parameters;
using UnityEngine.Perception.Randomization.Randomizers;

/// <summary>
/// Randomizador para randomizar o campo de visão (field of view) da câmera
/// </summary>
[Serializable]
[AddRandomizerMenu("Drone/CameraRandomizer")]
public class CameraRandomizer : Randomizer
{
    [Tooltip("Distribuição da aleatorização do FoV, sempre entre 0 e 1.")] public FloatParameter fov;

    /// <summary>
    /// Verifica a(s) câmera(s) que precisa(m) ter o fov alterado e o faz.
    /// </summary>
    protected override void OnIterationStart()
    {
        var tags = tagManager.Query<CameraTag>();
        foreach (var tag in tags)   //faz o loop da chamada do randomizador para cada objeto com CameraTag
        {
            tag.SetCamera(fov.Sample());    //chama o randomizador passando os parâmetro
                                            //contendo valores aleatorios de 0 a 1 pelo .Sample()
        }
    }
}
