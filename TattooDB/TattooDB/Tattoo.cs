using System;
using System.Collections.Generic;

namespace TattooDB
{
    public partial class Tattoo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage",
            "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tattoo()
        {
            this.Element = new HashSet<Element>();
            this.Record = new HashSet<Record>();
        }
        
        public int tattoo_id { get; set; }
        public int price { get; set; }
        public string complexity { get; set; }
        public string size { get; set; } 
        public Nullable<int> artist_id { get; set; }
        public Nullable<int> place_id { get; set; }
        
        public virtual Artist Artist { get; set; }
        public virtual Place Place { get; set; }
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Element> Element { get; set; }
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Record> Record { get; set; }
    }

}