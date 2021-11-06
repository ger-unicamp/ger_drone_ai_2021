using UnityEngine;
using UnityEngine.Perception.Randomization.Randomizers;

/// <summary>
/// Tag que deve ser componente da luz que deseja mudar (não tem parâmetros, eles só são alterados pelo LuzRandomizer)
/// </summary>
[AddComponentMenu("Perception/RandomizerTags/LuzTag")]
[RequireComponent(typeof(Light))]
public class LuzTag : RandomizerTag
{    
}