namespace GiamminLib.DomainModels
{
    public interface IDbEntity<T>
    {
        /// <summary>
        /// primary db key 
        /// </summary>
        T Id { get; set; }
    }
}