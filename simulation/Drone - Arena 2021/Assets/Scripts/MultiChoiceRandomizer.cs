using System;
using UnityEngine;
using UnityEngine.Perception.Randomization.Parameters;
using UnityEngine.Perception.Randomization.Randomizers;

[Serializable]
[AddRandomizerMenu("Drone/MultipleChoice")]
public class MultiChoiceRandomizer : Randomizer
{
    public FloatParameter choice;

    protected override void OnIterationStart()
    {
        for(int i = 0; i<2; i++)
        {
            System.Collections.Generic.IEnumerable<MultipleChoiceTag> tags;
            switch(i)
            {
                case 0:
                    tags = tagManager.Query<MultiMaterialTag>();
                break;

                default:
                case 1:
                    tags = tagManager.Query<MultiObjectTag>();
                break;
            }

            foreach (MultipleChoiceTag tag in tags)
            {
                tag.SetChoice(choice.Sample());
            }
        }
         

        
        
    }
}
