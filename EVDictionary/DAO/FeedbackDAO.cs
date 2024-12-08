using EVDictionary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVDictionary.DAO
{
    public class FeedbackDAO
    {
        public static void SendFeedback(Feedback newFeedback)
        {
            using var db = new EvdictionaryContext();
            db.Feedbacks.Add(newFeedback);
            db.SaveChanges();
        }
        public static List<Feedback> GetFeedbacks() 
        {
            var feedbacks = new List<Feedback>();
            try
            {
                using var db = new EvdictionaryContext();
                feedbacks = db.Feedbacks.ToList();
            }
            catch (Exception ex) { }
            return feedbacks;
        }
        public static void ApproveFeedback(int feedbackId)
        {
            try
            {
                using var db = new EvdictionaryContext();

                // Lấy phản hồi cần phê duyệt
                var feedback = db.Feedbacks.FirstOrDefault(f => f.FeedbackId == feedbackId);

                if (feedback != null)
                {
                    // Kiểm tra trạng thái của phản hồi
                    if (feedback.Status == "Pending")  // Nếu trạng thái là "Pending"
                    {
                        // Thay đổi trạng thái của phản hồi thành "Approved"
                        feedback.Status = "Approved";

                        // Lưu thay đổi vào cơ sở dữ liệu
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu có lỗi xảy ra
                Console.WriteLine($"Lỗi: {ex.Message}");
            }
        }
        public static void DeleteFeedback(int feedbackId)
        {
            try
            {
                using var db = new EvdictionaryContext();
                var feedback = db.Feedbacks.FirstOrDefault(f => f.FeedbackId == feedbackId);
                if (feedback != null)
                {
                    db.Feedbacks.Remove(feedback);
                    db.SaveChanges();
                }
            }
            catch (Exception ex) { }
        }
    }
}
