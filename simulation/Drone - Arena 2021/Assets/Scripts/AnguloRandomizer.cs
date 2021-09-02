using System;
using UnityEngine;
using UnityEngine.Perception.Randomization.Parameters;
using UnityEngine.Perception.Randomization.Randomizers;

[Serializable]
[AddRandomizerMenu("Perception/AnguloRandomizer")]
public class AnguloRandomizer : Randomizer
{
    public FloatParameter xRotation;
    public FloatParameter yRotation;
    public FloatParameter zRotation;


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
