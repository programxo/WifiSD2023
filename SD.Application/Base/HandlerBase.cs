using System.Reflection;

namespace SD.Application.Base
{
    public abstract class HandlerBase
    {
        protected void MapEntityProperties<TSource, TTarget>(TSource source, TTarget target, IList<string> excludeProperties)
        {
            var sourceType = source.GetType();
            var targetType = target.GetType();

            if (sourceType.BaseType.FullName != targetType.BaseType.FullName)
            {
                throw new ApplicationException("Base types are not matching!");
            }

            List<PropertyInfo> targetPropertyInfos = targetType.GetProperties(BindingFlags.Instance | BindingFlags.Public).ToList();
            targetPropertyInfos.ForEach(p =>
            {
                if(p.CanWrite && !(excludeProperties ?? new List<string>()).Contains(p.Name)) 
                { 
                    /* Try to find matching property in Source */
                    var sourceProperty = sourceType.GetProperty(p.Name, BindingFlags.Instance | BindingFlags.Public);

                    if (sourceProperty != null)
                    {
                        /* Read property value from source */
                        var sourcePropertyValue = sourceProperty.GetValue(source, null);
                        /* Write property value from source */
                        p.SetValue(target, sourcePropertyValue);
                    }
                }

            });


        }
    }
}
