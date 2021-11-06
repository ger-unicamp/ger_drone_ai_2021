using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Perception.Randomization.Randomizers;

/// <summary>
/// Tag que deve ser componente do objeto para randomizar seu ângulo espacial 
/// </summary>
[AddComponentMenu("Perception/RandomizerTags/AnguloTag")]
public class AnguloTag : RandomizerTag
{
    [Tooltip("Rotação mínima para o objeto no eixo X.")] public float minX;  //possibilita colocar maximo angulo e minimo para cada objeto
    [Tooltip("Posição máxima para o objeto no eixo X.")] public float maxX;
    [Tooltip("Posição mínima para o objeto no eixo Y.")] public float minY;
    [Tooltip("Posição máxima para o objeto no eixo Y.")] public float maxY;
    [Tooltip("Posição mínima para o objeto no eixo Z.")] public float minZ;
    [Tooltip("Posição máxima para o objeto no eixo Z.")] public float maxZ;

    /// <summary>
    /// Função que altera o ângulo espacial dos objetos em função de parâmetros aleatorizados em x, y e z
    /// </summary>
    /// <param name="xRotation">Variável aleatória entre 0 e 1 que é responsável pela parte aleatorizada da rotação em X.</param>
    /// <param name="yRotation">Variável aleatória entre 0 e 1 que é responsável pela parte aleatorizada da rotação em Y.</param>
    /// <param name="zRotation">Variável aleatória entre 0 e 1 que é responsável pela parte aleatorizada da rotação em Z.</param> 
    public void SetRotation(float xRotation, float yRotation, float zRotation)  //acao chamada para cada objeto com o tag pelo "AnguloRandomizer"
    {
        xRotation = (xRotation * (maxX-minX)) + minX;   //baseado nos 3 parametros de 0 a 1 para a randomizacao, atualiza a rotacao do objeto
        yRotation = (yRotation * (maxY-minY)) + minY;
        zRotation = (zRotation * (maxZ-minZ)) + minZ;
        Vector3 rotationVector = new Vector3(xRotation, yRotation, zRotation); //precisa alterar para quatérnio antes de rotacionar
        transform.localRotation = Quaternion.Euler(rotationVector); //funcao do unity pra rotacionar o objeto baseado no novo vetor    
    } 
}


