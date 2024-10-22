using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector2 new_pos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0)){
            new_pos = new Vector2(mousePosition.x,transform.position.y);
        }
        
        transform.position = Vector2.MoveTowards(transform.position,new_pos,Time.deltaTime *10f);
    }
}
