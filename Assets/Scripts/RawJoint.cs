using System.Collections.Generic;


public class RawJoint 
{
    private List<RawJoint> children = new List<RawJoint>(0);

    public string name { get; set; }

    public string offset { get; set; }

    public string channels { get; set; }

    public RawJoint getFirstChild() {

        return children[0];
    }

    public IEnumerable<RawJoint> getEnumerator() { 
            foreach(RawJoint child in children){
            yield return child; 
        }
    }

    public void Add(RawJoint child) {
        children.Add(child);
    }
}
