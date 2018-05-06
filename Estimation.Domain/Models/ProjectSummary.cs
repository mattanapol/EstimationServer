using System.Collections.Generic;

namespace Estimation.Domain.Models
{
    /// <summary>
    /// Project summary
    /// </summary>
    public class ProjectSummary: IPrintable
    {
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
        /// Childs project summary
        /// </summary>
        public IList<GroupSummary> ChildSummaries { get; set; }
        
        /// <summary>
        /// Add by group summary
        /// </summary>
        /// <param name="groupSummary"></param>
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
        public Dictionary<string, string> GetDataDictionary()
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
                    "##MISCELANEOUS##", Miscellaneous.ToString("N")
                },
                {
                    "##GRANDTOTAL##", GrandTotal.ToString("N")
                }
            };

            return dataDict;
        }
    }
}
