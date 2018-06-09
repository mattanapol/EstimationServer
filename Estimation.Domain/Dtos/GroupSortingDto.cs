using System;
using System.Collections.Generic;
using System.Text;

namespace Estimation.Domain.Dtos
{
    public class GroupSortingDto
    {
        /// <summary>
        /// Gets or sets the group ids.
        /// </summary>
        /// <value>
        /// The group ids.
        /// </value>
        public IEnumerable<int> GroupIds { get; set; }
    }
}
