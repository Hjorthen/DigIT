using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

/**
    summary:
        MonoBehaviour to deregister all services on scene reload
*/
public class ServiceRegistry : MonoBehaviour
{
    private List<object> services = new List<object>();
    private static ServiceRegistry instance;

    public void Awake() {
        if(instance == null) {
            instance = this;
        } else {
            throw new System.InvalidOperationException("A service registry has already been created!");
        }
    }
    
    private void RegisterServiceInstanced<T>(T instance) where T : class {
        services.Add(instance);
    }
    
    public static void RegisterService<T>(T service) where T : class {
        instance.RegisterServiceInstanced(service);
    }

    public T GetServiceInstanced<T>() where T : class {
        return services.FirstOrDefault(p => p is T) as T;
    }

    public static T GetService<T>() where T : class {
        return instance.GetServiceInstanced<T>();
    }
}
