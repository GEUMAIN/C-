using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class problemnum : MonoBehaviour
{
    public static int clearproblemnum = 0;
    public static int failproblemnum = 0;

    public TypeWriterEffect11 script11Instance;
    public TypeWriterEffect12 script12Instance;
    public TypeWriterEffect13 script13Instance;

    public void Start()
    {
        normalend();
        allfailend();
        allclearend();
    }
    public void normalend()
    {
        if (clearproblemnum < 26 && failproblemnum < 26)
        {
            script11Instance.enabled = true;
            script11Instance.StartTalk(script12Instance.Txtdialogue);
        }
    }
    public void allfailend()
    {
        if (failproblemnum == 26)
        {
            script12Instance.enabled = true;
            script12Instance.StartTalk(script12Instance.Txtdialogue);
        }
    }
    public void allclearend()
    {
        if (clearproblemnum == 26)
        {
            script13Instance.enabled = true;
            script13Instance.StartTalk(script12Instance.Txtdialogue);
        }
    }
}
