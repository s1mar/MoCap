using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameManager 
{
    public List<float> frameDataRaw = new List<float>(0);
    public int NumOfFrames { get; set; }
    public int Fps { get; set; }
    public float FrameTime { get {
            return this.FrameTime;
        
        } set {
            this.FrameTime = value;
            this.Fps = (int)(1 / value);
    } }
        
    public void addFrameData(float datum)
    {
        frameDataRaw.Add(datum);
    }

    //They are meant to be sequential in the given frame order, a design inelegance that I'd solve later
    private List<ChannelDescriptorAndOffset> frameData = new List<ChannelDescriptorAndOffset>(0);

    public void AddChannelDescriptorAndOffset(ChannelDescriptor descriptor, float frameData) {

        ChannelDescriptorAndOffset datum = new ChannelDescriptorAndOffset();
        datum.ChannelDescriptor = descriptor;
        datum.Offset = frameData;
    }

    public sealed class ChannelDescriptorAndOffset {

        public ChannelDescriptor ChannelDescriptor { get; set; }
        public float Offset { get; set; }
    }
}
