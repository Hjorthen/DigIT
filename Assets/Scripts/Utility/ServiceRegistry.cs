using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;

/**
    summary:
        MonoBehaviour to deregister all services on scene reload
*/
public class ServiceRegistry : MonoBehaviour
{
    private List<object> services = new List<object>();
    private static ServiceRegistry instance;

    [SerializeField]
    private bool DebugEnabled;

    public void Awake() {
        if(instance == null) {
            DebugLog("Registering ServiceRegistry");
            instance = this;
        } else {
            throw new System.InvalidOperationException("A service registry has already been created!");
        }
    }

    public static List<T> GetServices<T>() where T : class
    {
        return instance.GetServicesInstanced<T>();
    }

    private List<T> GetServicesInstanced<T>() where T : class
    {
        return services.Where(p => p is T).Select(t => t as T).ToList();
    }

    private void RegisterServiceInstanced<T>(T instance) where T : class {
        DebugLog(() => string.Format("Registering service {0}", instance));
        services.Add(instance);
    }
    
    public static void RegisterService<T>(T service) where T : class {
        instance.RegisterServiceInstanced(service);
    }

    public T GetServiceInstanced<T>() where T : class {
        T locatedService = services.FirstOrDefault(p => p is T) as T;
        DebugLog(() => string.Format("Requested service {0} found {1}.", typeof(T), locatedService));
        return locatedService;
    }

    public static T GetService<T>() where T : class {
        return instance.GetServiceInstanced<T>();
    }

    // Takes a lambda to only compute message when necessary
    private void DebugLog(Func<string> message) {
        if(DebugEnabled)
            DebugLog(message());
    }

    private void DebugLog(string message) {
        Debug.Log(message);
    }
}
