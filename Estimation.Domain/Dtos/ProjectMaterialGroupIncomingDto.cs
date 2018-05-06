namespace Estimation.Domain.Dtos
{
    public class ProjectMaterialGroupIncomingDto
    {
        /// <summary>
        /// Group code
        /// </summary>
        public string GroupCode { get; set; }

        /// <summary>
        /// Group name
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// Material type
        /// </summary>
        public string MaterialType { get; set; }

        /// <summary>
        /// Parent group identification
        /// </summary>
        public int? ParentGroupId { get; set; }
    }
}
