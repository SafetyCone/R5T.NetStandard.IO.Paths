using System;

using R5T.NetStandard.IO.Paths.Extensions;


namespace R5T.NetStandard.IO.Paths
{
    /// <summary>
    /// Strongly-typed string search-pattern for use with <see cref="System.IO"/> types.
    /// </summary>
    public class SearchPattern : TypedString
    {
        public const char WildCardSignifierChar = '*';
        public static readonly string WildCardSignifier = SearchPattern.WildCardSignifierChar.ToString();


        #region Static

        /// <summary>
        /// Finds all values using the <see cref="SearchPattern.WildCardSignifier"/> value.
        /// </summary>
        public static readonly SearchPattern All = new SearchPattern(SearchPattern.WildCardSignifier);


        /// <summary>
        /// Gets a search pattern that will find all with a specific file extension.
        /// Note the file extension can include the file extension separator token ('.') or not; the function will work either way.
        /// </summary>
        public static SearchPattern AllFilesWithFileExtension(FileExtension fileExtension)
        {
            var searchPattern = $"{SearchPattern.WildCardSignifier}{FileExtensionSeparator.Default}{fileExtension}".AsSearchPattern();
            return searchPattern;
        }

        #endregion


        public SearchPattern(string value)
            : base(value)
        {
        }
    }
}
