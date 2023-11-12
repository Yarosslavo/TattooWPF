using System;

namespace TattooDB
{
    public partial class Review
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage",
            "CA2214:DoNotCallOverridableMethodsInConstructors")]
        
        public int review_id { get; set; }
        public string textReview { get; set; }
        public int grade { get; set; }
        public Nullable<int> record_id { get; set; }
        
        public virtual Record Record { get; set; }
    }
}