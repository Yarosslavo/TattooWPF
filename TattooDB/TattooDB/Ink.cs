using System;
using System.Collections.Generic;

namespace TattooDB
{
    public partial class Ink
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage",
            "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ink()
        {
            this.Element = new HashSet<Element>();
        }
                
        public int ink_id { get; set; }
        public Nullable<int> inkType_id { get; set; }
        public string color { get; set; }
        public int price { get; set; }
        public string aviability { get; set; }
                
        public virtual InkType InkType { get; set; }
                
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Element> Element { get; set; }
    }

}