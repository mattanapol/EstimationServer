using System.Collections.Generic;
using System.Linq;

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
        /// Gets or sets the project information.
        /// </summary>
        /// <value>
        /// The project information.
        /// </value>
        public ProjectInfo ProjectInfo { get; set; }

        /// <summary>
        /// Sum of material price(Offer price)
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
        /// Gets or sets the net miscellaneous.
        /// </summary>
        /// <value>
        /// The net miscellaneous.
        /// </value>
        public int NetMiscellaneous { get; set; }

        /// <summary>
        /// Sum of installation price
        /// </summary>
        public int Installation { get; set; }

        /// <summary>
        /// Transportation price
        /// </summary>
        public int Transportation { get; set; }

        /// <summary>
        /// Gets or sets the net price.
        /// </summary>
        public int NetPrice { get; set; }

        /// <summary>
        /// Gets or sets the list price.
        /// </summary>
        /// <value>
        /// The list price.
        /// </value>
        public int ListPrice { get; set; }

        /// <summary>
        /// Gets or sets the manpower.
        /// </summary>
        /// <value>
        /// The manpower.
        /// </value>
        public decimal Manpower { get; set; }

        /// <summary>
        /// Overall price
        /// </summary>
        public int GrandTotal { get; set; }

        /// <summary>
        /// Gets or sets the net grand total.
        /// </summary>
        /// <value>
        /// The net grand total.
        /// </value>
        public int NetGrandTotal { get; set; }

        /// <summary>
        /// Gets the total material cost.
        /// </summary>
        /// <value>
        /// The total material cost.
        /// </value>
        /// <remarks>Only used in detail form(Form2)</remarks>
        public int TotalMaterialCost => GrandTotal - Installation;

        /// <summary>
        /// Gets the sub total.
        /// </summary>
        public int SubTotal => GrandTotal - Installation - Transportation;

        /// <summary>
        /// Gets the net sub total.
        /// </summary>
        public int NetSubTotal => NetGrandTotal - Installation - Transportation;

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
            NetGrandTotal += groupSummary.NetGrandTotal;
            NetPrice += groupSummary.NetPrice;
            ListPrice += groupSummary.ListPrice;
            Manpower += groupSummary.Manpower;
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
            NetPrice = 0;
            ListPrice = 0;
            NetGrandTotal = 0;
            Manpower = 0;
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
                    "MATERIALPRICE", MaterialPrice.ToCostString()
                },
                {
                    "NETPRICE", NetPrice.ToCostString()
                },
                {
                    "LISTPRICE", ListPrice.ToCostString()
                },
                {
                    "ACCESSORIES", Accessories.ToCostString()
                },
                {
                    "FITTINGS", Fittings.ToCostString()
                },
                {
                    "PAINTING", Painting.ToCostString()
                },
                {
                    "SUPPORTING", Supporting.ToCostString()
                },
                {
                    "INSTALLATION", Installation.ToCostString()
                },
                {
                    "TRANSPORTATION", Transportation.ToCostString()
                },
                {
                    "MISCELLANEOUS", Miscellaneous.ToCostString()
                },
                {
                    "GRANDTOTAL", GrandTotal.ToCostString()
                },
                {
                    "NETGRANDTOTAL", NetGrandTotal.ToCostString()
                },
                {
                    "MANPOWER", Manpower.ToCostString()
                },
                {
                    "TotalMaterialCost", TotalMaterialCost.ToCostString()
                },
                {
                    "subTotal", SubTotal.ToCostString()
                },
                {
                    "NetSubTotal", NetSubTotal.ToCostString()
                },
                {
                    "NetMiscellaneous", NetMiscellaneous.ToCostString()
                }
            };
            var projectInfoDataDict = ProjectInfo?.GetDataDictionary();

            if (projectInfoDataDict != null)
            {
                var result = dataDict.Combine(projectInfoDataDict);

                return result;
            }
            else
            {
                return dataDict;
            }
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
