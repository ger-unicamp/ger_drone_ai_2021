using System;
using UnityEngine;
using UnityEngine.Perception.Randomization.Parameters;
using UnityEngine.Perception.Randomization.Randomizers;

/// <summary>
/// Randomizador para randomizar a cor e a intensidade da iluminação (Light)
/// </summary>
[Serializable]
[AddRandomizerMenu("Perception/Luz Randomizer")]
public class LuzRandomizer : Randomizer
{
    [Tooltip("Intervalo de variação possível para a intensidade da luz")] public FloatParameter lightIntensityParameter;
    [Tooltip("Intervalo de variação do RGB da luz, cuidado para não criar cenários inconvenientes")] public ColorRgbParameter lightColorParameter;
   
    /// <summary>
    /// Verifica os objetos do tipo light que precisam ser randomizados e o faz.
    /// </summary>
    protected override void OnIterationStart()
    {
        var tags = tagManager.Query<LuzTag>();

        foreach (var tag in tags)
        {
            var light = tag.GetComponent<Light>();            
            light.intensity = lightIntensityParameter.Sample();
            light.color = lightColorParameter.Sample();            
        }
    }
}