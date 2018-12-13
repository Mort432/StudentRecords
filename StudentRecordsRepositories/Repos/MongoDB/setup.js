// Users

let connagh = {
    _id: new ObjectId(),
    firstName: "Connagh",
    lastName: "Muldoon",
    universityId: "s0000005",
    email: "connagh@glos.ac.uk",
    dateOfBirth: new Date("2000-01-01"),
    phoneNumber: "07000000000",
    role: "Student"
};

let thomas = {
    _id: new ObjectId(),
    firstName: "Thomas",
    lastName: "Clark",
    universityId: "s0000004",
    email: "tom@glos.ac.uk",
    dateOfBirth: new Date("2000-01-01"),
    phoneNumber: "07000000000",
    role: "Student"
};

let abu = {
	_id: new ObjectId(),
    firstName: "Abu",
    lastName: "Alam",
    universityId: "s0000003",
    email: "abu@glos.ac.uk",
    dateOfBirth: new Date("1990-01-01"),
    phoneNumber: "07000000000",
    role: "Lecturer"
};

// Modules
let ct1337 = {
    _id: new ObjectId(),
    code: "CT1337",
    title: "Applied Ass Kicking"
};

// ModuleRuns
let ct1337ModuleRun = {
    _id: new ObjectId(),
};

ct1337ModuleRun.lecturer = {
    _id: abu._id,
    value: abu.firstName + " " + abu.lastName
};

ct1337ModuleRun.module = {
    _id: ct1337._id,
    value: ct1337.code + ": " + ct1337.title
};

ct1337.runs = [
    {
        _id: ct1337ModuleRun._id,
        value: ct1337.code + ": " + ct1337.title + " (" + ct1337ModuleRun.lecturer.value + ")"
    }
];

// Assignments
let ct1337Assignment = {
    _id: new ObjectId(),
    assignmentName: "1. Chew Bubblegum",
};

ct1337Assignment.moduleRun = {
    _id: ct1337ModuleRun._id,
    value: ct1337.code + ": " + ct1337.title + " (" + ct1337ModuleRun.lecturer.value + ")"
};

ct1337ModuleRun.assignments = [
    {
        _id: ct1337Assignment._id,
        value: "[" + ct1337.code + ": " + ct1337.title + " (" + ct1337ModuleRun.lecturer.value + ")" + "] - " + ct1337Assignment.assignmentName
    }
];

// Courses
let computing = {
	_id: new ObjectId(),
    title: "Computing - BSc W/Hons",
};

computing.courseLeader = {
    _id: abu._id,
    value: abu.firstName + " " + abu.lastName
};

computing.moduleRuns = [
    {
        _id: ct1337ModuleRun._id,
        value: ct1337.code + ": " + ct1337.title + " (" + ct1337ModuleRun.lecturer.value + ")"
    }
];

computing.students = [
    {
        _id: connagh._id,
        value: connagh.firstName + " " + connagh.lastName
    }
];

// Results
let ct1337Result = {
	_id: new ObjectId(),
    grade: 74
};

ct1337Result.assignment = {
    _id: ct1337Assignment._id,
    value: "[" + ct1337.code + ": " + ct1337.title + " (" + ct1337ModuleRun.lecturer.value + ")" + "] - " + ct1337Assignment.assignmentName
};

ct1337Result.student = {
    _id: connagh._id,
    value: connagh.firstName + " " + connagh.lastName
};

ct1337Assignment.results = [
    {
        _id: ct1337Result._id,
        value: "74"
    }
];

// Student Enrollments
ct1337ModuleRun.students = [
    {
        _id: connagh._id,
        value: connagh.firstName + " " + connagh.lastName
    }
];
connagh.enrollments = [
    {
        _id: ct1337ModuleRun._id,
        value: ct1337.code + ": " + ct1337.title + " (" + ct1337ModuleRun.lecturer.value + ")"
    }
];
thomas.enrollments = [];

// Lecturer Enrollments
ct1337ModuleRun.lecturer = {
    _id: abu._id,
    value: abu.firstName + " " + abu.lastName
};
abu.enrollments = [
    {
        _id: ct1337ModuleRun._id,
        value: ct1337.code + ": " + ct1337.title + " (" + ct1337ModuleRun.lecturer.value + ")"
    }
];

// Course Users
connagh.course = {
    _id: computing._id,
    value: computing.title + " (" + abu.firstName + " " + abu.lastName + ")"
}
abu.course = {
    _id: computing._id,
    value: computing.title + " (" + abu.firstName + " " + abu.lastName + ")"
}

// Assignment Results
ct1337Result.assignment = {
    _id: ct1337Assignment._id,
    value: "[" + ct1337.code + ": " + ct1337.title + " (" + ct1337.lecturer + ")] - " + ct1337Assignment.title
};
ct1337Assignment.results = [
    {
        _id: ct1337Result._id,
        value: "74"
    }
];

// Database
db.assignments.drop();
db.courses.drop();
db.modules.drop();
db.results.drop();
db.runs.drop();
db.users.drop();

db.assignments.insertMany([
    ct1337Assignment
]);
db.courses.insertMany([
    computing
]);
db.modules.insertMany([
    ct1337
]);
db.results.insertMany([
    ct1337Result
]);
db.moduleruns.insertMany([
    ct1337ModuleRun
]);
db.users.insertMany([
    connagh,
    thomas,
    abu
]);

let cursor;

cursor = db.assignments.find();
while (cursor.hasNext()) {
    printjson(cursor.next());
}

cursor = db.courses.find();
while (cursor.hasNext()) {
    printjson(cursor.next());
}

cursor = db.modules.find();
while (cursor.hasNext()) {
    printjson(cursor.next());
}

cursor = db.results.find();
while (cursor.hasNext()) {
    printjson(cursor.next());
}

cursor = db.runs.find();
while (cursor.hasNext()) {
    printjson(cursor.next());
}

cursor = db.users.find();
while (cursor.hasNext()) {
    printjson(cursor.next());
}
