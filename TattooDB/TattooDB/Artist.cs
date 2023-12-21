using System;
using System.Collections.Generic;
using System.Windows;

namespace TattooDB
{
    public partial class Artist
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage",
            "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Artist()
        {
            this.Tattoo = new HashSet<Tattoo>();
        }
        
        public int artist_id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public Nullable<int> style_id { get; set; }
        public string expirience { get; set; }
        public virtual Style Style { get; set; }
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tattoo> Tattoo { get; set; }
    }

}