using System;
using System.Data.Entity;

namespace Reusable
{
    public class RepositoryFactory
    {

        public static BaseRepository<T> Create<T>(DbContext context, int? byUserId) where T : class
        {
            return null;
            //if (typeof(T).IsSubclassOf(typeof(BaseDocument)))
            //{   
            //    Type baseDocumentRepository = typeof(BaseDocumentRepository<>);
            //    Type[] typeArgs = { typeof(T) };
            //    var makeme = baseDocumentRepository.MakeGenericType(typeArgs);
            //    return (BaseRepository<T>)Activator.CreateInstance(makeme, new object[] { context, byUserId });

            //}
            //else if (typeof(T).IsSubclassOf(typeof(BaseEntity)))
            //{
            //    Type baseEntityRepository = typeof(BaseEntityRepository<>);
            //    Type[] typeArgs = { typeof(T) };
            //    var makeme = baseEntityRepository.MakeGenericType(typeArgs);
            //    return (BaseRepository<T>)Activator.CreateInstance(makeme, new object[] { context, byUserId });
            //}
            //else
            //{
            //    return new BaseRepository<T>(context, byUserId);
            //}
        }
    }
}
