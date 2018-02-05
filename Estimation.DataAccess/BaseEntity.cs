using System;
using System.Collections.Generic;
using System.Text;

namespace Estimation.DataAccess
{
    public class BaseEntity
    {
#if SQLITE
        [Column(TypeName = "INTEGER")]
#endif
        public DateTime? CreatedDate { get; set; }

#if SQLITE
        [Column(TypeName = "INTEGER")]
#endif
        public DateTime? LastModifiedDate { get; set; }
    }
}
