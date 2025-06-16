using System;
using System.Collections.Generic;
using AutismEducationPlatform.Web.Models.ViewModels;

namespace AutismEducationPlatform.Web.Models.ViewModels
{
    public class GuidanceViewModel
    {
        public string ChildName { get; set; }
        public int ChildAge { get; set; }
        public string ChildDiagnosis { get; set; }
        public List<ExamProgressViewModel> ExamResults { get; set; } = new List<ExamProgressViewModel>();
        public List<CourseProgressViewModel> CourseProgress { get; set; } = new List<CourseProgressViewModel>();
    }

    public class GuidanceRecommendation
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Type { get; set; } // success, warning, info
        public string Icon { get; set; }
    }
}
