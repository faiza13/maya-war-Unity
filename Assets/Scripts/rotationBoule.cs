using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class rotationBoule : MonoBehaviour
{
	
    // Use this for initialization
    void Start()
    {

         transform.rotation = Quaternion.Euler(0,transform.eulerAngles.z,0);

    }



    
}
