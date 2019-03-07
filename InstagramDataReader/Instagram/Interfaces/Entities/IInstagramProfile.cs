using System;

namespace InstagramDataReader.Interfaces
{
    public interface IInstagramProfile : IRootNode
    {
        DateTime Joined { get; }
        string Email { get; }
        string Genre { get; }
        bool Private { get; }
        string FullName { get; }
        string PhoneNumber { get; }
        string ProfilePic { get; }
        string UserName { get; }
    }
}