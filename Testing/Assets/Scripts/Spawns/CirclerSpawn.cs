using UnityEngine;
using System.Collections;

public class CirclerSpawn : MonoBehaviour
{
    public GameObject Circler;
    private CirclerAI circlerScript;
    private string spawnName;
    private string node;
    private string targetName;
    //private Transform superNode;

    void Start()
    {
        spawnName = gameObject.transform.name[12].ToString();
        node = "Node";
        targetName = node + spawnName;
        //superNode = GameObject.Find("SuperNode").transform;
    }


    void Update()
    {
       
    }

    public void spawn()
    {
        var circler = Instantiate(Circler, gameObject.transform.position, transform.rotation);
        //circler.transform.SetParent(superNode);
        circlerScript = circler.GetComponent<CirclerAI>();
        circlerScript.targetNodeString = (targetName);
        circlerScript.hasTarget = true;

    }
}