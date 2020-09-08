using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    // model for holding save data
    // now it's just scores
    [Serializable]
    public class SaveData
    {
        public List<int> scores = new List<int>();
    }
}