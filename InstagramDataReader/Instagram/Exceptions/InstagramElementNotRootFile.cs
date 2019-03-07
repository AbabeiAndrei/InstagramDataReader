using System;
using System.Runtime.Serialization;

namespace InstagramDataReader.Interfaces
{
    [Serializable]
    internal class InstagramElementNotRootFile : Exception
    {
        private readonly BaseInstagramDataElement _element;

        protected virtual BaseInstagramDataElement Element => _element;
        public InstagramElementNotRootFile()
        {
        }

        public InstagramElementNotRootFile(BaseInstagramDataElement element)
            : this()
        {
            _element = element;
        }

        public InstagramElementNotRootFile(string message) : base(message)
        {
        }

        public InstagramElementNotRootFile(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InstagramElementNotRootFile(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}