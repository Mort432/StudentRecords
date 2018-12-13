using Autofac;
using StudentRecordsRepositories.Repos;
using StudentRecordsRepositories.Repos.Mock;
using StudentRecordsServices.Services;
using StudentRecordsViewModels.ViewModels;
using StudentRecordsWPFUI.ViewModels;
using System.Windows;

namespace StudentRecordsWPFUI
{
    public partial class App : Application
    {
        public static IContainer Container { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            BuildContainer();
        }

        private void BuildContainer()
        {
            var builder = new ContainerBuilder();

            //Services
            builder.RegisterType<UsersService>().As<IUsersService>().SingleInstance();
            builder.RegisterType<ModulesService>().As<IModulesService>().SingleInstance();
            builder.RegisterType<AuthService>().As<IAuthService>().SingleInstance();
            builder.RegisterType<CoursesService>().As<ICoursesService>().SingleInstance();
            builder.RegisterType<ResultsService>().As<IResultsService>().SingleInstance();
            builder.RegisterType<AssignmentsService>().As<IAssignmentsService>().SingleInstance();
            builder.RegisterType<LecturerService>().As<ILecturerService>().SingleInstance();

            //View models
            builder.RegisterType<StudentProfileViewModel>().InstancePerDependency();
            builder.RegisterType<StudentEnrollmentsViewModel>().InstancePerDependency();
            builder.RegisterType<LoginViewModel>().InstancePerDependency();
            builder.RegisterType<MainPageViewModel>().InstancePerDependency();
            builder.RegisterType<LecturerProfileViewModel>().InstancePerDependency();
            builder.RegisterType<LecturerStudentManagementViewModel>().InstancePerDependency();
            builder.RegisterType<LecturerStudentAnalyticsViewModel>().InstancePerDependency();

            //Repos
            builder.RegisterType<MockUserRepo>().As<IUserRepo>().SingleInstance();
            builder.RegisterType<MockModuleRepo>().As<IModuleRepo>().SingleInstance();
            builder.RegisterType<MockModuleRunRepo>().As<IModuleRunRepo>().SingleInstance();
            builder.RegisterType<MockCourseRepo>().As<ICourseRepo>().SingleInstance();
            builder.RegisterType<MockResultRepo>().As<IResultRepo>().SingleInstance();
            builder.RegisterType<MockAssignmentRepo>().As<IAssignmentRepo>().SingleInstance();

            Container = builder.Build();
        }
    }
}
