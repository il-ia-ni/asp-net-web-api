using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace Tracking_api.Models
{
    public class Issue
    {
        public int Id { get; set; }
        [Required]  // validation attrs for a prop Title
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public Priority Priority { get; set; }
        public IssueType IssueType { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Completed { get; set; }  // the prop? is nullable
    }

    public enum Priority 
    {
        Low, Medium, High
    }

    public enum IssueType
    {
        Feature, Bug, Documentation
    }
}
