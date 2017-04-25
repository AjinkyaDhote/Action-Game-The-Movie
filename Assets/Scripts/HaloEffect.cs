using UnityEngine;

public class HaloEffect : MonoBehaviour
{

    private int frameCount;
    private int activeHalo = 0;
    // Use this for initialization
    void Start()
    {
        frameCount = 0;
        foreach (Transform t in transform)
        {
            if (t.name == "sub 1")
            {

                Component halo = t.GetComponent("Halo");
                halo.GetType().GetProperty("enabled").SetValue(halo, false, null);
            }
            else if (t.name == "sub 2")
            {

                Component halo = t.GetComponent("Halo");
                halo.GetType().GetProperty("enabled").SetValue(halo, false, null);
            }
            else if (t.name == "sub 3")
            {

                Component halo = t.GetComponent("Halo");
                halo.GetType().GetProperty("enabled").SetValue(halo, false, null);
            }
            else if (t.name == "sub 4")
            {

                Component halo = t.GetComponent("Halo");
                halo.GetType().GetProperty("enabled").SetValue(halo, false, null);
            }


        }

    }

    // Update is called once per frame
    void Update()
    {
        frameCount++;

        if (frameCount % 30 == 0)
        {

            activeHalo++;
            if (activeHalo % 4 == 0)
            {
                foreach (Transform t in transform)
                {
                    if (t.name == "sub 1")
                    {

                        Component halo = t.GetComponent("Halo");
                        halo.GetType().GetProperty("enabled").SetValue(halo, true, null);
                    }
                    else if (t.name == "sub 2")
                    {

                        Component halo = t.GetComponent("Halo");
                        halo.GetType().GetProperty("enabled").SetValue(halo, false, null);
                    }
                    else if (t.name == "sub 3")
                    {

                        Component halo = t.GetComponent("Halo");
                        halo.GetType().GetProperty("enabled").SetValue(halo, false, null);
                    }
                    else if (t.name == "sub 4")
                    {

                        Component halo = t.GetComponent("Halo");
                        halo.GetType().GetProperty("enabled").SetValue(halo, false, null);
                    }


                }
            }
            else if (activeHalo % 4 == 1)
            {
                foreach (Transform t in transform)
                {
                    if (t.name == "sub 1")
                    {

                        Component halo = t.GetComponent("Halo");
                        halo.GetType().GetProperty("enabled").SetValue(halo, false, null);
                    }
                    else if (t.name == "sub 2")
                    {

                        Component halo = t.GetComponent("Halo");
                        halo.GetType().GetProperty("enabled").SetValue(halo, true, null);
                    }
                    else if (t.name == "sub 3")
                    {

                        Component halo = t.GetComponent("Halo");
                        halo.GetType().GetProperty("enabled").SetValue(halo, false, null);
                    }
                    else if (t.name == "sub 4")
                    {

                        Component halo = t.GetComponent("Halo");
                        halo.GetType().GetProperty("enabled").SetValue(halo, false, null);
                    }


                }
            }
            else if (activeHalo % 4 == 2)
            {
                foreach (Transform t in transform)
                {
                    if (t.name == "sub 1")
                    {

                        Component halo = t.GetComponent("Halo");
                        halo.GetType().GetProperty("enabled").SetValue(halo, false, null);
                    }
                    else if (t.name == "sub 2")
                    {

                        Component halo = t.GetComponent("Halo");
                        halo.GetType().GetProperty("enabled").SetValue(halo, false, null);
                    }
                    else if (t.name == "sub 3")
                    {

                        Component halo = t.GetComponent("Halo");
                        halo.GetType().GetProperty("enabled").SetValue(halo, true, null);
                    }
                    else if (t.name == "sub 4")
                    {

                        Component halo = t.GetComponent("Halo");
                        halo.GetType().GetProperty("enabled").SetValue(halo, false, null);
                    }


                }
            }

            else if (activeHalo % 4 == 3)
            {
                foreach (Transform t in transform)
                {
                    if (t.name == "sub 1")
                    {

                        Component halo = t.GetComponent("Halo");
                        halo.GetType().GetProperty("enabled").SetValue(halo, false, null);
                    }
                    else if (t.name == "sub 2")
                    {

                        Component halo = t.GetComponent("Halo");
                        halo.GetType().GetProperty("enabled").SetValue(halo, false, null);
                    }
                    else if (t.name == "sub 3")
                    {

                        Component halo = t.GetComponent("Halo");
                        halo.GetType().GetProperty("enabled").SetValue(halo, false, null);
                    }
                    else if (t.name == "sub 4")
                    {

                        Component halo = t.GetComponent("Halo");
                        halo.GetType().GetProperty("enabled").SetValue(halo, true, null);
                    }
                }
            }
        }
    }
}
