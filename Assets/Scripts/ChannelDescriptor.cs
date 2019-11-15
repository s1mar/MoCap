using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ChannelDescriptor
{

public CHANNEL_TYPE ChannelType{get; set;}
List<float> frameData = new List<float>();
private int pointer = 0; //tells the current pos of the pointer on the frame data list

public void addFrameData(float frameDatum){
    frameData.Add(frameDatum);
}

public Double popFrameData(){

    if(pointer>=frameData.Count){
        throw new System.ArgumentOutOfRangeException();
    }

    Double frameDatum = frameData[pointer++];
    return frameDatum;
}

public enum CHANNEL_TYPE {
    X_POSITION,
    Y_POSITION,
    Z_POSITION,
    X_ROTATION,
    Y_ROTATION,
    Z_ROTATION    

}   
}

