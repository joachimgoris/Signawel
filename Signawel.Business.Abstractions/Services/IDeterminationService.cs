using System;
using Signawel.Dto.Determination;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Signawel.Business.Abstractions.Services
{
    public interface IDeterminationService
    {

        /// <summary>
        ///     Get the determination graph from the database
        /// </summary>
        /// <returns>
        ///     The <see cref="DeterminationGraphResponseDto"/> as in the database.
        /// </returns>
        Task<DeterminationGraphResponseDto> GetGraphAsync();

        /// <summary>
        ///     Set the determination graph in the database.
        /// </summary>
        /// <param name="creationDto">
        ///     Instance of <see cref="DeterminationGraphCreationRequestDto"/> containing details of the determination graph to save to the database.
        /// </param>
        /// <returns>
        ///     The <see cref="DeterminationGraphResponseDto"/> as in the database.
        /// </returns>
        Task<DeterminationGraphResponseDto> SetGraphAsync(DeterminationGraphCreationRequestDto creationDto);

    }
}
