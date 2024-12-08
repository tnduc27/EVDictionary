using EVDictionary.Models;
using EVDictionary.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVDictionary.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository _feedbackRepository;
        public FeedbackService()
        {
            _feedbackRepository = new FeedbackRepository();
        }

        public void ApproveFeedback(int feedbackId)
        {
            _feedbackRepository.ApproveFeedback(feedbackId);
        }

        public void DeleteFeedback(int feedbackId)
        {
            _feedbackRepository?.DeleteFeedback(feedbackId);
        }

        public List<Feedback> GetFeedbacks()
        {
           return _feedbackRepository.GetFeedbacks();
        }

        public void SendFeedback(Feedback newFeedback)
        {
           _feedbackRepository.SendFeedback(newFeedback);
        }
    }
}
