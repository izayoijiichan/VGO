using System;

namespace UniGLTFforUniVgo
{
    public partial class glTFBuffer
    {
        //IBytesBuffer Storage;

        public void OpenStorage(IStorage storage)
        {
            Storage = new ArraySegmentByteBuffer(storage.Get(uri));
            /*
            if (string.IsNullOrEmpty(uri))
            {
                Storage = (glbDataBytes);
            }
            else
            {
                Storage = new UriByteBuffer(baseDir, uri);
            }
            */
        }

        public glTFBuffer()
        {

        }

        public glTFBuffer(IBytesBuffer storage)
        {
            Storage = storage;
        }

        public glTFBufferView Append<T>(T[] array, glBufferTarget target) where T : struct
        {
            return Append(new ArraySegment<T>(array), target);
        }
        public glTFBufferView Append<T>(ArraySegment<T> segment, glBufferTarget target) where T : struct
        {
            var view = Storage.Extend(segment, target);
            byteLength = Storage.GetBytes().Count;
            return view;
        }

        public ArraySegment<Byte> GetBytes()
        {
            return Storage.GetBytes();
        }

    }
}
