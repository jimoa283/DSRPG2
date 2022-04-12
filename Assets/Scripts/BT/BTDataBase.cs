using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class BTDataBase
    {
        protected Dictionary<string, int> intDatas = new Dictionary<string, int>();
        protected Dictionary<string, float> floatDatas = new Dictionary<string, float>();
        protected Dictionary<string, string> stringDatas = new Dictionary<string, string>();
        protected Dictionary<string, Vector3> vector3Datas = new Dictionary<string, Vector3>();
        protected Dictionary<string, bool> boolDatas = new Dictionary<string, bool>();      

        public int GetInt(string key)
        {
            if (intDatas.ContainsKey(key))
                return intDatas[key];
            else
            {
                Debug.LogError("没有数据" + key);
                return int.MinValue;
            }               
        }

        public void SetInt(string key,int data)
        {
            if (intDatas.ContainsKey(key))
                intDatas[key] = data;
            else
                intDatas.Add(key, data);
        }

        public float GetFloat(string key)
        {
            if (floatDatas.ContainsKey(key))
                return floatDatas[key];
            else
            {
                Debug.LogError("没有数据" + key);
                return float.MinValue;
            }                
        }

        public string GetString(string key)
        {
            if (stringDatas.ContainsKey(key))
                return stringDatas[key];
            else
            {
                Debug.LogError("没有数据" + key);
                return "";
            }
        }

        public void SetString(string key,string data)
        {
            if (stringDatas.ContainsKey(key))
                stringDatas[key] = data;
            else
                stringDatas.Add(key, data);
        }

        public Vector3 GetVector3(string key)
        {
            if (vector3Datas.ContainsKey(key))
                return vector3Datas[key];
            else
            {
                Debug.LogError("没有数据" + key);
                return Vector3.zero;
            }
        }

        public void SetVector3(string key,Vector3 data)
        {
            if (vector3Datas.ContainsKey(key))
                vector3Datas[key] = data;
            else
                vector3Datas.Add(key, data);
        }

        public bool GetBool(string key)
        {
            if (boolDatas.ContainsKey(key))
                return boolDatas[key];
            else
            {
                Debug.LogError("没有数据" + key);
                return false;
            }
        }

        public void SetBool(string key,bool data)
        {
            if (boolDatas.ContainsKey(key))
                boolDatas[key] = data;
            else
                boolDatas.Add(key, data);
        }
    }
}

