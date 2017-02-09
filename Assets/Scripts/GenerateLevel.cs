using UnityEngine;
using System.Collections;

public class GenerateLevel : MonoBehaviour {

    public GameObject wall;
    int elementsInRow = 201;
    int elementsInColumn = 201;
    int x_center_adjust = 100;
    int y_center_adjust = 60;

    // Use this for initialization
    void Start () {
        TextAsset t1 = (TextAsset)Resources.Load("level", typeof(TextAsset));
        TextAsset t2 = (TextAsset)Resources.Load("rotation", typeof(TextAsset));

        string s = t1.text;
        string rotationValues = t2.text;

        int i;
        s = s.Replace("\r\n","");
        
        rotationValues = rotationValues.Replace("\r\n", "");
        
        for (i = 0; i < s.Length; i++)

        {
            
            if (s[i] == '1' )

            {

                int column, row;

                column = i % elementsInRow;

                row = i / elementsInColumn;

                GameObject t;
                
               t = (GameObject)(Instantiate(wall, new Vector3(x_center_adjust - column , 5.5f, y_center_adjust - row), Quaternion.identity));
                /*if (rotationValues[i] == '1')
                {

                   // t.transform.eulerAngles = new Vector3(0, 90, 0);
                }*/

            }

        }

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
