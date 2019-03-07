using System.Collections.Generic;

namespace InstagramDataReader.Interfaces
{
    public interface IInstagramContacts: IRootNode, IEnumerable<IInstagramContact>
    {

    }

    public interface IInstagramContact
    {
        string FirstName { get; }
        string LastName { get; }
        string Contact { get; }
    }
}