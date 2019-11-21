using System;
using System.Collections.Generic;
using System.Text;

namespace Signawel.Domain.DataResults
{
    public class DataError
    {

        /// <summary>
        ///     The code given to the error, this should be a unique value to the type of error.
        /// </summary>
        /// <remarks>
        ///     This code can be used to reference to documentation of this specific type of error.
        /// </remarks>
        /// <example>
        ///     The code can be any string;
        ///     
        ///     Example: DatabaseUnavailible
        ///         This code pertains to error given by the database
        ///         
        ///     Error code can also be urls to the documentation of the error, this can improve usability of the code.
        ///     
        ///     Example: https://site.com/errors/database-unavailible
        ///         The value of the code could lead to a documentation page explaining the nature of the error.
        /// </example>
        public string Code { get; private set; }

        /// <summary>
        ///     A string that can be used to provide additional information about the error.
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        ///     The visiblity of the error.
        ///     
        ///     Default: <see cref="DataErrorVisiblity.Internal"/>
        /// </summary>
        public DataErrorVisiblity Visiblity { get; private set; }

        /// <summary>
        ///     Create a new instance of the <see cref="DataError"/> class.
        /// </summary>
        /// <param name="code">
        ///     <see cref="Code"/>
        /// </param>
        /// <param name="value">
        ///     <see cref="Value"/>
        /// </param>
        /// <param name="visiblity">
        ///     <see cref="Visiblity"/>
        /// </param>
        internal DataError(string code, string value = null, DataErrorVisiblity visiblity = DataErrorVisiblity.Internal)
        {
            Code = code;
            Value = value;
            Visiblity = visiblity;
        }

    }
}
