namespace Estimation.Domain.Dtos
{
    /// <summary>
    /// Result of search material
    /// </summary>
    public class SearchResultMaterialDto
    {
        /// <summary>
        /// Material id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Material name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Material tags
        /// </summary>
        public string Tags { get; private set; }

        /// <summary>
        /// Add tag to tags property
        /// </summary>
        /// <param name="tag"></param>
        public void AddTag(string tag)
        {
            if (!string.IsNullOrEmpty(Tags))
                Tags += "|";
            Tags += tag;
        }
    }
}
