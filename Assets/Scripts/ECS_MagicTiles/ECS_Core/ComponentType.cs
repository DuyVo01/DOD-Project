using System;
using System.Collections.Generic;

namespace ECS_Core
{
    public struct ComponentType
    {
        public readonly Type Type;
        public readonly int Id;
        private static int NextId = 0;
        private static Dictionary<Type, ComponentType> typeRegistry = new();

        private ComponentType(Type type)
        {
            Type = type;
            Id = NextId++;
        }

        public static ComponentType Of<T>()
            where T : struct, IComponent
        {
            var type = typeof(T);
            if (!typeRegistry.ContainsKey(type))
            {
                typeRegistry[type] = new ComponentType(type);
            }

            return typeRegistry[type];
        }
    }
}
