namespace CurrencyCloud.Entity
{
    public class FindParameters
    {
        ///<summary>
        /// Page number
        ///</summary>
        [Param]
        public int? Page { get; set; }

        ///<summary>
        /// Number of results per page
        ///</summary>
        [Param]
        public int? PerPage { get; set; }

        ///<summary>
        /// Field name to change the sort order
        ///</summary>
        [Param]
        public string Order { get; set; }

        ///<summary>
        /// Sort in ascending or descending order
        ///</summary>
        [Param]
        public OrderDirection? OrderAscDesc { get; set; }

        [Param]
        public enum OrderDirection { Asc, Desc };
    }
}
