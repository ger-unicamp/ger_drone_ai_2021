using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Perception.Randomization.Randomizers;

[AddComponentMenu("Perception/RandomizerTags/AnguloTag")]
public class AnguloTag : RandomizerTag
{
    public float minX;  //possibilita colocar maximo angulo e minimo para cada objeto
    public float maxX;
    public float minY;
    public float maxY;
    public float minZ;
    public float maxZ;

    public void SetRotation(float xRotation, float yRotation, float zRotation)  //acao chamada para cada objeto com o tag pelo "AnguloRandomizer"
    {
        xRotation = (xRotation * (maxX-minX)) + minX;   //baseado nos 3 parametros de 0 a 1 para a randomizacao, atualiza a rotacao do objeto
        yRotation = (yRotation * (maxY-minY)) + minY;
        zRotation = (zRotation * (maxZ-minZ)) + minZ;
        Vector3 rotationVector = new Vector3(xRotation, yRotation, zRotation);
        transform.localRotation = Quaternion.Euler(rotationVector); //funcao do unity pra rotacionar o objeto baseado no novo vetor    
    } 
}


