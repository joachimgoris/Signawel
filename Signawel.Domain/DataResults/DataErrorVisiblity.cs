using System;
using System.Collections.Generic;
using System.Text;

namespace Signawel.Domain.DataResults
{
    /// <summary>
    ///     The visibility level of a <see cref="DataError"/>
    /// </summary>
    public enum DataErrorVisiblity
    {

        /// <summary>
        ///     This visibility level is meant for error that are returned outside of the application or can be seen by a user.
        /// </summary>
        Public,

        /// <summary>
        ///     This visibility level is meant for error that remean inside the application.
        /// </summary>
        Internal,

        /// <summary>
        ///     This visibility level is meant for error that should never be transfered outside of the DataResult object that it was created in.
        /// </summary>
        Private

    }
}
