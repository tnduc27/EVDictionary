using EVDictionary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVDictionary.Services
{
    public interface IFeedbackService
    { 
        List<Feedback> GetFeedbacks();
        void SendFeedback(Feedback newFeedback);
        void ApproveFeedback(int feedbackId);
        void DeleteFeedback(int feedbackId);
    }
}
