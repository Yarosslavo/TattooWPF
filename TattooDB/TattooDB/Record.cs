using System;
using System.Collections.Generic;

namespace TattooDB
{
    public partial class Record
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage",
            "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Record()
        {
            this.Review = new HashSet<Review>();
        }
        
        public int record_id { get; set; }
        public string appointment { get; set; }
        public Nullable<int> artist_id { get; set; }
        public Nullable<int> customer_id { get; set; }
        
        public virtual Customer Customer { get; set; }
        public virtual Artist Artist { get; set; }
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Review> Review { get; set; }
    }
}