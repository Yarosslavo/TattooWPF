using System.Collections.Generic;

namespace TattooDB
{
    public partial class Place
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage",
            "CA2214:DoNotCallOverridableMethodsInConstructors")]

        public Place()
        {
            this.Tattoo = new HashSet<Tattoo>();
        }
        
        public int place_id { get; set; }
        public string title { get; set; }
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tattoo> Tattoo { get; set; }
    }

}