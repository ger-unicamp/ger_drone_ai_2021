using System;
using UnityEngine;
using UnityEngine.Perception.Randomization.Parameters;
using UnityEngine.Perception.Randomization.Randomizers;

[Serializable]
[AddRandomizerMenu("Drone/MostradorRandomizer")]
public class MostradorRandomizer : Randomizer
{
    public IntegerParameter digit1;
    public IntegerParameter digit2;
    public IntegerParameter digit3;
    public BooleanParameter minus;
    public BooleanParameter one;

    protected override void OnIterationStart()
    {
        var tags = tagManager.Query<MostradorTag>();
        foreach (MostradorTag tag in tags)
        {
            tag.SetNumber(digit1.Sample(), digit2.Sample(), digit3.Sample(), minus.Sample(), one.Sample());
        }
        
    }
}
