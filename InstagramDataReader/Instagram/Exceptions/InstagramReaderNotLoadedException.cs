using System;
using System.Runtime.Serialization;

namespace InstagramDataReader.Interfaces
{
    [Serializable]
    internal class InstagramReaderNotLoadedException : Exception
    {
        public InstagramReaderNotLoadedException()
        {
        }

        public InstagramReaderNotLoadedException(string message) : base(message)
        {
        }

        public InstagramReaderNotLoadedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InstagramReaderNotLoadedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}