using System;
using UnityEngine;
using UnityEngine.Perception.Randomization.Parameters;
using UnityEngine.Perception.Randomization.Randomizers;

[Serializable]
[AddRandomizerMenu("Perception/RegionRandomizer")]
public class RegionRandomizer : Randomizer
{
    public FloatParameter xPosition;
    public FloatParameter yPosition;
    public FloatParameter zPosition;


    protected override void OnIterationStart()
    {
        var tags = tagManager.Query<RegionTag>();
        foreach (var tag in tags)
        {
            tag.SetPosition(xPosition.Sample(), yPosition.Sample(), zPosition.Sample());
        }
    }
}
