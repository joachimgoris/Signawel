using Microsoft.AspNetCore.Mvc;
using Signawel.API.BinderProviders;

namespace Signawel.API.Attributes
{
    /// <summary>
    ///     Attribute used to mark parts of a multipart form data request as a json serialized object.
    /// </summary>
    public class FromJsonAttribute : ModelBinderAttribute
    {

        public FromJsonAttribute()
        {
            BinderType = typeof(JsonModelBinder);
        }

    }
}
