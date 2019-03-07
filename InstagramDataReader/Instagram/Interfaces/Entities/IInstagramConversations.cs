using System;
using System.Collections.Generic;

namespace InstagramDataReader.Interfaces
{
    public interface IInstagramConversations : IRootNode, IEnumerable<IInstagramConversation>
    {

    }

    public interface IInstagramConversation : INode
    {
        IEnumerable<string> Participants { get; }
        IEnumerable<IInstagramMessage> Messages { get; }
        void RemoveParticipant(string participant);
    }

    public interface IInstagramMessage
    {
        string Sender { get; }
        DateTime Date { get; }
        string StoryShare { get; }
        string Text { get; }
        IInstagramMessageMedia Media { get; }
    }

    public interface IInstagramMessageMedia
    {
        IInstagramMessageMediaOriginal Original { get; }
    }

    public interface IInstagramMessageMediaOriginal
    {
        string Url { get; }
    }
}