using UnityEngine;

/// <summary>
/// Inherit from this base class to create a singleton.
/// <example>public class MyClassName : Singleton&lt;MyClassName&gt;{   }</example> 
/// </summary>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    // Check to see if we're about to be destroyed
    private static bool mShuttingDown = false;
    private static object m_Lock = new object();
    private static T m_Instance;

    public static T Instance
    {
        get
        {
            if (mShuttingDown)
            {
                #if UNITY_EDITOR
                Debug.LogWarning("[Singleton] Instance '" + typeof(T) + "' already destroyed. Returning null.");
                #endif
                
                return null;
            }

            lock (m_Lock)
            {
                if (m_Instance == null)
                {
                    // Search for exiting instance
                    m_Instance = (T) FindObjectOfType(typeof(T));
                    
                    // Create new instance if one doesn't already exist
                    if (m_Instance == null)
                    {
                        // Need to create a new GameObject to attach the singleton to
                        var singletonObject = new GameObject();
                        m_Instance = singletonObject.AddComponent<T>();
                        singletonObject.name = typeof(T) + " (Singleton)";
                        
                        // Make instance persistent
                        DontDestroyOnLoad(singletonObject);
                    }
                }

                return m_Instance;
            }
        }
    }

    private void OnApplicationQuit()
    {
        mShuttingDown = true;
    }

    private void OnDestroy()
    {
        mShuttingDown = true;
    }
}
