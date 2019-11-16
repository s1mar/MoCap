using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidController : MonoBehaviour
{
    [SerializeField] Transform Hips;
    
    [SerializeField] Transform LeftHip;
    [SerializeField] Transform RightHip;
    [SerializeField] Transform LeftKnee;
    [SerializeField] Transform RightKnee;
    [SerializeField] Transform LeftAnkle;
    [SerializeField] Transform RightAnkle;

    [SerializeField] Transform Chest;
    [SerializeField] Transform LeftCollar;
    [SerializeField] Transform RightCollar;
    [SerializeField] Transform LeftShoulder;
    [SerializeField] Transform RightShoulder;
    [SerializeField] Transform LeftElbow;
    [SerializeField] Transform RightElbow;
    [SerializeField] Transform LeftWrist;
    [SerializeField] Transform RightWrist;

    [SerializeField] Transform Neck;
    [SerializeField] Transform Head;


    private JointModel joint;

    private int numOfFrames;

    private int fps;

    private bool startEngine;

    private float subTimeAccumulator;

    void Start() {
        //parse and get the parent joint
        parseDataAndInitialize();

        //assign the respective transforms to the joint hierarchy and set-offsets
        assignTransformsInTheJointDataStructureAndSetOffsets();

        //Now that we have the skeleton setup according to an initial configuration
        //startEngine = true;

    }

    void Update() {

        if (startEngine && numOfFrames>0) {

            subTimeAccumulator += Time.deltaTime;
            if (subTimeAccumulator >= fps) {
                subTimeAccumulator = 0.0f; //reset the accumulator and execute a frame
                numOfFrames--;
                executeFrame();
            }

        }

    }

    void executeFrame(JointModel joint = null) {

        if (joint == null) {
            joint = this.joint;
        }
        Transform transformOfTheJoint = joint.getSelfTransform();
        Vector3 newPos = transformOfTheJoint.localPosition;
        Vector3 newRot = transformOfTheJoint.localEulerAngles;

        foreach (ChannelDescriptor channel in joint.getIteratorChannels()) {

            ChannelDescriptor.CHANNEL_TYPE channelType = channel.ChannelType;
            float frameDatum = channel.popFrameData();  


            switch (channelType) {

                case ChannelDescriptor.CHANNEL_TYPE.X_POSITION:
                    newPos.x = +frameDatum;
                    break;

                case ChannelDescriptor.CHANNEL_TYPE.Y_POSITION:
                    newPos.y += frameDatum;
                    break;

                case ChannelDescriptor.CHANNEL_TYPE.Z_POSITION:
                    newPos.z += frameDatum;
                    break;

                case ChannelDescriptor.CHANNEL_TYPE.X_ROTATION:
                    newRot.x = +frameDatum;
                    break;

                case ChannelDescriptor.CHANNEL_TYPE.Y_ROTATION:
                    newRot.y += frameDatum;
                    break;

                case ChannelDescriptor.CHANNEL_TYPE.Z_ROTATION:
                    newRot.z += frameDatum;
                    break;

            }
        
        }

        if (newPos != transformOfTheJoint.localPosition) {
            transformOfTheJoint.position = newPos;
        }

        if (newRot != transformOfTheJoint.localEulerAngles) {
            transformOfTheJoint.localEulerAngles = newRot;
        }

        foreach (JointModel child in joint.getIteratorChildJoints())
        {
            executeFrame(child);
        }

    }

    void assignTransformsInTheJointDataStructureAndSetOffsets() {
        findTransformAndAssignToJointWithOffsetAdjustments(joint);
    }

    void findTransformAndAssignToJointWithOffsetAdjustments(JointModel joint, JointModel parentJoint = null) {

        switch (joint.name) {

            case TXT_JOINTS.Hips:
                joint.setSelfTransform(Hips);
                break;

            case TXT_JOINTS.LeftHip:
                joint.setSelfTransform(LeftHip);
                break;

            case TXT_JOINTS.RightHip:
                joint.setSelfTransform(RightHip);
                break;

            case TXT_JOINTS.LeftKnee:

                joint.setSelfTransform(LeftKnee);
                break;

            case TXT_JOINTS.RightKnee:
                joint.setSelfTransform(RightKnee);
                break;

            case TXT_JOINTS.LeftAnkle:
                joint.setSelfTransform(LeftAnkle);
                break;

            case TXT_JOINTS.RightAnkle:
                joint.setSelfTransform(RightAnkle);
                break;

            case TXT_JOINTS.Chest:
                joint.setSelfTransform(Chest);
                break;

            case TXT_JOINTS.Neck:
                joint.setSelfTransform(Neck);
                break;

            case TXT_JOINTS.Head:
                joint.setSelfTransform(Head);
                break;

            case TXT_JOINTS.LeftCollar:
                joint.setSelfTransform(LeftCollar);
                break;

            case TXT_JOINTS.RightCollar:
                joint.setSelfTransform(RightCollar);
                break;

            case TXT_JOINTS.LeftShoulder:
                joint.setSelfTransform(LeftShoulder);
                break;

            case TXT_JOINTS.RightShoulder:
                joint.setSelfTransform(RightShoulder);
                break;

            case TXT_JOINTS.RightElbow:
                joint.setSelfTransform(RightElbow);
                break;

            case TXT_JOINTS.LeftElbow:
                joint.setSelfTransform(LeftElbow);
                break;

            case TXT_JOINTS.RightWrist:
                joint.setSelfTransform(RightWrist);
                break;

            case TXT_JOINTS.LeftWrist:
                joint.setSelfTransform(LeftWrist);
                break;
            
        }
            
        //set offset
        if (parentJoint != null) {
            joint.setPosRelativeToTheParent(parentJoint.getSelfTransform().localPosition);
        }

        foreach (JointModel child in joint.getIteratorChildJoints())
        {
            findTransformAndAssignToJointWithOffsetAdjustments(child,joint);
        }
    
    }

    void parseDataAndInitialize()
    {

        TextAsset rawBvhFile = Resources.Load("jog") as TextAsset;

        BParser.RawParseResult rawParseResult = BParser.parse(rawBvhFile.text);
        RawJoint rawJoint = rawParseResult.RawJoint;

        fps = (int)(1 / rawParseResult.FrameTime);
        numOfFrames = rawParseResult.NumFrames;

        List<ChannelDescriptor> channelsExtracted = new List<ChannelDescriptor>(0);

        joint = BParser.refineRawJointDataIntoJoints(rawJoint, ref channelsExtracted);
        
        //This statement below should technically update all the channel descriptors 
        
        BParser.assigningFrameDataToChannelDescriptors(new Queue<float>(rawParseResult.FrameData), channelsExtracted);
        
        
    }

   private sealed class TXT_JOINTS
    {
        public const string Hips = "Hips";
        public const string LeftHip = "LeftHip";
        public const string RightHip = "RightHip";
        public const string LeftKnee = "LeftKnee";
        public const string RightKnee = "RightKnee";
        public const string RightAnkle = "RightAnkle";
        public const string LeftAnkle = "LeftAnkle";
        public const string Chest = "Chest";
        public const string LeftCollar = "LeftCollar";
        public const string RightCollar = "RightCollar";
        public const string RightShoulder = "RightShoulder";
        public const string LeftShoulder = "LeftShoulder";
        public const string LeftElbow = "LeftElbow";
        public const string RightElbow = "RightElbow";
        public const string RightWrist = "RightWrist";
        public const string LeftWrist = "LeftWrist";
        public const string Neck = "Neck";
        public const string Head = "Head";
    }


}
