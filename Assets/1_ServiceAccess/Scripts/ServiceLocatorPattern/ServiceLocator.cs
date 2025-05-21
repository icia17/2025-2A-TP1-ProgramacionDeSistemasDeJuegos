using System;
using System.Collections.Generic;
using UnityEngine;

namespace Excercise1
{
    public static class ServiceLocator
    {
        public static readonly Dictionary<Type, object> Services = new();

        public static void AddService<T>(T service, bool overrideExisting = false)
        {
            if (!Services.TryAdd(typeof(T), service) && overrideExisting)
                Services[typeof(T)] = service;
        }

        public static bool GetService<T>(out T service)
        {
            if (Services.TryGetValue(typeof(T), out object serviceFound)) 
            {
                service = (T)serviceFound;
                return true;
            }

            Debug.LogWarning("Service Not Found, default assigned!");
            service = default(T);
            return false;
        }

        public static void RemoveService<T>()
            => Services.Remove(typeof(T));
    }
}
