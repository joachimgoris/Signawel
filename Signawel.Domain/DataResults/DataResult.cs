using System;
using System.Collections.Generic;
using System.Linq;

namespace Signawel.Domain.DataResults
{
    /// <summary>
    ///     Represents the result of an operation.
    /// </summary>
    public class DataResult
    {

        private readonly IList<DataError> _errors = new List<DataError>();

        #region Properties

        /// <summary>
        ///     Flag indicating whether if the operation succeeded or not.
        /// </summary>
        /// <value>
        ///     True if the operation succeeded, otherwise false.
        /// </value>
        public bool Succeeded => !Errors.Any();

        /// <summary>
        ///     An <see cref="IEnumerable{T}"/> of <see cref="DataError"/>s containing an errors that occurred during an operation.
        /// </summary>
        /// <value>
        ///     An <see cref="IEnumerable{T}"/> of <see cref="DataError"/>s.
        /// </value>
        public IEnumerable<DataError> Errors => _errors;

        #endregion

        #region Add errors

        /// <summary>
        ///     Add an <see cref="DataError"/> to this <see cref="DataResult"/>.
        /// </summary>
        /// <param name="code">
        ///     <see cref="DataError.Code"/>
        /// </param>
        /// <param name="value">
        ///     <see cref="DataError.Value"/>
        /// </param>
        /// <param name="visiblity">
        ///     <see cref="DataError.Visibility"/>
        /// </param>
        public void AddError(string code, string value = null, DataErrorVisibility visiblity = DataErrorVisibility.Internal)
        {
            _errors.Add(new DataError(code, value, visiblity));
        }

        /// <summary>
        ///     Add the errors of another <see cref="DataResult"/> to the current <see cref="DataResult"/>.
        /// </summary>
        /// <param name="dataResult">
        ///     <see cref="DataResult"/> to add the errors of to the current <see cref="DataResult"/>.
        /// </param>
        /// <remarks>
        ///     <see cref="DataError"/>s with a <see cref="DataError.Visibility"/> of <see cref="DataErrorVisibility.Private"/> will not be transferred.
        /// </remarks>
        public void AddErrorsFromDataResult(DataResult dataResult)
        {
            foreach (var error in dataResult.Errors)
            {
                if (error.Visibility == DataErrorVisibility.Private)
                    continue;

                AddError(error.Code, error.Value, error.Visibility);
            }
        }

        #endregion

        #region Get and check error

        /// <summary>
        ///     Get all <see cref="DataError"/>s with value of <see cref="DataError.Code"/> equal as <paramref name="code"/>
        /// </summary>
        /// <param name="code">
        ///     The value of the <see cref="DataError.Code"/> the filter on.
        /// </param>
        /// <param name="sc">
        ///     The <see cref="StringComparison"/> used to compare the <see cref="DataError.Code"/> value with the <paramref name="code"/> value.
        /// </param>
        /// <returns>
        ///     An <see cref="IEnumerable{T}"/> of <see cref="DataError"/>s with <see cref="DataError.Code"/> equal to <paramref name="code"/>
        /// </returns>
        public IEnumerable<DataError> GetErrors(string code, StringComparison sc = StringComparison.CurrentCulture)
        {
            return Errors.Where(e => e.Code.Equals(code, sc));
        }

        /// <summary>
        ///     Check if an <see cref="Errors"/> contains a <see cref="DataError"/> with <see cref="DataError.Code"/> equal to <paramref name="code"/>
        /// </summary>
        /// <param name="code">
        ///     The value of the <see cref="DataError.Code"/> to check for.
        /// </param>
        /// <param name="sc">
        ///     The <see cref="StringComparison"/> used to compare the <see cref="DataError.Code"/> value with the <paramref name="code"/> value.
        /// </param>
        /// <returns>
        ///     True if <see cref="Errors"/> contains at least one <see cref="DataError"/> with <see cref="DataError.Code"/> equal to <paramref name="code"/>, otherwise false.
        /// </returns>
        public bool HasError(string code, StringComparison sc = StringComparison.CurrentCulture)
        {
            return Errors.Any(e => e.Code.Equals(code, sc));
        }

        #endregion

        #region Static Helpers

        /// <summary>
        ///     Returns a <see cref="DataResult"/> indicating a successful operation.
        /// </summary>
        /// <returns>
        ///     A <see cref="DataResult"/> indicating a successful operation.
        /// </returns>
        public static DataResult Success => new DataResult();

        /// <summary>
        ///     Creates a <see cref="DataResult"/> indicating a failed operation, with a <see cref="DataError"/>.
        /// </summary>
        /// <param name="code">
        ///     <see cref="DataError.Code"/>
        /// </param>
        /// <param name="value">
        ///     <see cref="DataError.Value"/>
        /// </param>
        /// <param name="visibility">
        ///     <see cref="DataError.Visibility"/>
        /// </param>
        public static DataResult WithError(string code, string value, DataErrorVisibility visibility = DataErrorVisibility.Internal)
        {
            var dataResult = new DataResult();
            dataResult.AddError(code, value, visibility);
            return dataResult;
        }

        /// <summary>
        ///     Identical to <see cref="DataResult.WithError(string, string, DataErrorVisibility)"/> but with a public <see cref="DataError"/>.
        /// </summary>
        /// <param name="code">
        ///     <see cref="DataError.Code"/>
        /// </param>
        /// <param name="value">
        ///     <see cref="DataError.Value"/>
        /// </param>
        /// <param name="visiblity">
        ///     <see cref="DataError.Visibility"/>
        /// </param>
        public static DataResult WithPublicError(string code, string value)
        {
            return WithError(code, value, DataErrorVisibility.Public);
        }

        /// <summary>
        ///     Creates a <see cref="DataResult"/> indicating a failed operation, with adding errors from another <see cref="DataResult"/>.
        /// </summary>
        /// <param name="dataResult">
        ///     <see cref="DataResult"> to copy the <see cref="DataError"/>s from and add to the current <see cref="DataResult"/>.
        /// </param>
        /// <remarks>
        ///     <see cref="DataError"/>s with a <see cref="DataError.Visibility"/> of <see cref="DataErrorVisibility.Private"/> will not be transferred.
        /// </remarks>
        public static DataResult WithErrorsFromDataResult(DataResult dataResult)
        {
            var dr = new DataResult();
            dr.AddErrorsFromDataResult(dataResult);
            return dr;
        }

        #endregion

    }

}
