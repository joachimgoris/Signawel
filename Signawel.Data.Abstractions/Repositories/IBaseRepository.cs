using Signawel.Domain;
using System.Threading.Tasks;

namespace Signawel.Data.Abstractions
{
    public interface IBaseRepository<T> where T : Entity
    {
        #region AddEntity

        /// <summary>
        ///     Adds the given entity to the database.
        /// </summary>
        /// <param name="entity">
        ///     The given entity
        /// </param>
        /// <returns>
        ///     The entity added as it is added in the database.
        /// </returns>
        Task<T> AddEntityAsync(T entity);

        #endregion

        #region GetEntity

        /// <summary>
        ///     Get the entity referenced by the given id.
        /// </summary>
        /// <param name="id">
        ///     The given id.
        /// </param>
        /// <returns>
        ///     The entity as it is found in the database.
        /// </returns>
        Task<T> GetEntityAsync(string id);

        #endregion

        #region ModifyEntity

        /// <summary>
        ///     Update the given entity referenced by the id in the entity.
        /// </summary>
        /// <param name="entity">
        ///     The updated entity.
        /// </param>
        /// <returns>
        ///     The entity as updated in the database.
        /// </returns>
        Task<T> ModifyEntityAsync(T entity);

        #endregion

        #region DeleteEntity

        /// <summary>
        ///     Delete the entity in the database referenced by the given id.
        /// </summary>
        /// <param name="id">
        ///     The id of the entity that needs to be deleted.
        /// </param>
        Task DeleteEntityAsync(string id);

        #endregion
    }
}
