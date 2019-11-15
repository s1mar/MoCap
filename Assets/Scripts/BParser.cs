using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class BParser
{

    public static RawJoint parse(string body) {

        StringReader stringReader = new StringReader(body);
        string line;

        List<string> linesToProcess = new List<string>(0);
        while ((line = stringReader.ReadLine()) != null)
        {
            linesToProcess.Add(line);     
        }

        ParseResult<RawJoint> result= parseSetOfLines(linesToProcess);
        return result.Deliverable.getFirstChild();

    }

    public static Joint refineRawJointDataIntoJoints(RawJoint rawJoint) {

        Joint joint = new Joint();

        joint.name = rawJoint.name;

        joint.addChannels(extractChannels(rawJoint));

        joint.setOffset(extractVectorQuanity(rawJoint.offset));

        foreach (RawJoint j in rawJoint.getEnumerator()) {
            Joint jointChild = refineRawJointDataIntoJoints(j);
            joint.AddChild(jointChild);
        }

        return joint;
    }

    private static Vector3 extractVectorQuanity(string supposedVectorThreeAsString) {

        Vector3 offset = new Vector3(0, 0, 0);
        try
        {
            string[] splitted_components = supposedVectorThreeAsString.Split(null);
            float[] components = new float[] { 0.0f, 0.0f, 0.0f };

            int posCounterInTheFloatArrAtPos = 0;

            foreach (string s in splitted_components) {
                if (string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s)) {
                    continue;
                }
                components[posCounterInTheFloatArrAtPos++] = float.Parse(s);
            }

            offset.x = components[0];
            offset.y = components[1];
            offset.z = components[2];
        }
        catch (System.Exception ex) {
            Debug.Log("Error while extracting vector quantity: " + ex);
        }

        return offset;
    }

    private static List<ChannelDescriptor> extractChannels(RawJoint rawJoint) {
        List<ChannelDescriptor> channelDescriptors = new List<ChannelDescriptor>(0);
        if (string.IsNullOrEmpty(rawJoint.channels)) {
            return channelDescriptors;
        }
        else {
            string[] splittedChannelDescription = rawJoint.channels.Split(new char[] { ' ' });
            foreach (string c in splittedChannelDescription) {

                ChannelDescriptor channelDescriptor = new ChannelDescriptor();

                switch (c.ToLower()) {

                    case "xposition":
                        channelDescriptor.ChannelType = ChannelDescriptor.CHANNEL_TYPE.X_POSITION;
                        channelDescriptors.Add(channelDescriptor);
                        break;

                    case "yposition":
                        channelDescriptor.ChannelType = ChannelDescriptor.CHANNEL_TYPE.Y_POSITION;
                        channelDescriptors.Add(channelDescriptor);
                        break;

                    case "zposition":
                        channelDescriptor.ChannelType = ChannelDescriptor.CHANNEL_TYPE.Z_POSITION;
                        channelDescriptors.Add(channelDescriptor);
                        break;

                    case "xrotation":
                        channelDescriptor.ChannelType = ChannelDescriptor.CHANNEL_TYPE.X_ROTATION;
                        channelDescriptors.Add(channelDescriptor);
                        break;

                    case "yrotation":
                        channelDescriptor.ChannelType = ChannelDescriptor.CHANNEL_TYPE.Y_ROTATION;
                        channelDescriptors.Add(channelDescriptor);
                        break;

                    case "zrotation":
                        channelDescriptor.ChannelType = ChannelDescriptor.CHANNEL_TYPE.Z_ROTATION;
                        channelDescriptors.Add(channelDescriptor);
                        break;
                }
            }

            return channelDescriptors;
        }   
        
        
    }

    private static ParseResult<RawJoint> parseSetOfLines(List<string> lines, int index = 0,string nodeName=null) {

        RawJoint joint = new RawJoint();

        int i = 0;
        for (i = index; i < lines.Count; i++) {

            int lineIndexer;
            string lineBeingScanned = lines[i];

            if ((lineIndexer = lineHasToken(Token.CLOSING_BRACE, lineBeingScanned)) > -1)
            {
                break;
            }
            else if ((lineIndexer = lineHasToken(Token.OPENING_BRACE, lineBeingScanned)) > -1)
            {
                //This is the start of a new joint, it also means that the previous line has the joint name
                int j = i;
                string lineWhichHasName = lines[j - 1];
                int indexOfNameToken = lineHasJointIndex(lineWhichHasName);
                string nameOfJoint = extractJointNameFromLine(lineWhichHasName, indexOfNameToken);

                ParseResult<RawJoint> resultRecieved = parseSetOfLines(lines, i+1,nameOfJoint);
                i = resultRecieved.LastProcessed_Index;

                joint.Add(resultRecieved.Deliverable);
            }
            else {

                if ((lineIndexer = lineHasToken(Token.OFFSET, lineBeingScanned)) > -1)
                {
                    joint.offset = extractOffsetFromLine(lineBeingScanned, lineIndexer);
                }
                else if ((lineIndexer = lineHasToken(Token.CHANNELS, lineBeingScanned)) > -1) {
                    joint.channels = extractChannelsFromLine(lineBeingScanned, lineIndexer);
                }
            }

            
          
        }

        if (!string.IsNullOrEmpty(nodeName)) {
            joint.name = nodeName;
        }
        ParseResult<RawJoint> parseResult = new ParseResult<RawJoint>();
        parseResult.LastProcessed_Index = i;
        parseResult.Deliverable = joint;
        return parseResult;
    }
        
  

    private static int lineHasToken(string token,string line) {

        return line.IndexOf(token);
    }
    
    private static int lineHasJointIndex(string line) {
    //Check to see if there is a joint or root inside this line
    int indexOfJoint = line.IndexOf(Token.JOINT);
    indexOfJoint = indexOfJoint < 0 ? line.IndexOf(Token.ROOT) : indexOfJoint;
    return indexOfJoint;
    }

    private static string extractChannelsFromLine(string line, int index)
    {
        index += 9;
        string channels = line.Substring(index).Trim();
        return channels;

    }

    private static string extractOffsetFromLine(string line, int index) {
        index += 7;
        string offset = line.Substring(index).Trim();
        return offset;
    
    }

    
    private static string extractJointNameFromLine(string line, int index) {

        index = index + 5;
        string name = line.Substring(index).Trim(); // remove the extra white-spaces 
        return name;
    }

   sealed  class Token {
        public static readonly string ROOT = "ROOT";
        public static readonly string JOINT = "JOINT";
        public static readonly string OPENING_BRACE = "{";
        public static readonly string CLOSING_BRACE = "}";
        public static readonly string OFFSET = "OFFSET";
        public static readonly string CHANNELS = "CHANNELS";
    }

    public class ParseResult<T>{

        public int LastProcessed_Index { get; set; }
        public T Deliverable { get; set; }

    }
}
