using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isLocalPlayer = true;
    private SyncPositionRequest syncPosRequest;
    private Vector3 lastPosition = Vector3.zero;
    private float moveOffSet = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        if(isLocalPlayer)
        {
            GetComponent<Renderer>().material.color = Color.red;
            syncPosRequest = GetComponent<SyncPositionRequest>();
            InvokeRepeating("SyncPosition", 3, 0.1f);
        }
    }

    void SyncPosition()
    {
        if(Vector3.Distance(transform.position,lastPosition)>moveOffSet)
        {
            lastPosition = transform.position;
            syncPosRequest.pos = transform.position;
            syncPosRequest.DefaultRequest();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isLocalPlayer)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            transform.Translate(new Vector3(h, 0, v) * Time.deltaTime * 4);
        }
    }
}
