using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joint 
{
   
    public string name { get; set; }
    public bool isRoot{get;set;}
    private Vector3 offset;
    private Transform selfTransform;
    private Transform parentTransform;
    private List<ChannelDescriptor> channels;
    private List<Joint> childrenJoints = new List<Joint>();


    public void AddChild(Joint joint) {

        childrenJoints.Add(joint);
    }

    public void setOffset(Vector3 offset){
        this.offset = offset;
    }

    public Vector3 getOffset(){
        return offset;
    }

    public void setParentTransform(Transform parentTransform){
        this.parentTransform = parentTransform;
    }

    public Transform getParentTransform(){
        return parentTransform;
    }

    public Transform getSelfTransform(){
        return selfTransform;
    }

    public void setSelfTransform(Transform selfTransform){
        this.selfTransform = selfTransform;
    }

    public void addChannel(ChannelDescriptor channel){
        if (this.channels == null) {
            this.channels = new List<ChannelDescriptor>(0);
        }

        channels.Add(channel);
    }

    public void addChannels(List<ChannelDescriptor> channels)
    {

        this.channels = channels;
    }

    public void addJoint(Joint joint){
        childrenJoints.Add(joint);
    }

    public IEnumerable<Joint> getIteratorChildJoints(){
        foreach (var joint in childrenJoints)
        {
            yield return joint;
        }
    }

    public IEnumerable<ChannelDescriptor> getIteratorChannels(){
        foreach (var channel in channels)
        {
            yield return channel;
        }
    }      
}
