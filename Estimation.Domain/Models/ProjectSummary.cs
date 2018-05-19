using System.Collections.Generic;

namespace Estimation.Domain.Models
{
    /// <summary>
    /// Project summary
    /// </summary>
    public class ProjectSummary: IPrintable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectSummary"/> class.
        /// </summary>
        public ProjectSummary()
        {
            ChildSummaries = new List<GroupSummary>();
        }

        /// <summary>
        /// Sum of material price
        /// </summary>
        public int MaterialPrice { get; set; }

        /// <summary>
        /// Sum of accessories price
        /// </summary>
        public int Accessories { get; set; }

        /// <summary>
        /// Sum of fittings price
        /// </summary>
        public int Fittings { get; set; }

        /// <summary>
        /// Sum of supporting price
        /// </summary>
        public int Supporting { get; set; }

        /// <summary>
        /// Sum of painting price
        /// </summary>
        public int Painting { get; set; }

        /// <summary>
        /// Miscellaneous price
        /// </summary>
        public int Miscellaneous { get; set; }

        /// <summary>
        /// Sum of installation price
        /// </summary>
        public int Installation { get; set; }

        /// <summary>
        /// Transportation price
        /// </summary>
        public int Transportation { get; set; }

        /// <summary>
        /// Overall price
        /// </summary>
        public int GrandTotal { get; set; }

        /// <summary>
        /// Child project summary
        /// </summary>
        public IList<GroupSummary> ChildSummaries { get; set; }

        /// <summary>
        /// Adds the group summary by group summary.
        /// </summary>
        /// <param name="groupSummary">The group summary.</param>
        public void AddByGroupSummary(GroupSummary groupSummary)
        {
            Accessories += groupSummary.Accessories;
            Fittings += groupSummary.Fittings;
            Painting += groupSummary.Painting;
            Supporting += groupSummary.Supporting;
            Installation += groupSummary.Installation;
            MaterialPrice += groupSummary.MaterialPrice;
            Transportation += groupSummary.Transportation;
            Miscellaneous += groupSummary.Miscellaneous;
            GrandTotal += groupSummary.GrandTotal;
        }

        /// <summary>
        /// Add child group summary to this object
        /// </summary>
        /// <param name="childGroupSummary"></param>
        public void AddChildGroupSummary(GroupSummary childGroupSummary)
        {
            ChildSummaries.Add(childGroupSummary);
        }

        /// <summary>
        /// Clears the project summary.
        /// </summary>
        /// <returns></returns>
        public ProjectSummary ClearProjectSummary()
        {
            MaterialPrice = 0;
            Accessories = 0;
            Fittings = 0;
            Supporting = 0;
            Painting = 0;
            Miscellaneous = 0;
            Installation = 0;
            Transportation = 0;
            GrandTotal = 0;
            return this;
        }

        /// <summary>
        /// Calculates from child group summaries.
        /// </summary>
        /// <returns></returns>
        public ProjectSummary CalculateFromChildGroupSummaries()
        {
            ClearProjectSummary();
            foreach (var childGroupSummary in ChildSummaries)
            {
                AddByGroupSummary(childGroupSummary);
            }
            return this;
        }

        /// <inheritdoc />
        public virtual Dictionary<string, string> GetDataDictionary()
        {
            var dataDict = new Dictionary<string, string>
            {
                {
                    "##MATERIALPRICE##", MaterialPrice.ToString("N")
                },
                {
                    "##ACCESSORIES##", Accessories.ToString("N")
                },
                {
                    "##FITTINGS##", Fittings.ToString("N")
                },
                {
                    "##PAINTING##", Painting.ToString("N")
                },
                {
                    "##SUPPORTING##", Supporting.ToString("N")
                },
                {
                    "##INSTALLATION##", Installation.ToString("N")
                },
                {
                    "##TRANSPORTATION##", Transportation.ToString("N")
                },
                {
                    "##MISCELLANEOUS##", Miscellaneous.ToString("N")
                },
                {
                    "##GRANDTOTAL##", GrandTotal.ToString("N")
                }
            };

            return dataDict;
        }

        /// <inheritdoc />
        public virtual string TargetClass => "project-summary";

        /// <summary>
        /// Gets or sets the child.
        /// </summary>
        /// <value>
        /// The child.
        /// </value>
        public virtual IEnumerable<IPrintable> Child => ChildSummaries;
    }
}
