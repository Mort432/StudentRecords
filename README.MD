Student Records Readme
----------------------

NB: This project contains 2 UIs.
StudentRecordsUI - A UWP application. This supports the Mock and Mongo databases.
StudentRecordsWPFUI - A WPF application. This supports Mock, Mongo and Oracle databases.
To set the UWP database, please check in StudentRecordsUI, App.xaml.cs, line 130.
To set the WPF database, please check in StudentRecordsWPFUI, App.xaml.cs, line 48.

Before attempting to use either application with MongoDB, ensure you have run the setup.bat file.
This is included in StudentRecordsRepositories, in the MongoDB folder.
Also ensure that an instance of the MongoDB server is running.
This application was built against MongoDB 4.0.