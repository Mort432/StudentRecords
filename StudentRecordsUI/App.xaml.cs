using Autofac;
using StudentRecordsServices.Services;
using StudentRecordsUI.ViewModels;
using StudentRecordsViewModels.ViewModels;
using StudentRecordsRepositories.Repos.Mock;
using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using StudentRecordsUI.Views;
using StudentRecordsRepositories.Repos;
using StudentRecordsRepositories.Repos.Mongo;

namespace StudentRecordsUI
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        //Autofac DI container
        public static IContainer _container { get; set; }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                //Init Autofac DI container
                _container = AutofacConfig();

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    // When the navigation stack isn't restored navigate to the first page,
                    // configuring the new page by passing required information as a navigation
                    // parameter
                    rootFrame.Navigate(typeof(Login), e.Arguments);
                }
                // Ensure the current window is active
                Window.Current.Activate();
            }
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }

        //This configures Autofac's dependency injection.
        private static IContainer AutofacConfig()
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
            //If you want to try this app with the Mock DB, switch this line for BuildMockRepos.
            //Unfortunately, UWP does not support Oracle databases at this time.
            BuildMongoRepos(builder);

            return builder.Build();
        }

        private static void BuildMockRepos(ContainerBuilder builder)
        {
            builder.RegisterType<MockUserRepo>().As<IUserRepo>().SingleInstance();
            builder.RegisterType<MockModuleRepo>().As<IModuleRepo>().SingleInstance();
            builder.RegisterType<MockModuleRunRepo>().As<IModuleRunRepo>().SingleInstance();
            builder.RegisterType<MockCourseRepo>().As<ICourseRepo>().SingleInstance();
            builder.RegisterType<MockResultRepo>().As<IResultRepo>().SingleInstance();
            builder.RegisterType<MockAssignmentRepo>().As<IAssignmentRepo>().SingleInstance();
        }

        private static void BuildMongoRepos(ContainerBuilder builder)
        {
            builder.RegisterType<MongoUserRepo>().As<IUserRepo>().SingleInstance();
            builder.RegisterType<MongoModuleRepo>().As<IModuleRepo>().SingleInstance();
            builder.RegisterType<MongoModuleRunRepo>().As<IModuleRunRepo>().SingleInstance();
            builder.RegisterType<MongoCourseRepo>().As<ICourseRepo>().SingleInstance();
            builder.RegisterType<MongoResultRepo>().As<IResultRepo>().SingleInstance();
            builder.RegisterType<MongoAssignmentRepo>().As<IAssignmentRepo>().SingleInstance();
        }
    }
}
