using System;
using UnityEngine;
using UnityEngine.Perception.Randomization.Parameters;
using UnityEngine.Perception.Randomization.Randomizers;

[Serializable]
[AddRandomizerMenu("Perception/Luz Randomizer")]
public class LuzRandomizer : Randomizer
{
    public FloatParameter lightIntensityParameter;
    public ColorRgbParameter lightColorParameter;
   

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