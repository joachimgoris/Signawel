namespace Signawel.Domain.DataResults
{
    /// <summary>
    ///     The visibility level of a <see cref="DataError"/>
    /// </summary>
    public enum DataErrorVisibility
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
