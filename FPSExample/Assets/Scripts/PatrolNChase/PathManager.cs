﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PatrolNChase
{
    public class PathManager : MonoBehaviour
    {

        public Transform[] points;

        public static PathManager instance;

        private void Awake()
        {
            instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {

            //for(int i = 0; i < transform.childCount; i++)
            //{
            //    points[i] = transform.GetChild(i);
            //}

            //Transform[] temp = GetComponentsInChildren<Transform>();
            //List<Transform> list = new List<Transform>(temp);

            //// 만약 목록에 내 transform이 있다면
            //if(list.Contains(transform))
            //{
            //    // 제외하고 싶다.
            //    list.Remove(transform);
            //}

            //points = new Transform[temp.Length - 1];
            //for(int i = 0, j = 0; i<temp.Length; i++)
            //{
            //    if(temp[i] == transform)
            //    {
            //        continue;
            //    }
            //    points[j] = temp[i];
            //    j++;
            //}

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

