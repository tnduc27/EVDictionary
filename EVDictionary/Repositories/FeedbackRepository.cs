using EVDictionary.DAO;
using EVDictionary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVDictionary.Repositories
{
    public class FeedbackRepository : IFeedbackRepository
    {
        public void ApproveFeedback(int feedbackId) => FeedbackDAO.ApproveFeedback(feedbackId);

        public void DeleteFeedback(int feedbackId) => FeedbackDAO.DeleteFeedback(feedbackId);

        public List<Feedback> GetFeedbacks() => FeedbackDAO.GetFeedbacks();
        public void SendFeedback(Feedback newFeedback) => FeedbackDAO.SendFeedback(newFeedback);

    }
}
