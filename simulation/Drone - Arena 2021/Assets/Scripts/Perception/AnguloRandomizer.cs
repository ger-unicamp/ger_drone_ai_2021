using System;
using UnityEngine;
using UnityEngine.Perception.Randomization.Parameters;
using UnityEngine.Perception.Randomization.Randomizers;

/// <summary>
/// Randomizador para randomizar o ângulo espacial de um objeto
/// </summary>
[Serializable]
[AddRandomizerMenu("Perception/AnguloRandomizer")]
public class AnguloRandomizer : Randomizer
{
    [Tooltip("Distribuição da aleatorização dos ângulos em X, sempre entre 0 e 1.")] public FloatParameter xRotation;
    [Tooltip("Distribuição da aleatorização dos ângulos em Y, sempre entre 0 e 1.")] public FloatParameter yRotation;
    [Tooltip("Distribuição da aleatorização dos ângulos em Z, sempre entre 0 e 1.")] public FloatParameter zRotation;

    /// <summary>
    /// Verifica os objetos que precisam ser randomizados, e randomiza.
    /// </summary>
    protected override void OnIterationStart()
    {
        var tags = tagManager.Query<AnguloTag>();
        foreach (var tag in tags)   //faz o loop da chamada do randomizador para cada objeto com AnguloTag
        {
            tag.SetRotation(xRotation.Sample(), yRotation.Sample(), zRotation.Sample());    //chama o randomizador passando 3 parametros 
                                                                                            //contendo valores aleatorios de 0 a 1 pelo .Sample()
        }
    }
}
