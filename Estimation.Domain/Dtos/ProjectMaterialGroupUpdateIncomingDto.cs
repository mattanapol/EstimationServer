namespace Estimation.Domain.Dtos
{
    public class ProjectMaterialGroupUpdateIncomingDto
    {
        /// <summary>
        /// Group code
        /// </summary>
        public string GroupCode { get; set; }

        /// <summary>
        /// Gets or sets the order.
        /// </summary>
        /// <value>
        /// The order.
        /// </value>
        public int Order { get; set; }

        /// <summary>
        /// Group name
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// Gets or sets the remarks.
        /// </summary>
        /// <value>
        /// The remarks.
        /// </value>
        public string Remarks { get; set; }
    }
}
