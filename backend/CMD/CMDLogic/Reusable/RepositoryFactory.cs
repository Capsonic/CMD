using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDLogic.Reusable
{
    public class RepositoryFactory
    {
        public static BaseEntityRepository<T> Create<T>() where T : BaseEntity
        {
            if (typeof(T).IsSubclassOf(typeof(BaseDocument)))
            {
                Type baseDocumentRepository = typeof(BaseDocumentRepository<>);
                Type[] typeArgs = { typeof(T) };
                var makeme = baseDocumentRepository.MakeGenericType(typeArgs);
                return (BaseEntityRepository<T>)Activator.CreateInstance(makeme);
            }else
            {
                return new BaseEntityRepository<T>();
            }
        }
    }
}
