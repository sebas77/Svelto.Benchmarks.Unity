#if UNITY_NATIVE
using Svelto.ECS.DataStructures;

namespace Svelto.ECS.Native
{
    public readonly struct NativeEntitySwap
    {
        readonly AtomicNativeBags _swapQueue;
        readonly int             _nativeOperationIndex;

        internal NativeEntitySwap(AtomicNativeBags EGIDsToSwap, int nativeOperationIndex)
        {
            _swapQueue = EGIDsToSwap;
            _nativeOperationIndex = nativeOperationIndex;
        }

        public void SwapEntity(EGID from, EGID to, int threadIndex)
        {
            var simpleNativeBag = _swapQueue.GetBuffer(threadIndex);
            simpleNativeBag.Enqueue(_nativeOperationIndex);
            simpleNativeBag.Enqueue(new DoubleEGID(from, to));
            
        }

        public void SwapEntity(EGID from, ExclusiveBuildGroup to, int threadIndex)
        {
            var simpleNativeBag = _swapQueue.GetBuffer(threadIndex);
            simpleNativeBag.Enqueue(_nativeOperationIndex);
            simpleNativeBag.Enqueue(new DoubleEGID(from, new EGID(from.entityID, to)));
        }
    }
}
#endif