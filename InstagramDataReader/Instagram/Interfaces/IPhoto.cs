using System;
using System.Drawing;

namespace InstagramDataReader.Interfaces
{
    public interface IPhoto : IDisposable
    {
        Image Image { get; }
    }
}
