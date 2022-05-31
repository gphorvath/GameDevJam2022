using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Enemy", order = 1)]
    public class EnemyData : ScriptableObject
    {
        public int health;
        public int damange;
        public float speed;
    }
}