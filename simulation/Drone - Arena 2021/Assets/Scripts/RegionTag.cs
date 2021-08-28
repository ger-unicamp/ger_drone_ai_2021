using UnityEngine;
using UnityEngine.Perception.Randomization.Randomizers;

[AddComponentMenu("Perception/RandomizerTags/RegionTag")]
public class RegionTag : RandomizerTag
{
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    public float minZ;
    public float maxZ;

    public void SetPosition(float xPosition, float yPosition, float zPosition)
    {
        xPosition = (xPosition * (maxX-minX)) + minX;
        yPosition = (yPosition * (maxY-minY)) + minY;
        zPosition = (zPosition * (maxZ-minZ)) + minZ;

        //Debug all positions in one line
        Debug.Log(xPosition + " " + yPosition + " " + zPosition); 

        //Debug.Log(string.Format());
        
        //var transform = gameObject.GetComponent<Transform>();    

        //Change father transform
        


        transform.localPosition = new Vector3(xPosition, yPosition, zPosition);     
    } 
}

