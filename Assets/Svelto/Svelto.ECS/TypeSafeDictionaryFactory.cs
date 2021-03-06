using Svelto.ECS.Internal;

namespace Svelto.ECS
{
    static class TypeSafeDictionaryFactory<T> where T : struct, IEntityComponent
    {
        public static ITypeSafeDictionary Create()
        {
            return new TypeSafeDictionary<T>(1, ComponentBuilder<T>.IS_UNMANAGED);
        }
        
        public static ITypeSafeDictionary Create(uint size)
        {
            return new TypeSafeDictionary<T>(size, ComponentBuilder<T>.IS_UNMANAGED);
        }
    }
}