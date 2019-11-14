using System;

namespace Signawel.Mobile.Bootstrap.Abstract
{
    public interface IDependencyResolver
    {

        object Resolve(Type type);

        TType Resolve<TType>();

    }
}
