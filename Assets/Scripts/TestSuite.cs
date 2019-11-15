using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSuite : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        parseTest1();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void parseTest1() {
        
        TextAsset rawBvhFile = Resources.Load("ex1") as TextAsset;
        RawJoint rawJoint = BParser.parse(rawBvhFile.text);
        Joint refinedJoint = BParser.refineRawJointDataIntoJoints(rawJoint);
        Debug.Log("The name of the joint:");
    }
}
