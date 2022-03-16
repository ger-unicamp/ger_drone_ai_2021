using System;
using UnityEngine;
using UnityEngine.Perception.Randomization.Parameters;
using UnityEngine.Perception.Randomization.Randomizers;

/// <summary>
/// Randomiza a posição de objetos com a tag RegionTag.
/// </summary>
[Serializable]
[AddRandomizerMenu("Perception/RegionRandomizer")]
public class RegionRandomizer : Randomizer
{
    [Tooltip("Distribuição para amostrar a posição dos objetos no eixo X. Deve estar entre 0 e 1.")] public FloatParameter xPosition;
    [Tooltip("Distribuição para amostrar a posição dos objetos no eixo Y. Deve estar entre 0 e 1.")] public FloatParameter yPosition;
    [Tooltip("Distribuição para amostrar a posição dos objetos no eixo Z. Deve estar entre 0 e 1.")] public FloatParameter zPosition;


    /// <summary>
    /// Randomiza as posições dos objetos.
    /// </summary>
    protected override void OnIterationStart()
    {
        var tags = tagManager.Query<RegionTag>();
        foreach (var tag in tags)
        {
            tag.SetPosition(xPosition.Sample(), yPosition.Sample(), zPosition.Sample());
        }
    }
}
