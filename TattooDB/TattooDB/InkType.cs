using System.Collections.Generic;

namespace TattooDB
{
    public partial class InkType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage",
            "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public InkType()
        {
            this.Ink = new HashSet<Ink>();
        }
        
        public int inkType_id { get; set; }
        public string title { get; set; }
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ink> Ink { get; set; }
    }
}