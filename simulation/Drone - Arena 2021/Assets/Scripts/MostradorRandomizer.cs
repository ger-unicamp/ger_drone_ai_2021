using System;
using UnityEngine;
using UnityEngine.Perception.Randomization.Parameters;
using UnityEngine.Perception.Randomization.Randomizers;

/// <summary>
/// Randomizador para mudar o número que aparece nos mostradores digitais do Desafio de Robótica Petrobrás. 
/// Funciona com o MostradorTag.
/// </summary>
[Serializable]
[AddRandomizerMenu("Drone/MostradorRandomizer")]
public class MostradorRandomizer : Randomizer
{
    [Tooltip("Distribuição para amostrar a dezena do primeiro número. Deve estar entre 0 e 10.")] public IntegerParameter digit1;
    [Tooltip("Distribuição para amostrar a unidade do primeiro número. Deve estar entre 0 e 10.")] public IntegerParameter digit2;
    [Tooltip("Distribuição para amostrar a unidade do segundo número. Deve estar entre 0 e 10.")] public IntegerParameter digit3;
    [Tooltip("Distribuição para amostrar se o segundo número é negativo.")] public BooleanParameter minus;
    [Tooltip("Distribuição para amostrar a dezena do segundo número (1 ou 0).")] public BooleanParameter one;

    /// <summary>
    /// Verifica os objetos do tipo mostrador que precisam ter os números randomizados, e randomiza-os.
    /// </summary>
    protected override void OnIterationStart()
    {
        var tags = tagManager.Query<MostradorTag>();
        foreach (MostradorTag tag in tags)
        {
            tag.SetNumber(digit1.Sample(), digit2.Sample(), digit3.Sample(), minus.Sample(), one.Sample());
        }
        
    }
}
