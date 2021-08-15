using UnityEngine;

namespace Project.Scripts.Core
{
    /// <summary>
    /// Inherit from this base class to create a singleton that will create itself.
    /// <example>public class MyClassName : Singleton&lt;MyClassName&gt;{   }</example> 
    /// </summary>
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        private static T instance;

        public static T Instance {
            get {
        
                if (instance == null)
                {
                    var objs = FindObjectsOfType(typeof(T)) as T[];
            
                    if (objs.Length > 0)
                        instance = objs[0];
            
                    if (objs.Length > 1)
                    {
                        Debug.LogError ("There is more than one " + typeof(T).Name + " in the scene.");
                    }
            
                    if (instance == null)
                    {
                        GameObject obj = new GameObject();
                        instance = obj.AddComponent<T> ();
                    }
                }
                return instance;
            }
        }
    }

    /// <summary>
    /// Inherit from this base class to create a persistent singleton.
    /// <example>public class MyClassName : SingletonPersistent&lt;MyClassName&gt;{   }</example> 
    /// </summary>
    public class SingletonPersistent<T> : MonoBehaviour where T : Component
    {
        public static T Instance { get; private set; }

        public virtual void Awake ()
        {
            if (Instance == null)
            {
                Instance = this as T;
                DontDestroyOnLoad (this);
            } 
            else 
            {
                Destroy (gameObject);
            }
        }
    }
}