using Autofac;
using StudentRecordsRepositories.Repos;
using StudentRecordsRepositories.Repos.Mock;
using StudentRecordsRepositories.Repos.Mongo;
using StudentRecordsRepositories.Repos.Oracle;
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
            BuildOracleRepos(builder);

            Container = builder.Build();
        }

        private void BuildMockRepos(ContainerBuilder builder)
        {
            builder.RegisterType<MockUserRepo>().As<IUserRepo>().SingleInstance();
            builder.RegisterType<MockModuleRepo>().As<IModuleRepo>().SingleInstance();
            builder.RegisterType<MockModuleRunRepo>().As<IModuleRunRepo>().SingleInstance();
            builder.RegisterType<MockCourseRepo>().As<ICourseRepo>().SingleInstance();
            builder.RegisterType<MockResultRepo>().As<IResultRepo>().SingleInstance();
            builder.RegisterType<MockAssignmentRepo>().As<IAssignmentRepo>().SingleInstance();
        }
        private void BuildMongoRepos(ContainerBuilder builder)
        {
            builder.RegisterType<MongoUserRepo>().As<IUserRepo>().SingleInstance();
            builder.RegisterType<MongoModuleRepo>().As<IModuleRepo>().SingleInstance();
            builder.RegisterType<MongoModuleRunRepo>().As<IModuleRunRepo>().SingleInstance();
            builder.RegisterType<MongoCourseRepo>().As<ICourseRepo>().SingleInstance();
            builder.RegisterType<MongoResultRepo>().As<IResultRepo>().SingleInstance();
            builder.RegisterType<MongoAssignmentRepo>().As<IAssignmentRepo>().SingleInstance();
        }
        private void BuildOracleRepos(ContainerBuilder builder)
        {
            builder.RegisterType<OracleUserRepo>().As<IUserRepo>().SingleInstance();
            builder.RegisterType<OracleModuleRepo>().As<IModuleRepo>().SingleInstance();
            builder.RegisterType<OracleModuleRunRepo>().As<IModuleRunRepo>().SingleInstance();
            builder.RegisterType<OracleCourseRepo>().As<ICourseRepo>().SingleInstance();
            builder.RegisterType<OracleResultRepo>().As<IResultRepo>().SingleInstance();
            builder.RegisterType<OracleAssignmentRepo>().As<IAssignmentRepo>().SingleInstance();
        }
    }
}
