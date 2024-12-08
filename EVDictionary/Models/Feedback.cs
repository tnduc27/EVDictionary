using System;
using System.Collections.Generic;

namespace EVDictionary.Models;

public partial class Feedback
{
    public int FeedbackId { get; set; }

    public int UserId { get; set; }

    public string WordText { get; set; } = null!;

    public string FeedbackText { get; set; } = null!;

    public string Status { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
