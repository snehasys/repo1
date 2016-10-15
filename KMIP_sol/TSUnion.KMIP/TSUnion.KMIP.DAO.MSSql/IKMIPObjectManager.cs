using System;
using TSUnion.KMIP.Core;
namespace TSUnion.KMIP.DAO
{
   public interface IKMIPObjectManager
    {
        bool Archive(object obj);
        int DeleteAllObjects();
        BaseObject GetBaseObject(Guid id);
        BaseObject GetBaseObjectFromArchive(Guid id);
        object GetObject(Guid id);
        object GetObjectFromArchive(Guid id);
        Guid InsertObject(object obj);
        bool IsArchived(Guid guid);
        bool Recover(object obj);
        Guid UpdateObject(object obj);
    }
}
