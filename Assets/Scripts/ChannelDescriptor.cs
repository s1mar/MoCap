using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ChannelDescriptor
{

public CHANNEL_TYPE ChannelType{get; set;}
List<System.Double> frameData = new List<System.Double>();
private int pointer = 0; //tells the current pos of the pointer on the frame data list

public void addFrameData(Double frameDatum){
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

