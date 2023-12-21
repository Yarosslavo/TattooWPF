using System.Collections.Generic;

namespace TattooDB
{
    public partial class Style
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage",
            "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Style()
        {
            this.Artist = new HashSet<Artist>();
        }
        
        public int style_id { get; set; }
        public string title { get; set; }
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Artist> Artist { get; set; }
    }

}