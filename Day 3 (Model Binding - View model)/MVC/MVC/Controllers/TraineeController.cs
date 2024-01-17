using Microsoft.AspNetCore.Mvc;
using MVC.Entities;
using MVC.ViewModels;

namespace MVC.Controllers
{
    public class TraineeController : Controller
    {


        ITIContext dbContext = new ITIContext();
        public IActionResult Index()
        {
            List<TraineeResultVM> resultVM = new List<TraineeResultVM>();
            var result = dbContext.CrsResults.Select(r => new { traineeName = r.Trainee.Name, CourseName = r.Course.Name, Resulte = r.Degree });

            foreach(var i in result)
            {
                resultVM.Add(new TraineeResultVM { CourseName=i.CourseName,TraineeName=i.traineeName,Degree=i.Resulte} );
            }
            return View(resultVM);
        }

        public IActionResult TraineeCourseStatus(int traineeId ,int coursId )
        {
            var result = dbContext.CrsResults.Where(r=> r.TraineeId == traineeId && r.CourseId== coursId)
            .Select(r => new { traineeName = r.Trainee.Name, CourseName = r.Course.Name, Degree = r.Degree })
            .FirstOrDefault();
            TraineeResultVM traineeResultVM = new TraineeResultVM();

            if(result != null )
            {
                traineeResultVM.TraineeName = result.traineeName;
                traineeResultVM.CourseName = result.CourseName;
                traineeResultVM.Degree = result.Degree;
            }

            if (traineeResultVM.Degree < 65)
                traineeResultVM.Color = "red";
            else
            traineeResultVM.Color = "green";

            return View(traineeResultVM);


            //var result = dbContext.CrsResults.Where(r => r.TraineeId == traineeId && r.CourseId == coursId)
            //.Select(r => new { traineeName = r.Trainee.Name, CourseName = r.Course.Name, Resulte = r.Degree });
            //TraineeResultVM traineeResultVM = new TraineeResultVM();
            //foreach (var item in result)
            //{
            //    traineeResultVM.TraineeName = item.traineeName;
            //    traineeResultVM.CourseName = item.CourseName;
            //    traineeResultVM.Degree = item.Resulte;
            //}
            //if (traineeResultVM.Degree < 65)
            //{
            //    traineeResultVM.Color = "red";
            //}
            //else
            //    traineeResultVM.Color = "green";
            //return View(traineeResultVM);
        }
    }
}
