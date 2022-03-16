using System;
using UnityEngine;
using UnityEngine.Perception.Randomization.Parameters;
using UnityEngine.Perception.Randomization.Randomizers;


/// <summary>
/// Randomiza possíveis escolhas (ex. material ou objeto que será ativado).
/// Os componentes randomizados devem extender "MultipleChoiceTag".
/// </summary>
[Serializable]
[AddRandomizerMenu("Drone/MultipleChoice")]
public class MultiChoiceRandomizer : Randomizer
{
    [Tooltip("Distribuição para realizar as escolhas. Deve estar entre 0 e 1.")] public FloatParameter choice;

    /// <summary>
    /// Define as escolhas de todos os objetos.
    /// </summary>
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
