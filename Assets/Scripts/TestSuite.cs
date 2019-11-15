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
        
        BParser.RawParseResult rawParseResult = BParser.parse(rawBvhFile.text);
        RawJoint rawJoint = rawParseResult.RawJoint;
        
    
        List<ChannelDescriptor> channelsExtracted = new List<ChannelDescriptor>(0);
        
        Joint refinedJoint = BParser.refineRawJointDataIntoJoints(rawJoint,ref channelsExtracted);
        //This statement below should technically update all the channel descriptors 
        BParser.assigningFrameDataToChannelDescriptors(new Queue<float>(rawParseResult.FrameData), channelsExtracted);
        Debug.Log("The name of the joint:");
    }
}
