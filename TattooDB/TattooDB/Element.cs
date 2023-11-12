using System;

namespace TattooDB
{
    public partial class Element
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage",
            "CA2214:DoNotCallOverridableMethodsInConstructors")]
      
        
        public int element_id { get; set; }
        public Nullable<int> tattoo_id { get; set; }
        public Nullable<int> ink_id { get; set; }
        
        public virtual Tattoo Tattoo { get; set; }
        public virtual Ink Ink { get; set; }
    }
}