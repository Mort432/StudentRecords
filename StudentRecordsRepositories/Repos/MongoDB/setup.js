// Users
let lecturer = {
	_id: new ObjectId(),
    firstName: "Mock",
    lastName: "Lecturer",
    universityId: "s0000001",
    email: "mock@lec.com",
    dateOfBirth: new Date("1990-01-01"),
    phoneNumber: "07000000000",
    role: "Lecturer"
};

let student = {
	_id: new ObjectId(),
    firstName: "Mock",
    lastName: "Student",
    universityId: "s0000002",
    email: "mock@stu.com",
    dateOfBirth: new Date("2000-01-01"),
    phoneNumber: "07000000000",
    role: "Student",
    hasGraduated: true
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

let thomas = {
	_id: new ObjectId(),
    firstName: "Thomas",
    lastName: "Clark",
    universityId: "s0000004",
    email: "tom@glos.ac.uk",
    dateOfBirth: new Date("2000-01-01"),
    phoneNumber: "07000000000",
    username: "clark",
    password: "password",
    role: "Student"
};

let connagh = {
	_id: new ObjectId(),
    firstName: "Connagh",
    lastName: "Muldoon",
    universityId: "s0000005",
    email: "connagh@glos.ac.uk",
    dateOfBirth: new Date("2000-01-01"),
    phoneNumber: "07000000000",
    username: "muldoon",
    password: "password",
    role: "Student"
};

let laiba = {
	_id: new ObjectId(),
    firstName: "Laiba",
    lastName: "Khawar",
    universityId: "s0000006",
    email: "laiba@glos.ac.uk",
    dateOfBirth: new Date("2000-01-01"),
    phoneNumber: "07000000000",
    username: "khawar",
    password: "password",
    role: "Student"
};

// Courses
let computing = {
	_id: new ObjectId(),
    name: "Computing",
    courseLeader: abu
};

let photography = {
	_id: new ObjectId(),
    name: "Photography",
    courseLeader: abu
};

// Modules
let ct4009 = {
	_id: new ObjectId(),
    code: "CT4009",
    title: "Web Development"
};

let ct4010 = {
	_id: new ObjectId(),
    code: "CT4010",
    title: "Computers and Security"
};

let ct4020 = {
	_id: new ObjectId(),
    code: "CT4020",
    title: "Smart Business Computing"
};

let ct4021 = {
	_id: new ObjectId(),
    code: "CT4021",
    title: "Programming Fundamentals"
};

let ct4023 = {
	_id: new ObjectId(),
    code: "CT4023",
    title: "Systems Design and Development"
};

let ct5004 = {
	_id: new ObjectId(),
    code: "CT5004",
    title: "Computing Technology Placement"
};

let ct5006 = {
	_id: new ObjectId(),
    code: "CT5006",
    title: "Mobile Application Development"
};

let ct5018 = {
	_id: new ObjectId(),
    code: "CT5018",
    title: "Data Analytics"
};

let ct5022 = {
	_id: new ObjectId(),
    code: "CT5022",
    title: "Project Management"
};

let ct5023 = {
	_id: new ObjectId(),
    code: "CT5023",
    title: "Professional Issues"
};

let ct6005 = {
	_id: new ObjectId(),
    code: "CT6005",
    title: "Software Quality Assurance"
};

let ct6013 = {
	_id: new ObjectId(),
    code: "CT6013",
    title: "Advanced Database Systems"
};

let ct6028 = {
	_id: new ObjectId(),
    code: "CT6028",
    title: "Advanced Agile Methods"
};

let ct6038 = {
	_id: new ObjectId(),
    code: "CT6038",
    title: "Dissertation Research Methods"
};

let ct6042 = {
	_id: new ObjectId(),
    code: "CT6042",
    title: "Secure Coding"
};

let ad4902 = {
	_id: new ObjectId(),
    code: "AD4902",
    title: "Reading Photographs 1"
};

let ad4903 = {
	_id: new ObjectId(),
    code: "AD4903",
    title: "Personal Practice"
};

let ad4904 = {
	_id: new ObjectId(),
    code: "AD4904",
    title: "Studio Photography"
};

let ad4905 = {
	_id: new ObjectId(),
    code: "AD4905",
    title: "Documentary Photography"
};

let ad4906 = {
	_id: new ObjectId(),
    code: "AD4906",
    title: "Reading Photographs 2"
};

// Runs
let ct6005Run = {
	_id: new ObjectId(),
};

let ct6013Run = {
	_id: new ObjectId(),
};

let ct6028Run = {
	_id: new ObjectId(),
};

let ct6038Run = {
	_id: new ObjectId(),
};

let ct6042Run = {
	_id: new ObjectId(),
};

// Assignments
let ct6013Assignment = {
	_id: new ObjectId(),
    title: "Student Records",
};

let ct6028Assignment = {
	_id: new ObjectId(),
    title: "Organisational Agility",
};

let ct6038Assignment = {
	_id: new ObjectId(),
    title: "Dissertation Proposal",
};

let ct6042Assignment = {
	_id: new ObjectId(),
    title: "Security Vulnerabilities",
};

// Results
let ct6013Result = {
	_id: new ObjectId(),
    grade: 80
};

let ct6028Result = {
	_id: new ObjectId(),
    grade: 70
};

let ct6038Result = {
	_id: new ObjectId(),
    grade: 80
};

let ct6042Result = {
	_id: new ObjectId(),
    grade: 70
};


// Lecturer Enrolments
ct6013Run.lecturer = {
    _id: abu._id,
    value: abu.firstName + " " + abu.lastName
};
ct6042Run.lecturer = {
    _id: abu._id,
    value: abu.firstName + " " + abu.lastName
};
abu.enrolments = [
    {
        _id: ct6013Run._id,
        value: ct6013.code + ": " + ct6013.title + " (" + ct6013Run.lecturer + ")"
    },
    {
        _id: ct6042Run._id,
        value: ct6042.code + ": " + ct6042.title + " (" + ct6042Run.lecturer + ")"
    }
];

// Student Enrolments
ct6005Run.students = [
    {
        _id: thomas._id,
        value: thomas.firstName + " " + thomas.lastName
    },
    {
        _id: connagh._id,
        value: connagh.firstName + " " + connagh.lastName
    },
    {
        _id: laiba._id,
        value: laiba.firstName + " " + laiba.lastName
    }
];
ct6013Run.students = [
    {
        _id: thomas._id,
        value: thomas.firstName + " " + thomas.lastName
    },
    {
        _id: connagh._id,
        value: connagh.firstName + " " + connagh.lastName
    },
    {
        _id: laiba._id,
        value: laiba.firstName + " " + laiba.lastName
    }
];
ct6028Run.students = [
    {
        _id: thomas._id,
        value: thomas.firstName + " " + thomas.lastName
    },
    {
        _id: connagh._id,
        value: connagh.firstName + " " + connagh.lastName
    },
    {
        _id: laiba._id,
        value: laiba.firstName + " " + laiba.lastName
    }
];
ct6038Run.students = [
    {
        _id: thomas._id,
        value: thomas.firstName + " " + thomas.lastName
    },
    {
        _id: connagh._id,
        value: connagh.firstName + " " + connagh.lastName
    },
    {
        _id: laiba._id,
        value: laiba.firstName + " " + laiba.lastName
    }
];
ct6042Run.students = [
    {
        _id: thomas._id,
        value: thomas.firstName + " " + thomas.lastName
    },
    {
        _id: connagh._id,
        value: connagh.firstName + " " + connagh.lastName
    },
    {
        _id: laiba._id,
        value: laiba.firstName + " " + laiba.lastName
    }
];
thomas.enrolments = [
    {
        _id: ct6005Run._id,
        value: ct6005.code + ": " + ct6005.title + " (" + ct6005Run.lecturer + ")"
    },
    {
        _id: ct6013Run._id,
        value: ct6013.code + ": " + ct6013.title + " (" + ct6013Run.lecturer + ")"
    },
    {
        _id: ct6028Run._id,
        value: ct6028.code + ": " + ct6028.title + " (" + ct6028Run.lecturer + ")"
    },
    {
        _id: ct6038Run._id,
        value: ct6038.code + ": " + ct6038.title + " (" + ct6038Run.lecturer + ")"
    },
    {
        _id: ct6042Run._id,
        value: ct6042.code + ": " + ct6042.title + " (" + ct6042Run.lecturer + ")"
    }
];
connagh.enrolments = [
    {
        _id: ct6005Run._id,
        value: ct6005.code + ": " + ct6005.title + " (" + ct6005Run.lecturer + ")"
    },
    {
        _id: ct6013Run._id,
        value: ct6013.code + ": " + ct6013.title + " (" + ct6013Run.lecturer + ")"
    },
    {
        _id: ct6028Run._id,
        value: ct6028.code + ": " + ct6028.title + " (" + ct6028Run.lecturer + ")"
    },
    {
        _id: ct6038Run._id,
        value: ct6038.code + ": " + ct6038.title + " (" + ct6038Run.lecturer + ")"
    },
    {
        _id: ct6042Run._id,
        value: ct6042.code + ": " + ct6042.title + " (" + ct6042Run.lecturer + ")"
    }
];
laiba.enrolments = [
    {
        _id: ct6005Run._id,
        value: ct6005.code + ": " + ct6005.title + " (" + ct6005Run.lecturer + ")"
    },
    {
        _id: ct6013Run._id,
        value: ct6013.code + ": " + ct6013.title + " (" + ct6013Run.lecturer + ")"
    },
    {
        _id: ct6028Run._id,
        value: ct6028.code + ": " + ct6028.title + " (" + ct6028Run.lecturer + ")"
    },
    {
        _id: ct6038Run._id,
        value: ct6038.code + ": " + ct6038.title + " (" + ct6038Run.lecturer + ")"
    },
    {
        _id: ct6042Run._id,
        value: ct6042.code + ": " + ct6042.title + " (" + ct6042Run.lecturer + ")"
    }
];

// Course Modules
ct4009.course = {
    _id: computing._id,
    value: computing.name + " (" + computing.courseLeader + ")"
};
ct4010.course = {
    _id: computing._id,
    value: computing.name + " (" + computing.courseLeader + ")"
};
ct4020.course = {
    _id: computing._id,
    value: computing.name + " (" + computing.courseLeader + ")"
};
ct4021.course = {
    _id: computing._id,
    value: computing.name + " (" + computing.courseLeader + ")"
};
ct4023.course = {
    _id: computing._id,
    value: computing.name + " (" + computing.courseLeader + ")"
};
ct5004.course = {
    _id: computing._id,
    value: computing.name + " (" + computing.courseLeader + ")"
};
ct5006.course = {
    _id: computing._id,
    value: computing.name + " (" + computing.courseLeader + ")"
};
ct5018.course = {
    _id: computing._id,
    value: computing.name + " (" + computing.courseLeader + ")"
};
ct5022.course = {
    _id: computing._id,
    value: computing.name + " (" + computing.courseLeader + ")"
};
ct5023.course = {
    _id: computing._id,
    value: computing.name + " (" + computing.courseLeader + ")"
};
ct6005.course = {
    _id: computing._id,
    value: computing.name + " (" + computing.courseLeader + ")"
};
ct6013.course = {
    _id: computing._id,
    value: computing.name + " (" + computing.courseLeader + ")"
};
ct6028.course = {
    _id: computing._id,
    value: computing.name + " (" + computing.courseLeader + ")"
};
ct6038.course = {
    _id: computing._id,
    value: computing.name + " (" + computing.courseLeader + ")"
};
ct6042.course = {
    _id: computing._id,
    value: computing.name + " (" + computing.courseLeader + ")"
};
computing.modules = [
    {
        _id: ct4009._id,
        value: ct4009.code + ": " + ct4009.title
    },
    {
        _id: ct4010._id,
        value: ct4010.code + ": " + ct4010.title
    },
    {
        _id: ct4020._id,
        value: ct4020.code + ": " + ct4020.title
    },
    {
        _id: ct4021._id,
        value: ct4021.code + ": " + ct4021.title
    },
    {
        _id: ct4023._id,
        value: ct4023.code + ": " + ct4023.title
    },
    {
        _id: ct5004._id,
        value: ct5004.code + ": " + ct5004.title
    },
    {
        _id: ct5006._id,
        value: ct5006.code + ": " + ct5006.title
    },
    {
        _id: ct5018._id,
        value: ct5018.code + ": " + ct5018.title
    },
    {
        _id: ct5022._id,
        value: ct5022.code + ": " + ct5022.title
    },
    {
        _id: ct5023._id,
        value: ct5023.code + ": " + ct5023.title
    },
    {
        _id: ct6005._id,
        value: ct6005.code + ": " + ct6005.title
    },
    {
        _id: ct6013._id,
        value: ct6013.code + ": " + ct6013.title
    },
    {
        _id: ct6028._id,
        value: ct6028.code + ": " + ct6028.title
    },
    {
        _id: ct6038._id,
        value: ct6038.code + ": " + ct6038.title
    },
    {
        _id: ct6042._id,
        value: ct6042.code + ": " + ct6042.title
    }
];

ad4902.course = {
    _id: photography._id,
    value: photography.name + " (" + photography.courseLeader + ")"
};
ad4903.course = {
    _id: photography._id,
    value: photography.name + " (" + photography.courseLeader + ")"
};
ad4904.course = {
    _id: photography._id,
    value: photography.name + " (" + photography.courseLeader + ")"
};
ad4905.course = {
    _id: photography._id,
    value: photography.name + " (" + photography.courseLeader + ")"
};
ad4906.course = {
    _id: photography._id,
    value: photography.name + " (" + photography.courseLeader + ")"
};
photography.modules = [
    {
        _id: ad4902._id,
        value: ad4902.code + ": " + ad4902.title
    },
    {
        _id: ad4903._id,
        value: ad4903.code + ": " + ad4903.title
    },
    {
        _id: ad4904._id,
        value: ad4904.code + ": " + ad4904.title
    },
    {
        _id: ad4905._id,
        value: ad4905.code + ": " + ad4905.title
    },
    {
        _id: ad4906._id,
        value: ad4906.code + ": " + ad4906.title
    }
];

// Course Users
student.course = {
    _id: computing._id,
    value: computing.name + " (" + computing.courseLeader + ")"
};
thomas.course = {
    _id: computing._id,
    value: computing.name + " (" + computing.courseLeader + ")"
};
connagh.course = {
    _id: computing._id,
    value: computing.name + " (" + computing.courseLeader + ")"
};
laiba.course = {
    _id: computing._id,
    value: computing.name + " (" + computing.courseLeader + ")"
};
computing.users = [
    {
        _id: student._id,
        value: student.firstName + " " + student.lastName
    },
    {
        _id: thomas._id,
        value: thomas.firstName + " " + thomas.lastName
    },
    {
        _id: connagh._id,
        value: connagh.firstName + " " + connagh.lastName
    },
    {
        _id: laiba._id,
        value: laiba.firstName + " " + laiba.lastName
    }
];

// Module Runs
ct6005Run.module = {
    _id: ct6005._id,
    value: ct6005.code + ": " + ct6005.title
};
ct6005.runs = [
    {
        _id: ct6005Run._id,
        value: ct6005.code + ": " + ct6005.title + " (" + ct6005.lecturer + ")"
    }
];

ct6013Run.module = {
    _id: ct6013._id,
    value: ct6013.code + ": " + ct6013.title
};
ct6013.runs = [
    {
        _id: ct6013Run._id,
        value: ct6013.code + ": " + ct6013.title + " (" + ct6013.lecturer + ")"
    }
];

ct6028Run.module = {
    _id: ct6028._id,
    value: ct6028.code + ": " + ct6028.title
};
ct6028.runs = [
    {
        _id: ct6028Run._id,
        value: ct6028.code + ": " + ct6028.title + " (" + ct6028.lecturer + ")"
    }
];

ct6038Run.module = {
    _id: ct6038._id,
    value: ct6038.code + ": " + ct6038.title
};
ct6038.runs = [
    {
        _id: ct6038Run._id,
        value: ct6038.code + ": " + ct6038.title + " (" + ct6038.lecturer + ")"
    }
];

ct6042Run.module = {
    _id: ct6042._id,
    value: ct6042.code + ":" + ct6042.title
};
ct6042.runs = [
    {
        _id: ct6042Run._id,
        value: ct6042.code + ": " + ct6042.title + " (" + ct6042.lecturer + ")"
    }
];

// Run Assignments
ct6013Assignment.run = {
    _id: ct6013Run._id,
    value: ct6013.code + ": " + ct6013.title + " (" + ct6005.lecturer + ")"
};
ct6013Run.assignments = [
    {
        _id: ct6013Assignment._id,
        value: "[" + ct6013.code + ": " + ct6013.title + " (" + ct6005.lecturer + ")] - " + ct6013Assignment.title
    }
];

ct6028Assignment.run = {
    _id: ct6028Run._id,
    value: ct6028.code + ": " + ct6028.title + " (" + ct6028.lecturer + ")"
};
ct6028Run.assignments = [
    {
        _id: ct6028Assignment._id,
        value: "[" + ct6028.code + ": " + ct6028.title + " (" + ct60028.lecturer + ")] - " + ct6028Assignment.title
    }
];

ct6038Assignment.run = {
    _id: ct6038Run._id,
    value: ct6038.code + ": " + ct6038.title + " (" + ct6038.lecturer + ")"
};
ct6038Run.assignments = [
    {
        _id: ct6038Assignment._id,
        value: "[" + ct6038.code + ": " + ct6038.title + " (" + ct60038.lecturer + ")] - " + ct6038Assignment.title
    }
];

ct6042Assignment.run = {
    _id: ct6042Run._id,
    value: ct6042.code + ": " + ct6042.title + " (" + ct6042.lecturer + ")"
};
ct6042Run.assignments = [
    {
        _id: ct6042Assignment._id,
        value: "[" + ct6042.code + ": " + ct6042.title + " (" + ct6042.lecturer + ")] - " + ct6042Assignment.title
    }
];

// Assignment Results
ct6013Result.assignment = {
    _id: ct6013Assignment._id,
    value: "[" + ct6013.code + ": " + ct6013.title + " (" + ct6005.lecturer + ")] - " + ct6013Assignment.title
};
ct6013Assignment.results = [
    {
        _id: ct6013Result._id,
        value: 80
    }
];

ct6028Result.assignment = {
    _id: ct6028Assignment._id,
    value: "[" + ct6028.code + ": " + ct6028.title + " (" + ct6028.lecturer + ")] - " + ct6028Assignment.title
};
ct6028Assignment.results = [
    {
        _id: ct6028Result._id,
        value: 70
    }
];

ct6038Result.assignment = {
    _id: ct6038Assignment._id,
    value: "[" + ct6038.code + ": " + ct6038.title + " (" + ct6038.lecturer + ")] - " + ct6038Assignment.title
};
ct6038Assignment.results = [
    {
        _id: ct6038Result._id,
        value: 80
    }
];

ct6042Result.assignment = {
    _id: ct6042Assignment._id,
    value: "[" + ct6042.code + ": " + ct6042.title + " (" + ct6042.lecturer + ")] - " + ct6042Assignment.title
};
ct6042Assignment.results = [
    {
        _id: ct6042Result._id,
        value: 70
    }
];

// Student Results
ct6013Result.student = {
    _id: thomas._id,
    value: thomas.firstName + " " + thomas.lastName
};
ct6038Result.student = {
    _id: thomas._id,
    value: thomas.firstName + " " + thomas.lastName
};
thomas.results = [
    {
        _id: ct6013Result._id,
        value: 80
    },
    {
        _id: ct6038Result._id,
        value: 80
    }
];

ct6042Result.student = {
    _id: connagh._id,
    value: connagh.firstName + " " + connagh.lastName
};
connagh.results = [
    {
        _id: ct6042Result._id,
        value: 70
    }
];

ct6028Result.student = {
    _id: laiba._id,
    value: laiba.firstName + " " + laiba.lastName
};
laiba.results = [
    {
        _id: ct6028Result._id,
        value: 70
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
    ct6013Assignment,
    ct6028Assignment,
    ct6038Assignment,
    ct6042Assignment
]);
db.courses.insertMany([
    computing,
    photography
]);
db.modules.insertMany([
    ct4009,
    ct4010,
    ct4020,
    ct4021,
    ct4023,
    ct5004,
    ct5006,
    ct5018,
    ct5022,
    ct5023,
    ct6005,
    ct6013,
    ct6028,
    ct6038,
    ct6042,
    ad4902,
    ad4903,
    ad4904,
    ad4905,
    ad4906
]);
db.results.insertMany([
    ct6013Result,
    ct6028Result,
    ct6038Result,
    ct6042Result
]);
db.runs.insertMany([
    ct6005Run,
    ct6013Run,
    ct6028Run,
    ct6038Run,
    ct6042Run
]);
db.users.insertMany([
    admin,
    lecturer,
    student,
    abu,
    thomas,
    connagh,
    laiba
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
