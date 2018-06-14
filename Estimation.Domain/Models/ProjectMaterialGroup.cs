using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Estimation.Domain.Models
{
    public class ProjectMaterialGroup: IPrintable
    {
        /// <summary>
        /// Group identification
        /// </summary>
        public int Id { get; set; }

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
        /// Material type
        /// </summary>
        public string MaterialType { get; set; }

        /// <summary>
        /// Parent group identification
        /// </summary>
        public int? ParentGroupId { get; set; }

        /// <summary>
        /// Materials
        /// </summary>
        public Collection<ProjectMaterial> Materials { get; set; }

        /// <summary>
        /// Project id that this group belong to.
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// Project information this group belong to
        /// </summary>
        public ProjectInfo ProjectInfo { get; set; }

        /// <summary>
        /// Child group of this material group
        /// </summary>
        public Collection<ProjectMaterialGroup> ChildGroups { get; set; }

        /// <summary>
        /// Miscellaneous cost
        /// </summary>
        public Cost Miscellaneous { get; set; }

        /// <summary>
        /// Transportation cost
        /// </summary>
        public Cost Transportation { get; set; }

        /// <summary>
        /// Gets or sets the remarks.
        /// </summary>
        /// <value>
        /// The remarks.
        /// </value>
        public string Remarks { get; set; }

        /// <inheritdoc />
        public Dictionary<string, string> GetDataDictionary()
        {
            var dataDict = new Dictionary<string, string>
            {
                {
                    "GROUPID", Id.ToString()
                },
                {
                    "GROUPCODE", GroupCode
                },
                {
                    "GROUPNAME", GroupName
                },
                {
                    "REMARKS", Remarks
                }
            };

            return dataDict;
        }


        /// <inheritdoc />
        public string TargetClass => ParentGroupId != null && ParentGroupId != 0 ? "subgroup" : "group";

        /// <summary>
        /// Gets or sets the child.
        /// </summary>
        /// <value>
        /// The child.
        /// </value>
        public IEnumerable<IPrintable> Child
        {
            get
            {
                if (Materials?.Count != 0)
                    return Materials;
                else if (ChildGroups?.Count != 0)
                    return ChildGroups;
                else
                    return null;
            }
        }

        /// <summary>
        /// Gets the materials quantity.
        /// </summary>
        /// <returns>All material quantity</returns>
        public int GetMaterialsQuantity()
        {
            int materialsQuantity = 0;
            if (ChildGroups.Count != 0)
                foreach (var childGroup in ChildGroups)
                    materialsQuantity += childGroup.GetMaterialsQuantity();
            else if (Materials.Count != 0)
                foreach (var material in Materials)
                    materialsQuantity += material.Quantity;

            return materialsQuantity;
        }
    }
}
