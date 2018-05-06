using System;

namespace Estimation.Domain.Dtos
{
    public class ProjectInfoLightDto
    {
        /// <summary>
        /// Project id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Project name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Project code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Created Date
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Last Modified date
        /// </summary>
        public DateTime? LastModifiedDate { get; set; }

        /// <summary>
        /// Project owner
        /// </summary>
        public string Owner { get; set; }

        /// <summary>
        /// Project submit by
        /// </summary>
        public string SubmitBy { get; set; }
    }
}
