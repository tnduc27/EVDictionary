using EVDictionary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVDictionary.Repositories
{
    public interface IFeedbackRepository
    {
        void SendFeedback(Feedback newFeedback);
        List<Feedback> GetFeedbacks();
        void ApproveFeedback(int feedbackId);
        void DeleteFeedback(int feedbackId);
    }
}
