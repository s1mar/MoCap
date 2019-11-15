using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;


/**
The parser will go through a bvh file and generate a hierarchy or skeleton of joints
Assumptions:
1. Only 1 root;
Pseudo-Algo:
1 Determine the root joint and it's opening and closing braces, also the total number of frames and the frame time.
2 Using the frame-time,determine the approx frame rate using Math.round

 */
public class BVHParser 
{
    /*
    private char[] body;
    
    private string rootJointName;
    private int rootStartBraceIndex=-1; //the default value of -1 signifies that this brace has yet to be encountered
    private int rootEndBraceIndex=-1;
    
    private int numberOfFrames;
    private float frameTime;
    private int frameRate;
  
    
   public BVHParser(string body){
        this.body = body.ToCharArray();
    }
    */
    //Pseudo steps 1 & 2   
   /* void stepUnoParsing_FindRoot(){
           
            int currentIndex = 0;
           
            for (int i = 0; i < body.Length; i++) {

                if (body[i] == 'R' && body[i+1] =='O' && body[i+2] == 'O' && body[i+3] == 'T') {
               
                    //The next sequence of chars would determine the name of the root joint
                    int j = i + 4;
                    StringBuilder stringBuilderRootName = new StringBuilder(); 
                    while (rootStartBraceIndex < 0) {

                    char charUnderScanner = body[j];
                    if (charUnderScanner == '{')
                    {
                        // update the root name and the start index indices and then break out of this
                        rootJointName = stringBuilderRootName.ToString().Trim();
                        rootStartBraceIndex = j;
                    }
                    else {

                        if (!System.Char.IsWhiteSpace(charUnderScanner)) {
                            stringBuilderRootName.Append(charUnderScanner);
                        }

                    }

                        j += 1;
                    }
              
                }
            
            }
               
    }
*/


    /*
    //This function should be more than enought to extract all the joints as raw data
    public static RawJoint recursiveRawParse(string body, int braceStart = 0) {

        Debug.Log("Times called: "+ ++timesCalled);

        string bodyToProcess = body.Substring(braceStart); //We'll start processing from the first '{' brace encountered
        RawJoint joint = new RawJoint();
        //StringBuilder strBuilder = new StringBuilder();
        //StringFast strBuilder = new StringFast();
        int strPointer = braceStart;
        foreach (char s in bodyToProcess) {
            if (s == '}')
            {
                break;
            }
            else if (s == '{')
            {
                //We have another joint
                joint.Add(recursiveRawParse(bodyToProcess.Substring(strPointer)));

            }
            else {
                //strBuilder.Append(s);
            }

            strPointer++;
       }
        //joint.SelfBody = strBuilder.ToString().Trim();
        Debug.Log("Joint detected: " + joint.SelfBody);
        return joint;
    }
    */

  /*  public static ParseResult<RawJoint> parseJointHierarchy(string body, int indexOfStartBrace = -1) {

        RawJoint joint = new RawJoint();
        indexOfStartBrace = indexOfStartBrace == -1? body.IndexOf("{"):indexOfStartBrace;
       
        char[] name = new char[24]; //Assumption that a joint name can never go beyond 24 characters in length
        int nameAppendCounter = 0;
        
        for (int i = indexOfStartBrace; i >= 0; i--) {

            char scannedChar = body[i];
            if (!System.Char.IsWhiteSpace(scannedChar))
            {
                name[nameAppendCounter++] = scannedChar;
            }
            else {
                break;
            }
        
        }
        joint.name = new string(name);
        joint.name = joint.name.Trim();

        //Now that we have the joint name, let's get it's rest of the body 

        StringFast bodyChars = new StringFast();

        int j;
        for (j = indexOfStartBrace + 1; j < body.Length; j++) {

            char scannedChar = body[j];
            if (scannedChar == '}')
            {
                break;
            }
            else if (scannedChar == '{')
            {
                ParseResult<RawJoint> parseResult = (parseJointHierarchy(body, j));
                j = parseResult.LastProcessed_Index; //This would ensure that the method resumes parsing after the processed block
                joint.Add(parseResult.Deliverable);
            }
            else {
                    bodyChars.Append(scannedChar);   
            }

        }
        joint.header = bodyChars.ToString().Trim();
        ParseResult<RawJoint> result = new ParseResult<RawJoint>();
        result.LastProcessed_Index = j;
        result.Deliverable = joint;

        return result;
    }

    public class ParseResult<T> {

        public int LastProcessed_Index { get; set; }
        public T Deliverable { get; set; }
        
    }*/

}
