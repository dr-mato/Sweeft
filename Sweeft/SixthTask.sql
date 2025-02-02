CREATE TYPE gender_type AS ENUM ('Male', 'Female');

CREATE TABLE Teacher (
	teacher_id SERIAL PRIMARY KEY,
	first_name VARCHAR(50) NOT NULL,
	last_name VARCHAR(50) NOT NULL,
	gender gender_type,
	subject VARCHAR(100)
);

CREATE TABLE Pupil (
	pupil_id SERIAL PRIMARY KEY,
	first_name VARCHAR(50) NOT NULL,
	last_name VARCHAR(50) NOT NULL,
	gender gender_type,
	grade SMALLINT
);

CREATE TABLE Teacher_Pupil (
	teacher_id INT REFERENCES Teacher(teacher_id) ON DELETE CASCADE,
	pupil_id INT REFERENCES Pupil(pupil_id) ON DELETE CASCADE,
	PRIMARY KEY (teacher_id, pupil_id)
);

SELECT DISTINCT t.first_name, t.last_name, t.subject
FROM Teacher_Pupil tp
JOIN Teacher t ON tp.teacher_id = t.teacher_id
JOIN Pupil p ON tp.pupil_id = p.pupil_id
WHERE p.first_name = 'გიორგი';








