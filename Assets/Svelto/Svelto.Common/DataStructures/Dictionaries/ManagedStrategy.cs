using System;
using System.Runtime.CompilerServices;

namespace Svelto.DataStructures
{
    public class ManagedStrategy<T> : IBufferStrategy<T>
    {
        IBuffer<T> buffer;
        MB<T> realBuffer;

        public void Alloc(uint size)
        {
            MB<T> b = new MB<T>();
            b.Set(new T[size], size);
            buffer = b;
            this.realBuffer = b;
        }

        public uint capacity => buffer.capacity;

        public void Resize(uint size)
        {
            DBC.Common.Check.Require(size > 0, "Resize requires a size greater than 0");
            
            var realBuffer = buffer.ToManagedArray(out _);
            Array.Resize(ref realBuffer, (int) size);
            MB<T> b = new MB<T>();
            b.Set(realBuffer, size);
            buffer = b;
            this.realBuffer = b;
        }

        public void Clear() => realBuffer.Clear();

        public ref T this[uint index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ref realBuffer[index];
        }

        public ref T this[int index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ref realBuffer[index];
        }

        public IntPtr ToNativeArray() => throw new NotImplementedException();
        public IBuffer<T> ToBuffer() => buffer;

        public void Dispose() {  }
    }
}