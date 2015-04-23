﻿using UnityEngine;
using System.Collections;

public class test : MonoBehaviour
{
    float x;
    float y;
    public Animation idle;
    public Animation attack;
    public Animation skill;
    public Animation run;
    Vector3 startPosion;
    Quaternion startRoation;
    
    // Use this for initialization
    void Start()
    {
        x = transform.eulerAngles.y;
        y = transform.eulerAngles.x;

        transform.GetComponent<Animation>().AddClip(idle.clip,"idle");
        transform.GetComponent<Animation>().AddClip(attack.clip, "attack");
        transform.GetComponent<Animation>().AddClip(skill.clip, "skill");
        transform.GetComponent<Animation>().AddClip(run.clip, "run");

        Input.multiTouchEnabled = true;

        startPosion = transform.position;
        startRoation = transform.rotation;
    }
    float touchTime=0;
    bool moving = false;
    bool running = false;
    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1)
        {
            if (moving==false&& Input.GetTouch(0).phase == TouchPhase.Stationary)
            {
                touchTime += Time.deltaTime;
            }

            if(touchTime>0.5f)
            {
                transform.GetComponent<Animation>().clip = run.clip;
                transform.GetComponent<Animation>().Play();

                running = true;
                transform.Translate(0, 0, 3*Time.deltaTime);
            }

            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                moving = true;
                x -= Input.GetAxis("Mouse X") * 5;
                y -= Input.GetAxis("Mouse Y") * 2.4f;

                transform.rotation = Quaternion.Euler(y, x, 0);
            }

            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                if (!moving && touchTime <= 0.5f)
                {
                    transform.GetComponent<Animation>().clip = attack.clip;
                    transform.GetComponent<Animation>().Play();
                }
                touchTime = 0;
                moving = false;
                if (running)
                {
                    transform.position = startPosion;
                }
                running = false;
            }

            

            //if (Input.GetTouch(0).phase == TouchPhase.Stationary)
            //{
            //    if(Input.touches[0].deltaTime<0.5f)
            //    {
            //        transform.GetComponent<Animation>().clip = attack.clip;
            //        transform.GetComponent<Animation>().Play();
            //    }
            //    if(Input.touches[0].deltaTime>0.5f)
            //    {
            //        transform.GetComponent<Animation>().clip = run.clip;
            //        transform.GetComponent<Animation>().Play();
            //    }
            //}
        }
        else if(Input.touchCount>1)
        {
            Vector2[] fingerPos=new Vector2[2];

            Vector2[] fingerDeltapos = new Vector2[2];

            float move;

            if(Input.touches[0].phase==TouchPhase.Moved&&Input.touches[1].phase==TouchPhase.Moved)
            {
                moving = true;
                fingerPos[0] = Input.touches[0].position;
                fingerPos[1] = Input.touches[1].position;
                fingerDeltapos[0] = Input.touches[0].deltaPosition;
                fingerDeltapos[1] = Input.touches[1].deltaPosition;

                move = fingerPos[0].x >= fingerPos[1].x ? fingerDeltapos[0].x : fingerDeltapos[1].x;
                move += (fingerPos[0].y >= fingerPos[1].y ? fingerDeltapos[0].y : fingerDeltapos[1].y);

                Camera.main.transform.Translate(0, 0, move*Time.deltaTime);
            }
        }
        

        if(!transform.GetComponent<Animation>().isPlaying)
        {
            Invoke("idleMode", 0.2f);
        }
    }

    void idleMode()
    {
        transform.GetComponent<Animation>().clip = idle.clip;
        transform.GetComponent<Animation>().Play();
    }

    //void OnGUI()
    //{
    //   if( GUI.Button(new Rect(10, 10, 100, 30), "L"))
    //   {
    //       transform.GetComponent<Animation>().clip = run.clip;
    //       transform.GetComponent<Animation>().Play();
    //   }
    //}
}
