start cmd /k "mongod --dbpath ""C:\ProgramData\MongoDB"""
mongo localhost:27017/student-records "%~dp0setup.js"
pause