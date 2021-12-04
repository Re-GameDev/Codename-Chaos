using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RageBall
{
    // only this time do not use DontDestroyOnLoad, we need this gameobject to find new spawn points on scene refresh.
    public class SpawnManager : MonoBehaviour
    {
        private static SpawnManager _instance;
        public static SpawnManager instance 
        {
            get => _instance ?? new GameObject("SpawnManager").AddComponent<SpawnManager>();
        }

        IEnumerable<Spawner> spawners;

        void Awake()
        {
            if( _instance != null )
            {
                DestroyImmediate( gameObject );
                return;
            }

            _instance = this;
            Refresh();
        }

        public List<Spawner> GetSpawnPoints() => spawners.ToList();
        
        public void Refresh() => spawners = FindObjectsOfType<Spawner>();
    }
}
