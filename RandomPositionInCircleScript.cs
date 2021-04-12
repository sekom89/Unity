using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPositionInCircleScript : MonoBehaviour
{

    public GameObject playerObj;
    public GameObject tempPointObj;

    public float outerCircleRadius = 0;
    public float innerCircleRadius = 0;

    public int max_count = 1;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < max_count; ++i){
            var pos = GameObject.Instantiate(tempPointObj, playerObj.transform);
            pos.transform.position = GetRandomPositionInCircle(outerCircleRadius, innerCircleRadius);
        }        
    }


     private Vector3 GetRandomPositionInCircle( float outerCircleRadius, float innerCircleRadius )
     { 
        var innerRadius = ( outerCircleRadius -  innerCircleRadius ) * 0.5f;
        var outerRadius = innerRadius + innerCircleRadius; 
        
        //6.28 = PI * 2
        var rndAngle = Random.value * 6.28f;
     
        // determine position
        var cX = Mathf.Sin( rndAngle );
        var cZ = Mathf.Cos( rndAngle );
     
        var outerPos = new Vector3( cX, 0, cZ );
        outerPos *= outerRadius;

        var sPos = Random.insideUnitSphere * innerRadius;

        var resultPos = outerPos + sPos;
        return resultPos;  
    }
}
