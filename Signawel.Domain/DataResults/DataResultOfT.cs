using System;
using System.Collections.Generic;
using System.Text;

namespace Signawel.Domain.DataResults
{
    /// <summary>
    ///     Represents the result of an entity operation.
    /// </summary>
    public class DataResult<TEntity> : DataResult
    {

        #region Properties

        /// <summary>
        ///     Entity that was returned by the operation.
        /// </summary>
        public TEntity Entity { get; set; }

        #endregion

        #region Static Helpers

        /// <summary>
        ///     Creates a <see cref="DataResult{TEntity}"/> indicating a successful operation.
        /// </summary>
        /// <returns>
        ///     A <see cref="DataResult{TEntity}"/> indicating a successful operation.
        /// </returns>
        public new static DataResult<TEntity> Success(TEntity entity)
        {
            return new DataResult<TEntity>
            {
                Entity = entity
            };
        }

        /// <summary>
        ///     Creates a <see cref="DataResult{TEntity}"/> indicating a failed operation, with a <see cref="DataError"/>.
        /// </summary>
        /// <param name="code">
        ///     <see cref="DataError.Code"/>
        /// </param>
        /// <param name="value">
        ///     <see cref="DataError.Value"/>
        /// </param>
        /// <param name="visiblity">
        ///     <see cref="DataError.Visiblity"/>
        /// </param>
        public static new DataResult<TEntity> WithError(string code, string value, DataErrorVisiblity visiblity = DataErrorVisiblity.Internal)
        {
            var dataResult = new DataResult<TEntity>();
            dataResult.AddError(code, value, visiblity);
            return dataResult;
        }

        /// <summary>
        ///     Creates a <see cref="DataResult{TEntity}"/> indicating a failed operation, with adding errors from another <see cref="DataResult"/>.
        /// </summary>
        /// <param name="dataResult">
        ///     <see cref="DataResult"> to copy the <see cref="DataError"/>s from and add to the current <see cref="DataResult"/>.
        /// </param>
        /// <remarks>
        ///     <see cref="DataError"/>s with a <see cref="DataError.Visiblity"/> of <see cref="DataErrorVisiblity.Private"/> will not be transferred.
        /// </remarks>
        public static new DataResult<TEntity> WithErrorsFromDataResult(DataResult dataResult)
        {
            var dr = new DataResult<TEntity>();
            dr.AddErrorsFromDataResult(dataResult);
            return dr;
        }

        /// <summary>
        ///     Creates a <see cref="DataResult{TEntity}"/> with <see cref="DataResult{TEntity}.Entity"/> as <paramref name="entity"/>. When <paramref name="entity"/> is null, a <see cref="DataError"/> will be added.
        /// </summary>
        /// <param name="entity">
        ///     Entity to add to the <see cref="DataResult{TEntity}"/>
        /// </param>
        /// <param name="code">
        ///     <see cref="DataError.Code"/>
        /// </param>
        /// <param name="value">
        ///     <see cref="DataError.Value"/>
        /// </param>
        /// <param name="visiblity">
        ///     <see cref="DataError.Visiblity"/>
        /// </param>
        /// <returns>
        ///     A <see cref="DataResult{TEntity}"/> with <paramref name="entity"/> as <see cref="DataResult{TEntity}.Entity"/> or a <see cref="DataError"/>.
        /// </returns>
        public static DataResult<TEntity> WithEntityOrError(TEntity entity, string code, string value, DataErrorVisiblity visiblity = DataErrorVisiblity.Internal)
        {
            if (entity == null)
            {
                return WithError(code, value, visiblity);
            }

            return Success(entity);
        }

        #endregion

    }
}
