#if UNITY_NATIVE
using Svelto.ECS.DataStructures;

namespace Svelto.ECS.Native
{
    public readonly struct NativeEntityRemove
    {
        readonly AtomicNativeBags _removeQueue;
        readonly int             _nativeOperationIndex;

        internal NativeEntityRemove(AtomicNativeBags EGIDsToRemove, int nativeOperationIndex)
        {
            _removeQueue = EGIDsToRemove;
            _nativeOperationIndex = nativeOperationIndex;
        }

        public void RemoveEntity(EGID egid, int threadIndex)
        {
            var simpleNativeBag = _removeQueue.GetBuffer(threadIndex);
            
            simpleNativeBag.Enqueue(_nativeOperationIndex);
            simpleNativeBag.Enqueue(egid);
        }
    }
}
#endif